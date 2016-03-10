using System;

namespace WinSync.Service
{
    public class SyncDirInfo : SyncElementInfo
    {
        public MyDirInfo DirInfo => (MyDirInfo)ElementInfo;
        public SyncDirExecutionInfo SyncDirExecutionInfo
        {
            get { return (SyncDirExecutionInfo)SyncExecutionInfo; }
            set { SyncExecutionInfo = value; }
        }

        public SyncDirInfo(SyncInfo syncInfo, MyDirInfo dirInfo, bool initStatus) : base(syncInfo, dirInfo, initStatus)
        { }
    }
}
