using WinSync.Data;
using System;
using System.Collections.Generic;

namespace WinSync.Service
{
    public class SyncInfo
    {
        private TimeSpan _timePaused = TimeSpan.Zero;
        private double? _lastSizeApplied; // in Megabit
        private DateTime? _lastTime;

        public Link Link { get; set; }

        public bool Paused;
        public bool Running;
        public bool Finished;

        public List<MyDirInfo> SyncDirs;
        public List<MyFileInfo> SyncFiles;
        public List<MyDirInfo> ConflictDirs;
        public List<MyFileInfo> ConflictFiles;
        public Stack<LogMessage> LogStack;

        public DirTree DirTree;

        public ISyncListener Listener;

        /// <summary>
        /// create SyncInfo
        /// </summary>
        /// <param name="link">link data (will be copied not referenced)</param>
        public SyncInfo(Link link)
        {
            Link = link.Clone();

            Running = false;
            Paused = false;
            TotalSize = 0;
            SizeApplied = 0;
            DirsDone = 0;
            FilesDone = 0;
            SyncDirs = new List<MyDirInfo>();
            SyncFiles = new List<MyFileInfo>();
            ConflictFiles = new List<MyFileInfo>();
            ConflictDirs = new List<MyDirInfo>();
            LogStack = new Stack<LogMessage>();

            DirTree = new DirTree(new MyDirInfo("", "", null));
        }
        
        /// <summary>
        /// synchronisation start time
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// synchronisation end time
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// the time, when the last pause started
        /// </summary>
        public DateTime LastPauseStart { get; private set; }

        /// <summary>
        /// the sum of time, that the synchronisation paused
        /// </summary>
        public TimeSpan TimePaused => Paused ? _timePaused + (DateTime.Now - LastPauseStart) : _timePaused;

        /// <summary>
        /// the total time, that the synchronisation was running minus the pused time
        /// </summary>
        public TimeSpan TotalTime => Running ? DateTime.Now - StartTime - TimePaused : EndTime - StartTime - TimePaused;
        
        /// <summary>
        /// if conflicts appeared while synchronizing
        /// </summary>
        public bool Conflicted => ConflictFiles.Count > 0 || ConflictDirs.Count > 0;

        /// <summary>
        /// sum of all file sizes except files to remove
        /// in byte
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        /// sum of the copied files sizes
        /// in byte
        /// </summary>
        public long SizeApplied { get; set; }

        /// <summary>
        /// the calculated average speed
        /// in byte/ms
        /// </summary>
        public double TotalSpeed => SizeApplied / (DateTime.Now - StartTime).TotalMilliseconds;

        /// <summary>
        /// the sum of remaining file sizes to copy
        /// in byte
        /// </summary>
        public long SizeRemaining => TotalSize - SizeApplied;

        /// <summary>
        /// synchronisation progress percentage
        /// </summary>
        public float Progress
        {
            get
            {
                if (State == SyncState.DetectingChanges)
                    return 0;
                if (State == SyncState.Finished)
                    return 100;
                if(TotalSize == 0 || SizeApplied == 0)
                    return 0;
                return 100f / TotalSize * SizeApplied;
            }
        }
        
        /// <summary>
        /// synchronisation status
        /// </summary>
        public SyncState State { get; set; } = SyncState.DetectingChanges;

        /// <summary>
        /// count of remaining files to synchronise
        /// </summary>
        public long FilesRemaining => DirsFound - FilesFound;

        /// <summary>
        /// estimated time until the synchronisation finishs
        /// calculated all synchronised files
        /// </summary>
        public TimeSpan TimeRemainingEst
        {
            get
            {
                double s = TotalSpeed;
                return TimeSpan.FromMilliseconds(s > 0 ? SizeRemaining / s : 0);
            }
        }

        /// <summary>
        /// count of detected files the schould be synchronised
        /// </summary>
        public long FilesFound => SyncFiles.Count;

        /// <summary>
        /// count of synchronised files
        /// </summary>
        public long FilesDone { get; set; }

        /// <summary>
        /// count of detected directories to synchronise
        /// </summary>
        public long DirsFound => SyncDirs.Count;

        /// <summary>
        /// count of synchronised directories (independent of files in this directories)
        /// </summary>
        public long DirsDone { get; set; }

        /// <summary>
        /// actual synchronisation speed
        /// this isn't only calculated from the last file, but from so much latest synchronised files 
        /// that their sizes sum is larger than SpeedMinCalcFileSize
        /// </summary>
        public double ActSpeed { get; private set; }

        /// <summary>
        /// speed of last file synchronisation
        /// in Megabits / second
        /// </summary>
        public double LastFileSyncSpeed { get; private set; }

        /// <summary>
        /// in Megabits / second
        /// </summary>
        public double AverageSpeed => SizeApplied / 131072.0 / TotalTime.TotalSeconds;

        /// <summary>
        /// call when synchronisation started
        /// </summary>
        public void SyncStarted()
        {
            StartTime = DateTime.Now;
            Running = true;
        }

        /// <summary>
        /// call when synchronisation finished
        /// </summary>
        public void SyncFinished()
        {
            Running = false;
            Finished = true;
            State = SyncState.Finished;
            EndTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been cancelled
        /// </summary>
        public void SyncCancelled()
        {
            Running = false;
            Finished = true;
            State = SyncState.Aborted;
            EndTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been paused
        /// </summary>
        public void SyncPaused()
        {
            Paused = true;
            LastPauseStart = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been continued
        /// </summary>
        public void SyncContinued()
        {
            Paused = false;
            _timePaused += DateTime.Now - LastPauseStart;
        }

        public void FileFound(MyFileInfo file)
        {
            DirTree.AddFile(file);
        }

        public void DirFound(MyDirInfo dir)
        {
            DirTree.AddDir(dir);
        }

        /// <summary>
        /// call when a difference in the files has been detected
        /// </summary>
        /// <param name="sfi">file information</param>
        public void FileChangeDetected(MyFileInfo fi)
        {
            SyncFiles.Add(fi);
            if (!fi.SyncInfo.Remove) TotalSize += fi.Size;
            Listener?.OnFileChangeDetected(fi);
        }

        /// <summary>
        /// call when a difference in the directory tree has been detected
        /// </summary>
        /// <param name="sdi">directory information</param>
        public void DirChangeDetected(MyDirInfo di)
        {
            SyncDirs.Add(di);
        }

        /// <summary>
        /// call when detecting of file started
        /// </summary>
        /// <param name="path">relative file path</param>
        public void DetectingFile(MyFileInfo fi)
        {
            Listener?.OnDetectingFileStarted(fi);
        }

        /// <summary>
        /// call when succesfully copied or deleted change to file
        /// </summary>
        /// <param name="sfi">synchronised file information</param>
        public void AppliedFileChange(MyFileInfo fi)
        {
            if(!fi.SyncInfo.Remove)
                SizeApplied += fi.Size;
            FilesDone++;

            if (fi.SyncInfo.Conflicted) return;

            LastFileSyncSpeed = fi.SyncInfo.Speed;

            RecalculateActSpeed();

            Listener?.OnFileSynced(fi);
        }

        /// <summary>
        /// call when succesfully created or deleted directory
        /// </summary>
        /// <param name="sdi">directory information</param>
        public void AppliedDirChange(MyDirInfo di)
        {
            DirsDone++;
        }

        /// <summary>
        /// call when file conflicted
        /// </summary>
        /// <param name="sfi">file information with conflict infos</param>
        public void FileConflicted(MyFileInfo fi)
        {
            ConflictFiles.Add(fi);
            Listener?.OnFileConflicted(fi);
        }

        /// <summary>
        /// call when directory conflicted
        /// </summary>
        /// <param name="sdi">directory information with conflict infos</param>
        public void DirConflicted(MyDirInfo di)
        {
            ConflictDirs.Add(di);
            Listener?.OnDirConflicted(di);
        }

        public void Log(LogMessage message)
        {
            LogStack.Push(message);
            Listener?.OnLog(message);
        }

        /// <summary>
        /// calculate the actual speed ActSpeed with delta time and size applied difference since the last recalculation
        /// </summary>
        public void RecalculateActSpeed()
        {
            if (_lastTime != null && _lastSizeApplied != null)
            {
                double timeDif = (DateTime.Now - _lastTime.Value).TotalSeconds;
                double sizeAppliedDif = SizeApplied / 131072.0 - _lastSizeApplied.Value;
                ActSpeed = sizeAppliedDif / timeDif;
            }
            else
            {
                _lastTime = DateTime.Now;
                _lastSizeApplied = 0;
            }
        }
    }
}
