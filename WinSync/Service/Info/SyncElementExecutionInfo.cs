using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public abstract class SyncElementExecutionInfo
    {
        public SyncElementInfo SyncElementInfo { get; }

        public SyncElementExecutionInfo(SyncElementInfo syncElementInfo, SyncDirection dir, bool remove)
        {
            SyncElementInfo = syncElementInfo;
            syncElementInfo.SyncExecutionInfo = this;
            Direction = dir;
            Remove = remove;
            SyncElementInfo.SyncState = SyncElementState.ChangeFound;
        }

        /// <summary>
        /// synchronisation direction
        /// </summary>
        public SyncDirection Direction { get; set; }

        /// <summary>
        /// if destination file should be removed
        /// </summary>
        public bool Remove { get; set; }

        /// <summary>
        /// set synchronisation start time of file to now
        /// </summary>
        public void StartedNow()
        {
            SyncStart = DateTime.Now;
        }

        /// <summary>
        /// set synchronisation end time of file to now
        /// </summary>
        public void EndedNow()
        {
            SyncEnd = DateTime.Now;
        }

        /// <summary>
        /// synchronisation start time
        /// </summary>
        public DateTime SyncStart { get; private set; }

        /// <summary>
        /// synchronisation end time
        /// </summary>
        public DateTime? SyncEnd { get; private set; }

        /// <summary>
        /// in milliseconds
        /// </summary>
        public TimeSpan SyncDuration => Synced ? (SyncEnd - SyncStart).Value : TimeSpan.Zero;

        /// <summary>
        /// if synchronisation has finished
        /// </summary>
        public bool Synced => SyncEnd != null;

        public string AbsoluteSourcePath => Direction == SyncDirection.To2 ? SyncElementInfo.AbsolutePath1 : SyncElementInfo.AbsolutePath2;
        public string AbsoluteDestPath => Direction == SyncDirection.To1 ? SyncElementInfo.AbsolutePath1 : SyncElementInfo.AbsolutePath2;
    }
}
