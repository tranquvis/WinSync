using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncDirExecutionInfo : SyncElementExecutionInfo
    {
        public SyncDirInfo SyncDirInfo => (SyncDirInfo)SyncElementInfo;

        public SyncDirExecutionInfo(SyncDirInfo syncDirInfo, SyncDirection dir, bool remove) : base(syncDirInfo, dir, remove){}
        
    }
}
