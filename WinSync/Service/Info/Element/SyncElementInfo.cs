using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public abstract class SyncElementInfo
    {
        private SyncElementStatus _syncStatus;

        /// <summary>
        /// create SyncElementInfo
        /// </summary>
        /// <param name="syncInfo">synchronisation info</param>
        /// <param name="elementInfo">element info</param>
        /// <param name="initStatus">if true: SyncStatus is set to ElementFound</param>
        public SyncElementInfo(SyncInfo syncInfo, MyElementInfo elementInfo, bool initStatus)
        {
            SyncInfo = syncInfo;
            ElementInfo = elementInfo;
            ElementInfo.SyncElementInfo = this;

            if (initStatus)
                SyncStatus = SyncElementStatus.ElementFound;
        }

        public SyncInfo SyncInfo { get; protected set; }

        public MyElementInfo ElementInfo { get; protected set; }

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
        /// conflict info: null if no conflict
        /// </summary>
        public ElementConflictInfo ConflictInfo { get; protected set; }

        /// <summary>
        /// check if conflict appeared while synchronisation
        /// </summary>
        public bool IsConflicted => ConflictInfo != null;

        /// <summary>
        /// absolute element path 1 with name
        /// </summary>
        public string AbsolutePath1 => SyncInfo.Link.Path1 + ElementInfo.FullPath;

        /// <summary>
        /// absolute element path 1 with name
        /// </summary>
        public string AbsolutePath2 => SyncInfo.Link.Path2 + ElementInfo.FullPath;

        /// <summary>
        /// call when conflicted
        /// </summary>
        /// <param name="ci">conflict info</param>
        public void Conflicted(ElementConflictInfo ci)
        {
            ConflictInfo = ci;
            SyncStatus = SyncElementStatus.Conflicted;
        }
    }
}
