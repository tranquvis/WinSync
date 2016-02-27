using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class FileConflictInfo : ElementConflictInfo
    {
        public FileConflictInfo(SyncFileInfo syncFileInfo, ConflictType type, int conflictPath, string context, string message, Exception exception) 
            : base(syncFileInfo, type, conflictPath, context, message, exception) {}
    }
}
