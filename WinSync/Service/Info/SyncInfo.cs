using WinSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinSync.Service
{
    public class SyncInfo
    {
        TimeSpan _timePaused = TimeSpan.Zero;
        double? _lastSizeApplied; // in Megabit
        DateTime? _lastTime;
        SyncStatus _status;

        public SyncLink Link { get; private set; }

        public bool Paused { get; private set; }
        public bool Running => !Finished;
        public bool Finished => Status == SyncStatus.Finished || Status == SyncStatus.Conflicted || Status == SyncStatus.Aborted;

        /// <summary>
        /// synchronisation status
        /// </summary>
        public SyncStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                _listener?.OnSyncStatusChanged(_status);
            }
        }

        public List<SyncDirExecutionInfo> SyncDirExecutionInfos { get; private set; }
        public List<SyncFileExecutionInfo> SyncFileExecutionInfos { get; private set; }

        public List<ElementConflictInfo> ConflictInfos { get; private set; }
        public Stack<LogMessage> LogStack { get; private set; }

        public DirTree DirTree { get; private set; }

        private ISyncListener _listener;

        /// <summary>
        /// create SyncInfo
        /// </summary>
        /// <param name="link">link data (will be copied not referenced)</param>
        public SyncInfo(SyncLink link)
        {
            Link = link;
            
            Paused = false;
            TotalFileSizeToCopy = 0;
            TotalFileSizeToRemove = 0;
            FileSizeCopied = 0;
            FileSizeRemoved = 0;
            CreatedDirs = 0;
            RemovedDirs = 0;
            CopiedFiles = 0;
            RemovedFiles = 0;
            FilesFound = 0;
            DirsFound = 0;
            DetectedFilesCount = 0;
            DetectedDirsCount = 0;
            SyncDirExecutionInfos = new List<SyncDirExecutionInfo>();
            SyncFileExecutionInfos = new List<SyncFileExecutionInfo>();
            ConflictInfos = new List<ElementConflictInfo>();
            LogStack = new Stack<LogMessage>();

            MyDirInfo rootDir = new MyDirInfo("\\", "");
            SyncDirInfo sdi = new SyncDirInfo(this, rootDir, false);
            DirTree = new DirTree(rootDir, null, null);

            Status = SyncStatus.DetectingChanges;
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
        /// sum of all file sizes
        /// in byte
        /// </summary>
        public long TotalFileSize => TotalFileSizeToCopy + TotalFileSizeToRemove;

        /// <summary>
        /// sum of all file sizes, which should be copied
        /// in byte
        /// </summary>
        public long TotalFileSizeToCopy { get; private set; }

        /// <summary>
        /// sum of all file sizes, which should be removed
        /// in byte
        /// </summary>
        public long TotalFileSizeToRemove { get; private set; }

        /// <summary>
        /// sum of the copied and removed files sizes
        /// in byte
        /// </summary>
        public long SizeApplied => FileSizeCopied + FileSizeRemoved;

        /// <summary>
        /// sum of the copied files sizes
        /// in byte
        /// </summary>
        public long FileSizeCopied { get; private set; }

        /// <summary>
        /// sum of the removed files sizes
        /// in byte
        /// </summary>
        public long FileSizeRemoved { get; private set; }

        /// <summary>
        /// the calculated average speed
        /// in byte/ms
        /// </summary>
        public double TotalSpeed => SizeApplied / (DateTime.Now - StartTime).TotalMilliseconds;

        /// <summary>
        /// the sum of remaining file sizes to copy
        /// in byte
        /// </summary>
        public long SizeRemaining => TotalFileSize - SizeApplied;

        /// <summary>
        /// synchronisation progress percentage
        /// </summary>
        public float SyncProgress
        {
            get
            {
                if (Status == SyncStatus.DetectingChanges)
                    return 0;
                if (Status == SyncStatus.Finished)
                    return 100;
                if(TotalFileSize == 0 || SizeApplied == 0)
                    return 0;
                return 100f / TotalFileSize * SizeApplied;
            }
        }

        /// <summary>
        /// count of remaining files to synchronise
        /// </summary>
        public long FilesRemaining => ChangedFilesFound - FileChangesApplied;

        public float DetectProgress => DetectedFilesCount * 100f / FilesFound;

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
        /// count of files that has been found in the sync dir
        /// </summary>
        public long FilesFound { get; private set; }

        /// <summary>
        /// count of subdirs that has been found in the sync dir
        /// </summary>
        public long DirsFound { get; private set; }

        /// <summary>
        /// count of files, on which the detect changes process has been applied
        /// </summary>
        public long DetectedFilesCount { get; private set; }

        /// <summary>
        /// count of subdirs, on which the detect changes process has been applied
        /// </summary>
        public long DetectedDirsCount { get; private set; }

        /// <summary>
        /// count of detected files, which schould be synchronised
        /// </summary>
        public long ChangedFilesFound => SyncFileExecutionInfos.Count;

        /// <summary>
        /// count of detected directories, which schould be synchronised
        /// </summary>
        public long ChangedDirsFound => SyncDirExecutionInfos.Count;

        /// <summary>
        /// count of detected files, which schould be copied
        /// </summary>
        public long ChangedFilesToCopyFound { get; private set; }

        /// <summary>
        /// count of detected files, which schould be removed
        /// </summary>
        public long ChangedFilesToRemoveFound { get; private set; }

        /// <summary>
        /// count of detected directories, which schould be copied
        /// </summary>
        public long ChangedDirsToCreateFound { get; private set; }

        /// <summary>
        /// count of detected directories, which schould be removed
        /// </summary>
        public long ChangedDirsToRemoveFound { get; private set; }

        /// <summary>
        /// count of synchronised files
        /// </summary>
        public long FileChangesApplied => CopiedFiles + RemovedFiles;

        /// <summary>
        /// count of synchronised directories
        /// </summary>
        public long DirChangesApplied => CreatedDirs + RemovedDirs;

        /// <summary>
        /// count of copied files
        /// </summary>
        public long CopiedFiles { get; private set; }

        /// <summary>
        /// count of removed files
        /// </summary>
        public long RemovedFiles { get; private set; }

        /// <summary>
        /// count of created directories
        /// </summary>
        public long CreatedDirs { get; private set; }

        /// <summary>
        /// count of removed directories
        /// </summary>
        public long RemovedDirs { get; private set; }

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
        }

        /// <summary>
        /// call when synchronisation finished
        /// </summary>
        public void SyncFinished()
        {
            Status = SyncStatus.Finished;
            EndTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been cancelled
        /// </summary>
        public void SyncCancelled()
        {
            Status = SyncStatus.Aborted;
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

        public void SyncElementStatusChanged(SyncElementInfo sei)
        {
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sei.SyncStatus)
            {
                case SyncElementStatus.ElementFound:
                    if (isFile)
                    {
                        FilesFound++;
                        DirTree.AddFile((MyFileInfo)sei.ElementInfo);
                    }
                    else
                    {
                        DirsFound++;
                        DirTree.AddDir((MyDirInfo)sei.ElementInfo);
                    }
                    break;
                case SyncElementStatus.ChangeDetectingStarted:

                    break;
                case SyncElementStatus.NoChangeFound:

                    break;
                case SyncElementStatus.ChangeFound:
                    if (isFile)
                    {
                        SyncFileExecutionInfos.Add((SyncFileExecutionInfo)sei.SyncExecutionInfo);

                        if (sei.SyncExecutionInfo.Remove)
                        {
                            ChangedFilesToRemoveFound++;
                            TotalFileSizeToRemove += ((MyFileInfo)sei.ElementInfo).Size;
                        }
                        else
                        {
                            ChangedFilesToCopyFound++;
                            TotalFileSizeToCopy += ((MyFileInfo)sei.ElementInfo).Size;
                        }
                    }
                    else
                    {
                        SyncDirExecutionInfos.Add((SyncDirExecutionInfo)sei.SyncExecutionInfo);

                        if (sei.SyncExecutionInfo.Remove)
                            ChangedDirsToRemoveFound++;
                        else
                            ChangedDirsToCreateFound++;
                    }
                    break;
                case SyncElementStatus.ChangeApplied:
                    if (isFile)
                    {
                        if (sei.SyncExecutionInfo.Remove)
                        {
                            FileSizeRemoved += ((SyncFileInfo)sei).FileInfo.Size;
                            RemovedFiles++;
                        }
                        else
                        {
                            FileSizeCopied += ((SyncFileInfo)sei).FileInfo.Size;
                            CopiedFiles++;
                        }
                    }
                    else
                    {
                        if (sei.SyncExecutionInfo.Remove)
                            RemovedDirs++;
                        else
                            CreatedDirs++;
                    }
                    break;
                case SyncElementStatus.Conflicted:
                    ConflictInfos.Add(sei.ConflictInfo);
                    break;
            }

            _listener?.OnSyncElementStatusChanged(sei);
        }

        public void DetectingEnded(SyncElementInfo sei)
        {
            if (typeof(SyncFileInfo) == sei.GetType())
                DetectedFilesCount++;
            else
                DetectedDirsCount++;
        }
        
        public void Log(LogMessage message)
        {
            LogStack.Push(message);
            _listener?.OnLog(message);

            if (message.Exception != null)
            {

            }
                
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
