using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public enum SyncElementStatus
    {
        ElementFound, ChangeDetectingStarted, NoChangeFound, ChangeFound, ChangeApplied, Conflicted
    }
}
