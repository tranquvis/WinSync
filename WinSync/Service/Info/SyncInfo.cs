using WinSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinSync.Service
{
    public class SyncInfo
    {
        private TimeSpan _timePaused = TimeSpan.Zero;
        private double? _lastSizeApplied; // in Megabit
        private DateTime? _lastTime;

        public Link Link { get; private set; }

        public bool Paused { get; set; }
        public bool Running { get; set; }
        public bool Finished { get; set; }

        /// <summary>
        /// synchronisation status
        /// </summary>
        public SyncState State { get; set; } = SyncState.DetectingChanges;

        public List<SyncDirInfo> SyncDirInfos { get; private set; }
        public List<SyncFileInfo> SyncFileInfos { get; private set; }

        public List<ConflictInfo> ConflictInfos { get; private set; }
        public Stack<LogMessage> LogStack { get; private set; }

        public DirTree DirTree { get; private set; }

        private ISyncListener _listener;

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
            SyncDirInfos = new List<SyncDirInfo>();
            SyncFileInfos = new List<SyncFileInfo>();
            ConflictInfos = new List<ConflictInfo>();
            LogStack = new Stack<LogMessage>();

            DirTree = new DirTree(new MyDirInfo("\\", ""));
        }

        /// <summary>
        /// set listener if it is not already set
        /// </summary>
        /// <param name="listener"></param>
        public void SetListener(ISyncListener listener)
        {
            if(_listener == null)
                _listener = listener;
        }

        /// <summary>
        /// remove listener
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(ISyncListener listener)
        {
            if (_listener == listener)
                _listener = null;
        }

        /// <summary>
        /// synchronisation start time
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// synchronisation end time
        /// </summary>
        public DateTime EndTime { get; private set; }

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
        public bool Conflicted => ConflictInfos.Count > 0;

        /// <summary>
        /// sum of all file sizes except files to remove
        /// in byte
        /// </summary>
        public long TotalSize { get; private set; }

        /// <summary>
        /// sum of the copied files sizes
        /// in byte
        /// </summary>
        public long SizeApplied { get; private set; }

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
        /// count of detected files, which schould be synchronised
        /// </summary>
        public long FilesFound => SyncFileInfos.Count;

        /// <summary>
        /// count of detected directories to synchronise
        /// </summary>
        public long DirsFound => SyncDirInfos.Count;

        /// <summary>
        /// count of synchronised files
        /// </summary>
        public long FilesDone { get; private set; }

        /// <summary>
        /// count of synchronised directories (independent of files in this directories)
        /// </summary>
        public long DirsDone { get; private set; }

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

        public void SyncElementStateChanged(SyncElementInfo sei)
        {
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sei.SyncState)
            {
                case SyncElementState.ElementFound:
                    if (isFile) DirTree.AddFile((MyFileInfo)sei.ElementInfo);
                    else DirTree.AddDir((MyDirInfo)sei.ElementInfo);
                    break;
                case SyncElementState.ChangeDetectingStarted:

                    break;
                case SyncElementState.NoChangeFound:

                    break;
                case SyncElementState.ChangeFound:
                    if (isFile)
                    {
                        SyncFileInfos.Add((SyncFileInfo)sei);
                        if(!sei.SyncExecutionInfo.Remove)
                            TotalSize += ((MyFileInfo)sei.ElementInfo).Size;
                    }
                    else
                        SyncDirInfos.Add((SyncDirInfo)sei);
                    break;
                case SyncElementState.ChangeApplied:
                    if (isFile) FilesDone++;
                    else DirsDone++;
                    break;
                case SyncElementState.Conflicted:
                    ConflictInfos.Add(sei.ConflictInfo);
                    break;
            }

            _listener?.OnSyncElementStateChanged(sei);
        }


        /*
        public void FileFound(MyFileInfo file)
        {
            List<DirTree> path = _dirTree.AddFile(file);
            _listener?.OnFileFound(file);
        }

        public void DirFound(MyDirInfo dir)
        {
            List<DirTree> path = _dirTree.AddDir(dir);
            _listener?.OnDirFound(dir);
        }
        
        /// <summary>
        /// call when a difference in the files has been detected
        /// </summary>
        /// <param name="sfi">file information</param>
        public void FileChangeDetected(MyFileInfo fi)
        {
            SyncFiles.Add(fi);
            if (!fi.SyncInfo.Remove) TotalSize += fi.Size;
            _listener?.OnFileChangeDetected(fi);
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
        /// call when succesfully created or deleted a directory or file
        /// </summary>
        /// <param name="sei">sync element information</param>
        public void AppliedChange(SyncElementInfo sei)
        {
            if (typeof(SyncFileInfo) == sei.GetType())
                FilesDone++;
            else
                DirsDone++;

            SyncElementStateChanged(sei);
        }
        /// <summary>
        /// call when dir or file conflicted
        /// </summary>
        /// <param name="ci">conflict info</param>
        public void ElementConflicted(ConflictInfo ci)
        {
            ConflictInfos.Add(ci);
            Listener?.OnConflicted(ci);
        }
        
        */

        public void Log(LogMessage message)
        {
            LogStack.Push(message);
            _listener?.OnLog(message);
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
