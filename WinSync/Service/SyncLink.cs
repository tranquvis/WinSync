using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncLink : Data.Link
    {
        /// <summary>
        /// contains the synchronisation executer
        /// </summary>
        public SyncTask SyncTask { get; private set; }

        /// <summary>
        /// contains the status information of the synchronisation
        /// </summary>
        public SyncInfo SyncInfo { get; private set; }

        /// <summary>
        /// create Sync Link
        /// <param name="link">Link Data</param>
        /// </summary>
        public SyncLink(Data.Link link)
            : base(link.Title, link.Path1, link.Path2, link.Direction, link.Remove) {}

        /// <summary>
        /// check if synchronisation is running
        /// </summary>
        /// <returns></returns>
        public bool IsRunning
        {
            get
            {
                return SyncInfo != null && SyncInfo.Running && SyncTask != null;
            }
        }

        /// <summary>
        /// execute synchronisation, set listener before
        /// </summary>
        /// <param name="syncListener">listener or null if no listener should be set</param>
        public void Sync(ISyncListener syncListener)
        {
            SyncInfo = new SyncInfo(this);
            if(syncListener != null)
                SyncInfo.SetListener(syncListener);

            SyncTask = new SyncTask2(SyncInfo);
            SyncTask.Execute();
        }

        /// <summary>
        /// cancel synchronisation
        /// </summary>
        public void CancelSync()
        {
            if (!IsRunning) return;

            SyncTask.Cancel();
            SyncInfo.SyncCancelled();
            SyncTask = null;
        }

        /// <summary>
        /// pause synchronisation
        /// </summary>
        public void PauseSync()
        {
            if (!IsRunning) return;

            SyncTask.Pause();
            SyncInfo.SyncPaused();
        }

        /// <summary>
        /// resume synchronisation
        /// </summary>
        public void ResumeSync()
        {
            if (!IsRunning) return;

            SyncTask.Resume();
            SyncInfo.SyncContinued();
        }

        /// <summary>
        /// check if the sync is executable on the current system
        /// </summary>
        /// <returns></returns>
        public bool IsExecutable()
        {
            return Delimon.Win32.IO.Directory.Exists(Path1) && Delimon.Win32.IO.Directory.Exists(Path2);
        }
    }
}
