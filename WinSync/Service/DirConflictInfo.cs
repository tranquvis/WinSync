namespace WinSync.Service
{
    public class DirConflictInfo : ConflictInfo
    {
        public SyncDirInfo SyncDirInfo { get; set; }

        /// <summary>
        /// create ConflictInfo for directory
        /// </summary>
        /// <param name="type"></param>
        /// <param name="conflictPath">
        /// 1: if the conflict occured in directory 1 of link 
        /// 2: if in directory 2
        /// </param>
        /// <param name="context">context where the conflict occurred</param>
        /// <param name="syncDirInfo">owner</param>
        public DirConflictInfo(ConflictType type, int conflictPath, string context, string message, SyncDirInfo syncDirInfo) : 
            base(type, conflictPath, context, message)
        {
            SyncDirInfo = syncDirInfo;
        }

        public override string GetAbsolutePath()
        {
            return (ConflictPath == 1 ? SyncDirInfo.SyncInfo.Link.Path1 : SyncDirInfo.SyncInfo.Link.Path2) + SyncDirInfo.DirInfo.FullPath;
        }
    }
}