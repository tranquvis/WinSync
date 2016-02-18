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
        List<MyFileInfo> _files = new List<MyFileInfo>();
        List<DirTree> _dirs = new List<DirTree>();

        public DirTree(MyDirInfo info)
        {
            _info = info;
        }

        public MyDirInfo Info
        {
            get { return _info; }
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
            if (file.Path == "")
            {
                file.TreePath = new List<DirTree>();
                this._files.Add(file);
                return file.TreePath;
            }

            string[] dirs = file.Path.Split('\\');

            DirTree dir = this;
            List<DirTree> path = new List<DirTree>();
            for (int i = 1; i < dirs.Length; i++)
            {
                dir = dir._dirs.FirstOrDefault(x => x.Info.Name == dirs[i]);
                if (dir == null)
                    return null;
                path.Add(dir);
            }

            file.TreePath = path;
            dir._files.Add(file);
            return path;
        }

        public List<DirTree> AddDir(MyDirInfo newDir)
        {
            if (newDir.Path == "")
            {
                newDir.TreePath = new List<DirTree>();
                this._dirs.Add(new DirTree(newDir));
                return newDir.TreePath;
            }

            string[] dirs = newDir.Path.Split('\\');

            DirTree dir = this;
            List<DirTree> path = new List<DirTree>();
            for (int i = 1; i < dirs.Length; i++)
            {
                dir = dir._dirs.FirstOrDefault(x => x.Info.Name == dirs[i]);
                if (dir == null)
                    return null;
                path.Add(dir);
            }

            newDir.TreePath = path;
            dir._dirs.Add(new DirTree(newDir));
            return path;
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
