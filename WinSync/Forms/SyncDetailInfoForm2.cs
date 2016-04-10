using WinSync.Data;
using WinSync.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;

namespace WinSync.Forms
{
    public partial class SyncDetailInfoForm2 : WinSyncForm, ISyncListener
    {
        readonly SyncLink _l;
        SyncInfo SI { get { return _l.SyncInfo; } }

        bool _updateRoutineRunning;
        bool _updateStatsAsyncRunning;

        bool _statusChangedEventsCheckingRunning;
        bool _statusChangedEventsAToken;
        List<StatusChangedEvent> _statusChangedEvents = new List<StatusChangedEvent>();

        #region variables for more performant form updating
        const int uProgressIGeneralIF = 4; //importance-factor for updating general progress infos (0 = max. importance)
        const int updateRoutineTimeout = 500; //in milliseconds
        const int updatingSyncInfoTimeout = 250; //in milliseconds

        SyncStatus _oldStatus;
        bool _oldPaused;
        int _generalRoundCount = 0;
        #endregion

        /// <summary>
        /// create SyncDetailInfoForm2 that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public SyncDetailInfoForm2(SyncLink l)
        {
            _l = l;

            InitializeComponent();

            statusProgressBar1.StatusTitles = SyncStatus.GetGroupedTitles();
            statusProgressBar1.ActivateStatus("fetching files/dirs");

            SyncStatusFormHelper.Init(this);

            tabControl_left1.SelectedIndex = 1;
            label_title.Text = _l.Title;
            label_link_folder1.Text = _l.Path1;
            label_link_folder2.Text = _l.Path2;
            label_link_direction.Text = _l.Direction.ToString();

            if (SI != null)
            {
                //build tree (pause sync while building)
                bool running = _l.IsRunning;
                if (running) _l.PauseSync();
                int ct = 0;
                int i = 0;
                while (_l.SyncTask != null && _l.SyncTask.TasksRunning() > 0 && i < 5)
                {
                    if (ct == _l.SyncTask.TasksRunning())
                        i++;
                    else
                    {
                        ct = _l.SyncTask.TasksRunning();
                        i = 0;
                    }
                    Thread.Sleep(300);
                }
                if (_l.SyncTask != null)
                {
                    BuildTreeBase(_l.SyncInfo.DirTree);
                    SI.SetListener(this);
                    if (running) _l.ResumeSync();
                }
            }

            Load += delegate { StartUpdateRoutine(); };
        }
        
        /// <summary>
        /// start checking, if a new sync-execution is running, in background-task
        /// </summary>
        public void StartUpdateRoutine()
        {
            if (_updateRoutineRunning)
                return;

            Task.Run(() =>
            {
                _updateRoutineRunning = true;
                while (!IsDisposed)
                {
                    if (_l.IsRunning)
                    {
                        SI?.SetListener(this);
                        Invoke(new Action(() => {
                            StartUpdatingSyncInfo();
                            StartStatusChangedEventsCheckingAsync();
                        }));
                    }

                    Invoke(new Action(() =>
                    {
                        label_syst_runningTasks.Text = (_l.SyncTask == null ? 0 : _l.SyncTask.TasksRunning()).ToString();
                    }));

                    Task.Delay(updateRoutineTimeout);
                }
                _updateRoutineRunning = false;
            });
        }

        /// <summary>
        /// start updating sync-info and sync-controls async while synchronisation is running
        /// </summary>
        public async void StartUpdatingSyncInfo()
        {
            if (_updateStatsAsyncRunning)
                return;
            _updateStatsAsyncRunning = true;

            while (_l.IsRunning)
            {
                if(tabControl_left1.SelectedTab == tabPage_syncInfo)
                {
                    UpdateSyncControls();
                    UpdateSyncProgressInfos(false);
                }
                await Task.Delay(updatingSyncInfoTimeout);
            }

            UpdateSyncControls();
            UpdateSyncProgressInfos(false);

            _updateStatsAsyncRunning = false;
        }

        /// <summary>
        /// update controls concerning synchronisation in form
        /// </summary>
        public void UpdateSyncControls()
        {
            if (_l.IsRunning)
            {
                //display cancel and pause/continue button
                button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;
                button_pr.Visible = true;
                button_pr.BackgroundImage = SI.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;

            }
            else
            {
                button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
                button_pr.Visible = false;
                button_pr.BackgroundImage = Properties.Resources.ic_play_white;
            }
        }

        /// <summary>
        /// update tab "Sync Info" in Form
        /// </summary>
        /// <param name="updateAll">if false some update processes may suspend, due to their lesser importance</param>
        public void UpdateSyncProgressInfos(bool updateAll)
        {
            // suspend updating general infos {uProgressIGeneralIF} times
            if (updateAll || _generalRoundCount <= 0)
            {
                progressBar.Value = (int)(SI.SyncProgress * 10);
                label_syst_progress.Text = $"{SI.SyncProgress:0.00}%";
                label_syst_totalTime.Text = SI.Time.Total.ToString(@"hh\:mm\:ss");

                if (SI.Paused != _oldPaused)
                {
                    button_pr.BackgroundImage = SI.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
                    _oldPaused = SI.Paused;
                }

                if (SI.Status != _oldStatus)
                {
                    panel_header.BackColor = SI.Status.Color;
                    statusProgressBar1.ActivateStatus(SI.Status.Title);
                    statusProgressBar1.ActivatedStatusColor = SI.Status.Color;

                    if (_oldStatus != null)
                    {
                        SyncStatusFormHelper oldHelper = SyncStatusFormHelper.GetFromStatus(_oldStatus);
                        foreach (Control c in oldHelper.InfControls)
                            c.Visible = false;
                    }

                    SyncStatusFormHelper newHelper = SyncStatusFormHelper.GetFromStatus(SI.Status);
                    foreach (Control c in newHelper.InfControls)
                        c.Visible = true;

                    _oldStatus = SI.Status;
                }

                _generalRoundCount = uProgressIGeneralIF;
            }
            else
                _generalRoundCount--;

            if (SI.Status == SyncStatus.FetchingElements)
            {
                label_fetchFD_filesFound.Text = SI.Files.FoundCount.ToString();
                label_fetchFD_foldersFound.Text = SI.Dirs.FoundCount.ToString();
            }
            else if(SI.Status == SyncStatus.DetectingChanges)
            {
                label_detectCh_changesDetected.Text = (SI.Files.ChangedFoundCount + SI.Dirs.ChangedFoundCount).ToString();
                label_detectCh_FDDone.Text = (SI.Files.DetectedCount + SI.Dirs.DetectedCount).ToString();

                label_detectCh_filesToCopy.Text = SI.Files.ToCopyCount.ToString();
                label_detectCh_filesToRemove.Text = SI.Files.ToRemoveCount.ToString();
                label_detectCh_foldersToCreate.Text = SI.Dirs.ToCreateCount.ToString();
                label_detectCh_foldersToRemove.Text = SI.Dirs.ToRemoveCount.ToString();
            }
            else if (SI.Status == SyncStatus.CreatingFolders)
            {
                label_crDirs_dirsCreated.Text = $"{SI.Dirs.ChangesAppliedCount} of {SI.Dirs.FoundCount}";
            }
            else if (SI.Status == SyncStatus.ApplyingFileChanges)
            {
                label_applyCh_speed_current.Text = $"{SI.ActSpeed:0.00} Mbit/s";
                label_applyCh_speed_average.Text = $"{SI.AverageSpeed:0.00} Mbit/s";

                label_applyCh_copiedFilesCount.Text = $"{SI.Files.CopiedCount:#,#} of {SI.Files.ToCopyCount:#,#}";
                label_applyCh_copiedFilesSize.Text = $"{SI.Files.CopiedSize / (1024.0 * 1024.0):#,#0.00} of " +
                        $"{SI.Files.TotalSizeToCopy / (1024.0 * 1024.0):#,#0.00}MB";

                label_applyCh_removedFilesCount.Text = $"{SI.Files.RemovedCount:#,#} of {SI.Files.ToCopyCount:#,#}";
                label_applyCh_removedFilesSize.Text = $"{SI.Files.RemovedSize / (1024.0 * 1024.0):#,#0.00} of " +
                        $"{SI.Files.TotalSizeToRemove / (1024.0 * 1024.0):#,#0.00}MB";
            }
            else if (SI.Status == SyncStatus.RemoveDirs)
            {
                label_remDirs_foldersRemoved.Text = $"{SI.Dirs.RemovedCount} of {SI.Dirs.ToRemoveCount}";
            }
        }

        /// <summary>
        /// update tab "Sync Element Info" in Form
        /// </summary>
        private void UpdateSyncElementInfo()
        {
            SyncElementTreeViewNode node = (SyncElementTreeViewNode)treeView1.SelectedNode;
            if (node == null)
                return;
            MyElementInfo ei = node.ElementInfo;

            label_sei_type.Text = ei.GetType() == typeof(MyFileInfo) ? "File" : "Dir";
            label_sei_name.Text = ei.Name;

            if (ei.SyncElementInfo != null)
            {
                label_sei_path1.Text = ei.SyncElementInfo.AbsolutePath1;
                label_sei_path2.Text = ei.SyncElementInfo.AbsolutePath2;

                SyncElementExecutionInfo seei = ei.SyncElementInfo.SyncExecutionInfo;
                if (seei != null)
                {
                    label_sei_direction.Text = seei.Direction.ToString();
                    label_sei_remove.Text = seei.Remove ? "Yes" : "No";

                    label_sei_startTime.Text = seei.SyncStart != null ? seei.SyncStart.Value.ToString(@"hh\:mm\:ss") : "";
                    label_sei_endTime.Text = seei.SyncEnd != null ? seei.SyncEnd.Value.ToString(@"hh\:mm\:ss") : "";
                    label_sei_duration.Text = $"{seei.SyncDuration.TotalSeconds}s";
                }
                else
                {
                    label_sei_direction.Text = "";
                    label_sei_remove.Text = "";
                    label_sei_startTime.Text = "";
                    label_sei_endTime.Text = "";
                    label_sei_duration.Text = "";
                }

                label_sei_syncStatus.Text = Enum.GetName(typeof(SyncElementStatus), ei.SyncElementInfo.SyncStatus);

                if (ei.SyncElementInfo.IsConflicted)
                {
                    textBox_sei_info.Text = $"{Enum.GetName(typeof(ConflictType), ei.SyncElementInfo.ConflictInfo.Type)}, {ei.SyncElementInfo.ConflictInfo.Message}";
                }
                else
                {
                    textBox_sei_info.Text = "";
                }
            }
            else
            {
                label_sei_path1.Text = "";
                label_sei_path2.Text = "";
                label_sei_syncStatus.Text = "";
            }
        }

        /// <summary>
        /// is called when the status of a sync-element has changed
        /// </summary>
        /// <param name="sei"></param>
        public void OnSyncElementStatusChanged(SyncElementInfo sei)
        {
            while (_statusChangedEventsAToken || _statusChangedEvents.Count > 1000)
                Thread.Sleep(1);
            _statusChangedEventsAToken = true;
            // Add events to list, in order to process them in another thread.
            // So the sync execution is not delayed.
            _statusChangedEvents.Add(new StatusChangedEvent(sei, sei.SyncStatus)); 
            _statusChangedEventsAToken = false;
        }

        /// <summary>
        /// is called when the status of the sync has changed
        /// </summary>
        /// <param name="status"></param>
        public void OnSyncStatusChanged(SyncStatus status)
        {
            if (status == SyncStatus.DetectingChanges)
            {
                Invoke(new Action(() =>
                {
                    StartUpdatingSyncInfo();
                    StartStatusChangedEventsCheckingAsync();
                }));
            }
        }

        /// <summary>
        /// check if new status-changed-events has been added to the list and process them
        /// </summary>
        public async void StartStatusChangedEventsCheckingAsync()
        {
            if (_statusChangedEventsCheckingRunning)
                return;

            _statusChangedEventsCheckingRunning = true;

            await Task.Run(async () =>
            {
                while (_l.IsRunning || _statusChangedEvents.Count > 0)
                {
                    //update tree
                    while (_statusChangedEventsAToken)
                        await Task.Delay(1);
                    _statusChangedEventsAToken = true;


                    List<StatusChangedEvent> tempSCE = new List<StatusChangedEvent>(_statusChangedEvents);
                    _statusChangedEvents = new List<StatusChangedEvent>();

                    _statusChangedEventsAToken = false;

                    foreach (StatusChangedEvent sce in tempSCE.Where(x => x.CreateStatus == x.SyncElementInfo.SyncStatus))
                    {
                        ProcessStatusChangedEventAsync(sce);
                    }
                    //------------------

                    await Task.Delay(1);
                }
            });

            _statusChangedEventsCheckingRunning = false;

            UpdateSyncControls();
            UpdateSyncProgressInfos(true);
        }

        /// <summary>
        /// process a status-changed-event
        /// </summary>
        /// <param name="sce">status-changed-event</param>
        public void ProcessStatusChangedEventAsync(StatusChangedEvent sce)
        {
            SyncElementInfo sei = sce.SyncElementInfo;
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sce.CreateStatus)
            {
                case SyncElementStatus.ElementFound:
                    UpdateTreeNodeIfVisible(sei.ElementInfo, true);
                    break;
                case SyncElementStatus.ChangeDetectingStarted:
                    break;
                case SyncElementStatus.NoChangeFound:
                    UpdateTreeNodeIfVisible(sei.ElementInfo, true);
                    break;
                case SyncElementStatus.ChangeFound:
                    UpdateTreeNodeIfVisible(sei.ElementInfo, true);
                    break;
                case SyncElementStatus.ChangeApplied:
                    UpdateTreeNodeIfVisible(sei.ElementInfo, true);
                    break;
                case SyncElementStatus.Conflicted:
                    string elementType = isFile ? "file" : "dir";
                    string conflictType = "";
                    switch (sei.ConflictInfo.Type)
                    {
                        case ConflictType.IO:
                            conflictType = "IO";
                            break;
                        case ConflictType.UA:
                            conflictType = "Access Denied";
                            break;
                        case ConflictType.DirNotEmpty:
                            conflictType = "Dir Not Empty";
                            break;
                        case ConflictType.Unknown:
                            conflictType = "Unknown";
                            break;
                    }

                    Invoke(new Action(() => 
                        AddLogLine($"Conflict ({conflictType}) at {elementType} in {sei.ConflictInfo.Context}: {sei.ConflictInfo.GetAbsolutePath()}")
                    ));

                    UpdateTreeNodeIfVisible(sei.ElementInfo, true);

                    break;
            }
        }

        /// <summary>
        /// add process line on log message received
        /// </summary>
        /// <param name="message"></param>
        public void OnLog(LogMessage message)
        {
            string text = "";
            switch (message.Type)
            {
                case LogType.ERROR:
                    text = "Error:";
                    break;
                case LogType.INFO:
                    text = "Info:";
                    break;
            }
            text += message.Message;
            Invoke(new Action(() => AddLogLine(text)));
        }

        /// <summary>
        /// add line to process listBox
        /// </summary>
        /// <param name="text">line</param>
        private void AddLogLine(string text)
        {
            Console.WriteLine(text);
            try
            {
                listBox_log.Items.Insert(0, text);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (InvalidOperationException)
            {
                //window handle wasn't created
            }
        }

        #region tree management
        /// <summary>
        /// update or create treenode if it is visible
        /// </summary>
        /// <param name="ei">element info</param>
        /// <param name="invoke">if the operations on treeview should be invoked</param>
        private void UpdateTreeNodeIfVisible(MyElementInfo ei, bool invoke)
        {
            TreeNodeCollection tnc = treeView1.Nodes;
            SyncDirTreeViewNode treeNode = null;

            for (int i = 1; i < ei.TreePath.Count; i++)
            {
                treeNode = (SyncDirTreeViewNode)tnc[ei.TreePath[i].Info.Name];
                if (treeNode == null) return;

                //check if treenode is visible
                bool expanded = false;
                Action ae = new Action(() => expanded = treeNode.IsExpanded);
                if (invoke) treeView1.Invoke(ae);
                else ae();
                if (!expanded) return;

                //update tree node (according to the status of ei)
                treeNode.ChildStatus = ei.SyncElementInfo.SyncStatus;
                Action a = new Action(treeNode.Update);
                if (invoke) treeView1.Invoke(a);
                else a();

                tnc = treeNode.Nodes;
            }

            if (ei.ElementTreeViewNode == null)
            {
                //create treenode
                if (ei.GetType() == typeof(MyFileInfo))
                    ei.ElementTreeViewNode = new SyncFileTreeViewNode((MyFileInfo)ei);
                else
                    ei.ElementTreeViewNode = new SyncDirTreeViewNode((MyDirInfo)ei);

                Action aa = new Action(() => tnc.Add(ei.ElementTreeViewNode));
                if (invoke) treeView1.Invoke(aa);
                else aa();
            }

            //update result tree node
            Action au = new Action(ei.ElementTreeViewNode.Update);
            if (invoke) treeView1.Invoke(au);
            else au();
        }

        /// <summary>
        /// build base treenodes in treeView
        /// </summary>
        /// <param name="dirTree"></param>
        private void BuildTreeBase(DirTree dirTree)
        {
            foreach (DirTree dir in dirTree.Dirs)
            {
                dir.Info.DirTreeViewNode = new SyncDirTreeViewNode(dir.Info);
                treeView1.Nodes.Add(dir.Info.DirTreeViewNode);
            }

            foreach (MyFileInfo file in dirTree.Files)
            {
                file.FileTreeViewNode = new SyncFileTreeViewNode(file);
                treeView1.Nodes.Add(file.FileTreeViewNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //toggle tree node on click
            if (e.Node.GetType() == typeof(SyncFileTreeViewNode))
                return;

            SyncDirTreeViewNode dirNode = (SyncDirTreeViewNode)e.Node;
            dirNode.Toggle();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tabControl_left1.SelectedTab = tabPage_syncElementInfo;
            tableLayoutPanel_sei.Visible = true;

            UpdateSyncElementInfo();
        }
        #endregion

        #region event handler
        private void SyncDetailInfoForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            SI?.RemoveListener(this);
        }

        /// <summary>
        /// start/stop synchronisation on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sync_Click(object sender, EventArgs e)
        {
            if (!_l.IsRunning)
            {
                _l.Sync(this);
                listBox_log.Items.Clear();
                treeView1.Nodes.Clear();
                _statusChangedEvents = new List<StatusChangedEvent>();
                
                StartUpdatingSyncInfo();
                StartStatusChangedEventsCheckingAsync();
            }
            else
            {
                _l.CancelSync();
            }
        }

        /// <summary>
        /// resume/pause synchronisation on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_pr_Click(object sender, EventArgs e)
        {
            if (SI == null)
                return;

            if (SI.Paused)
                _l.ResumeSync();
            else
                _l.PauseSync();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SyncDetailInfoForm2_Resize(object sender, EventArgs e)
        {
        }

        private void listBox_syncInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetData(DataFormats.Text, (string)listBox_log.SelectedItem);
            }
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
            UpdateSyncElementInfo();
        }
        #endregion

        #region form helper 
        private class SyncStatusFormHelper
        {
            private static SyncStatusFormHelper[] helpers;

            public SyncStatus Status { get; }
            public List<Control> InfControls { get; }

            /// <summary>
            /// create SyncStatusHelper
            /// </summary>
            /// <param name="status"></param>
            /// <param name="infControls">controls that should be displayed when the associated Status is active</param>
            private SyncStatusFormHelper(SyncStatus status, List<Control> infControls)
            {
                Status = status;
                InfControls = infControls;
            }

            public static void Init(SyncDetailInfoForm2 form)
            {
                helpers = new SyncStatusFormHelper[7];

                helpers[0] = new SyncStatusFormHelper(SyncStatus.FetchingElements, new List<Control>()
                    {form.panel_fetchFD});
                helpers[1] = new SyncStatusFormHelper(SyncStatus.DetectingChanges, new List<Control>()
                    {form.panel_detectCh, form.panel_detectCh_chTypes});
                helpers[2] = new SyncStatusFormHelper(SyncStatus.CreatingFolders, new List<Control>()
                    {form.panel_crDirs });
                helpers[3] = new SyncStatusFormHelper(SyncStatus.ApplyingFileChanges, new List<Control>()
                    {form.panel_applyCh_speed, form.panel_applyCh_syncedFiles});
                helpers[4] = new SyncStatusFormHelper(SyncStatus.RemoveDirs, new List<Control>()
                    {form.panel_remDirs});
                helpers[5] = new SyncStatusFormHelper(SyncStatus.Finished, new List<Control>()
                { });
                helpers[6] = new SyncStatusFormHelper(SyncStatus.Aborted, new List<Control>()
                { });
            }

            public static SyncStatusFormHelper GetFromStatus(SyncStatus status)
            {
                return helpers.First(x => x.Status == status);
            }
        }
        #endregion
    }

    public class StatusChangedEvent
    {
        public SyncElementInfo SyncElementInfo { get; set; }

        /// <summary>
        /// The preserved SyncElementStatus, from the time of the StatusChangedEvent creation.
        /// Note that the status in SyncElementInfo changes during the sync.
        /// </summary>
        public SyncElementStatus CreateStatus { get; set; }

        /// <summary>
        /// create StatusChangedEvent
        /// </summary>
        /// <param name="syncElementInfo">reference to the element, whose status has been changed</param>
        /// <param name="createStatus">the status of the element, which should be preserved</param>
        public StatusChangedEvent(SyncElementInfo syncElementInfo, SyncElementStatus createStatus)
        {
            SyncElementInfo = syncElementInfo;
            CreateStatus = createStatus;
        }
    }
}
