using WinSync.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WinSync.Service
{
    public class SyncInfo
    {
        double? _lastSizeApplied; // in Megabit
        DateTime? _lastTime;
        SyncStatus _status;
        ISyncListener _listener;

        #region time measurement
        TimeSpan _pausedTime = TimeSpan.Zero;
        DateTime _startTime;
        DateTime _endTime;
        DateTime _lastPauseStartTime;
        #endregion

        #region files info
        long _filesTotalSizeToCopy = 0;
        long _filesTotalSizeToRemove = 0;
        long _fileSizeCopied = 0;
        long _fileSizeRemoved = 0;
        long _filesFoundCount = 0;
        long _filesDetectedCount = 0;
        long _filesCopiedCount = 0;
        long _filesRemovedCount = 0;
        long _filesToCopyCount = 0;
        long _filesToRemoveCount = 0;
        #endregion

        #region dirs info
        long _dirsFoundCount = 0;
        long _dirsDetectedCount = 0;
        long _dirsToCreateCount = 0;
        long _dirsToRemoveCount = 0;
        long _dirsCreatedCount = 0;
        long _dirsRemovedCount = 0;
        #endregion

        /// <summary>
        /// create SyncInfo
        /// </summary>
        /// <param name="link">link data (will be copied not referenced)</param>
        public SyncInfo(SyncLink link)
        {
            Link = link;
            Time = new TimeMeasurement(this);
            Files = new FilesInfo(this);
            Dirs = new DirsInfo(this);

            Paused = false;
            SyncDirExecutionInfos = new List<SyncDirExecutionInfo>();
            SyncFileExecutionInfos = new List<SyncFileExecutionInfo>();
            ConflictInfos = new List<ElementConflictInfo>();
            LogStack = new Stack<LogMessage>();

            MyDirInfo rootDir = new MyDirInfo("\\", "");
            SyncDirInfo sdi = new SyncDirInfo(this, rootDir, false);
            DirTree = new DirTree(rootDir, null, null);

            Status = SyncStatus.DetectingChanges;
        }

        public bool Running
        {
            get { return !Finished; }
        }
        public bool Finished
        {
            get
            {
                return Status == SyncStatus.Finished || Status == SyncStatus.Conflicted || Status == SyncStatus.Aborted;
            }
        }

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

        public TimeMeasurement Time { get; }
        public FilesInfo Files { get; }
        public DirsInfo Dirs { get; }

        public SyncLink Link { get; private set; }
        public bool Paused { get; private set; }
        public List<SyncDirExecutionInfo> SyncDirExecutionInfos { get; private set; }
        public List<SyncFileExecutionInfo> SyncFileExecutionInfos { get; private set; }
        public List<ElementConflictInfo> ConflictInfos { get; private set; }
        public Stack<LogMessage> LogStack { get; private set; }
        public DirTree DirTree { get; private set; }

        #region listener
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
        #endregion

        /// <summary>
        /// if conflicts appeared while synchronizing
        /// </summary>
        public bool Conflicted
        {
            get { return ConflictInfos.Count > 0; }
        }

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
                if (Files.TotalSize == 0 || Files.AppliedSize == 0)
                    return 0;
                return 100f / Files.TotalSize * Files.AppliedSize;
            }
        }

        #region sync-speed
        /// <summary>
        /// the calculated average speed
        /// in byte/ms
        /// </summary>
        public double TotalSpeed
        {
            get { return Files.AppliedSize / (DateTime.Now - Time.Start).TotalMilliseconds; }
        }

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
        public double AverageSpeed
        {
            get { return Files.AppliedSize / 131072.0 / Time.Total.TotalSeconds; }
        }

        /// <summary>
        /// calculate the actual speed ActSpeed with delta time and size applied difference since the last recalculation
        /// </summary>
        public void RecalculateActSpeed()
        {
            if (_lastTime != null && _lastSizeApplied != null)
            {
                double timeDif = (DateTime.Now - _lastTime.Value).TotalSeconds;
                double sizeAppliedDif = Files.AppliedSize / 131072.0 - _lastSizeApplied.Value;
                ActSpeed = sizeAppliedDif / timeDif;
            }
            else
            {
                _lastTime = DateTime.Now;
                _lastSizeApplied = 0;
            }
        }
        #endregion

        #region events
        /// <summary>
        /// call when synchronisation started
        /// </summary>
        public void SyncStarted()
        {
            _startTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation finished
        /// </summary>
        public void SyncFinished()
        {
            Status = SyncStatus.Finished;
            _endTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been cancelled
        /// </summary>
        public void SyncCancelled()
        {
            Status = SyncStatus.Aborted;
            _endTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been paused
        /// </summary>
        public void SyncPaused()
        {
            Paused = true;
            _lastPauseStartTime = DateTime.Now;
        }

        /// <summary>
        /// call when synchronisation has been continued
        /// </summary>
        public void SyncContinued()
        {
            Paused = false;
            _pausedTime += DateTime.Now - _lastPauseStartTime;
        }

        public void SyncElementStatusChanged(SyncElementInfo sei)
        {
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sei.SyncStatus)
            {
                case SyncElementStatus.ElementFound:
                    if (isFile)
                    {
                        _filesFoundCount++;
                        DirTree.AddFile((MyFileInfo)sei.ElementInfo);
                    }
                    else
                    {
                        _dirsFoundCount++;
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
                            _filesToRemoveCount++;
                            _filesTotalSizeToRemove += ((MyFileInfo)sei.ElementInfo).Size;
                        }
                        else
                        {
                            _filesToCopyCount++;
                            _filesTotalSizeToCopy += ((MyFileInfo)sei.ElementInfo).Size;
                        }
                    }
                    else
                    {
                        SyncDirExecutionInfos.Add((SyncDirExecutionInfo)sei.SyncExecutionInfo);

                        if (sei.SyncExecutionInfo.Remove)
                            _dirsToRemoveCount++;
                        else
                            _dirsToCreateCount++;
                    }
                    break;
                case SyncElementStatus.ChangeApplied:
                    if (isFile)
                    {
                        if (sei.SyncExecutionInfo.Remove)
                        {
                            _fileSizeRemoved += ((SyncFileInfo)sei).FileInfo.Size;
                            _filesRemovedCount++;
                        }
                        else
                        {
                            _fileSizeCopied += ((SyncFileInfo)sei).FileInfo.Size;
                            _filesCopiedCount++;
                        }
                    }
                    else
                    {
                        if (sei.SyncExecutionInfo.Remove)
                            _dirsRemovedCount++;
                        else
                            _dirsCreatedCount++;
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
                _filesDetectedCount++;
            else
                _dirsDetectedCount++;
        }
        
        public void Log(LogMessage message)
        {
            LogStack.Push(message);
            _listener?.OnLog(message);

            if (message.Exception != null)
            {

            }
                
        }
        #endregion

        #region inner classes
        public class TimeMeasurement
        {
            SyncInfo _si;

            public TimeMeasurement(SyncInfo si)
            {
                _si = si;
            }

            /// <summary>
            /// synchronisation start time
            /// </summary>
            public DateTime Start
            {
                get { return _si._startTime; }
            }

            /// <summary>
            /// synchronisation end time
            /// </summary>
            public DateTime End
            {
                get { return _si._endTime; }
            }

            /// <summary>
            /// the time, when the last pause started
            /// </summary>
            public DateTime LastPauseStart
            {
                get { return _si._lastPauseStartTime; }
            }

            /// <summary>
            /// the sum of time, that the synchronisation paused
            /// </summary>
            public TimeSpan Paused
            {
                get { return _si.Paused ? _si._pausedTime + (DateTime.Now - LastPauseStart) : _si._pausedTime; }
            }

            /// <summary>
            /// the total time, that the synchronisation was running minus the pused time
            /// </summary>
            public TimeSpan Total
            {
                get { return _si.Running ? DateTime.Now - Start - Paused : End - Start - Paused; }
            }
            
            /// <summary>
            /// estimated time until the synchronisation finishs
            /// calculated all synchronised files
            /// </summary>
            public TimeSpan RemainingEst
            {
                get
                {
                    double s = _si.TotalSpeed;
                    return TimeSpan.FromMilliseconds(s > 0 ? _si.Files.RemainingSize / s : 0);
                }
            }
        }

        public class FilesInfo
        {
            SyncInfo _si;

            public FilesInfo(SyncInfo si)
            {
                _si = si;
            }

            /// <summary>
            /// sum of all file sizes, which should be copied
            /// in byte
            /// </summary>
            public long TotalSizeToCopy
            {
                get { return _si._filesTotalSizeToCopy; }
            }

            /// <summary>
            /// sum of all file sizes, which should be removed
            /// in byte
            /// </summary>
            public long TotalSizeToRemove
            {
                get { return _si._filesTotalSizeToRemove; }
            }

            /// <summary>
            /// sum of all file sizes
            /// in byte
            /// </summary>
            public long TotalSize
            {
                get { return TotalSizeToCopy + TotalSizeToRemove; }
            }

            /// <summary>
            /// sum of the copied files sizes
            /// in byte
            /// </summary>
            public long CopiedSize
            {
                get { return _si._fileSizeCopied; }
            }

            /// <summary>
            /// sum of the removed files sizes
            /// in byte
            /// </summary>
            public long RemovedSize
            {
                get { return _si._fileSizeRemoved; }
            }

            /// <summary>
            /// sum of the copied and removed files sizes
            /// in byte
            /// </summary>
            public long AppliedSize
            {
                get { return CopiedSize + RemovedSize; }
            }

            /// <summary>
            /// the sum of remaining file sizes to copy
            /// in byte
            /// </summary>
            public long RemainingSize
            {
                get { return TotalSize - AppliedSize; }
            }

            /// <summary>
            /// count of remaining files to synchronise
            /// </summary>
            public long RemainingCount
            {
                get { return ChangedFoundCount - ChangesAppliedCount; }
            }

            /// <summary>
            /// count of files that has been found in the sync dir
            /// </summary>
            public long FoundCount
            {
                get { return _si._filesFoundCount; }
            }

            /// <summary>
            /// count of files, on which the detect changes process has been applied
            /// </summary>
            public long DetectedCount
            {
                get { return _si._filesDetectedCount; }
            }

            /// <summary>
            /// count of detected files, which schould be synchronised
            /// </summary>
            public long ChangedFoundCount
            {
                get
                {
                    return _si.SyncFileExecutionInfos.Count;
                }
            }

            /// <summary>
            /// count of synchronised files
            /// </summary>
            public long ChangesAppliedCount
            {
                get { return CopiedCount + RemovedCount; }
            }

            /// <summary>
            /// count of copied files
            /// </summary>
            public long CopiedCount
            {
                get { return _si._filesCopiedCount; }
            }

            /// <summary>
            /// count of removed files
            /// </summary>
            public long RemovedCount
            {
                get { return _si._filesRemovedCount; }
            }

            /// <summary>
            /// count of detected files, which schould be copied
            /// </summary>
            public long ToCopyCount
            {
                get { return _si._filesToCopyCount; }
            }

            /// <summary>
            /// count of detected files, which schould be removed
            /// </summary>
            public long ToRemoveCount
            {
                get { return _si._filesToRemoveCount; }
            }
        }

        public class DirsInfo
        {
            SyncInfo _si;

            public DirsInfo(SyncInfo si)
            {
                _si = si;
            }

            /// <summary>
            /// count of subdirs that has been found in the sync dir
            /// </summary>
            public long FoundCount
            {
                get { return _si._dirsFoundCount; }
            }

            /// <summary>
            /// count of subdirs, on which the detect changes process has been applied
            /// </summary>
            public long DetectedCount
            {
                get { return _si._dirsDetectedCount; }
            }

            /// <summary>
            /// count of detected directories, which schould be synchronised
            /// </summary>
            public long ChangedFoundCount
            {
                get { return _si.SyncDirExecutionInfos.Count; }
            }

            /// <summary>
            /// count of synchronised directories
            /// </summary>
            public long ChangesAppliedCount
            {
                get { return CreatedCount + RemovedCount; }
            }

            /// <summary>
            /// count of created directories
            /// </summary>
            public long CreatedCount
            {
                get { return _si._dirsCreatedCount; }
            }

            /// <summary>
            /// count of removed directories
            /// </summary>
            public long RemovedCount
            {
                get { return _si._dirsRemovedCount; }
            }

            /// <summary>
            /// count of detected directories, which schould be copied
            /// </summary>
            public long ToCreateCount
            {
                get { return _si._dirsToCreateCount; }
            }

            /// <summary>
            /// count of detected directories, which schould be removed
            /// </summary>
            public long ToRemoveCount
            {
                get { return _si._dirsToRemoveCount; }
            }
        }
        #endregion
    }
}
