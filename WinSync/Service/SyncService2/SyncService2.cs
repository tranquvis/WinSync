using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinSync.Service
{
    class SyncService2
    {
        private SyncInfo _si;
        private CancellationTokenSource _ts;
        private CancellationToken _ct;

        public SyncService2(SyncInfo si)
        {
            _si = si;
        }
        
        public int TasksRunning { get; set; }

        public void ExecuteSync()
        {
            _ts = new CancellationTokenSource();
            _ct = _ts.Token;

            if (!Delimon.Win32.IO.Directory.Exists(_si.Link.Path1) || !Delimon.Win32.IO.Directory.Exists(_si.Link.Path2))
            {
                throw new DirectoryNotFoundException("The directories to sync do not exist on this System.");
            }
            
            _si.Status = SyncStatus.FetchingElements;

            if (_si.Link.Direction == SyncDirection.To1)
            {
                Helper.FetchFilesInDirRecursively_OneWay(_si.Link.Path2, _si.Link.Path1, new MyDirInfo("", ""),
                _si, (sfi) => { },
                CheckInterrupt1);

                _si.Status = SyncStatus.DetectingChanges;

                foreach (MyFileInfo fi in DirTree.GetFiles(_si.DirTree))
                    Helper.DoFileComparison_OneWay(_si.Link.Path2, _si.Link.Path1, fi.SyncFileInfo, CheckInterrupt1);

                Helper.GetRemoveInfosOfDirRecursively_OneWay(_si.Link.Path2, _si.Link.Path1, new MyDirInfo("", ""), 
                    _si, CheckInterrupt1);
            }
            else if (_si.Link.Direction == SyncDirection.To2)
            {
                Helper.FetchFilesInDirRecursively_OneWay(_si.Link.Path1, _si.Link.Path2, new MyDirInfo("", ""),
                _si, (sfi) => { },
                CheckInterrupt1);

                _si.Status = SyncStatus.DetectingChanges;

                foreach (MyFileInfo fi in DirTree.GetFiles(_si.DirTree))
                    Helper.DoFileComparison_OneWay(_si.Link.Path2, _si.Link.Path1, fi.SyncFileInfo, CheckInterrupt1);

                Helper.GetRemoveInfosOfDirRecursively_OneWay(_si.Link.Path1, _si.Link.Path2, new MyDirInfo("", ""), 
                    _si, CheckInterrupt1);
            }
            else if (_si.Link.Direction == SyncDirection.TwoWay)
            {
                Helper.FetchFilesInDirRecursively_TwoWay(new MyDirInfo("", ""), _si, 
                    (sfi) => { }, CheckInterrupt1);

                _si.Status = SyncStatus.DetectingChanges;

                foreach (MyFileInfo fi in DirTree.GetFiles(_si.DirTree))
                    Helper.DoFileComparison_TwoWay(fi.SyncFileInfo, CheckInterrupt1);
            }


            _si.Status = SyncStatus.CreatingFolders;
            CreateFolders(_si.SyncDirExecutionInfos);

            _si.Status = SyncStatus.ApplyingFileChanges;
            DoApplyFileChanges(_si.SyncFileExecutionInfos);

            _si.Status = SyncStatus.RemoveDirs;
            RemoveFolders(_si.SyncDirExecutionInfos);
        }

        public void Cancel()
        {
            _ts.Cancel();
        }

        /// <summary>
        /// create directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private void CreateFolders(List<SyncDirExecutionInfo> exeInfos)
        {
            foreach (SyncDirExecutionInfo ei in exeInfos.Where(d => !d.Remove))
            {
                Helper.CreateFolder(ei, CheckInterrupt1);
            }
        }

        /// <summary>
        /// delete directories
        /// </summary>
        /// <param name="syncDirs">directory informations</param>
        /// <returns></returns>
        private void RemoveFolders(List<SyncDirExecutionInfo> syncDirs)
        {
            foreach (SyncDirExecutionInfo sdei in syncDirs.Where(d => d.Remove).Reverse())
            {
                Helper.DeleteFolder(sdei, CheckInterrupt1);
            }
        }

        /// <summary>
        /// apply file changes to all synchronisation files async
        /// </summary>
        /// <returns>task</returns>
        private void DoApplyFileChanges(List<SyncFileExecutionInfo> syncFiles)
        {
            foreach (SyncFileExecutionInfo sfei in syncFiles)
            {
                Helper.ApplyFileChange(sfei, CheckInterrupt1);
            }
        }

        /// <summary>
        /// pause task if requested until continuation
        /// </summary>
        /// <returns>true if the operation was canceled</returns>
        private bool checkIfInterrRequ(bool catchCanceledException)
        {
            bool tEnded = false;

            while (_si.Paused)
            {
                if (!tEnded)
                {
                    tEnded = true;
                    TasksRunning--;
                }

                try
                {
                    Task.Delay(500, _ct).Wait(_ct);
                }
                catch (OperationCanceledException)
                {
                    if (catchCanceledException)
                        return true;
                    else throw;
                }
            }

            if (_ct.IsCancellationRequested)
            {
                TasksRunning--;
                if (catchCanceledException)
                    return true;
                else _ct.ThrowIfCancellationRequested();
            }

            if (tEnded)
                TasksRunning++;

            return false;
        }

        private bool CheckInterrupt1()
        {
            return checkIfInterrRequ(false);
        }
    }
}
