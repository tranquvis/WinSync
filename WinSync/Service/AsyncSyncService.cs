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

        #region task management
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

        public int MainTaskId;

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
        #endregion

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
            MainTaskId = TaskStarted(NextTaskId);

            if (!Delimon.Win32.IO.Directory.Exists(_si.Link.Path1) || !Delimon.Win32.IO.Directory.Exists(_si.Link.Path2))
            {
                throw new DirectoryNotFoundException("The directories to sync do not exist on this System.");
            }

            _si.Status = SyncStatus.DetectingChanges;

            if (_si.Link.Direction == SyncDirection.To1)
            {
                await GetSyncFilesOneWay(_si.Link.Path2, _si.Link.Path1);
                Helper.GetRemoveInfosOfDirRecursively_OneWay(_si.Link.Path2, _si.Link.Path1, new MyDirInfo("", ""), 
                    _si, () => pauseIfRequested(false, MainTaskId));
            }
            else if (_si.Link.Direction == SyncDirection.To2)
            {
                await GetSyncFilesOneWay(_si.Link.Path1, _si.Link.Path2);
                Helper.GetRemoveInfosOfDirRecursively_OneWay(_si.Link.Path1, _si.Link.Path2, new MyDirInfo("", ""),
                    _si, () => pauseIfRequested(false, MainTaskId));
            }
            else if (_si.Link.Direction == SyncDirection.TwoWay)
            {
                await GetSyncFilesTwoWay();
            }


            _si.Status = SyncStatus.CreatingFolders;
            await CreateFolders(_si.SyncDirExecutionInfos);

            _si.Status = SyncStatus.ApplyingFileChanges;
            await DoApplyFileChanges(_si.SyncFileExecutionInfos);

            _si.Status = SyncStatus.RemoveRedundantDirs;
            await RemoveFolders(_si.SyncDirExecutionInfos);
        }

        /// <summary>
        /// create directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private async Task CreateFolders(List<SyncDirExecutionInfo> syncDirs)
        {
            foreach (SyncDirExecutionInfo sdi in syncDirs.Where(d => !d.Remove))
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
        private async Task RemoveFolders(List<SyncDirExecutionInfo> syncDirs)
        {
            foreach (SyncDirExecutionInfo sdei in syncDirs.Where(d => d.Remove).Reverse())
            {
                Task t = RunFolderDeletionTask(sdei);
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
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if (Helper.DeleteFolder(sdei, () => pauseIfRequested(true, taskId)))
                    return;
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
                if (Helper.CreateFolder(sdei, () => pauseIfRequested(true, taskId)))
                    return;
                TaskEnded(taskId);
            }, _ct);
        }

        /// <summary>
        /// apply file changes to all synchronisation files async
        /// </summary>
        /// <returns>task</returns>
        private async Task DoApplyFileChanges(List<SyncFileExecutionInfo> syncFiles)
        {
            //add apply file change tasks
            foreach (SyncFileExecutionInfo sfei in syncFiles)
            {
                Task<SyncFileExecutionInfo> t = RunApplyFileChangeTask(sfei);
                _fileApplyTasks.Add(t);
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
                if(Helper.ApplyFileChange(sfei, () => pauseIfRequested(true, taskId)))
                    return null;
                TaskEnded(taskId);
                return sfei;
            }, _ct);
        }

        /// <summary>
        /// detect all changes for Two-Way synchronisation async
        /// The order of the files does not matter
        /// </summary>
        /// <returns></returns>
        private async Task GetSyncFilesTwoWay()
        {
            //Fetching files recursively
            Helper.FetchFilesInDirRecursively_TwoWay(new MyDirInfo("", ""), _si, 
                (sfi) => _detectFileTasks.Add(RunTwoWayFileCompareTask(sfi)),
                () => pauseIfRequested(false, MainTaskId));

            while (_detectFileTasks.Count > 0)
            {
                Task<SyncFileInfo> t = await Task.WhenAny(_detectFileTasks);
                _ct.ThrowIfCancellationRequested();
                _detectFileTasks.Remove(t);
            }
            _detectFileTasks = null;
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
            Helper.FetchFilesInDirRecursively_OneWay(sourceHomePath, destHomePath, new MyDirInfo("", ""),
                _si, (sfi) => _detectFileTasks.Add(RunOneWayFileCompareTask(sourceHomePath, destHomePath, sfi)),
                () => pauseIfRequested(false, MainTaskId));
            
            await Task.WhenAll(_detectFileTasks);
            _detectFileTasks = null;
        }
        
        /// <summary>
        /// compare 2 files for one way synchronisation in new task
        /// </summary>
        /// <see cref="Helper.DoFileComparison_OneWay(string, string, SyncFileInfo, Func{bool})"/>
        private Task<SyncFileInfo> RunOneWayFileCompareTask(string sourcePath, string destPath, SyncFileInfo file)
        {
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                Helper.DoFileComparison_OneWay(sourcePath, destPath, file, () => pauseIfRequested(true, taskId));
                TaskEnded(taskId);
                return file;
            }, _ct);
        }

        /// <summary>
        /// compare 2 files for two way synchronisation in new task
        /// </summary>
        /// <see cref="RunTwoWayFileCompareTask(SyncFileInfo)"/>
        private Task<SyncFileInfo> RunTwoWayFileCompareTask(SyncFileInfo file)
        {
            return Task.Run(() =>
            {
                int taskId = TaskStarted(NextTaskId);
                if(Helper.DoFileComparison_TwoWay(file, () => pauseIfRequested(true, taskId))) return null;
                TaskEnded(taskId);
                return file;
            }, _ct);
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
