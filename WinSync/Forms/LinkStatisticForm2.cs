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
    public partial class LinkStatisticForm2 : Form, ISyncListener
    {
        readonly Link _l;
        bool _initFlag;
        bool updateStatsAsyncRunning;
        MainForm _mainForm;

        bool statusChangedEventsAToken;
        List<StatusChangedEvent> statusChangedEvents = new List<StatusChangedEvent>();

        /// <summary>
        /// create a LinkStatisticsForm that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public LinkStatisticForm2(Link l, MainForm mainForm)
        {
            _l = l;
            _mainForm = mainForm;
            InitializeComponent();

            tabControl_left1.SelectedIndex = 1;
            label_title.Text = _l.Title;
            label_link_folder1.Text = _l.Path1;
            label_link_folder2.Text = _l.Path2;
            label_link_direction.Text = _l.Direction.ToString();

            if (_l.SyncInfo != null)
            {
                //build tree (pause sync while building)
                bool running = _l.IsRunning();
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
                    BuildTreeRecursively(treeView1.Nodes, _l.SyncInfo.DirTree);
                    _l.SyncInfo.SetListener(this);
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
                    if (!updateStatsAsyncRunning && _l.IsRunning())
                    {
                        _l.SyncInfo?.SetListener(this);
                        Invoke(new Action(() => {
                            UpdateStatsAsync();
                        }));
                        StartStatusChangedEventsCheckingAsync();
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
        /// update all synchronisation stats once
        /// </summary>
        public void UpdateStats()
        {
            if (_l.IsRunning())
            {
                //display cancel and pause/continue button
                button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;
                button_pr.Visible = true;
                button_pr.BackgroundImage = _l.SyncInfo.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;

                UpdateProgressInfos();
            }
            else
            {
                button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
                button_pr.Visible = false;

                progressBar.Value = (int)(_l.SyncInfo.SyncProgress * 10);
                label_syst_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
                label_syst_status.Text = _l.SyncInfo.Status.Title;

                UpdateProgressInfos();
            }
        }

        /// <summary>
        /// start updating all synchronisation stats every 100 ms
        /// </summary>
        public async void UpdateStatsAsync()
        {
            updateStatsAsyncRunning = true;
            _initFlag = true;

            //display cancel and pause button
            button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;
            button_pr.Visible = true;

            while (_l.IsRunning())
            {
                button_pr.BackgroundImage = _l.SyncInfo.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
                UpdateProgressInfos();

                await Task.Delay(200);
            }

            updateStatsAsyncRunning = false;

            //after synchronisation
            button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
            button_pr.Visible = false;

            progressBar.Value = (int)(_l.SyncInfo.SyncProgress * 10);
            label_syst_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
            label_syst_status.Text = _l.SyncInfo.Status.Title;

            UpdateProgressInfos();
        }

        /// <summary>
        /// update progress stats in form
        /// </summary>
        public void UpdateProgressInfos()
        {
            label_syst_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
            label_syst_status.Text = _l.SyncInfo.Status.Title;
            panel_header.BackColor = _l.SyncInfo.Status.Color;

            label_syst_syncedFilesCount.Text = $"{ _l.SyncInfo.FileChangesApplied:#,#} of {_l.SyncInfo.ChangedFilesFound:#,#}";
            label_syst_syncedFilesSize.Text = $"{_l.SyncInfo.SizeApplied / (1024.0 * 1024.0):#,#0.00} of " +
                    $"{_l.SyncInfo.TotalSize / (1024.0 * 1024.0):#,#0.00}MB";

            if (_l.SyncInfo.Status == SyncStatus.ApplyingFileChanges || _initFlag)
            {
                progressBar.Value = (int)(_l.SyncInfo.SyncProgress * 10);

                label_syst_speed.Text = $"{_l.SyncInfo.ActSpeed:0.00} Mbit/s";
                label_syst_averageSpeed.Text = $"{_l.SyncInfo.AverageSpeed:0.00} Mbit/s";
            }

            label_syst_totalTime.Text = _l.SyncInfo.TotalTime.ToString(@"hh\:mm\:ss");

            _initFlag = false;
        }

        private void LinkStatisticForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _l.SyncInfo?.RemoveListener(this);
        }

        /// <summary>
        /// start/stop synchronisation on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sync_Click(object sender, EventArgs e)
        {
            if (!_l.IsRunning())
            {
                _l.Sync();
                _l.SyncInfo.SetListener(this);
                listBox_log.Items.Clear();
                treeView1.Nodes.Clear();
                statusChangedEvents = new List<StatusChangedEvent>();
            }
            else
            {
                _l.CancelSync();
                UpdateStats();
            }
        }

        /// <summary>
        /// resume/pause synchronisation on button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_pr_Click(object sender, EventArgs e)
        {
            if (_l.SyncInfo.Paused)
                _l.ResumeSync();
            else
                _l.PauseSync();

            UpdateStats();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkStatisticForm_Resize(object sender, EventArgs e)
        {
        }

        private void listBox_syncInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetData(DataFormats.Text, (string)listBox_log.SelectedItem);
            }
        }

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

            if(ei.GetType() == typeof(MyFileInfo))
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
                    if(!create) return null;

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
            if(resultNode == null)
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
            if(ei.SyncElementInfo != null)
            {
                SyncElementStatusHelper helper = SyncElementStatusHelper.GetFromSES(ei.SyncElementInfo.SyncStatus);
                tn.ForeColor = helper.TextColor;
            }
            if(childStatus != null)
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

        public void OnSyncElementStatusChanged(SyncElementInfo sei)
        {
            while (statusChangedEventsAToken || statusChangedEvents.Count > 1000)
                Thread.Sleep(1);
            statusChangedEventsAToken = true;
            statusChangedEvents.Add(new StatusChangedEvent(sei, sei.SyncStatus));
            statusChangedEventsAToken = false;
        }

        List<StatusChangedEvent> tempSCE;
        public async void StartStatusChangedEventsCheckingAsync()
        {
            await Task.Run(async () =>
            {
                while (_l.IsRunning())
                {
                    //update tree
                    while (statusChangedEventsAToken)
                        await Task.Delay(1);
                    statusChangedEventsAToken = true;

                    tempSCE = new List<StatusChangedEvent>(statusChangedEvents);
                    statusChangedEvents = new List<StatusChangedEvent>();

                    statusChangedEventsAToken = false;

                    foreach (StatusChangedEvent sce in tempSCE.Where(x => x.CreateStatus == x.SyncElementInfo.SyncStatus))
                    {
                        ProcessStatusChangedEventAsync(sce);
                    }
                    //------------------

                    await Task.Delay(1);
                }
            });
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

                    if (isFile)
                    {
                        Console.WriteLine("file change detected:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                    }
                    else
                    {
                        Console.WriteLine("directory change detected:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                    }
                    break;
                case SyncElementStatus.ChangeApplied:
                    TreeNode tn2 = getTreeNode(sei.ElementInfo, true, true, true);

                    if (isFile)
                    {
                        if (sei.SyncExecutionInfo.Remove)
                            Console.WriteLine("file deleted:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                        else
                            Console.WriteLine("file copied:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                    }
                    else
                    {
                        if (sei.SyncExecutionInfo.Remove)
                            Console.WriteLine("directory removed:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                        else
                            Console.WriteLine("Directory created:" + sei.SyncExecutionInfo.AbsoluteDestPath);
                    }
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
    }

    public class SyncElementStatusHelper
    {
        private static readonly SyncElementStatusHelper[] SyncStatuses;

        static SyncElementStatusHelper()
        {
            SyncStatuses = new SyncElementStatusHelper[6];
            SyncStatuses[(int)SyncElementStatus.ElementFound] = new SyncElementStatusHelper(Color.Black, 1, 0);
            SyncStatuses[(int)SyncElementStatus.ChangeDetectingStarted] = new SyncElementStatusHelper(Color.Black, 1, 0);
            SyncStatuses[(int)SyncElementStatus.NoChangeFound] = new SyncElementStatusHelper(Color.Black, 1, 0);
            SyncStatuses[(int)SyncElementStatus.ChangeFound] = new SyncElementStatusHelper(Color.Blue, 2, 0);
            SyncStatuses[(int)SyncElementStatus.ChangeApplied] = new SyncElementStatusHelper(Color.Green, 3, 0);
            SyncStatuses[(int)SyncElementStatus.Conflicted] = new SyncElementStatusHelper(Color.Red, 4, 0);
        }

        /// <summary>
        /// get SyncElementStatusHelper from SyncElementStatus
        /// </summary>
        public static SyncElementStatusHelper GetFromSES(SyncElementStatus ses)
        {
            return SyncStatuses[(int)ses];
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
