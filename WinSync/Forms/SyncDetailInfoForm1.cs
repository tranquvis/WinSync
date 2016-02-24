using WinSync.Data;
using WinSync.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace WinSync.Forms
{
    public partial class SyncDetailInfoForm1 : Form, ISyncListener
    {
        readonly Link _l;
        bool _initFlag;
        bool updateStatsAsyncRunning;
        MainForm _mainForm;

        /// <summary>
        /// create SyncDetailInfoForm1 that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public SyncDetailInfoForm1(Link l, MainForm mainForm)
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
                if(_l.SyncTask != null)
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
                        Invoke(new Action(() => {
                            _l.SyncInfo?.SetListener(this);
                            UpdateStatsAsync();
                        }));

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
            if(_l.IsRunning())
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
                label_detail_status.Text = _l.SyncInfo.Status.Title;

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
            label_detail_status.Text = _l.SyncInfo.Status.Title;

            UpdateProgressInfos();
        }

        /// <summary>
        /// update progress stats in form
        /// </summary>
        public void UpdateProgressInfos()
        {
            label_detail_progress.Text = $"{_l.SyncInfo.SyncProgress:0.00}%";
            label_detail_status.Text = _l.SyncInfo.Status.Title;
            panel_header.BackColor = _l.SyncInfo.Status.Color;
            
            label_syncedFilesCount.Text = $"{ _l.SyncInfo.FileChangesApplied:#,#} of {_l.SyncInfo.ChangedFilesFound:#,#}";
            label_syncedFilesSize.Text = $"{_l.SyncInfo.SizeApplied / (1024.0 * 1024.0):#,#0.00} of " +
                    $"{_l.SyncInfo.TotalFileSize / (1024.0 * 1024.0):#,#0.00}MB";

            if (_l.SyncInfo.Status == SyncStatus.ApplyingFileChanges || _initFlag)
            {
                progressBar.Value = (int)(_l.SyncInfo.SyncProgress * 10);
                
                label_speed.Text = $"{_l.SyncInfo.ActSpeed:0.00} Mbit/s";
                label_averageSpeed.Text = $"{_l.SyncInfo.AverageSpeed:0.00} Mbit/s";
            }

            label_totalTime.Text = _l.SyncInfo.TotalTime.ToString(@"hh\:mm\:ss");

            _initFlag = false;
        }
        
        private void SyncDetailInfoForm1_FormClosing(object sender, FormClosingEventArgs e)
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
                UpdateStatsAsync();
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
        private void SyncDetailInfoForm1_Resize(object sender, EventArgs e)
        {
            if(panel2.Height < 5)
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
            foreach(MyFileInfo file in dirTree.Files)
            {
                AddFileTreeNode(nodes, file);
            }

            foreach (DirTree dir in dirTree.Dirs)
            {
                TreeNode tn = AddDirTreeNode(nodes, dir.Info);
                BuildTreeRecursively(tn.Nodes, dir);
            }
        }

        private TreeNode AddFileTreeNode(TreeNodeCollection nodes, MyFileInfo file)
        {
            TreeNode tn = new TreeNode(file.Name);
            tn.ImageIndex = 2;
            tn.SelectedImageIndex = 2;
            tn.Name = file.Name;
            nodes.Add(tn);
            return tn;
        }

        private TreeNode AddDirTreeNode(TreeNodeCollection nodes, MyDirInfo dir)
        {
            TreeNode tn = new TreeNode(dir.Name);
            tn.ImageIndex = 0;
            tn.SelectedImageIndex = 1;
            tn.Name = dir.Name;
            nodes.Add(tn);
            return tn;
        }

        private TreeNode getTreeNode(MyElementInfo ei)
        {
            TreeNodeCollection tnc = treeView1.Nodes;
            TreeNode treeNode;
            for (int i = 0; i < ei.TreePath.Count; i++)
            {
                treeNode = tnc[ei.TreePath[i].Info.Name];
                if (treeNode == null)
                    return null;

                tnc = treeNode.Nodes;
            }

            return tnc[ei.Name];
        }

        private IEnumerable<TreeNode> NextParentNode(TreeNode tn)
        {
            TreeNode node = tn;
            while(node.Parent != null)
                yield return node = node.Parent;
        }

        public void OnSyncElementStatusChanged(SyncElementInfo sei)
        {
            bool isFile = typeof(SyncFileInfo) == sei.GetType();

            switch (sei.SyncStatus)
            {
                case SyncElementStatus.ElementFound:
                    
                    //update treeview
                    TreeNodeCollection tnc = treeView1.Nodes;
                    TreeNode treeNode;
                    for (int i = 0; i < sei.ElementInfo.TreePath.Count; i++)
                    {
                        treeNode = tnc[sei.ElementInfo.TreePath[i].Info.Name];
                        if (treeNode == null)
                            return;

                        tnc = treeNode.Nodes;
                    }
                    treeView1.Invoke(new Action(() =>
                    {
                        if (isFile)
                            AddFileTreeNode(tnc, (MyFileInfo)sei.ElementInfo);
                        else
                            AddDirTreeNode(tnc, (MyDirInfo)sei.ElementInfo);
                    }));
                        
                    break;
                case SyncElementStatus.ChangeDetectingStarted:
                    Console.WriteLine("Start detecting file:" + sei.ElementInfo.FullPath);
                    break;
                case SyncElementStatus.NoChangeFound:

                    break;
                case SyncElementStatus.ChangeFound:
                    TreeNode tn1 = getTreeNode(sei.ElementInfo);
                    if (tn1 == null)
                        break;
                    tn1.ForeColor = Color.Blue;
                    
                    if (tn1 != null)
                        treeView1.Invoke( new Action(() => {
                            foreach (TreeNode ptn in NextParentNode(tn1))
                                ptn.Text = ptn.Text + " - #cf";
                        }));
                    
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
                    TreeNode tn2 = getTreeNode(sei.ElementInfo);
                    if (tn2 != null)
                        treeView1.Invoke(new Action(() => {
                            foreach (TreeNode ptn in NextParentNode(tn2))
                                ptn.Text = ptn.Text + " - #ca";
                        }));

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

                    AddLogLine($"Conflict ({conflictType}) at {elementType}: {sei.ConflictInfo.GetAbsolutePath()}");
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
            AddLogLine(text);
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
                listBox_syncInfo.Invoke(new Action(() =>
                        listBox_syncInfo.Items.Insert(0, text)
                    ));
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
}
