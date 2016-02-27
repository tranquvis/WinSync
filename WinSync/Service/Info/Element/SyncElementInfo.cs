using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public abstract class SyncElementInfo
    {
        public SyncInfo SyncInfo { get; protected set; }

        public MyElementInfo ElementInfo { get; protected set; }

        private SyncElementStatus _syncStatus;
        public SyncElementStatus SyncStatus {
            get { return _syncStatus; }
            set
            {
                if (_syncStatus == SyncElementStatus.ChangeDetectingStarted)
                    SyncInfo.DetectingEnded(this);
                _syncStatus = value;
                SyncInfo.SyncElementStatusChanged(this);
            }
        }

        public SyncElementExecutionInfo SyncExecutionInfo { get; set; }

        /// <summary>
        /// create SyncElementInfo
        /// </summary>
        public SyncElementInfo(SyncInfo syncInfo, MyElementInfo elementInfo)
        {
            SyncInfo = syncInfo;
            ElementInfo = elementInfo;
            ElementInfo.SyncElementInfo = this;

            SyncStatus = SyncElementStatus.ElementFound;
        }

        /// <summary>
        /// call when conflicted
        /// </summary>
        /// <param name="ci">conflict info</param>
        public void Conflicted(ElementConflictInfo ci)
        {
            ConflictInfo = ci;
            SyncStatus = SyncElementStatus.Conflicted;
        }

        /// <summary>
        /// conflict info: null if no conflict
        /// </summary>
        public ElementConflictInfo ConflictInfo { get; protected set; }

        /// <summary>
        /// check if conflict appeared while synchronisation
        /// </summary>
        public bool IsConflicted => ConflictInfo != null;

        public string AbsolutePath1 => SyncInfo.Link.Path1 + ElementInfo.FullPath;
        public string AbsolutePath2 => SyncInfo.Link.Path2 + ElementInfo.FullPath;
    }
}
