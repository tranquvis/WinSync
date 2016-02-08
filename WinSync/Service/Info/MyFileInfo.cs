using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class MyFileInfo : MyElementInfo
    {
        public SyncFileInfo SyncFileInfo
        {
            get { return (SyncFileInfo)SyncElementInfo; }
            set { SyncElementInfo = value; }
        }

        /// <summary>
        /// file size in byte
        /// </summary>
        public long Size { get; set; }

        public MyFileInfo(string path, string name) : base(path, name) { }
    }
}
