using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class FileConflictInfo : ConflictInfo
    {
        public FileConflictInfo(SyncFileInfo syncFileInfo, ConflictType type, int conflictPath, string context, string message) 
            : base(syncFileInfo, type, conflictPath, context, message) {}
    }
}
