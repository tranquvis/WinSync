using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class MyFileInfo
    {
        /// <summary>
        /// relative file path without name
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// relative file path with name
        /// </summary>
        public string FullPath
        {
            get { return Path + @"\" + Name; }
        }

        /// <summary>
        /// file size in byte
        /// </summary>
        public long Size { get; set; }

        public bool ChangeDetected => SyncInfo != null;

        public SyncFileInfo SyncInfo { get; set; }

        public MyFileInfo(string path, string name, SyncFileInfo syncInfo)
        {
            Path = path;
            Name = name;
            SyncInfo = syncInfo;
        }
    }
}
