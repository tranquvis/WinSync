using System;

namespace WinSync.Service
{
    public class SyncFileInfo : SyncElementInfo
    {
        public MyFileInfo FileInfo => (MyFileInfo)ElementInfo;
        public SyncFileExecutionInfo SyncFileExecutionInfo
        {
            get { return (SyncFileExecutionInfo)SyncExecutionInfo; }
            set { SyncExecutionInfo = value; }
        }

        public SyncFileInfo(SyncInfo syncInfo, MyFileInfo fileInfo) 
            : base(syncInfo, fileInfo)
        { }
    }
}
