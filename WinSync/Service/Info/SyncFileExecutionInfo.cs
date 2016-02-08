using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncFileExecutionInfo : SyncElementExecutionInfo
    {
        public SyncFileInfo SyncFileInfo => (SyncFileInfo)SyncElementInfo;

        public SyncFileExecutionInfo(SyncFileInfo syncFileInfo, SyncDirection dir, bool remove) : base(syncFileInfo, dir, remove){}

        /// <summary>
        /// in Megabits/second
        /// </summary>
        public double Speed => (((MyFileInfo)SyncElementInfo.ElementInfo).Size * 8.0 / (1024.0 * 1024.0)) / (SyncDuration.TotalSeconds);
    }
}
