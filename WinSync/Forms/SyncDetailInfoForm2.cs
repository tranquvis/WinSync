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
    public partial class SyncDetailInfoForm2 : Form, ISyncListener
    {
        readonly SyncLink _l;
        bool _updateStatsAsyncRunning;
        bool _statusChangedEventsCheckingRunning;
        MainForm _mainForm;
        bool _statusChangedEventsAToken;
        List<StatusChangedEvent> _statusChangedEvents = new List<StatusChangedEvent>();

        //for more performant form updating
        SyncStatus _oldStatus;
        bool _oldPaused;
        const int _generalSuspendRounds = 4;
        int _generalRoundCount = 0;

        SyncInfo SI => _l.SyncInfo;

        /// <summary>
        /// create SyncDetailInfoForm2 that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public SyncDetailInfoForm2(SyncLink l, MainForm mainForm)
        {
            _l = l;
            _mainForm = mainForm;
            InitializeComponent();
            SyncStatusHelper.Init(this);

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
                    BuildTreeRecursively(treeView1.Nodes, SI.DirTree);
                    SI.SetListener(this);
                    if (running) _l.ResumeSync();
                }
            }
            StartUpdateRoutine();

        }
        
        public void StartUpdateRoutine()
        {
            Task.Run(async () =>
            {
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

                    await Task.Delay(500);
                }
            });
        }

        /// <summary>
        /// start updating synchronisation info async while synchronisation is running
        /// </summary>
        public async void StartUpdatingSyncInfo()
        {
            if (_updateStatsAsyncRunning)
                return;
            _updateStatsAsyncRunning = true;

            while (_l.IsRunning)
            {
                UpdateSyncInfo(false);
                await Task.Delay(250);
            }
            UpdateSyncInfo(true);

            _updateStatsAsyncRunning = false;
        }

        /// <summary>
        /// update synchronisation info
        /// </summary>
        /// <param name="updateAll">if false some update processes may suspend, due to their lesser importance</param>
        public void UpdateSyncInfo(bool updateAll)
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
                progressBar.Value = (int)(SI.SyncProgress * 10);
                label_syst_progress.Text = $"{SI.SyncProgress:0.00}%";
            }
            UpdateProgressInfos(updateAll);
        }

        /// <summary>
        /// update progress stats in form
        /// </summary>
        /// <param name="updateAll">if false some update processes may suspend, due to their lesser importance</param>
        public void UpdateProgressInfos(bool updateAll)
        {
            DateTime t = DateTime.Now;

            if(updateAll || _generalRoundCount <= 0)
            {
                #region general
                progressBar.Value = (int)(SI.SyncProgress * 10);
                label_syst_progress.Text = $"{SI.SyncProgress:0.00}%";
                label_syst_totalTime.Text = SI.TotalTime.ToString(@"hh\:mm\:ss");
                #endregion

                if (SI.Paused != _oldPaused)
                {
                    button_pr.BackgroundImage = SI.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
                    _oldPaused = SI.Paused;
                }

                if (SI.Status != _oldStatus)
                {
                    panel_header.BackColor = SI.Status.Color;
                    statusProgressBar1.ActivatedStatus = SI.Status;

                    if (_oldStatus != null)
                    {
                        SyncStatusHelper oldHelper = SyncStatusHelper.GetFromStatus(_oldStatus);
                        foreach (Control c in oldHelper.InfControls)
                            c.Visible = false;
                    }

                    SyncStatusHelper newHelper = SyncStatusHelper.GetFromStatus(SI.Status);
                    foreach (Control c in newHelper.InfControls)
                        c.Visible = true;

                    _oldStatus = SI.Status;
                }

                _generalRoundCount = _generalSuspendRounds;
            }
            else
                _generalRoundCount--;

            if (SI.Status == SyncStatus.FetchingElements)
            {
                label_fetchFD_filesFound.Text = SI.FilesFound.ToString();
                label_fetchFD_foldersFound.Text = SI.DirsFound.ToString();
            }
            else if(SI.Status == SyncStatus.DetectingChanges)
            {
                label_detectCh_changesDetected.Text = (SI.ChangedFilesFound + SI.ChangedDirsFound).ToString();
                label_detectCh_FDDone.Text = (SI.DetectedFilesCount + SI.DetectedDirsCount).ToString();

                label_detectCh_filesToCopy.Text = SI.ChangedFilesToCopyFound.ToString();
                label_detectCh_filesToRemove.Text = SI.ChangedFilesToRemoveFound.ToString();
                label_detectCh_foldersToCreate.Text = SI.ChangedDirsToCreateFound.ToString();
                label_detectCh_foldersToRemove.Text = SI.ChangedDirsToRemoveFound.ToString();
            }
            else if (SI.Status == SyncStatus.CreatingFolders)
            {
                label_crDirs_dirsCreated.Text = $"{SI.DirChangesApplied} of {SI.DirsFound}";
            }
            else if (SI.Status == SyncStatus.ApplyingFileChanges)
            {
                label_applyCh_speed_current.Text = $"{SI.ActSpeed:0.00} Mbit/s";
                label_applyCh_speed_average.Text = $"{SI.AverageSpeed:0.00} Mbit/s";

                label_applyCh_copiedFilesCount.Text = $"{ SI.CopiedFiles:#,#} of {SI.ChangedFilesToCopyFound:#,#}";
                label_applyCh_copiedFilesSize.Text = $"{SI.FileSizeCopied / (1024.0 * 1024.0):#,#0.00} of " +
                        $"{SI.TotalFileSizeToCopy / (1024.0 * 1024.0):#,#0.00}MB";

                label_applyCh_removedFilesCount.Text = $"{ SI.RemovedFiles:#,#} of {SI.ChangedFilesToCopyFound:#,#}";
                label_applyCh_removedFilesSize.Text = $"{SI.FileSizeRemoved / (1024.0 * 1024.0):#,#0.00} of " +
                        $"{SI.TotalFileSizeToRemove / (1024.0 * 1024.0):#,#0.00}MB";
            }
            else if (SI.Status == SyncStatus.RemoveDirs)
            {
                label_remDirs_foldersRemoved.Text = $"{SI.RemovedDirs} of {SI.ChangedDirsToRemoveFound}";
            }
        }

        public void OnSyncElementStatusChanged(SyncElementInfo sei)
        {
            while (_statusChangedEventsAToken || _statusChangedEvents.Count > 1000)
                Thread.Sleep(1);
            _statusChangedEventsAToken = true;
            _statusChangedEvents.Add(new StatusChangedEvent(sei, sei.SyncStatus));
            _statusChangedEventsAToken = false;
        }

        List<StatusChangedEvent> tempSCE;
        public async void StartStatusChangedEventsCheckingAsync()
        {
            if (_statusChangedEventsCheckingRunning)
                return;

            _statusChangedEventsCheckingRunning = true;

            await Task.Run(async () =>
            {
                while (_l.IsRunning)
                {
                    //update tree
                    while (_statusChangedEventsAToken)
                        await Task.Delay(1);
                    _statusChangedEventsAToken = true;

                    tempSCE = new List<StatusChangedEvent>(_statusChangedEvents);
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
            UpdateSyncInfo(true);
        }

        public void ProcessStatusChangedEventAsync(StatusChangedEvent sce)
        {
            SyncElementInfo sei = sce.SyncElementInfo;
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sce.CreateStatus)
            {
                case SyncElementStatus.ElementFound:
                    //update treeview
                    getTreeNode(sei.ElementInfo, true, false, true);
                    break;
                case SyncElementStatus.ChangeDetectingStarted:
                    break;
                case SyncElementStatus.NoChangeFound:
                    break;
                case SyncElementStatus.ChangeFound:
                    TreeNode tn1 = getTreeNode(sei.ElementInfo, true, true, true);
                    break;
                case SyncElementStatus.ChangeApplied:
                    TreeNode tn2 = getTreeNode(sei.ElementInfo, true, true, true);
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
                        case ConflictType.Unknown:
                            conflictType = "Unknown";
                            break;
                    }

                    Invoke(new Action(() => 
                        AddLogLine($"Conflict ({conflictType}) at {elementType}: {sei.ConflictInfo.GetAbsolutePath()}")
                    ));
                    
                    TreeNode tn3 = getTreeNode(sei.ElementInfo, true, true, true);

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
        private void BuildTreeRecursively(TreeNodeCollection nodes, DirTree dirTree)
        {
            foreach (MyFileInfo file in dirTree.Files)
            {
                nodes.Add(GetTreeNodeFromElement(file));
            }

            foreach (DirTree dir in dirTree.Dirs)
            {
                TreeNode tn = GetTreeNodeFromElement(dir.Info);
                nodes.Add(tn);
                BuildTreeRecursively(tn.Nodes, dir);
            }
        }

        private TreeNode GetTreeNodeFromElement(MyElementInfo ei)
        {
            SyncElementStatusHelper helper = SyncElementStatusHelper.GetFromSES(ei.SyncElementInfo != null ? ei.SyncElementInfo.SyncStatus : 0);

            TreeNode tn = new TreeNode(ei.Name);

            if (ei.GetType() == typeof(MyFileInfo))
            {
                tn.ImageIndex = helper.FileImageIndex;
                tn.SelectedImageIndex = helper.FileImageIndex;
            }
            else
            {
                tn.ImageIndex = helper.FolderImageIndex;
                tn.SelectedImageIndex = helper.FolderImageIndex;
            }

            tn.Name = ei.Name;

            return tn;
        }

        /// <summary>
        /// get TreeNode of treeView1 from ElementInfo
        /// </summary>
        /// <param name="ei">ElementInfo</param>
        /// <param name="create">if the tree nodes should be created if they do not exist</param>
        /// <returns>null if the required tree nodes do not exist (only possible if create is disabled)</returns>
        private TreeNode getTreeNode(MyElementInfo ei, bool create, bool update, bool invoke)
        {
            TreeNodeCollection tnc = treeView1.Nodes;
            TreeNode treeNode;
            for (int i = 0; i < ei.TreePath.Count; i++)
            {
                treeNode = tnc[ei.TreePath[i].Info.Name];
                if (treeNode == null)
                {
                    if (!create) return null;

                    //create tree node
                    treeNode = GetTreeNodeFromElement(ei.TreePath[i].Info);
                    Action a = new Action(() => tnc.Add(treeNode));
                    if (invoke) treeView1.Invoke(a);
                    else a();
                }
                if (update)
                {
                    UpdateTreeNode(treeNode, ei.TreePath[i].Info, ei.SyncElementInfo.SyncStatus, invoke);
                }

                tnc = treeNode.Nodes;
            }

            TreeNode resultNode = tnc[ei.Name];
            if (resultNode == null)
            {
                resultNode = GetTreeNodeFromElement(ei);

                Action a = new Action(() => tnc.Add(resultNode));
                if (invoke) treeView1.Invoke(a);
                else a();
            }
            if (update)
            {
                UpdateTreeNode(resultNode, ei, null, invoke);
            }

            return resultNode;
        }

        /// <summary>
        /// update tree node in treeview1
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="ei"></param>
        /// <param name="childStatus"></param>
        private void UpdateTreeNode(TreeNode tn, MyElementInfo ei, SyncElementStatus? childStatus, bool invoke)
        {
            if (ei.SyncElementInfo != null)
            {
                SyncElementStatusHelper helper = SyncElementStatusHelper.GetFromSES(ei.SyncElementInfo.SyncStatus);
                tn.ForeColor = helper.TextColor;
            }
            if (childStatus != null)
            {
                SyncElementStatusHelper childHelper = SyncElementStatusHelper.GetFromSES(childStatus.Value);
                if (ei.GetType() == typeof(MyDirInfo))
                {
                    if (childHelper.FolderImageIndex > tn.ImageIndex)
                    {
                        Action action = new Action(() =>
                        {
                            tn.ImageIndex = childHelper.FolderImageIndex;
                            tn.SelectedImageIndex = childHelper.FolderImageIndex;
                        });

                        if (invoke) treeView1.Invoke(action);
                        else action();
                    }
                }
            }
        }

        private IEnumerable<TreeNode> NextParentNode(TreeNode tn)
        {
            TreeNode node = tn;
            while (node.Parent != null)
                yield return node = node.Parent;
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
                _l.Sync();
                listBox_log.Items.Clear();
                treeView1.Nodes.Clear();
                SI.SetListener(this);
                StartUpdatingSyncInfo();
                _statusChangedEvents = new List<StatusChangedEvent>();
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
        #endregion

        #region debug
        DateTime _deubgMT;
        private void MTS()
        {
            _deubgMT = DateTime.Now;
        }
        private double MTE()
        {
            return (DateTime.Now - _deubgMT).TotalMilliseconds;
        }
        private double MTEP()
        {
            double span = (DateTime.Now - _deubgMT).TotalMilliseconds;
            Console.WriteLine($"{span}");
            return span;
        }
        #endregion

        private class SyncStatusHelper
        {
            private static SyncStatusHelper[] helpers;

            public SyncStatus Status { get; }
            public List<Control> InfControls { get; }

            /// <summary>
            /// create SyncStatusHelper
            /// </summary>
            /// <param name="status"></param>
            /// <param name="infControls">controls that should be displayed when the associated Status is active</param>
            private SyncStatusHelper(SyncStatus status, List<Control> infControls)
            {
                Status = status;
                InfControls = infControls;
            }

            public static void Init(SyncDetailInfoForm2 form)
            {
                helpers = new SyncStatusHelper[7];

                helpers[0] = new SyncStatusHelper(SyncStatus.FetchingElements, new List<Control>()
                    {form.panel_fetchFD});
                helpers[1] = new SyncStatusHelper(SyncStatus.DetectingChanges, new List<Control>()
                    {form.panel_detectCh, form.panel_detectCh_chTypes});
                helpers[2] = new SyncStatusHelper(SyncStatus.CreatingFolders, new List<Control>()
                    {form.panel_crDirs });
                helpers[3] = new SyncStatusHelper(SyncStatus.ApplyingFileChanges, new List<Control>()
                    {form.panel_applyCh_speed, form.panel_applyCh_syncedFiles});
                helpers[4] = new SyncStatusHelper(SyncStatus.RemoveDirs, new List<Control>()
                    {form.panel_remDirs});
                helpers[5] = new SyncStatusHelper(SyncStatus.Finished, new List<Control>()
                { });
                helpers[6] = new SyncStatusHelper(SyncStatus.Aborted, new List<Control>()
                { });
            }

            public static SyncStatusHelper GetFromStatus(SyncStatus status)
            {
                return helpers.First(x => x.Status == status);
            }
        }
    }

    public class SyncElementStatusHelper
    {
        private static readonly SyncElementStatusHelper[] helpers;

        static SyncElementStatusHelper()
        {
            helpers = new SyncElementStatusHelper[6];
            helpers[(int)SyncElementStatus.ElementFound] = new SyncElementStatusHelper(Color.Black, 1, 0);
            helpers[(int)SyncElementStatus.ChangeDetectingStarted] = new SyncElementStatusHelper(Color.Black, 1, 0);
            helpers[(int)SyncElementStatus.NoChangeFound] = new SyncElementStatusHelper(Color.Black, 1, 0);
            helpers[(int)SyncElementStatus.ChangeFound] = new SyncElementStatusHelper(Color.Blue, 2, 0);
            helpers[(int)SyncElementStatus.ChangeApplied] = new SyncElementStatusHelper(Color.Green, 3, 0);
            helpers[(int)SyncElementStatus.Conflicted] = new SyncElementStatusHelper(Color.Red, 4, 0);
        }

        /// <summary>
        /// get SyncElementStatusHelper from SyncElementStatus
        /// </summary>
        public static SyncElementStatusHelper GetFromSES(SyncElementStatus ses)
        {
            return helpers[(int)ses];
        }
        
        public Color TextColor { get; }
        public int FolderImageIndex { get; }
        public int FileImageIndex { get; }

        private SyncElementStatusHelper(Color textColor, int folderImageIndex, int fileImageIndex)
        {
            TextColor = textColor;
            FolderImageIndex = folderImageIndex;
            FileImageIndex = fileImageIndex;
        }
    }

    public class StatusChangedEvent
    {
        public SyncElementInfo SyncElementInfo { get; set; }
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
