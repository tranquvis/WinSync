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
        private List<Task<SyncFileExecutionInfo>> _fileApplyTasks = new List<Task<SyncFileExecutionInfo>>();

        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        private int _tasksRunning = 0;
        public int TasksRunning
        {
            get
            {
                int count = 0;
                int nt = 0;
                for (int i = 0; i < runningTasks.Length; i++)
                {
                    if (runningTasks[i] != 0)
                    {
                        count++;
                        nt = 0;
                    }
                    else
                    {
                        if (nt > 5)
                            break;
                        nt++;
                    }
                }
                return count;
            }
        }

        private int[] runningTasks = new int[1000];

        private int _taskId = 1;
        public int NextTaskId
        {
            get { return _taskId++; }
        }
        public int TaskStarted(int taskId)
        {
            _tasksRunning++;
            bool added = false;
            for(int i = 0; i < runningTasks.Length; i++)
            {
                if (runningTasks[i] == 0)
                {
                    runningTasks[i] = taskId;
                    added = true;
                    break;
                }
            }

            if (!added)
            {

            }

            return taskId;
        }
        public void TaskEnded(int taskId)
        {
            _tasksRunning--;

            bool removed = false;
            for (int i = 0; i < runningTasks.Length; i++)
            {
                if (runningTasks[i] == taskId)
                {
                    runningTasks[i] = 0;
                    removed = true;
                    break;
                }
            }

            if (!removed)
            {
                //bool added = runningTasksAdded.Contains(taskId);
                //bool alreadyRemoved = runningTasksRemoved.Contains(taskId);
            }
        }

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

        public int MainTaskId; 
        /// <summary>
        /// execute synchronisation async
        /// </summary>
        public async Task ExecuteSync()
        {
            _ts = new CancellationTokenSource();
            _ct = _ts.Token;
            MainTaskId = TaskStarted(NextTaskId);

            if (!Delimon.Win32.IO.Directory.Exists(_si.Link.Path1) || !Delimon.Win32.IO.Directory.Exists(_si.Link.Path2))
            {
                throw new DirectoryNotFoundException("The directories to sync do not exist on this System.");
            }

            _si.State = SyncState.DetectingChanges;

            if (_si.Link.Direction == SyncDirection.To1)
            {
                await GetSyncFilesOneWay(_si.Link.Path2, _si.Link.Path1);
                GetRemoveInfosOfDirOneWay(_si.Link.Path2, _si.Link.Path1, new MyDirInfo("", ""));
            }
            else if (_si.Link.Direction == SyncDirection.To2)
            {
                await GetSyncFilesOneWay(_si.Link.Path1, _si.Link.Path2);
                GetRemoveInfosOfDirOneWay(_si.Link.Path1, _si.Link.Path2, new MyDirInfo("", ""));
            }
            else if (_si.Link.Direction == SyncDirection.TwoWay)
            {
                await GetSyncFilesTwoWay();
            }


            _si.State = SyncState.CreatingFolders;
            await CreateFolders(_si.SyncDirInfos);

            _si.State = SyncState.ApplyingFileChanges;
            await ApplyingFileChanges(_si.SyncFileInfos);

            _si.State = SyncState.RemoveRedundantDirs;
            await RemoveFolders(_si.SyncDirInfos);
        }

        /// <summary>
        /// create directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private async Task CreateFolders(List<SyncDirInfo> syncDirs)
        {
            foreach (SyncDirInfo sdi in syncDirs.Where(d => !d.SyncExecutionInfo.Remove))
            {
                Task t = RunFolderCreationTask(sdi.SyncDirExecutionInfo);
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
            foreach (SyncDirInfo sdi in syncDirs.Where(d => d.SyncExecutionInfo.Remove).Reverse())
            {
                Task t = RunFolderDeletionTask(sdi.SyncDirExecutionInfo);
                if (t != null) await t;
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
        private Task RunFolderDeletionTask(SyncDirExecutionInfo sdei)
        {
            string ddp = sdei.AbsoluteDestPath;
            Delimon.Win32.IO.DirectoryInfo ddi = new Delimon.Win32.IO.DirectoryInfo(ddp);

            if (!ddi.Exists)
            {
                return null;
            }

            //do not remove if directory is not empty
            if (ddi.GetFiles().Length > 0 || ddi.GetDirectories().Length > 0)
            {
                sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.DirNotEmpty, 
                    sdei.Direction == SyncDirection.To1 ? 1 : 2, "RunFolderDeletionTask", 
                    $"The directory to be deleted was not empty. Path: {sdei.SyncDirInfo.DirInfo.FullPath}"));
                return null;
            }

            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (pauseIfRequested(true, taskId)) return;

                sdei.StartedNow();
                try
                {
                    ddi.Delete();
                    sdei.SyncDirInfo.SyncState = SyncElementState.ChangeApplied;
                }
                catch (Exception e)
                {
                    sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.Unknown, 
                        sdei.Direction == SyncDirection.To2 ? 2 : 1, "RunFolderDeletionTask", e.Message));
                }
                sdei.EndedNow();

                TaskEnded(taskId);
            }, _ct);
        }

        /// <summary>
        /// create missing folders in new task
        /// </summary>
        /// <param name="di">direcotry info</param>
        /// <returns></returns>
        private Task RunFolderCreationTask(SyncDirExecutionInfo sdei)
        {
            string sdp = sdei.AbsoluteSourcePath;
            string ddp = sdei.AbsoluteDestPath;
            
            if (!Delimon.Win32.IO.Directory.Exists(sdp))
                return null;

            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (pauseIfRequested(true, taskId)) return;

                sdei.StartedNow();
                try
                {
                    Delimon.Win32.IO.Directory.CreateDirectory(ddp);
                    sdei.SyncElementInfo.SyncState = SyncElementState.ChangeApplied;
                }
                catch (Exception e)
                {
                    sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.Unknown,
                        sdei.Direction == SyncDirection.To2 ? 2 : 1, "RunFolderCreationTask", e.Message));
                }
                sdei.EndedNow();

                TaskEnded(taskId);
            }, _ct);
        }

        /// <summary>
        /// apply file changes to all synchronisation files async
        /// </summary>
        /// <returns>task</returns>
        private async Task ApplyingFileChanges(List<SyncFileInfo> syncFiles)
        {
            //add apply file change tasks
            foreach (SyncFileInfo sfi in syncFiles)
            {
                Task<SyncFileExecutionInfo> t;
                try
                {
                    t = RunApplyFileChangeTask(sfi.SyncFileExecutionInfo);
                    _fileApplyTasks.Add(t);
                }
                catch(Exception e)
                {

                }
            }

            //run apply file change tasks
            while (_fileApplyTasks.Count > 0)
            {
                Task<SyncFileExecutionInfo> t = await Task.WhenAny(_fileApplyTasks);

                _ct.ThrowIfCancellationRequested();
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
        private Task<SyncFileExecutionInfo> RunApplyFileChangeTask(SyncFileExecutionInfo sfei)
        {
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (pauseIfRequested(true, taskId)) return null;

                string sfp = sfei.AbsoluteSourcePath;
                string dfp = sfei.AbsoluteDestPath;

                Delimon.Win32.IO.FileInfo sfi = new Delimon.Win32.IO.FileInfo(sfp);

                sfei.StartedNow();

                try
                {

                    if (sfei.Remove)
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
                    sfei.SyncFileInfo.SyncState = SyncElementState.ChangeApplied;
                }
                catch (IOException ioe)
                {
                    string path = ioe.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.IO, conflictPath, "RunApplyFileChangeTask", ioe.Message));
                }
                catch (UnauthorizedAccessException uae)
                {
                    string path = uae.Message.Split('\"')[1];
                    int conflictPath = path.Contains(_si.Link.Path1) ? 1 : 2;

                    sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.UA, conflictPath, "RunApplyFileChangeTask", uae.Message));
                }
                catch (Exception e)
                {
                    sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.Unknown, 0, "RunApplyFileChangeTask", e.Message));
                }

                sfei.EndedNow();

                TaskEnded(taskId);

                return sfei;
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
            GetSyncFilesOfDirTwoWay(new MyDirInfo("", ""));

            while (_detectFileTasks.Count > 0)
            {
                Task<SyncFileInfo> t = await Task.WhenAny(_detectFileTasks);
                _ct.ThrowIfCancellationRequested();
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
            pauseIfRequested(false, MainTaskId);

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
                        dirNames.Add(newDirname);

                        MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);
                        new SyncDirInfo(_si, newDir);
                        GetSyncFilesOfDirTwoWay(newDir);
                    }
                }
                else
                {
                    if (dir.SyncDirInfo != null && di1.Parent != null)
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
                                new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To2, true);
                            }
                        }
                        else
                        {
                            //if directory 2 is newer than the parent directory 1 -> create directory 1
                            new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To1, false);
                        }
                    }
                }

                if (di2.Exists)
                {
                    //loop through path2 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path2))
                    {
                        string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                        if (!dirNames.Contains(newDirname))
                        {
                            MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);
                            new SyncDirInfo(_si, newDir);
                            GetSyncFilesOfDirTwoWay(newDir);
                        }
                    }
                }
                else
                {
                    if (dir.SyncDirInfo != null && di2.Parent != null)
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
                                new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To1, true);
                            }
                        }
                        else
                        {
                            //if directory 1 is newer than the parent directory 2 -> create directory 2
                            new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To2, false);
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
                        MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                        new SyncFileInfo(_si, file);

                        fileNames.Add(name);
                        Task<SyncFileInfo> t = RunTwoWayFileCompareTask(file.SyncFileInfo);
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

                        MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                        new SyncFileInfo(_si, file);

                        Task<SyncFileInfo> t = RunTwoWayFileCompareTask(file.SyncFileInfo);
                        _detectFileTasks.Add(t);
                    }
                }
            }
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
            GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, new MyDirInfo("", ""));
            await Task.WhenAll(_detectFileTasks);
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
            pauseIfRequested(false, MainTaskId);

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //create destination directory if not exists
                if (dir.SyncDirInfo != null && !new Delimon.Win32.IO.DirectoryInfo(destPath).Exists)
                {
                    new SyncDirExecutionInfo(dir.SyncDirInfo, _si.Link.Direction, false);
                }

                //detect source child directories
                foreach (string dirpath in Delimon.Win32.IO.Directory.GetDirectories(sourcePath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(dirpath);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, name);
                    new SyncDirInfo(_si, newDir);
                    GetSyncFilesOfDirOneWay(sourceHomePath, destHomePath, newDir);
                }

                //loop through all files in source directory
                foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(sourcePath))
                {
                    //detect changes of file asynchronously
                    string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                    MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                    new SyncFileInfo(_si, file);
                    Task<SyncFileInfo> t = RunOneWayFileCompareTask(sourceHomePath, destHomePath, file.SyncFileInfo);
                    _detectFileTasks.Add(t);
                }
            }
            catch (OperationCanceledException)
            {

            }
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
            pauseIfRequested(false, MainTaskId);

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //get directories to remove
                //detect destination child directories
                foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(destPath))
                {
                    string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);

                    //remove destination directory if source directory doesn't exist (if remove is enabled)
                    if (_si.Link.Remove && !new Delimon.Win32.IO.DirectoryInfo(newDir.FullPath).Exists)
                    {
                        new SyncDirInfo(_si, dir);
                        new SyncDirExecutionInfo(newDir.SyncDirInfo, _si.Link.Direction, true);
                    }

                    GetRemoveInfosOfDirOneWay(sourceHomePath, destHomePath, newDir);
                }

                //get files to remove
                //Loop through all files in destination directory
                foreach (string path in Delimon.Win32.IO.Directory.GetFiles(destPath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(path);

                    MyFileInfo file = new MyFileInfo(dir.FullPath, name);

                    string sourceFilePath = sourceHomePath + file.FullPath;
                    string destFilePath = destHomePath + file.FullPath;

                    //remove destination file if source file doesn't exist (if remove is enabled)
                    if (!new Delimon.Win32.IO.FileInfo(sourceFilePath).Exists)
                    {
                        new SyncFileInfo(_si, file);
                        if (_si.Link.Remove)
                        {
                            file.Size = new Delimon.Win32.IO.FileInfo(destFilePath).Length;
                            new SyncFileExecutionInfo(file.SyncFileInfo, _si.Link.Direction, true);
                        }
                    }
                }
            }
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
        private Task<SyncFileInfo> RunOneWayFileCompareTask(string sourcePath, string destPath, SyncFileInfo file)
        {
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (pauseIfRequested(true, taskId)) return null;

                Delimon.Win32.IO.FileInfo srcFileInfo;
                Delimon.Win32.IO.FileInfo destFileInfo;

                file.SyncState = SyncElementState.ChangeDetectingStarted;

                string sf = sourcePath + file.FileInfo.FullPath;
                string df = destPath + file.FileInfo.FullPath;

                srcFileInfo = new Delimon.Win32.IO.FileInfo(sf);
                destFileInfo = new Delimon.Win32.IO.FileInfo(df);

                try
                {
                    if (CompareFilesForOneWay(srcFileInfo, destFileInfo, taskId))
                    {
                        file.FileInfo.Size = srcFileInfo.Length;
                        new SyncFileExecutionInfo(file, _si.Link.Direction, false);
                    }
                }
                catch(Exception e)
                {
                    if (file.SyncInfo != null)
                        file.Conflicted(new FileConflictInfo(file, ConflictType.Unknown, 0, "RunOneWayFileCompareTask", e.Message));
                    else
                        _si.Log(new LogMessage(LogType.ERROR, e.Message));
                }

                TaskEnded(taskId);

                return file;
            }, _ct);
        }

        /// <summary>
        /// compare file in paths for two way synchronisation in new task
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <param name="relativePath">file path relative to the homedir without filename</param>
        /// <returns></returns>
        private Task<SyncFileInfo> RunTwoWayFileCompareTask(SyncFileInfo file)
        {
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (pauseIfRequested(true, taskId)) return null;

                file.SyncState = SyncElementState.ChangeDetectingStarted;

                string pd1 = _si.Link.Path1 + file.FileInfo.Path;
                string pd2 = _si.Link.Path2 + file.FileInfo.Path;

                //get parent directory infos
                Delimon.Win32.IO.DirectoryInfo pdi1;
                while (!(pdi1 = new Delimon.Win32.IO.DirectoryInfo(pd1)).Exists)
                    pd1 = pd1.Substring(0, pd1.LastIndexOf(@"\", StringComparison.Ordinal));

                Delimon.Win32.IO.DirectoryInfo pdi2;
                while (!(pdi2 = new Delimon.Win32.IO.DirectoryInfo(pd2)).Exists)
                    pd2 = pd2.Substring(0, pd2.LastIndexOf(@"\", StringComparison.Ordinal));

                string f1 = file.AbsolutePath1;
                string f2 = file.AbsolutePath2;

                //file info
                Delimon.Win32.IO.FileInfo fi1 = new Delimon.Win32.IO.FileInfo(f1);
                Delimon.Win32.IO.FileInfo fi2 = new Delimon.Win32.IO.FileInfo(f2);
                
                if (pauseIfRequested(true, taskId)) return null;

                try
                {
                    //compare
                    TwoWayCompareResult compResult = CompareFilesForTwoWay(fi1, fi2, _si.Link.Remove, pdi1, pdi2);

                    if (compResult == null)
                        file.SyncState = SyncElementState.NoChangeFound;
                    else
                    {
                        file.FileInfo.Size = fi1.Exists ? fi1.Length : fi2.Length;
                        new SyncFileExecutionInfo(file, compResult.Direction, compResult.Remove);
                    }
                }
                catch (Exception e)
                {
                    file.Conflicted(new FileConflictInfo(file, ConflictType.Unknown, 0, "RunTwoWayFileCompareTask", e.Message));
                }

                TaskEnded(taskId);
                return file;
            }, _ct);
        }

        /// <summary>
        /// Check if 2 Files are updated for one way synchronisation
        /// </summary>
        /// <param name="sfi">source file</param>
        /// <param name="dfi">destination file</param>
        /// <returns>true if the files are not updated</returns>
        private bool CompareFilesForOneWay(Delimon.Win32.IO.FileInfo sfi, Delimon.Win32.IO.FileInfo dfi, int taskId)
        {
            bool d = !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime || 
                (sfi.LastWriteTime < dfi.LastWriteTime && !FilesAreEqual(sfi, dfi, taskId));
            return !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime ||
                (sfi.LastWriteTime < dfi.LastWriteTime && !FilesAreEqual(sfi, dfi, taskId));
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
        private bool FilesAreEqual(Delimon.Win32.IO.FileInfo first, Delimon.Win32.IO.FileInfo second, int taskId)
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
                    
                    pauseIfRequested(false, taskId);
                }
            }

            return true;
        }

        /// <summary>
        /// pause task if requested until continuation
        /// </summary>
        /// <returns>true if the operation was canceled</returns>
        private bool pauseIfRequested(bool catchCanceledException, int taskId)
        {
            bool tEnded = false;

            while (_si.Paused)
            {
                if (!tEnded)
                {
                    tEnded = true;
                    TaskEnded(taskId);
                }

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

            if(_ct.IsCancellationRequested)
            {
                TaskEnded(taskId);
                if (catchCanceledException)
                    return true;
                else _ct.ThrowIfCancellationRequested();
            }

            if (tEnded)
                TaskStarted(taskId);

            return false;
        }
    }
}
