using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class MyDirInfo : MyElementInfo
    {
        public SyncDirInfo SyncDirInfo
        {
            get { return (SyncDirInfo)SyncElementInfo; }
            set { SyncElementInfo = value; }
        }

        public MyDirInfo(string path, string name) : base(path, name) {}
    }
}
