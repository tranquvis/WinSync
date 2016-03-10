using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class DirTree
    {
        MyDirInfo _info;
        DirTree _parent;
        DirTree _root;
        List<MyFileInfo> _files = new List<MyFileInfo>();
        List<DirTree> _dirs = new List<DirTree>();

        /// <summary>
        /// create DirTree
        /// </summary>
        /// <param name="info">directory info</param>
        /// <param name="parent">parent dir | null if root</param>
        /// <param name="root">this if null</param>
        public DirTree(MyDirInfo info, DirTree parent, DirTree root)
        {
            _info = info;
            _parent = parent;
            if (root == null)
                _root = this;
            else
                _root = root;
        }

        public MyDirInfo Info
        {
            get { return _info; }
        }

        public DirTree Parent
        {
            get { return _parent; }
        }

        public DirTree Root
        {
            get { return _root; }
        }

        public List<MyFileInfo> Files
        {
            get { return _files; }
        }

        public List<DirTree> Dirs
        {
            get { return _dirs; }
        }

        public List<DirTree> AddFile(MyFileInfo file)
        {
            DirTree parentDir;
            file.TreePath = GetAbsoluteTreePath(file.Path, out parentDir);
            parentDir._files.Add(file);
            return file.TreePath;
        }

        public List<DirTree> AddDir(MyDirInfo newDir)
        {
            DirTree dt = new DirTree(newDir, this, Root);
            DirTree parentDir;
            newDir.DirTreeInfo = dt;
            newDir.TreePath = GetAbsoluteTreePath(newDir.Path, out parentDir);
            parentDir._dirs.Add(dt);
            return newDir.TreePath;
        }

        /// <summary>
        /// create tree path from path text
        /// </summary>
        /// <param name="path"></param>
        /// <param name="lastDir">last dir in path</param>
        /// <returns></returns>
        private List<DirTree> GetAbsoluteTreePath(string path, out DirTree lastDir)
        {
            List<DirTree> treePath = new List<DirTree>() { Root };
            DirTree dir = Root;

            if (path != "")
            {
                string[] dirs = path.Split('\\');
                for (int i = 1; i < dirs.Length; i++)
                {
                    dir = dir._dirs.FirstOrDefault(x => x.Info.Name == dirs[i]);
                    if (dir == null)
                    {
                        lastDir = null;
                        return null;
                    }
                    treePath.Add(dir);
                }
            }

            lastDir = dir;
            return treePath;
        }

        public List<string> DirNames => _dirs.ConvertAll<string>(dir => dir.Info.Name);

        public static IEnumerable<MyFileInfo> GetFiles(DirTree dt)
        {
            foreach (DirTree subdir in dt.Dirs)
                foreach (var f in GetFiles(subdir))
                    yield return f;

            foreach (MyFileInfo f in dt.Files)
            {
                yield return f;
            }
        }

        //public bool HasDuplicateDirs => _dirs.Count(dir => _dirs.Count(x => x.Info.Name == dir.Info.Name) > 1) > 0;
    }
}
