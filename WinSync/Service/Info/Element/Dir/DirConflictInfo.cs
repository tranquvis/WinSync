using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class DirConflictInfo : ElementConflictInfo
    {
        public DirConflictInfo(SyncDirInfo syncDirInfo, ConflictType type, int conflictPath, string context, string message, Exception exception) 
            : base(syncDirInfo, type, conflictPath, context, message, exception) {}
    }
}
