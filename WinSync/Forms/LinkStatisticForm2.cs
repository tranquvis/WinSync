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

        bool stateChangedEventsAToken;
        List<StateChangedEvent> stateChangedEvents = new List<StateChangedEvent>();

        /// <summary>
        /// create a LinkStatisticsForm that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public LinkStatisticForm2(Link l, MainForm mainForm)
        {
            _l = l;
            _mainForm = mainForm;
            InitializeComponent();

            label_title.Text = _l.Title;
            label_folder1.Text = _l.Path1;
            label_folder2.Text = _l.Path2;
            label_direction.Text = _l.Direction.ToString();

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
                        StartStateChangedEventsCheckingAsync();
                    }

                    Invoke(new Action(() =>
                    {
                        label_runningTasks.Text = (_l.SyncTask == null ? 0 : _l.SyncTask.TasksRunning()).ToString();
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
                label_detail_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
                label_detail_status.Text = _l.SyncInfo.State.Title;

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
            label_detail_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
            label_detail_status.Text = _l.SyncInfo.State.Title;

            UpdateProgressInfos();
        }

        /// <summary>
        /// update progress stats in form
        /// </summary>
        public void UpdateProgressInfos()
        {
            label_detail_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
            label_detail_status.Text = _l.SyncInfo.State.Title;
            panel_header.BackColor = _l.SyncInfo.State.Color;

            label_syncedFilesCount.Text = $"{ _l.SyncInfo.FileChangesApplied:#,#} of {_l.SyncInfo.ChangedFilesFound:#,#}";
            label_syncedFilesSize.Text = $"{_l.SyncInfo.SizeApplied / (1024.0 * 1024.0):#,#0.00} of " +
                    $"{_l.SyncInfo.TotalSize / (1024.0 * 1024.0):#,#0.00}MB";

            if (_l.SyncInfo.State == SyncState.ApplyingFileChanges || _initFlag)
            {
                progressBar.Value = (int)(_l.SyncInfo.SyncProgress * 10);

                label_speed.Text = $"{_l.SyncInfo.ActSpeed:0.00} Mbit/s";
                label_averageSpeed.Text = $"{_l.SyncInfo.AverageSpeed:0.00} Mbit/s";
            }

            label_totalTime.Text = _l.SyncInfo.TotalTime.ToString(@"hh\:mm\:ss");

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
                listBox_syncInfo.Items.Clear();
                treeView1.Nodes.Clear();
                stateChangedEvents = new List<StateChangedEvent>();
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
        /// hide or show shadows when resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkStatisticForm_Resize(object sender, EventArgs e)
        {
            if (panel2.Height < 5)
            {
                shadow_top2.Visible = false;
                shadow_bottom2.Visible = false;
            }
            else
            {
                shadow_top2.Visible = true;
                shadow_bottom2.Visible = true;
            }

            if (panel1.Height < 5)
            {
                shadow_top1.Visible = false;
                shadow_bottom1.Visible = false;
            }
            else
            {
                shadow_top1.Visible = true;
                shadow_bottom1.Visible = true;
            }
        }

        private void listBox_syncInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetData(DataFormats.Text, (string)listBox_syncInfo.SelectedItem);
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
            SyncElementStateHelper helper = SyncElementStateHelper.GetFromSES(ei.SyncElementInfo != null ? ei.SyncElementInfo.SyncState : 0);

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
                    UpdateTreeNode(treeNode, ei.TreePath[i].Info, ei.SyncElementInfo.SyncState, invoke);
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
        /// <param name="childState"></param>
        private void UpdateTreeNode(TreeNode tn, MyElementInfo ei, SyncElementState? childState, bool invoke)
        {
            if(ei.SyncElementInfo != null)
            {
                SyncElementStateHelper helper = SyncElementStateHelper.GetFromSES(ei.SyncElementInfo.SyncState);
                tn.ForeColor = helper.TextColor;
            }
            if(childState != null)
            {
                SyncElementStateHelper childHelper = SyncElementStateHelper.GetFromSES(childState.Value);
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

        public void OnSyncElementStateChanged(SyncElementInfo sei)
        {
            while (stateChangedEventsAToken || stateChangedEvents.Count > 1000)
                Thread.Sleep(1);
            stateChangedEventsAToken = true;
            stateChangedEvents.Add(new StateChangedEvent(sei, sei.SyncState));
            stateChangedEventsAToken = false;
        }

        List<StateChangedEvent> tempSCE;
        public async void StartStateChangedEventsCheckingAsync()
        {
            await Task.Run(async () =>
            {
                while (_l.IsRunning())
                {
                    //update tree
                    while (stateChangedEventsAToken)
                        await Task.Delay(1);
                    stateChangedEventsAToken = true;

                    tempSCE = new List<StateChangedEvent>(stateChangedEvents);
                    stateChangedEvents = new List<StateChangedEvent>();

                    stateChangedEventsAToken = false;

                    foreach (StateChangedEvent sce in tempSCE.Where(x => x.CreateState == x.SyncElementInfo.SyncState))
                    {
                        ProcessStateChangedEventAsync(sce);
                    }
                    //------------------

                    await Task.Delay(1);
                }
            });
        }

        public void ProcessStateChangedEventAsync(StateChangedEvent sce)
        {
            SyncElementInfo sei = sce.SyncElementInfo;

            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sce.CreateState)
            {
                case SyncElementState.ElementFound:

                    //update treeview
                    getTreeNode(sei.ElementInfo, true, false, true);

                    break;
                case SyncElementState.ChangeDetectingStarted:
                    break;
                case SyncElementState.NoChangeFound:
                    break;
                case SyncElementState.ChangeFound:
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
                case SyncElementState.ChangeApplied:
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
                case SyncElementState.Conflicted:
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
                listBox_syncInfo.Items.Insert(0, text);
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

    public class SyncElementStateHelper
    {
        private static readonly SyncElementStateHelper[] SyncStates;

        static SyncElementStateHelper()
        {
            SyncStates = new SyncElementStateHelper[6];
            SyncStates[(int)SyncElementState.ElementFound] = new SyncElementStateHelper(Color.Black, 1, 0);
            SyncStates[(int)SyncElementState.ChangeDetectingStarted] = new SyncElementStateHelper(Color.Black, 1, 0);
            SyncStates[(int)SyncElementState.NoChangeFound] = new SyncElementStateHelper(Color.Black, 1, 0);
            SyncStates[(int)SyncElementState.ChangeFound] = new SyncElementStateHelper(Color.Blue, 2, 0);
            SyncStates[(int)SyncElementState.ChangeApplied] = new SyncElementStateHelper(Color.Green, 3, 0);
            SyncStates[(int)SyncElementState.Conflicted] = new SyncElementStateHelper(Color.Red, 4, 0);
        }

        /// <summary>
        /// get SyncElementStateHelper from SyncElementState
        /// </summary>
        public static SyncElementStateHelper GetFromSES(SyncElementState ses)
        {
            return SyncStates[(int)ses];
        }
        
        public Color TextColor { get; }
        public int FolderImageIndex { get; }
        public int FileImageIndex { get; }

        private SyncElementStateHelper(Color textColor, int folderImageIndex, int fileImageIndex)
        {
            TextColor = textColor;
            FolderImageIndex = folderImageIndex;
            FileImageIndex = fileImageIndex;
        }
    }

    public class StateChangedEvent
    {
        public SyncElementInfo SyncElementInfo { get; set; }
        public SyncElementState CreateState { get; set; }

        /// <summary>
        /// create StateChangedEvent
        /// </summary>
        /// <param name="syncElementInfo">reference to the element, which state changed</param>
        /// <param name="createState">the state of the element, which should be preserved</param>
        public StateChangedEvent(SyncElementInfo syncElementInfo, SyncElementState createState)
        {
            SyncElementInfo = syncElementInfo;
            CreateState = createState;
        }
    }
}
