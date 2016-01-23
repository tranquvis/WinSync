using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace WinSync.Service
{
    internal class AsyncSyncService
    {
        private readonly SyncInfo _si;

        private List<Task<SyncFileInfo>> _detectFileTasks = new List<Task<SyncFileInfo>>();
        private List<Task<SyncFileInfo>> _fileApplyTasks = new List<Task<SyncFileInfo>>();

        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        /// <summary>
        /// create SyncService
        /// </summary>
        /// <param name="si">information provider: will be updated while synchronisation is running</param>
        public AsyncSyncService(SyncInfo si)
        {
            _si = si;
        }

        /// <summary>
        /// cancel synchronisation
        /// </summary>
        public void Cancel()
        {
            _ts.Cancel();
        }

        /// <summary>
        /// execute synchronisation async
        /// </summary>
        public async Task ExecuteSync()
        {
            _ts = new CancellationTokenSource();
            _ct = _ts.Token;

            _si.State = SyncState.DetectingChanges;

            if (_si.Link.Direction == SyncDirection.To1)
            {
                await GetSyncFilesOneWay(_si.Link.Path2, _si.Link.Path1);
                GetRemoveInfosOfDirOneWay(_si.Link.Path2, _si.Link.Path1, "");
            }
            else if (_si.Link.Direction == SyncDirection.To2)
            {
                await GetSyncFilesOneWay(_si.Link.Path1, _si.Link.Path2);
                GetRemoveInfosOfDirOneWay(_si.Link.Path1, _si.Link.Path2, "");
            }
            else if (_si.Link.Direction == SyncDirection.TwoWay)
            {
                await GetSyncFilesTwoWay();
            }


            _si.State = SyncState.CreatingFolders;
            await CreateFolders(_si.SyncDirs);

            _si.State = SyncState.ApplyingFileChanges;
            await ApplyingFileChanges(_si.SyncFiles);

            _si.State = SyncState.RemoveRedundantDirs;
            await RemoveFolders(_si.SyncDirs);
        }

        /// <summary>
        /// create directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private async Task CreateFolders(List<SyncDirInfo> syncDirs)
        {
            foreach (SyncDirInfo sdi in syncDirs.Where(d => !d.Remove))
            {
                Task t = RunFolderCreationTask(sdi);
                if (t != null) await t;
            }
        }

        /// <summary>
        /// delete directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private async Task RemoveFolders(List<SyncDirInfo> syncDirs)
        {
            foreach (SyncDirInfo sdi in syncDirs.Where(d => d.Remove).Reverse())
            {
                int result;
                Task t = RunFolderDeletionTask(sdi, out result);
                if (t != null) await t;
                else
                {
                    if (result == 2)
                        _si.Log(new LogMessage(LogType.ERROR, $"The directory to be deleted was not empty. Path: {sdi.Path}"));
                }
            }
        }

        /// <summary>
        /// delete directory in new task
        /// </summary>
        /// <param name="syncdi">directory information</param>
        /// <param name="result">
        /// 0: no error
        /// 1: the directory to be deleted was not found
        /// 2: the directory to be deleted was not empty
        /// </param>
        /// <returns>null if an error occurred</returns>
        private Task RunFolderDeletionTask(SyncDirInfo syncdi, out int result)
        {
            string ddp = (syncdi.Dir == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + syncdi.Path;
            Delimon.Win32.IO.DirectoryInfo ddi = new Delimon.Win32.IO.DirectoryInfo(ddp);

            if (!ddi.Exists)
            {
                result = 1;
                return null;
            }

            //do not remove if directory is not empty
            if (ddi.GetFiles().Length > 0 || ddi.GetDirectories().Length > 0)
            {
                result = 2;
                return null;
            }

            result = 0;

            return Task.Run(() =>
            {
                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                }

                syncdi.StartedNow();
                ddi.Delete();
                syncdi.EndedNow();
                _si.AppliedDirChange(syncdi);
            }, _ct);
        }

        /// <summary>
        /// create missing folders in new task
        /// </summary>
        /// <param name="syncdi">direcotry info</param>
        /// <returns></returns>
        private Task RunFolderCreationTask(SyncDirInfo syncdi)
        {
            string sdp = (syncdi.Dir == SyncDirection.To2 ? _si.Link.Path1 : _si.Link.Path2) + syncdi.Path;
            string ddp = (syncdi.Dir == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + syncdi.Path;

            Delimon.Win32.IO.DirectoryInfo sdi = new Delimon.Win32.IO.DirectoryInfo(sdp);
            Delimon.Win32.IO.DirectoryInfo ddi = new Delimon.Win32.IO.DirectoryInfo(ddp);

            if (!sdi.Exists)
                return null;

            return Task.Run(() =>
            {
                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                }

                syncdi.StartedNow();
                ddi.Create();
                syncdi.EndedNow();
                _si.AppliedDirChange(syncdi);
            }, _ct);
        }

        /// <summary>
        /// apply file changes to all synchronisation files async
        /// </summary>
        /// <returns>task</returns>
        private async Task ApplyingFileChanges(List<SyncFileInfo> syncFiles)
        {
            Console.WriteLine("test");
            //add apply file change tasks
            foreach (SyncFileInfo sfi in syncFiles)
            {
                Task<SyncFileInfo> t = RunApplyFileChangeTask(sfi);
                if (t != null)
                    _fileApplyTasks.Add(t);
            }

            //run apply file change tasks
            while (_fileApplyTasks.Count > 0)
            {
                Task<SyncFileInfo> t = await Task.WhenAny(_fileApplyTasks);

                _ct.ThrowIfCancellationRequested();

                if (t.Result.Conflicted)
                    _si.FileConflicted(t.Result);
                else
                    _si.AppliedFileChange(t.Result);

                _fileApplyTasks.Remove(t);
            }

            _fileApplyTasks = null;
        }

        /// <summary>
        /// apply change to a file in new task 
        /// including file deleting
        /// </summary>
        /// <param name="syncfi"></param>
        /// <returns></returns>
        private Task<SyncFileInfo> RunApplyFileChangeTask(SyncFileInfo syncfi)
        {
            return Task.Run(() =>
            {
                string sfp = (syncfi.Dir == SyncDirection.To2 ? _si.Link.Path1 : _si.Link.Path2) + syncfi.Path;
                string dfp = (syncfi.Dir == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + syncfi.Path;

                Delimon.Win32.IO.FileInfo sfi = new Delimon.Win32.IO.FileInfo(sfp);

                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }

                syncfi.StartedNow();

                try
                {
                    if (_ct.IsCancellationRequested) return null;

                    if (syncfi.Remove)
                    {
                        File.SetAttributes(dfp, FileAttributes.Normal); //change attributes to avoid UnauthorizedAccessException
                        File.Delete(dfp);
                    }
                    else if (sfi.Exists)
                    {
                        //uncomment Copy Methods:
                        //FMove(sfp, dfp);
                        File.Copy(sfp, dfp, true);
                    }
                    else
                        return null;

                    syncfi.EndedNow();
                }
                catch (IOException ioe)
                {
                    syncfi.EndedNow();

                    string path = ioe.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    syncfi.FileConflicted(new FileConflictInfo(ConflictType.IO, conflictPath, "RunApplyFileChangeTask", syncfi));
                }
                catch (UnauthorizedAccessException uae)
                {
                    syncfi.EndedNow();

                    string path = uae.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    syncfi.FileConflicted(new FileConflictInfo(ConflictType.UA, conflictPath, "RunApplyFileChangeTask", syncfi));
                }
                catch (Exception e)
                {
                    _si.Log(new LogMessage(LogType.ERROR, e.Message));
                }

                return syncfi;
            }, _ct);
        }

        ///// <summary> 
        ///// Fast file move with big buffers
        ///// http://www.codeproject.com/Tips/777322/A-Faster-File-Copy
        ///// </summary>
        ///// <param name="source">Source file path</param> 
        ///// <param name="destination">Destination file path</param>
        //private void FMove(string source, string destination)
        //{
        //    int arrayLength = (int)Math.Pow(2, 19);
        //    byte[] dataArray = new byte[arrayLength];
        //    using (FileStream fsread = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.None, arrayLength))
        //    {
        //        using (BinaryReader bwread = new BinaryReader(fsread))
        //        {
        //            using (FileStream fswrite = new FileStream(destination, FileMode.Create, FileAccess.Write, FileShare.None, arrayLength))
        //            {
        //                using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
        //                {
        //                    while(true)
        //                    {
        //                        while (_si.Paused)
        //                            Thread.Sleep(500);
        //                        _ct.ThrowIfCancellationRequested();

        //                        int read = bwread.Read(dataArray, 0, arrayLength);
        //                        if (0 == read)
        //                            break;
        //                        bwwrite.Write(dataArray, 0, read);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// detect all changes for Two-Way synchronisation async
        /// The order of the files does not matter
        /// </summary>
        /// <returns></returns>
        private async Task GetSyncFilesTwoWay()
        {
            //Fetching files recursively
            GetSyncFilesOfDirTwoWay("");

            while (_detectFileTasks.Count > 0)
            {
                Task<SyncFileInfo> t = await Task.WhenAny(_detectFileTasks);

                _ct.ThrowIfCancellationRequested();

                if (t.Result != null)
                {
                    if (t.Result.Conflicted)
                        _si.FileConflicted(t.Result);
                    else
                        _si.FileChangeDetected(t.Result);
                }
                _detectFileTasks.Remove(t);
            }
            _detectFileTasks = null;
        }

        /// <summary>
        /// detect changes for Two-Way synchronisation in directory relativePath recursively
        /// and compare tasks to _detectTasks
        /// files and directories to remove are also detected
        /// </summary>
        /// <param name="relativePath">path relative to the linked folders</param>
        private void GetSyncFilesOfDirTwoWay(string relativePath)
        {
            while (_si.Paused)
                Task.Delay(500, _ct).Wait(_ct);
            _ct.ThrowIfCancellationRequested();

            string path1 = _si.Link.Path1 + relativePath;
            string path2 = _si.Link.Path2 + relativePath;
            //directory info
            Delimon.Win32.IO.DirectoryInfo di1 = new Delimon.Win32.IO.DirectoryInfo(path1);
            Delimon.Win32.IO.DirectoryInfo di2 = new Delimon.Win32.IO.DirectoryInfo(path2);

            List<string> dirNames = new List<string>();
            List<string> fileNames = new List<string>();

            try
            {
                #region detect changes of directories
                if (di1.Exists)
                {
                    //loop through path1 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path1))
                    {
                        string dn = Delimon.Win32.IO.Path.GetFileName(name);
                        dirNames.Add(dn);
                        GetSyncFilesOfDirTwoWay(relativePath + "/" + dn);
                    }
                }
                else
                {
                    if (relativePath != "" && di1.Parent != null)
                    {
                        //compare the newest time of last write time or creation time
                        DateTime pd1ChangeTime = di1.Parent.CreationTime > di1.Parent.LastWriteTime ? di1.Parent.CreationTime : di1.Parent.LastWriteTime;
                        DateTime d2ChangeTime = di2.CreationTime > di2.LastWriteTime ? di2.CreationTime : di2.LastWriteTime;

                        if (pd1ChangeTime > d2ChangeTime)
                        {
                            if (_si.Link.Remove)
                            {
                                //remove directory 2 if remove is enabled and the parent directory 1 is new than directory 2
                                //note that directories only will be removed if they are empty after applying file changes
                                _si.DirChangeDetected(new SyncDirInfo(_si, relativePath, SyncDirection.To2, true));
                            }
                        }
                        else
                        {
                            //if directory 2 is newer than the parent directory 1 -> create directory 1
                            _si.DirChangeDetected(new SyncDirInfo(_si, relativePath, SyncDirection.To1, false));
                        }
                    }
                }

                if (di2.Exists)
                {
                    //loop through path2 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path2))
                    {
                        string dn = Delimon.Win32.IO.Path.GetFileName(name);
                        if (!dirNames.Contains(dn))
                            GetSyncFilesOfDirTwoWay(relativePath + "/" + dn);
                    }
                }
                else
                {
                    if (relativePath != "" && di2.Parent != null)
                    {
                        //compare the newest time of last write time or creation time
                        DateTime pd2ChangeTime = di2.Parent.LastWriteTime > di2.Parent.CreationTime ? di2.Parent.LastWriteTime : di2.Parent.CreationTime;
                        DateTime d1ChangeTime = di1.LastWriteTime > di1.CreationTime ? di1.LastWriteTime : di1.CreationTime;
                        if (pd2ChangeTime > d1ChangeTime)
                        {
                            if (_si.Link.Remove)
                            {
                                //remove directory 1 if remove is enabled and the parent directory 2 is new than directory 1
                                //note that directories only will be removed if they are empty after applying file changes
                                _si.DirChangeDetected(new SyncDirInfo(_si, relativePath, SyncDirection.To1, true));
                            }
                        }
                        else
                        {
                            //if directory 1 is newer than the parent directory 2 -> create directory 2
                            _si.DirChangeDetected(new SyncDirInfo(_si, relativePath, SyncDirection.To2, false));
                        }
                    }
                }
                #endregion

                #region detect changes of files
                if (di1.Exists)
                {
                    //Loop through all files in path1
                    foreach (string name in Delimon.Win32.IO.Directory.GetFiles(path1))
                    {
                        //detect changes of file asynchronously
                        fileNames.Add(name);
                        Task<SyncFileInfo> t = RunTwoWayFileCompareTask(name, relativePath);
                        _detectFileTasks.Add(t);
                    }
                }

                if (di2.Exists)
                {
                    //Loop through all files in path2
                    foreach (string name in Delimon.Win32.IO.Directory.GetFiles(path2))
                    {
                        //detect changes of file asynchronously
                        if (fileNames.Contains(name)) continue;
                        Task<SyncFileInfo> t = RunTwoWayFileCompareTask(name, relativePath);
                        _detectFileTasks.Add(t);
                    }
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                string path = uae.Message.Split('\"')[1];
                int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
                sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetSyncFilesOfDirTwoWay", sdi));
                _si.DirConflicted(sdi);
            }
            #endregion
        }

        Task<SyncFileInfo> debug_t;
        /// <summary>
        /// detect changes for One-Way synchronisation async
        /// </summary>
        /// <param name="sourceHomePath">source folder path</param>
        /// <param name="destHomePath">destination folder path</param>
        /// <returns></returns>
        private async Task GetSyncFilesOneWay(string sourceHomePath, string destHomePath)
        {
            //Fetching files recursively
            GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, "");

            while (_detectFileTasks.Count > 0)
            {
                Task<SyncFileInfo> t = await Task.WhenAny(_detectFileTasks);
                debug_t = t;
                _ct.ThrowIfCancellationRequested();

                if (t.Result != null)
                {
                    if (t.Result.Conflicted)
                        _si.FileConflicted(t.Result);
                    else
                        _si.FileChangeDetected(t.Result);
                }
                _detectFileTasks.Remove(t);
            }
            _detectFileTasks = null;
        }

        /// <summary>
        /// detect changes for One-Way synchronisation in directories recursively
        /// </summary>
        /// <param name="sourceHomePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destHomePath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="relativePath">path relative to the homepath</param>
        private void GetSyncFilesOfDirOneWay(string sourceHomePath, string destHomePath, string relativePath)
        {
            while (_si.Paused)
                Task.Delay(500, _ct).Wait(_ct);
            _ct.ThrowIfCancellationRequested();

            string sourcePath = sourceHomePath + relativePath;
            string destPath = destHomePath + relativePath;

            try
            {
                //create destination directory if not exists
                if (!new Delimon.Win32.IO.DirectoryInfo(destPath).Exists)
                    _si.DirChangeDetected(new SyncDirInfo(_si, relativePath, _si.Link.Direction, false));

                //detect source child directories
                foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(sourcePath))
                {
                    string dn = Delimon.Win32.IO.Path.GetFileName(name);
                    GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, relativePath + @"\" + dn);
                }

                //loop through all files in source directory
                foreach (string name in Delimon.Win32.IO.Directory.GetFiles(sourcePath))
                {
                    //detect changes of file asynchronously
                    Task<SyncFileInfo> t = RunOneWayFileCompareTask(sourcePath, destPath, name, relativePath);
                    _detectFileTasks.Add(t);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                string path = uae.Message.Split('\"')[1] + @"\";
                int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
                sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetSyncFilesOfDirOneWay", sdi));
                _si.DirConflicted(sdi);
            }
        }

        /// <summary>
        /// detect files and directories that should be removed for One-Way synchronisation recursively
        /// </summary>
        /// <param name="sourceHomePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destHomePath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="relativePath">path relative to the homepath</param>
        private void GetRemoveInfosOfDirOneWay(string sourceHomePath, string destHomePath, string relativePath)
        {
            while (_si.Paused)
                Task.Delay(500, _ct).Wait(_ct);
            _ct.ThrowIfCancellationRequested();

            string sourcePath = sourceHomePath + relativePath;
            string destPath = destHomePath + relativePath;

            try
            {
                //get directories to remove
                //detect destination child directories
                foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(destPath))
                {
                    string dn = Delimon.Win32.IO.Path.GetFileName(name);
                    string sourceDirPath = sourcePath + @"\" + dn;

                    //remove destination directory if source directory doesn't exist (if remove is enabled)
                    if (_si.Link.Remove && !new Delimon.Win32.IO.DirectoryInfo(sourceDirPath).Exists)
                    {
                        _si.DirChangeDetected(new SyncDirInfo(_si, relativePath + @"\" + dn, _si.Link.Direction, true));
                    }

                    GetRemoveInfosOfDirOneWay(sourceHomePath, destHomePath, relativePath + @"\" + dn);
                }

                //get files to remove
                //Loop through all files in destination directory
                foreach (string path in Delimon.Win32.IO.Directory.GetFiles(destPath))
                {
                    string fn = Delimon.Win32.IO.Path.GetFileName(path);
                    string sourceFilePath = sourcePath + @"\" + fn;
                    string destFilePath = destPath + @"\" + fn;

                    //remove destination file if source file doesn't exist (if remove is enabled)
                    if (_si.Link.Remove && !new Delimon.Win32.IO.FileInfo(sourceFilePath).Exists)
                    {
                        _si.FileChangeDetected(new SyncFileInfo(_si, relativePath + @"\" + fn,
                            new Delimon.Win32.IO.FileInfo(destFilePath).Length, _si.Link.Direction, true));
                    }
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                string path = uae.Message.Split('\"')[1] + @"\";
                int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
                sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetRemoveInfosOfDirOneWay", sdi));
                _si.DirConflicted(sdi);
            }
        }

        SyncFileInfo d_sfi;
        /// <summary>
        /// compare 2 files for one way synchronisation in new task
        /// </summary>
        /// <param name="sourcePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destPath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="fileName">file name</param>
        /// <param name="relativePath">file path relative to the homepath without filename</param>
        /// <returns></returns>
        private Task<SyncFileInfo> RunOneWayFileCompareTask(string sourcePath, string destPath, string fileName, string relativePath)
        {
            return Task.Run(() =>
            {
                Delimon.Win32.IO.FileInfo srcFileInfo;
                Delimon.Win32.IO.FileInfo destFileInfo;

                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }

                string fn = Delimon.Win32.IO.Path.GetFileName(fileName);
                string filePath = relativePath + @"\" + fn;

                _si.DetectingFile(filePath);

                string sf = sourcePath + @"\" + fn;
                string df = destPath + @"\" + fn;

                srcFileInfo = new Delimon.Win32.IO.FileInfo(sf);
                destFileInfo = new Delimon.Win32.IO.FileInfo(df); //TODO long file path not supported

                SyncFileInfo sfi = new SyncFileInfo(_si, filePath, srcFileInfo.Length, _si.Link.Direction, false);
                d_sfi = sfi;
                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }

                try
                {
                    if (CompareFilesForOneWay(srcFileInfo, destFileInfo))
                    {
                        Console.WriteLine($@"Detected:{relativePath}\{fn}");
                    }
                    else return null;
                }
                catch (IOException ioe)
                {
                    string path = ioe.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    Console.WriteLine($"File Conflicted: {filePath}");
                    sfi.FileConflicted(new FileConflictInfo(ConflictType.IO, conflictPath, "RunOneWayFileCompareTask", sfi));
                }
                catch
                {
                    sfi.FileConflicted(new FileConflictInfo(ConflictType.Unknown, 0, "RunOneWayFileCompareTask", sfi));
                }

                return sfi;
            }, _ct);
        }

        /// <summary>
        /// compare file in paths for two way synchronisation in new task
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <param name="relativePath">file path relative to the homedir without filename</param>
        /// <returns></returns>
        private Task<SyncFileInfo> RunTwoWayFileCompareTask(string fileName, string relativePath)
        {
            return Task.Run(() =>
            {
                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }

                string fn = Delimon.Win32.IO.Path.GetFileName(fileName);
                string relativeFilePath = relativePath + @"\" + fn;

                _si.DetectingFile(relativeFilePath);

                string pd1 = _si.Link.Path1 + relativePath;
                string pd2 = _si.Link.Path2 + relativePath;
                //parent directory info
                Delimon.Win32.IO.DirectoryInfo pdi1;
                while (!(pdi1 = new Delimon.Win32.IO.DirectoryInfo(pd1)).Exists)
                    pd1 = pd1.Substring(0, pd1.LastIndexOf(@"\", StringComparison.Ordinal));

                Delimon.Win32.IO.DirectoryInfo pdi2;
                while (!(pdi2 = new Delimon.Win32.IO.DirectoryInfo(pd2)).Exists)
                    pd2 = pd2.Substring(0, pd2.LastIndexOf(@"\", StringComparison.Ordinal));

                string f1 = pd1 + @"\" + fn;
                string f2 = pd2 + @"\" + fn;
                //file info
                Delimon.Win32.IO.FileInfo fi1 = new Delimon.Win32.IO.FileInfo(f1);
                Delimon.Win32.IO.FileInfo fi2 = new Delimon.Win32.IO.FileInfo(f2);


                while (_si.Paused)
                {
                    try
                    {
                        Task.Delay(500, _ct).Wait(_ct);
                    }
                    catch (OperationCanceledException)
                    {
                        return null;
                    }
                }

                SyncFileInfo sfi = new SyncFileInfo(_si, relativeFilePath, fi1.Exists ? fi1.Length : fi2.Length);
                try
                {
                    //compare
                    TwoWayCompareResult compResult = CompareFilesForTwoWay(fi1, fi2, _si.Link.Remove, pdi1, pdi2);

                    if (compResult != null)
                    {
                        sfi.Dir = compResult.Direction;
                        sfi.Remove = compResult.Remove;
                    }
                    else
                        return null;
                }
                catch (IOException ioe)
                {
                    string path = ioe.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;
                    sfi.FileConflicted(new FileConflictInfo(ConflictType.IO, conflictPath, "RunTwoWayFileCompareTask", sfi));
                }
                catch (Exception)
                {
                    sfi.FileConflicted(new FileConflictInfo(ConflictType.Unknown, 0, "RunTwoWayFileCompareTask", sfi));
                }

                return sfi;
            }, _ct);
        }

        /// <summary>
        /// Check if 2 Files are updated for one way synchronisation
        /// </summary>
        /// <param name="sfi">source file</param>
        /// <param name="dfi">destination file</param>
        /// <returns>true if the files are not updated</returns>
        private bool CompareFilesForOneWay(Delimon.Win32.IO.FileInfo sfi, Delimon.Win32.IO.FileInfo dfi)
        {
            return !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime ||
                (sfi.LastWriteTime < dfi.LastWriteTime && !FilesAreEqual(sfi, dfi));
        }

        /// <summary>
        /// Check if 2 Files are updated for two way synchronisation
        /// The order of the files does not matter
        /// </summary>
        /// <param name="fi1">file 1</param>
        /// <param name="fi2">file 2</param>
        /// <param name="remove">if remove is enabled</param>
        /// <param name="parentDir1">parent directory of file 1</param>
        /// <param name="parentDir2">parent directory of file 2</param>
        /// <returns>compare result</returns>
        private static TwoWayCompareResult CompareFilesForTwoWay(Delimon.Win32.IO.FileInfo fi1, Delimon.Win32.IO.FileInfo fi2, bool remove,
            Delimon.Win32.IO.DirectoryInfo parentDir1, Delimon.Win32.IO.DirectoryInfo parentDir2)
        {
            if (!fi1.Exists)
            {
                // if the parent directory of file 1 is older than file 2 -> create file in parent directory 1
                if (fi2.LastWriteTime >= parentDir1.LastWriteTime)
                    return new TwoWayCompareResult(SyncDirection.To1, false);

                // otherwise remove file 2 if remove ist enabled
                if (remove)
                    return new TwoWayCompareResult(SyncDirection.To2, true);

                return null;
            }

            if (!fi2.Exists)
            {
                // if the parent directory of file 2 is older than file 1 -> create file in parent directory 2
                if (fi1.LastWriteTime >= parentDir2.LastWriteTime)
                    return new TwoWayCompareResult(SyncDirection.To2, false);

                // otherwise remove file 1 if remove ist enabled
                if (remove)
                    return new TwoWayCompareResult(SyncDirection.To1, true);

                return null;
            }

            // update file 1 if file 2 is newer
            if (fi1.LastWriteTime < fi2.LastWriteTime)
                return new TwoWayCompareResult(SyncDirection.To1, false);

            // update file 2 if file 1 is newer
            if (fi1.LastWriteTime > fi2.LastWriteTime)
                return new TwoWayCompareResult(SyncDirection.To2, false);

            return null;
        }

        /// <summary>
        /// Compare Files Byte for Byte
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private bool FilesAreEqual(Delimon.Win32.IO.FileInfo first, Delimon.Win32.IO.FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;

                    while (_si.Paused)
                        Task.Delay(500, _ct).Wait(_ct);
                    _ct.ThrowIfCancellationRequested();
                }
            }

            return true;
        }
    }
}
