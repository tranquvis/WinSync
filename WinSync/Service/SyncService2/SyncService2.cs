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
                throw new DirectoryNotFoundException();
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
                Helper.FetchElementsInDirRecursively_TwoWay(new MyDirInfo("", ""), _si, 
                    (sfi) => { }, CheckInterrupt1);

                _si.Status = SyncStatus.DetectingChanges;

                Helper.FetchChangesInDirRecursively_TwoWay(_si.DirTree, CheckInterrupt1, null);
            }


            _si.Status = SyncStatus.CreatingFolders;
            CreateFolders(_si.DirTree);

            _si.Status = SyncStatus.ApplyingFileChanges;
            DoApplyFileChanges(_si.DirTree);

            _si.Status = SyncStatus.RemoveDirs;
            RemoveFolders(_si.DirTree);
        }

        public void Cancel()
        {
            _ts.Cancel();
        }

        /// <summary>
        /// create directories
        /// </summary>
        /// <param name="dirTree">directory tree</param>
        /// <returns></returns>
        private void CreateFolders(DirTree dirTree)
        {
            foreach (DirTree childDir in dirTree.Dirs)
            {
                CreateFolders(childDir);
                if (childDir.Info.SyncDirInfo?.SyncDirExecutionInfo != null && !childDir.Info.SyncDirInfo.SyncDirExecutionInfo.Remove)
                    Helper.CreateFolder(childDir.Info.SyncDirInfo.SyncDirExecutionInfo, CheckInterrupt1);
            }
        }

        /// <summary>
        /// delete directories
        /// </summary>
        /// <param name="dirTree">directory tree</param>
        /// <returns></returns>
        private void RemoveFolders(DirTree dirTree)
        {
            foreach(DirTree childDir in dirTree.Dirs)
            {
                RemoveFolders(childDir);
                if(childDir.Info.SyncDirInfo?.SyncDirExecutionInfo != null && childDir.Info.SyncDirInfo.SyncDirExecutionInfo.Remove)
                    Helper.DeleteFolder(childDir.Info.SyncDirInfo.SyncDirExecutionInfo, CheckInterrupt1);
            }
        }

        /// <summary>
        /// apply file changes to all synchronisation files
        /// </summary>
        /// <param name="dirTree">directory tree</param>
        /// <returns></returns>
        private void DoApplyFileChanges(DirTree dirTree)
        {
            foreach (DirTree childDir in dirTree.Dirs)
                DoApplyFileChanges(childDir);

            foreach(MyFileInfo file in dirTree.Files)
            {
                if (file.SyncFileInfo?.SyncFileExecutionInfo != null)
                    Helper.ApplyFileChange(file.SyncFileInfo.SyncFileExecutionInfo, CheckInterrupt1);
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
