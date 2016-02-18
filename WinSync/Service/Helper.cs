using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class Helper
    {
        /// <summary>
        /// Compare Files Byte for Byte
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns></returns>
        public static bool FilesAreEqual(Delimon.Win32.IO.FileInfo first, Delimon.Win32.IO.FileInfo second, Func<bool> interruptChecker)
        {
            if (first.Length != second.Length)
                return false;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;

                    interruptChecker();
                }
            }

            return true;
        }

        /// <summary>
        /// compare 2 files for one way synchronisation in new task
        /// </summary>
        /// <param name="sourcePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destPath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="fileName">file name</param>
        /// <param name="relativePath">file path relative to the homepath without filename</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the operation was canceled</returns>
        public static bool DoFileComparison_OneWay(string sourcePath, string destPath, SyncFileInfo file, Func<bool> interruptChecker)
        {
            if(interruptChecker()) return true;

            Delimon.Win32.IO.FileInfo srcFileInfo;
            Delimon.Win32.IO.FileInfo destFileInfo;

            file.SyncState = SyncElementState.ChangeDetectingStarted;

            string sf = sourcePath + file.FileInfo.FullPath;
            string df = destPath + file.FileInfo.FullPath;

            srcFileInfo = new Delimon.Win32.IO.FileInfo(sf);
            destFileInfo = new Delimon.Win32.IO.FileInfo(df);

            try
            {
                if (Helper.CompareFiles_OneWay(srcFileInfo, destFileInfo, interruptChecker))
                {
                    file.FileInfo.Size = srcFileInfo.Length;
                    new SyncFileExecutionInfo(file, file.SyncInfo.Link.Direction, false);
                }
            }
            catch (Exception e)
            {
                if (file.SyncInfo != null)
                    file.Conflicted(new FileConflictInfo(file, ConflictType.Unknown, 0, "RunOneWayFileCompareTask", e.Message, e));
                else
                    file.SyncInfo.Log(new LogMessage(LogType.ERROR, e.Message, e));
            }

            return false;
        }

        /// <summary>
        /// compare file in paths for two way synchronisation in new task
        /// </summary>
        /// <param name="fileName">filename</param>
        /// <param name="relativePath">file path relative to the homedir without filename</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the operation was canceled</returns>
        public static bool DoFileComparison_TwoWay(SyncFileInfo file, Func<bool> interruptChecker)
        {
            if (interruptChecker()) return true;

            file.SyncState = SyncElementState.ChangeDetectingStarted;
            
            string pd1 = file.SyncInfo.Link.Path1 + file.FileInfo.Path;
            string pd2 = file.SyncInfo.Link.Path2 + file.FileInfo.Path;

            //get parent directory infos
            Delimon.Win32.IO.DirectoryInfo pdi1;
            while (!(pdi1 = new Delimon.Win32.IO.DirectoryInfo(pd1)).Exists)
                pd1 = pd1.Substring(0, pd1.LastIndexOf(@"\", StringComparison.Ordinal));

            Delimon.Win32.IO.DirectoryInfo pdi2;
            while (!(pdi2 = new Delimon.Win32.IO.DirectoryInfo(pd2)).Exists)
                pd2 = pd2.Substring(0, pd2.LastIndexOf(@"\", StringComparison.Ordinal));

            string f1 = file.AbsolutePath1;
            string f2 = file.AbsolutePath2;

            //file info
            Delimon.Win32.IO.FileInfo fi1 = new Delimon.Win32.IO.FileInfo(f1);
            Delimon.Win32.IO.FileInfo fi2 = new Delimon.Win32.IO.FileInfo(f2);

            if (interruptChecker()) return true;

            try
            {
                //compare
                TwoWayCompareResult compResult = Helper.CompareFiles_TwoWay(fi1, fi2, file.SyncInfo.Link.Remove, pdi1, pdi2);

                if (compResult == null)
                    file.SyncState = SyncElementState.NoChangeFound;
                else
                {
                    file.FileInfo.Size = fi1.Exists ? fi1.Length : fi2.Length;
                    new SyncFileExecutionInfo(file, compResult.Direction, compResult.Remove);
                }
            }
            catch (Exception e)
            {
                file.Conflicted(new FileConflictInfo(file, ConflictType.Unknown, 0, "RunTwoWayFileCompareTask", e.Message, e));
            }

            return false;
        }

        /// <summary>
        /// Check if 2 Files are updated for one way synchronisation
        /// </summary>
        /// <param name="sfi">source file</param>
        /// <param name="dfi">destination file</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the files are not updated</returns>
        public static bool CompareFiles_OneWay(Delimon.Win32.IO.FileInfo sfi, Delimon.Win32.IO.FileInfo dfi, Func<bool> interruptChecker)
        {
            bool d = !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime ||
                (sfi.LastWriteTime < dfi.LastWriteTime && !Helper.FilesAreEqual(sfi, dfi, interruptChecker));
            return !dfi.Exists || sfi.LastWriteTime > dfi.LastWriteTime ||
                (sfi.LastWriteTime < dfi.LastWriteTime && !Helper.FilesAreEqual(sfi, dfi, interruptChecker));
        }

        /// <summary>
        /// Check if 2 Files are updated for two way synchronisation
        /// The order of the files does not matter
        /// </summary>
        /// <param name="fi1">file 1</param>
        /// <param name="fi2">file 2</param>
        /// <param name="remove">if remove is enabled</param>
        /// <param name="parentDir1">parent directory of file 1</param>
        /// <param name="parentDir2">parent directory of file 2</param>
        /// <returns>compare result</returns>
        public static TwoWayCompareResult CompareFiles_TwoWay(Delimon.Win32.IO.FileInfo fi1, Delimon.Win32.IO.FileInfo fi2, bool remove,
            Delimon.Win32.IO.DirectoryInfo parentDir1, Delimon.Win32.IO.DirectoryInfo parentDir2)
        {
            if (!fi1.Exists)
            {
                // if the parent directory of file 1 is older than file 2 -> create file in parent directory 1
                if (fi2.LastWriteTime >= parentDir1.LastWriteTime)
                    return new TwoWayCompareResult(SyncDirection.To1, false);

                // otherwise remove file 2 if remove ist enabled
                if (remove)
                    return new TwoWayCompareResult(SyncDirection.To2, true);

                return null;
            }

            if (!fi2.Exists)
            {
                // if the parent directory of file 2 is older than file 1 -> create file in parent directory 2
                if (fi1.LastWriteTime >= parentDir2.LastWriteTime)
                    return new TwoWayCompareResult(SyncDirection.To2, false);

                // otherwise remove file 1 if remove ist enabled
                if (remove)
                    return new TwoWayCompareResult(SyncDirection.To1, true);

                return null;
            }

            // update file 1 if file 2 is newer
            if (fi1.LastWriteTime < fi2.LastWriteTime)
                return new TwoWayCompareResult(SyncDirection.To1, false);

            // update file 2 if file 1 is newer
            if (fi1.LastWriteTime > fi2.LastWriteTime)
                return new TwoWayCompareResult(SyncDirection.To2, false);

            return null;
        }

        /// <summary>
        /// delete folder
        /// </summary>
        /// <param name="sdei">sync execution information about directory</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the operation was canceled</returns>
        public static bool DeleteFolder(SyncDirExecutionInfo sdei, Func<bool> interruptChecker)
        {
            if (interruptChecker()) return true;

            string ddp = sdei.AbsoluteDestPath;
            Delimon.Win32.IO.DirectoryInfo ddi = new Delimon.Win32.IO.DirectoryInfo(ddp);

            if (!ddi.Exists)
                return false;

            //do not remove if directory is not empty
            if (ddi.GetFiles().Length > 0 || ddi.GetDirectories().Length > 0)
            {
                sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.DirNotEmpty,
                    sdei.Direction == SyncDirection.To1 ? 1 : 2, "RunFolderDeletionTask",
                    $"The directory to be deleted was not empty. Path: {sdei.SyncDirInfo.DirInfo.FullPath}", null));
                return false;
            }

            interruptChecker();

            sdei.StartedNow();
            try
            {
                ddi.Delete();
                sdei.SyncDirInfo.SyncState = SyncElementState.ChangeApplied;
            }
            catch (Exception e)
            {
                sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.Unknown,
                    sdei.Direction == SyncDirection.To2 ? 2 : 1, "RunFolderDeletionTask", e.Message, e));
            }
            sdei.EndedNow();

            return false;
        }

        /// <summary>
        /// create folder
        /// </summary>
        /// <param name="sdei">sync execution information about directory</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the operation was canceled</returns>
        public static bool CreateFolder(SyncDirExecutionInfo sdei, Func<bool> interruptChecker)
        {
            if(interruptChecker()) return true;

            string sdp = sdei.AbsoluteSourcePath;
            string ddp = sdei.AbsoluteDestPath;

            if (!Delimon.Win32.IO.Directory.Exists(sdp))
                return false;

            sdei.StartedNow();
            try
            {
                Delimon.Win32.IO.Directory.CreateDirectory(ddp);
                sdei.SyncElementInfo.SyncState = SyncElementState.ChangeApplied;
            }
            catch (Exception e)
            {
                sdei.SyncDirInfo.Conflicted(new DirConflictInfo(sdei.SyncDirInfo, ConflictType.Unknown,
                    sdei.Direction == SyncDirection.To2 ? 2 : 1, "RunFolderCreationTask", e.Message, e));
            }
            sdei.EndedNow();

            return false;
        }
        
        /// <summary>
        /// detect files and directories that should be removed for One-Way synchronisation recursively
        /// </summary>
        /// <param name="sourceHomePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destHomePath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="dir">the directory, in which you want to search</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        /// <returns>true if the operation was canceled</returns>
        public static void GetRemoveInfosOfDirRecursively_OneWay(string sourceHomePath, string destHomePath, MyDirInfo dir, 
            SyncInfo si, Func<bool> interruptChecker)
        {
            interruptChecker();

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //get directories to remove
                //detect destination child directories
                foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(destPath))
                {
                    string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);

                    //remove destination directory if source directory doesn't exist (if remove is enabled)
                    if (si.Link.Remove && !new Delimon.Win32.IO.DirectoryInfo(newDir.FullPath).Exists)
                    {
                        new SyncDirInfo(si, dir);
                        new SyncDirExecutionInfo(newDir.SyncDirInfo, si.Link.Direction, true);
                    }

                    GetRemoveInfosOfDirRecursively_OneWay(sourceHomePath, destHomePath, newDir, si, interruptChecker);
                }

                //get files to remove
                //Loop through all files in destination directory
                foreach (string path in Delimon.Win32.IO.Directory.GetFiles(destPath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(path);

                    MyFileInfo file = new MyFileInfo(dir.FullPath, name);

                    string sourceFilePath = sourceHomePath + file.FullPath;
                    string destFilePath = destHomePath + file.FullPath;

                    //remove destination file if source file doesn't exist (if remove is enabled)
                    if (!new Delimon.Win32.IO.FileInfo(sourceFilePath).Exists)
                    {
                        new SyncFileInfo(si, file);
                        if (si.Link.Remove)
                        {
                            file.Size = new Delimon.Win32.IO.FileInfo(destFilePath).Length;
                            new SyncFileExecutionInfo(file.SyncFileInfo, si.Link.Direction, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                si.Log(new LogMessage(LogType.ERROR, e.Message, e));
            }
        }

        /// <summary>
        /// fetch files and detect subdirectory changes for Two-Way synchronisation recursively
        /// </summary>
        /// <param name="dir">the directory, in which you want to search</param>
        /// <param name="si">sync info</param>
        /// <param name="onFileFound">is called when a file was found</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        public static void FetchFilesInDirRecursively_TwoWay(MyDirInfo dir, SyncInfo si, 
            Action<SyncFileInfo> onFileFound, Func<bool> interruptChecker)
        {
            interruptChecker();

            string path1 = si.Link.Path1 + dir.FullPath;
            string path2 = si.Link.Path2 + dir.FullPath;

            //directory info
            Delimon.Win32.IO.DirectoryInfo di1 = new Delimon.Win32.IO.DirectoryInfo(path1);
            Delimon.Win32.IO.DirectoryInfo di2 = new Delimon.Win32.IO.DirectoryInfo(path2);

            List<string> dirNames = new List<string>();
            List<string> fileNames = new List<string>();

            try
            {
                #region detect changes of directories
                if (di1.Exists)
                {
                    //loop through path1 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path1))
                    {
                        string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                        dirNames.Add(newDirname);

                        MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);
                        new SyncDirInfo(si, newDir);
                        FetchFilesInDirRecursively_TwoWay(newDir, si, onFileFound, interruptChecker);
                    }
                }
                else
                {
                    if (dir.SyncDirInfo != null && di1.Parent != null)
                    {
                        //compare the newest time of last write time or creation time
                        DateTime pd1ChangeTime = di1.Parent.CreationTime > di1.Parent.LastWriteTime ? di1.Parent.CreationTime : di1.Parent.LastWriteTime;
                        DateTime d2ChangeTime = di2.CreationTime > di2.LastWriteTime ? di2.CreationTime : di2.LastWriteTime;

                        if (pd1ChangeTime > d2ChangeTime)
                        {
                            if (si.Link.Remove)
                            {
                                //remove directory 2 if remove is enabled and the parent directory 1 is new than directory 2
                                //note that directories only will be removed if they are empty after applying file changes
                                new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To2, true);
                            }
                        }
                        else
                        {
                            //if directory 2 is newer than the parent directory 1 -> create directory 1
                            new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To1, false);
                        }
                    }
                }

                if (di2.Exists)
                {
                    //loop through path2 dir
                    foreach (string name in Delimon.Win32.IO.Directory.GetDirectories(path2))
                    {
                        string newDirname = Delimon.Win32.IO.Path.GetFileName(name);
                        if (!dirNames.Contains(newDirname))
                        {
                            MyDirInfo newDir = new MyDirInfo(dir.FullPath, newDirname);
                            new SyncDirInfo(si, newDir);
                            FetchFilesInDirRecursively_TwoWay(newDir, si, onFileFound, interruptChecker);
                        }
                    }
                }
                else
                {
                    if (dir.SyncDirInfo != null && di2.Parent != null)
                    {
                        //compare the newest time of last write time or creation time
                        DateTime pd2ChangeTime = di2.Parent.LastWriteTime > di2.Parent.CreationTime ? di2.Parent.LastWriteTime : di2.Parent.CreationTime;
                        DateTime d1ChangeTime = di1.LastWriteTime > di1.CreationTime ? di1.LastWriteTime : di1.CreationTime;
                        if (pd2ChangeTime > d1ChangeTime)
                        {
                            if (si.Link.Remove)
                            {
                                //remove directory 1 if remove is enabled and the parent directory 2 is new than directory 1
                                //note that directories only will be removed if they are empty after applying file changes
                                new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To1, true);
                            }
                        }
                        else
                        {
                            //if directory 1 is newer than the parent directory 2 -> create directory 2
                            new SyncDirExecutionInfo(dir.SyncDirInfo, SyncDirection.To2, false);
                        }
                    }
                }
                #endregion

                #region detect changes of files
                if (di1.Exists)
                {
                    //Loop through all files in path1
                    foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(path1))
                    {
                        //detect changes of file asynchronously
                        string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                        MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                        new SyncFileInfo(si, file);

                        fileNames.Add(name);

                        onFileFound(file.SyncFileInfo);
                    }
                }

                if (di2.Exists)
                {
                    //Loop through all files in path2
                    foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(path2))
                    {
                        //detect changes of file asynchronously
                        string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                        if (fileNames.Contains(name)) continue;

                        MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                        new SyncFileInfo(si, file);

                        onFileFound(file.SyncFileInfo);
                    }
                }
            }
            catch (Exception e)
            {
                si.Log(new LogMessage(LogType.ERROR, e.Message, e));
            }
            #endregion
        }

        /// <summary>
        /// fetch files and detect subdirectory changes for One-Way synchronisation recursively
        /// </summary>
        /// <param name="sourceHomePath">absolute source folder path (homepath as defined in link)</param>
        /// <param name="destHomePath">absolute destination folder path (homepath as defined in link)</param>
        /// <param name="dir">the directory, in which you want to search</param>
        /// <param name="si">sync info</param>
        /// <param name="onFileFound">is called when a file was found</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        public static void FetchFilesInDirRecursively_OneWay(string sourceHomePath, string destHomePath, MyDirInfo dir, 
            SyncInfo si, Action<SyncFileInfo> onFileFound, Func<bool> interruptChecker)
        {
            interruptChecker();

            string sourcePath = sourceHomePath + dir.FullPath;
            string destPath = destHomePath + dir.FullPath;

            try
            {
                //create destination directory if not exists
                if (dir.SyncDirInfo != null && !new Delimon.Win32.IO.DirectoryInfo(destPath).Exists)
                {
                    new SyncDirExecutionInfo(dir.SyncDirInfo, si.Link.Direction, false);
                }

                //detect source child directories
                foreach (string dirpath in Delimon.Win32.IO.Directory.GetDirectories(sourcePath))
                {
                    string name = Delimon.Win32.IO.Path.GetFileName(dirpath);
                    MyDirInfo newDir = new MyDirInfo(dir.FullPath, name);
                    new SyncDirInfo(si, newDir);
                    FetchFilesInDirRecursively_OneWay(sourceHomePath, destHomePath, newDir, 
                        si, onFileFound, interruptChecker);
                }

                //loop through all files in source directory
                foreach (string filepath in Delimon.Win32.IO.Directory.GetFiles(sourcePath))
                {
                    //detect changes of file asynchronously
                    string name = Delimon.Win32.IO.Path.GetFileName(filepath);
                    MyFileInfo file = new MyFileInfo(dir.FullPath, name);
                    new SyncFileInfo(si, file);
                    onFileFound(file.SyncFileInfo);
                }
            }
            catch (OperationCanceledException)
            {

            }
            catch (Exception e)
            {
                si.Log(new LogMessage(LogType.ERROR, e.Message, e));
            }
        }


        /// <summary>
        /// apply change to a file in new task (deleting or copying)
        /// </summary>
        /// <param name="sfei">sync execution information about file</param>
        /// <param name="interruptChecker">is called when the cancellation or pause request should be checked in order to handel them</param>
        public static bool ApplyFileChange(SyncFileExecutionInfo sfei, Func<bool> interruptChecker)
        {
            if(interruptChecker()) return true;

            string sfp = sfei.AbsoluteSourcePath;
            string dfp = sfei.AbsoluteDestPath;

            Delimon.Win32.IO.FileInfo sfi = new Delimon.Win32.IO.FileInfo(sfp);

            sfei.StartedNow();

            try
            {

                if (sfei.Remove)
                {
                    File.SetAttributes(dfp, FileAttributes.Normal); //change attributes to avoid UnauthorizedAccessException
                    File.Delete(dfp);
                }
                else if (sfi.Exists)
                {
                    //Copy Methods:
                    //FileMove2(sfp, dfp, interruptChecker);
                    File.Copy(sfp, dfp, true);
                }
                sfei.SyncFileInfo.SyncState = SyncElementState.ChangeApplied;
            }
            catch (IOException ioe)
            {
                string[] parts = ioe.Message.Split('\"');
                if (parts.Length > 1)
                {

                    string path = parts[1];
                    int conflictPath = path.Contains(sfei.SyncFileInfo.SyncInfo.Link.Path1) ? 1 : 2;
                    sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.IO, conflictPath, "RunApplyFileChangeTask", ioe.Message, ioe));
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                string[] parts = uae.Message.Split('\"');
                if (parts.Length > 1)
                {

                    string path = parts[1];
                    int conflictPath = path.Contains(sfei.SyncFileInfo.SyncInfo.Link.Path1) ? 1 : 2;

                    sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.UA, conflictPath, "RunApplyFileChangeTask", uae.Message, uae));
                }
            }
            catch (Exception e)
            {
                sfei.SyncFileInfo.Conflicted(new FileConflictInfo(sfei.SyncFileInfo, ConflictType.Unknown, 0, "RunApplyFileChangeTask", e.Message, e));
            }

            sfei.EndedNow();

            return false;
        }

        /// <summary> 
        /// fast file move with big buffers
        /// http://www.codeproject.com/tips/777322/a-faster-file-copy
        /// </summary>
        /// <param name="source">source file path</param> 
        /// <param name="destination">destination file path</param>
        private void FileMove2(string source, string destination, Func<bool> interruptChecker)
        {
            int arraylength = (int)Math.Pow(2, 19);
            byte[] dataarray = new byte[arraylength];
            using (FileStream fsread = new FileStream(source, FileMode.Open, FileAccess.Read, FileShare.None, 
                arraylength))
            {
                using (BinaryReader bwread = new BinaryReader(fsread))
                {
                    using (FileStream fswrite = new FileStream(destination, FileMode.Create, FileAccess.Write, 
                        FileShare.None, arraylength))
                    {
                        using (BinaryWriter bwwrite = new BinaryWriter(fswrite))
                        {
                            while (true)
                            {
                                interruptChecker();

                                int read = bwread.Read(dataarray, 0, arraylength);
                                if (0 == read) break;
                                bwwrite.Write(dataarray, 0, read);
                            }
                        }
                    }
                }
            }
        }
    }
}
