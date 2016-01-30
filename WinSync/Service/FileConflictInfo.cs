namespace WinSync.Service
{
    public class FileConflictInfo : ConflictInfo
    {
        public SyncFileInfo SyncFileInfo { get; set; }

        /// <summary>
        /// create ConflictInfo for file
        /// </summary>
        /// <param name="type"></param>
        /// <param name="conflictPath">
        /// 1: if the conflict occured in directory 1 of link 
        /// 2: if in directory 2
        /// </param>
        /// <param name="context">context where the conflict occurred</param>
        /// <param name="syncFileInfo">owner</param>
        public FileConflictInfo(ConflictType type, int conflictPath, string context, string message, SyncFileInfo syncFileInfo) : 
            base(type, conflictPath, context, message)
        {
            SyncFileInfo = syncFileInfo;
        }

        public override string GetAbsolutePath()
        {
            return (ConflictPath == 1 ? SyncFileInfo.SyncInfo.Link.Path1 : SyncFileInfo.SyncInfo.Link.Path2) + SyncFileInfo.FileInfo.FullPath;
        }
    }
}