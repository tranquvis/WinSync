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

        public void AddFile(MyFileInfo file)
        {
            if (file.Path == "")
            {
                this._files.Add(file);
                return;
            }

            string[] dirs = file.Path.Split('\\');

            DirTree dir = this;
            for (int i = 1; i < dirs.Length; i++)
            {
                dir = dir._dirs.FirstOrDefault(x => x.Info.Name == dirs[i]);
                if (dir == null)
                    return;
            }
            dir._files.Add(file);
        }

        public void AddDir(MyDirInfo newDir)
        {
            if (newDir.Path == "")
            {
                this._dirs.Add(new DirTree(newDir));
                return;
            }

            string[] dirs = newDir.Path.Split('\\');

            DirTree dir = this;
            for (int i = 1; i < dirs.Length; i++)
            {
                dir = dir._dirs.FirstOrDefault(x => x.Info.Name == dirs[i]);
                if (dir == null)
                    return;
            }
            dir._dirs.Add(new DirTree(newDir));
        }

        public List<string> DirNames => _dirs.ConvertAll<string>(dir => dir.Info.Name);

        //public bool HasDuplicateDirs => _dirs.Count(dir => _dirs.Count(x => x.Info.Name == dir.Info.Name) > 1) > 0;
    }
}
