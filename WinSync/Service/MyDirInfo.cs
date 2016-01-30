using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class MyDirInfo
    {
        /// <summary>
        /// relative dir path without name
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// dir name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// relative dir path with name
        /// </summary>
        public string FullPath
        {
            get { return Name == "" ? "" : Path + @"\" + Name; }
        }

        public bool ChangeDetected => SyncInfo != null;

        public SyncDirInfo SyncInfo { get; set; }

        public MyDirInfo(string path, string name, SyncDirInfo syncInfo)
        {
            Path = path;
            Name = name;
            SyncInfo = syncInfo;
        }
    }
}
