using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WinSync.Service;

namespace WinSync.Data
{
    public class Link
    {
        private string _title;
        private string _path1;
        private string _path2;
        
        /// <summary>
        /// title must be unique
        /// </summary>
        public string Title
        {
            get { return _title; }
            set 
            {
                if (value.Trim().Length == 0)
                    throw new Exception("The Title must not be empty!");
                _title = value; 
            }
        }
        
        public string Path1
        {
            get { return _path1; }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Path 1 must not be empty!");
                string p = value.Replace('/', '\\');
                p.TrimEnd('\\');
                _path1 = p;
            }
        }
        
        public string Path2
        {
            get { return _path2; }
            set
            {
                if (value.Trim().Length == 0)
                    throw new Exception("Path 2 must not be empty!");
                string p = value.Replace('/', '\\');
                p.TrimEnd('\\');
                _path2 = p;
            }
        }

        public SyncDirection Direction { get; set; }

        /// <summary>
        /// enables removing files and folders in destination directory if the source file doesn't exist
        /// </summary>
        public bool Remove { get; set; }

        /// <summary>
        /// create a link providing synchronisation information
        /// </summary>
        /// <param name="title"></param>
        /// <param name="path1">absolute path to folder 1</param>
        /// <param name="path2">absolute path to folder 2</param>
        /// <param name="direction"></param>
        /// <param name="remove">enables removing files and folders in destination directory if the source file doesn't exist</param>
        public Link(string title, string path1, string path2, SyncDirection direction, bool remove)
        {
            Title = title;
            Path1 = path1;
            Path2 = path2;
            Direction = direction;
            Remove = remove;
        }
        
        /// <summary>
        /// get line from link to store
        /// </summary>
        /// <returns>line with following format: "{Title}":"{Path1}","{Path2}","{DirectionName}","{Remove}"</returns>
        public override string ToString()
        {
            return $"\"{Title}\":\"{Path1}\",\"{Path2}\",\"{Direction}\",\"{Remove}\"";
        }

        /// <summary>
        /// create link from line
        /// </summary>
        /// <param name="line">
        /// must have following format: "{Title}":"{Path1}","{Path2}","{DirectionName}","{Remove}"
        /// whitespace is allowed
        /// </param>
        /// <returns>the created link or null if the format isn't valid</returns>
        public static Link CreateFromLine(string line)
        {
            const int argCount = 5;

            string[] parts = line.Split('\"');

            if (parts.Length != 1 + 2 * argCount)
                return null;
            if (!parts[2].Trim().Equals(":") || !parts[4].Trim().Equals(",") || !parts[6].Trim().Equals(","))
                return null;

            string title = parts[1];
            string path1 = parts[3];
            string path2 = parts[5];

            SyncDirection direction;
            bool remove;

            try
            {
                direction = SyncDirection.Parse(parts[7]);
                remove = bool.Parse(parts[9]);
            }
            catch (Exception)
            {
                return null;
            }

            return new Link(title, path1, path2, direction, remove);
        }

        /// <summary>
        /// Creates a new object, that is a copy of this instance.
        /// </summary>
        /// <returns></returns>
        public Link Clone()
        {
            return new Link(Title, Path1, Path2, Direction, Remove);
        }

        /// <summary>
        /// Copy link data to Link l
        /// </summary>
        /// <param name="l"></param>
        public void CopyDataTo(Link l)
        {
            l.Title = Title;
            l.Path1 = Path1;
            l.Path2 = Path2;
            l.Remove = Remove;
            l.Direction = Direction;
        }
    }
}
