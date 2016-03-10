using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public abstract class MyElementInfo
    {
        /// <summary>
        /// relative file path without name
        /// </summary>
        public string Path { get; set; }

        public List<DirTree> TreePath { get; set; }

        /// <summary>
        /// file name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// parent directory
        /// </summary>
        public MyDirInfo Parent
        {
            get { return TreePath[TreePath.Count - 1].Info; }
        }

        /// <summary>
        /// relative file path with name
        /// </summary>
        public string FullPath
        {
            get { return Name == "" ? "" : Path + @"\" + Name; }
        }

        public bool ChangeDetected => SyncElementInfo != null;

        public SyncElementInfo SyncElementInfo { get; set; }

        public MyElementInfo(string path, string name)
        {
            Path = path;
            Name = name;
        }
    }
}
