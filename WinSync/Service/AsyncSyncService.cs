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

        private List<Task<MyFileInfo>> _detectFileTasks = new List<Task<MyFileInfo>>();
        private List<Task<MyFileInfo>> _fileApplyTasks = new List<Task<MyFileInfo>>();

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

            if(!Delimon.Win32.IO.Directory.Exists(_si.Link.Path1) || !Delimon.Win32.IO.Directory.Exists(_si.Link.Path2))
            {
                throw new DirectoryNotFoundException("The directories to sync do not exist on this System.");
            }

            _si.State = SyncState.DetectingChanges;

            if (_si.Link.Direction == SyncDirection.To1)
            {
                await GetSyncFilesOneWay(_si.Link.Path2, _si.Link.Path1);
                GetRemoveInfosOfDirOneWay(_si.Link.Path2, _si.Link.Path1, new MyDirInfo("", "", null));
            }
            else if (_si.Link.Direction == SyncDirection.To2)
            {
                await GetSyncFilesOneWay(_si.Link.Path1, _si.Link.Path2);
                GetRemoveInfosOfDirOneWay(_si.Link.Path1, _si.Link.Path2, new MyDirInfo("", "", null));
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
        private async Task CreateFolders(List<MyDirInfo> syncDirs)
        {
            foreach (MyDirInfo di in syncDirs.Where(d => !d.SyncInfo.Remove))
            {
                Task t = RunFolderCreationTask(di);
                if (t != null) await t;
                if (di.SyncInfo.Conflicted)
                    _si.DirConflicted(di);
                else
                    _si.AppliedDirChange(di);
            }
        }

        /// <summary>
        /// delete directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private async Task RemoveFolders(List<MyDirInfo> syncDirs)
        {
            foreach (MyDirInfo di in syncDirs.Where(d => d.SyncInfo.Remove).Reverse())
            {
                Task t = RunFolderDeletionTask(di);
                if (t != null) await t;
                if (di.SyncInfo.Conflicted)
                    _si.DirConflicted(di);
                else
                    _si.AppliedDirChange(di);
            }
        }

        /// <summary>
        /// delete directory in new task
        /// </summary>
        /// <param name="di">directory information</param>
        /// <param name="result">
        /// 0: no error
        /// 1: the directory to be deleted was not found
        /// 2: the directory to be deleted was not empty
        /// </param>
        /// <returns>null if an error occurred</returns>
        private Task RunFolderDeletionTask(MyDirInfo di)
        {
            string ddp = (di.SyncInfo.Direction == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + di.FullPath;
            Delimon.Win32.IO.DirectoryInfo ddi = new Delimon.Win32.IO.DirectoryInfo(ddp);

            if (!ddi.Exists)
            {
                return null;
            }

            //do not remove if directory is not empty
            if (ddi.GetFiles().Length > 0 || ddi.GetDirectories().Length > 0)
            {
                di.SyncInfo.DirConflicted(new DirConflictInfo(ConflictType.DirNotEmpty, di.SyncInfo.Direction == SyncDirection.To1 ? 1 : 2,
                    "RunFolderDeletionTask", $"The directory to be deleted was not empty. Path: {di.FullPath}", di.SyncInfo));
                return null;
            }

            return Task.Run(() =>
            {
                if (pauseIfRequested(true)) return;

                di.SyncInfo.StartedNow();
                try
                {
                    ddi.Delete();
                }
                catch (Exception e)
                {
                    di.SyncInfo.DirConflicted(new DirConflictInfo(ConflictType.Unknown, di.SyncInfo.Direction == SyncDirection.To2 ? 2 : 1,
                        "RunFolderDeletionTask", e.Message, di.SyncInfo));
                }
                di.SyncInfo.EndedNow();
            }, _ct);
        }

        /// <summary>
        /// create missing folders in new task
        /// </summary>
        /// <param name="di">direcotry info</param>
        /// <returns></returns>
        private Task RunFolderCreationTask(MyDirInfo di)
        {
            string sdp = (di.SyncInfo.Direction == SyncDirection.To2 ? _si.Link.Path1 : _si.Link.Path2) + di.FullPath;
            string ddp = (di.SyncInfo.Direction == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + di.FullPath;
            
            if (!Delimon.Win32.IO.Directory.Exists(sdp))
                return null;

            return Task.Run(() =>
            {
                if (pauseIfRequested(true)) return;

                di.SyncInfo.StartedNow();
                try
                {
                    Delimon.Win32.IO.Directory.CreateDirectory(ddp);
                }
                catch (Exception e)
                {
                    di.SyncInfo.DirConflicted(new DirConflictInfo(ConflictType.Unknown, di.SyncInfo.Direction == SyncDirection.To2 ? 2 : 1, 
                        "RunFolderCreationTask", e.Message, di.SyncInfo));
                }
                di.SyncInfo.EndedNow();
            }, _ct);
        }

        /// <summary>
        /// apply file changes to all synchronisation files async
        /// </summary>
        /// <returns>task</returns>
        private async Task ApplyingFileChanges(List<MyFileInfo> syncFiles)
        {
            //add apply file change tasks
            foreach (MyFileInfo sfi in syncFiles)
            {
                Task<MyFileInfo> t = RunApplyFileChangeTask(sfi);
                if (t != null)
                    _fileApplyTasks.Add(t);
            }

            //run apply file change tasks
            while (_fileApplyTasks.Count > 0)
            {
                Task<MyFileInfo> t = await Task.WhenAny(_fileApplyTasks);

                _ct.ThrowIfCancellationRequested();

                if (t.Result != null)
                {
                    if (t.Result.SyncInfo.Conflicted)
                        _si.FileConflicted(t.Result);
                    else
                        _si.AppliedFileChange(t.Result);
                }
                _fileApplyTasks.Remove(t);
            }

            _fileApplyTasks = null;
        }

        /// <summary>
        /// apply change to a file in new task 
        /// including file deleting
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private Task<MyFileInfo> RunApplyFileChangeTask(MyFileInfo file)
        {
            return Task.Run(() =>
            {
                string sfp = (file.SyncInfo.Direction == SyncDirection.To2 ? _si.Link.Path1 : _si.Link.Path2) + file.FullPath;
                string dfp = (file.SyncInfo.Direction == SyncDirection.To1 ? _si.Link.Path1 : _si.Link.Path2) + file.FullPath;

                Delimon.Win32.IO.FileInfo sfi = new Delimon.Win32.IO.FileInfo(sfp);

                if(pauseIfRequested(true)) return null;

                file.SyncInfo.StartedNow();

                try
                {
                    if (_ct.IsCancellationRequested) return null;

                    if (file.SyncInfo.Remove)
                    {
                        File.SetAttributes(dfp, FileAttributes.Normal); //change attributes to avoid UnauthorizedAccessException
                        File.Delete(dfp);
                    }
                    else if (sfi.Exists)
                    {
                        //Copy Methods:
                        //FMove(sfp, dfp);
                        File.Copy(sfp, dfp, true);
                    }
                }
                catch (IOException ioe)
                {
                    string path = ioe.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    file.SyncInfo.FileConflicted(new FileConflictInfo(ConflictType.IO, conflictPath, "RunApplyFileChangeTask", ioe.Message, file.SyncInfo));
                }
                catch (UnauthorizedAccessException uae)
                {
                    string path = uae.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    file.SyncInfo.FileConflicted(new FileConflictInfo(ConflictType.UA, conflictPath, "RunApplyFileChangeTask", uae.Message, file.SyncInfo));
                }
                catch (Exception e)
                {
                    file.SyncInfo.FileConflicted(new FileConflictInfo(ConflictType.Unknown, 0, "RunApplyFileChangeTask", e.Message, file.SyncInfo));
                }
                file.SyncInfo.EndedNow();

                return file;
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
            GetSyncFilesOfDirTwoWay(new MyDirInfo("", "", null));

            while (_detectFileTasks.Count > 0)
            {
                Task<MyFileInfo> t = await Task.WhenAny(_detectFileTasks);

                _ct.ThrowIfCancellationRequested();

                if (t.Result.SyncInfo != null)
                {
                    if (t.Result.SyncInfo.Conflicted)
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
        private void GetSyncFilesOfDirTwoWay(MyDirInfo dir)
        {
            pauseIfRequested(false);
            _ct.ThrowIfCancellationRequested();

            if (dir.Name != "") _si.DirFound(dir);

            string path1 = _si.Link.Path1 + dir.FullPath;
            string path2 = _si.Link.Path2 + dir.FullPath;
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
                        string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                        MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname, null);
                        dirNames.Add(newDirname);
                        GetSyncFilesOfDirTwoWay(newDir);
                    }
                }
                else
                {
                    if (dir.FullPath != "" && di1.Parent != null)
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
                                dir.SyncInfo = new SyncDirInfo(_si, dir, SyncDirection.To2, true);
                                _si.DirChangeDetected(dir);
                            }
                        }
                        else
                        {
                            //if directory 2 is newer than the parent directory 1 -> create directory 1
                            dir.SyncInfo = new SyncDirInfo(_si, dir, SyncDirection.To1, false);
                            _si.DirChangeDetected(dir);
                        }
                    }
                }

                if (di2.Exists)
                {
                    //loop through path2 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path2))
                    {
                        string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                        MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname, null);
                        if (!dirNames.Contains(newDirname))
                            GetSyncFilesOfDirTwoWay(newDir);
                    }
                }
                else
                {
                    if (dir.FullPath != "" && di2.Parent != null)
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
                                dir.SyncInfo = new SyncDirInfo(_si, dir, SyncDirection.To1, true);
                                _si.DirChangeDetected(dir);
                            }
                        }
                        else
                        {
                            //if directory 1 is newer than the parent directory 2 -> create directory 2
                            dir.SyncInfo = new SyncDirInfo(_si, dir, SyncDirection.To2, false);
                            _si.DirChangeDetected(dir);
                        }
                    }
                }
                #endregion

                #region detect changes of files
                if (di1.Exists)
                {
                    //Loop through all files in path1
                    foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(path1))
                    {
                        //detect changes of file asynchronously
                        string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                        MyFileInfo file = new MyFileInfo(dir.FullPath, name, null);
                        _si.FileFound(file);
                        fileNames.Add(name);
                        Task<MyFileInfo> t = RunTwoWayFileCompareTask(file);
                        _detectFileTasks.Add(t);
                    }
                }

                if (di2.Exists)
                {
                    //Loop through all files in path2
                    foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(path2))
                    {
                        //detect changes of file asynchronously
                        string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                        if (fileNames.Contains(name)) continue;
                        MyFileInfo file = new MyFileInfo(dir.FullPath, name, null);
                        _si.FileFound(file);
                        Task<MyFileInfo> t = RunTwoWayFileCompareTask(file);
                        _detectFileTasks.Add(t);
                    }
                }
            }
            //catch (UnauthorizedAccessException uae)
            //{
            //    string path = uae.Message.Split('\"')[1];
            //    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

            //    SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
            //    sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetSyncFilesOfDirTwoWay", sdi));
            //    _si.DirConflicted(sdi);
            //}
            catch (Exception e)
            {
                _si.Log(new LogMessage(LogType.ERROR, e.Message));
            }
            #endregion
        }
        
        /// <summary>
        /// detect changes for One-Way synchronisation async
        /// </summary>
        /// <param name="sourceHomePath">source folder path</param>
        /// <param name="destHomePath">destination folder path</param>
        /// <returns></returns>
        private async Task GetSyncFilesOneWay(string sourceHomePath, string destHomePath)
        {
            //Fetching files recursively
            GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, new MyDirInfo("", "", null));

            while (_detectFileTasks.Count > 0)
            {
                Task<MyFileInfo> t = await Task.WhenAny(_detectFileTasks);
                _ct.ThrowIfCancellationRequested();

                if (t.Result.SyncInfo != null)
                {
                    if (t.Result.SyncInfo.Conflicted)
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
        private void GetSyncFilesOfDirOneWay(string sourceHomePath, string destHomePath, MyDirInfo dir)
        {
            pauseIfRequested(false);
            _ct.ThrowIfCancellationRequested();

            if(dir.Name != "") _si.DirFound(dir);

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //create destination directory if not exists
                if (!new Delimon.Win32.IO.DirectoryInfo(destPath).Exists)
                {
                    dir.SyncInfo = new SyncDirInfo(_si, dir, _si.Link.Direction, false);
                    _si.DirChangeDetected(dir);
                }

                //detect source child directories
                foreach (string dirpath in Delimon.Win32.IO.Directory.GetDirectories(sourcePath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(dirpath);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, name, null);
                    GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, newDir);
                }

                //loop through all files in source directory
                foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(sourcePath))
                {
                    //detect changes of file asynchronously
                    string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                    MyFileInfo file = new MyFileInfo(dir.FullPath, name, null);
                    _si.FileFound(file);
                    Task<MyFileInfo> t = RunOneWayFileCompareTask(sourceHomePath, destHomePath, file);
                    _detectFileTasks.Add(t);
                }
            }
            //catch (UnauthorizedAccessException uae)
            //{
            //    string path = uae.Message.Split('\"')[1] + @"\";
            //    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

            //    SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
            //    sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetSyncFilesOfDirOneWay", sdi));
            //    _si.DirConflicted(sdi);
            //}
            catch (Exception e)
            {
                _si.Log(new LogMessage(LogType.ERROR, e.Message));
            }
        }

        /// <summary>
        /// detect files and directories that should be removed for One-Way synchronisation recursively
        /// </summary>
        /// <param name="sourceHomePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destHomePath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="relativePath">path relative to the homepath</param>
        private void GetRemoveInfosOfDirOneWay(string sourceHomePath, string destHomePath, MyDirInfo dir)
        {
            pauseIfRequested(false);
            _ct.ThrowIfCancellationRequested();

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //get directories to remove
                //detect destination child directories
                foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(destPath))
                {
                    string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname, null);

                    //remove destination directory if source directory doesn't exist (if remove is enabled)
                    if (_si.Link.Remove && !new Delimon.Win32.IO.DirectoryInfo(newDir.FullPath).Exists)
                    {
                        newDir.SyncInfo = new SyncDirInfo(_si, dir, _si.Link.Direction, true);
                        _si.DirChangeDetected(newDir);
                    }

                    GetRemoveInfosOfDirOneWay(sourceHomePath, destHomePath, newDir);
                }

                //get files to remove
                //Loop through all files in destination directory
                foreach (string path in Delimon.Win32.IO.Directory.GetFiles(destPath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(path);
                    MyFileInfo file = new MyFileInfo(dir.FullPath, name, null);
                    string sourceFilePath = sourceHomePath + file.FullPath;
                    string destFilePath = destHomePath + file.FullPath;

                    //remove destination file if source file doesn't exist (if remove is enabled)
                    if (!new Delimon.Win32.IO.FileInfo(sourceFilePath).Exists)
                    {
                        _si.FileFound(file);

                        if (_si.Link.Remove)
                        {
                            file.Size = new Delimon.Win32.IO.FileInfo(destFilePath).Length;
                            file.SyncInfo = new SyncFileInfo(_si, file, _si.Link.Direction, true);
                            _si.FileChangeDetected(file);
                        }
                    }
                }
            }
            //catch (UnauthorizedAccessException uae)
            //{
            //    string path = uae.Message.Split('\"')[1] + @"\";
            //    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

            //    SyncDirInfo sdi = new SyncDirInfo(_si, relativePath);
            //    sdi.DirConflicted(new DirConflictInfo(ConflictType.UA, conflictPath, "GetRemoveInfosOfDirOneWay", sdi));
            //    _si.DirConflicted(sdi);
            //}
            catch(Exception e)
            {
                _si.Log(new LogMessage(LogType.ERROR, e.Message));
            }
        }
        
        /// <summary>
        /// compare 2 files for one way synchronisation in new task
        /// </summary>
        /// <param name="sourcePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destPath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="fileName">file name</param>
        /// <param name="relativePath">file path relative to the homepath without filename</param>
        /// <returns></returns>
        private Task<MyFileInfo> RunOneWayFileCompareTask(string sourcePath, string destPath, MyFileInfo file)
        {
            return Task.Run(() =>
            {
                Delimon.Win32.IO.FileInfo srcFileInfo;
                Delimon.Win32.IO.FileInfo destFileInfo;

                if (pauseIfRequested(true)) return null;

                _si.DetectingFile(file);

                string sf = sourcePath + file.FullPath;
                string df = destPath + file.FullPath;

                srcFileInfo = new Delimon.Win32.IO.FileInfo(sf);
                destFileInfo = new Delimon.Win32.IO.FileInfo(df);

                try
                {
                    if (CompareFilesForOneWay(srcFileInfo, destFileInfo))
                    {
                        file.Size = srcFileInfo.Length;
                        file.SyncInfo = new SyncFileInfo(_si, file, _si.Link.Direction, false);
                    }
                }
                //catch (IOException ioe)
                //{
                //    string path = ioe.Message.Split('\"')[1];
                //    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                //    Console.WriteLine($"File Conflicted: {filePath}");
                //    sfi.FileConflicted(new FileConflictInfo(ConflictType.IO, conflictPath, "RunOneWayFileCompareTask", sfi));
                //}
                catch(Exception e)
                {
                    if (file.SyncInfo != null)
                        file.SyncInfo.FileConflicted(new FileConflictInfo(ConflictType.Unknown, 0, "RunOneWayFileCompareTask", e.Message, file.SyncInfo));
                    else
                        _si.Log(new LogMessage(LogType.ERROR, e.Message));
                }

                return file;
            }, _ct);
        }

        /// <summary>
        /// compare file in paths for two way synchronisation in new task
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <param name="relativePath">file path relative to the homedir without filename</param>
        /// <returns></returns>
        private Task<MyFileInfo> RunTwoWayFileCompareTask(MyFileInfo file)
        {
            return Task.Run(() =>
            {
                if (pauseIfRequested(true)) return null;

                _si.DetectingFile(file);

                string pd1 = _si.Link.Path1 + file.Path;
                string pd2 = _si.Link.Path2 + file.Path;

                //get parent directory infos
                Delimon.Win32.IO.DirectoryInfo pdi1;
                while (!(pdi1 = new Delimon.Win32.IO.DirectoryInfo(pd1)).Exists)
                    pd1 = pd1.Substring(0, pd1.LastIndexOf(@"\", StringComparison.Ordinal));

                Delimon.Win32.IO.DirectoryInfo pdi2;
                while (!(pdi2 = new Delimon.Win32.IO.DirectoryInfo(pd2)).Exists)
                    pd2 = pd2.Substring(0, pd2.LastIndexOf(@"\", StringComparison.Ordinal));

                string f1 = _si.Link.Path1 + file.FullPath;
                string f2 = _si.Link.Path2 + file.FullPath;

                //file info
                Delimon.Win32.IO.FileInfo fi1 = new Delimon.Win32.IO.FileInfo(f1);
                Delimon.Win32.IO.FileInfo fi2 = new Delimon.Win32.IO.FileInfo(f2);


                if (pauseIfRequested(true)) return null;
                
                try
                {
                    //compare
                    TwoWayCompareResult compResult = CompareFilesForTwoWay(fi1, fi2, _si.Link.Remove, pdi1, pdi2);

                    if (compResult != null)
                    {
                        file.Size = fi1.Exists ? fi1.Length : fi2.Length;
                        file.SyncInfo = new SyncFileInfo(_si, file, compResult.Direction, compResult.Remove);
                    }
                }
                catch (Exception e)
                {
                    if (file.SyncInfo != null)
                        file.SyncInfo.FileConflicted(new FileConflictInfo(ConflictType.Unknown, 0, "RunTwoWayFileCompareTask", e.Message, file.SyncInfo));
                    else
                        _si.Log(new LogMessage(LogType.ERROR, e.Message));
                }

                return file;
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
            bool d = !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime || 
                (sfi.LastWriteTime < dfi.LastWriteTime && !FilesAreEqual(sfi, dfi));
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

                    pauseIfRequested(false);
                    _ct.ThrowIfCancellationRequested();
                }
            }

            return true;
        }

        /// <summary>
        /// pause task if requested until continuation
        /// </summary>
        /// <returns>true if the operation was canceled</returns>
        private bool pauseIfRequested(bool catchCanceledException)
        {
            while (_si.Paused)
            {
                try
                {
                    Task.Delay(500, _ct).Wait(_ct);
                }
                catch (OperationCanceledException)
                {
                    if(catchCanceledException)
                        return true;
                    else throw;
                }
            }

            return false;
        }
    }
}
