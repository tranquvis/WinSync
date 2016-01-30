using System;

namespace WinSync.Service
{
    public class SyncDirInfo
    {
        public SyncInfo SyncInfo { get; set; }

        public MyDirInfo DirInfo { get; set; }

        /// <summary>
        /// create SyncDirInfo
        /// </summary>
        /// <param name="syncInfo">owner</param>
        /// <param name="path">relative file path</param>
        public SyncDirInfo(SyncInfo syncInfo, MyDirInfo dirInfo)
        {
            SyncInfo = syncInfo;
            DirInfo = dirInfo;
        }

        /// <summary>
        /// create SyncDirInfo
        /// </summary>
        /// <param name="syncInfo">owner</param>
        /// <param name="path">relative file path</param>
        /// <param name="dir">synchronisation direction</param>
        /// <param name="remove">if destination directory should be removed</param>
        public SyncDirInfo(SyncInfo syncInfo, MyDirInfo dirInfo, SyncDirection dir, bool remove) : this(syncInfo, dirInfo)
        {
            Direction = dir;
            Remove = remove;
        }

        /// <summary>
        /// synchronisation direction
        /// </summary>
        public SyncDirection Direction { get; set; }

        /// <summary>
        /// if desitnation directory should be removed
        /// </summary>
        public bool Remove { get; set; }

        /// <summary>
        /// set synchronisation start time of directory to now
        /// </summary>
        public void StartedNow()
        {
            SyncStart = DateTime.Now;
        }

        /// <summary>
        /// set synchronisation end time of directory to now
        /// </summary>
        public void EndedNow()
        {
            SyncEnd = DateTime.Now;
        }

        public DateTime? SyncStart { get; private set; }

        public DateTime? SyncEnd { get; private set; }

        /// <summary>
        /// in milliseconds
        /// </summary>
        public double SyncDuration => SyncStart != null && SyncEnd != null ? (SyncEnd - SyncStart).Value.TotalMilliseconds : 0;

        /// <summary>
        /// if synchronisation has finished
        /// </summary>
        public bool Synced => SyncEnd != null;

        /// <summary>
        /// set conflict to dir info
        /// </summary>
        /// <param name="ci">conflict info</param>
        public void DirConflicted(DirConflictInfo ci)
        {
            ConflictInfo = ci;
        }

        /// <summary>
        /// conflict info: null if no conflict
        /// </summary>
        public DirConflictInfo ConflictInfo { get; private set; }

        /// <summary>
        /// check if conflict appeared for dir while synchronisation
        /// </summary>
        public bool Conflicted => ConflictInfo != null;
    }
}
