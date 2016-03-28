using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSync.Service;

namespace WinSync.Data
{
    public class Link
    {
        private string _title;
        private string _path1;
        private string _path2;
        private SyncDirection _direction;
        private bool _remove;
        private string _drive1Label;
        private string _drive2Label;

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

        public SyncDirection Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// enables removing files and folders in destination directory if the source file doesn't exist
        /// </summary>
        public bool Remove
        {
            get { return _remove; }
            set { _remove = value; }
        }

        /// <summary>
        /// if not null the drive of Path1 is recognized by its label instead of its letter <para />
        /// the drive of path 1 will be changed to match the drive label
        /// </summary>
        public string Drive1Label
        {
            get { return _drive1Label; }
            set
            {
                _drive1Label = value;
            }
        }

        /// <summary>
        /// if not null the drive of Path2 is recognized by its label instead of its letter
        /// the drive of path 1 will be changed to match the drive label
        /// </summary>
        public string Drive2Label
        {
            get { return _drive2Label; }
            set
            {
                _drive2Label = value;
            }
        }

        public void UpdatePath1DriveLetter()
        {
            UpdatePathDriveLetter(ref _path1, Drive1Label);
        }

        public void UpdatePath2DriveLetter()
        {
            UpdatePathDriveLetter(ref _path2, Drive2Label);
        }

        private void UpdatePathDriveLetter(ref string path, string driveLabel)
        {
            char? driveName = Helper.GetDriveLetterFromLabel(driveLabel);

            if (driveName != null)
                path = $"{driveName}{path.Substring(1)}";
        }

        /// <summary>
        /// create a link providing synchronisation information
        /// </summary>
        /// <param name="title"></param>
        /// <param name="path1">absolute path to folder 1</param>
        /// <param name="path2">absolute path to folder 2</param>
        /// <param name="direction"></param>
        /// <param name="remove">enables removing files and folders in destination directory if the source file doesn't exist</param>
        /// <param name="drive1Label">
        /// if not empty or null the drive of Path1 is recognized by its label instead of its letter <para />
        /// the drive of path 1 will be changed to match the drive label
        /// </param>
        /// <param name="drive2Label">
        /// if not empty or null the drive of Path2 is recognized by its label instead of its letter
        /// the drive of path 1 will be changed to match the drive label
        /// </param>
        public Link(string title, string path1, string path2, SyncDirection direction, bool remove, string drive1Label, string drive2Label)
        {
            Title = title;
            Path1 = path1;
            Path2 = path2;
            Direction = direction;
            Remove = remove;
            Drive1Label = drive1Label == "" ? null : drive1Label;
            Drive2Label = drive2Label == "" ? null : drive2Label;
        }
        
        /// <summary>
        /// get line from link to store
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"\"{Title}\":\"{Path1}\",\"{Path2}\",\"{Direction}\",\"{Remove}\",\"{Drive1Label}\",\"{Drive2Label}\"";
        }

        /// <summary>
        /// create link from line
        /// </summary>
        /// <param name="line"></param>
        /// <returns>the created link or null if the format isn't valid</returns>
        public static Link CreateFromLine(string line)
        {
            const int argCount = 7;

            string[] parts = line.Split('\"');

            if (parts.Length != 1 + 2 * argCount)
                return null;

            if (!parts[2].Trim().Equals(":") || parts.Where((x, i) => i > 2 && i < 2 * argCount && i % 2 == 0 && x.Trim() != ",").Count() > 0) 
                return null;

            /*!parts[4].Trim().Equals(",") || !parts[6].Trim().Equals(",") 
            || !parts[8].Trim().Equals(",") || !parts[10].Trim().Equals(","))*/

            int argIndex = 1;

            string title = parts[argIndex];
            string path1 = parts[argIndex += 2];
            string path2 = parts[argIndex += 2];

            SyncDirection direction;
            bool remove;
            try
            {
                direction = SyncDirection.Parse(parts[argIndex += 2]);
                remove = bool.Parse(parts[argIndex += 2]);
            }
            catch (Exception)
            {
                return null;
            }

            string drive1Label = parts[argIndex += 2];
            string drive2Label = parts[argIndex += 2];

            Link l = new Link(title, path1, path2, direction, remove, drive1Label, drive2Label);
            if (l.Drive1Label != null)
                l.UpdatePath1DriveLetter();
            if (l.Drive2Label != null)
                l.UpdatePath2DriveLetter();
            return l;
        }

        /// <summary>
        /// Creates a new object, that is a copy of this instance.
        /// </summary>
        /// <returns></returns>
        public Link Clone()
        {
            return new Link(Title, Path1, Path2, Direction, Remove, Drive1Label, Drive2Label);
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
            l.Drive1Label = Drive1Label;
            l.Drive2Label = Drive2Label;
        }
    }
}
