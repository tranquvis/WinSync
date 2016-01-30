using WinSync.Data;
using WinSync.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace WinSync.Forms
{
    public partial class LinkStatisticForm : Form, ISyncListener
    {
        readonly Link _l;
        bool _initFlag;
        bool updateStatsAsyncRunning;
        MainForm _mainForm;

        /// <summary>
        /// create a LinkStatisticsForm that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public LinkStatisticForm(Link l, MainForm mainForm)
        {
            //if (l.SyncInfo == null) Close();
            
            _l = l;
            _mainForm = mainForm;
            InitializeComponent();

            label_title.Text = _l.Title;
            label_folder1.Text = _l.Path1;
            label_folder2.Text = _l.Path2;
            label_direction.Text = _l.Direction.ToString();

            if (_l.SyncInfo != null)
            {
                _l.SyncInfo.Listener = this;
                UpdateStatsAsync();
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
                            if (_l.SyncInfo != null && _l.SyncInfo.Listener == null)
                                _l.SyncInfo.Listener = this;
                            UpdateStatsAsync();
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

                progressBar.Value = (int)(_l.SyncInfo.Progress * 10);
                label_detail_progress.Text = $"{_l.SyncInfo.Progress:0.00}%";
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

            progressBar.Value = (int)(_l.SyncInfo.Progress * 10);
            label_detail_progress.Text = $"{_l.SyncInfo.Progress:0.00}%";
            label_detail_status.Text = _l.SyncInfo.State.Title;

            UpdateProgressInfos();
        }

        /// <summary>
        /// update progress stats in form
        /// </summary>
        public void UpdateProgressInfos()
        {
            label_detail_progress.Text = $"{_l.SyncInfo.Progress:0.00}%";
            label_detail_status.Text = _l.SyncInfo.State.Title;
            panel_header.BackColor = _l.SyncInfo.State.Color;
            
            label_syncedFilesCount.Text = $"{ _l.SyncInfo.FilesDone:#,#} of {_l.SyncInfo.FilesFound:#,#}";
            label_syncedFilesSize.Text = $"{_l.SyncInfo.SizeApplied / (1024.0 * 1024.0):#,#0.00} of " +
                    $"{_l.SyncInfo.TotalSize / (1024.0 * 1024.0):#,#0.00}MB";

            if (_l.SyncInfo.State == SyncState.ApplyingFileChanges || _initFlag)
            {
                progressBar.Value = (int)(_l.SyncInfo.Progress * 10);
                
                label_speed.Text = $"{_l.SyncInfo.ActSpeed:0.00} Mbit/s";
                label_averageSpeed.Text = $"{_l.SyncInfo.AverageSpeed:0.00} Mbit/s";
            }

            label_totalTime.Text = _l.SyncInfo.TotalTime.ToString(@"hh\:mm\:ss");

            _initFlag = false;
        }

        /// <summary>
        /// add line to process listBox
        /// </summary>
        /// <param name="text">line</param>
        void AddProcessLine(string text)
        {
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
        
        private void LinkStatisticForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_l.SyncInfo != null)
                _l.SyncInfo.Listener = null;
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
                _mainForm.Invoke(new Action(() => { _l.Sync(); }));
                _l.SyncInfo.Listener = this;
                listBox_syncInfo.Items.Clear();
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
        /// add process line on start detecting file
        /// </summary>
        /// <param name="path">file path</param>
        public void OnDetectingFileStarted(MyFileInfo fi)
        {
            AddProcessLine("Start detecting file:" + fi.FullPath);
        }

        /// <summary>
        /// add process line on file change detected
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileChangeDetected(MyFileInfo fi)
        {
            TreeNode tn = getTreeNode(fi);
            tn.ForeColor = Color.Blue;

            AddProcessLine("File change detected:" + fi.FullPath);
        }

        /// <summary>
        /// add process line on file synced
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileSynced(MyFileInfo fi)
        {
            TreeNode tn = getTreeNode(fi);
            tn.ForeColor = Color.Green;

            if (fi.SyncInfo.Remove)
                AddProcessLine("File deleted:" + fi.FullPath);
            else
                AddProcessLine("File copied:" + fi.FullPath);
        }

        public void OnDirSynced(MyDirInfo di)
        {
            TreeNode tn = getTreeNode(di);
            tn.ForeColor = Color.Green;

            if (di.SyncInfo.Remove)
                AddProcessLine("Directory removed:" + di.FullPath);
            else
                AddProcessLine("Directory created:" + di.FullPath);
        }

        /// <summary>
        /// add process line on file conflicted
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileConflicted(MyFileInfo fi)
        {
            switch (fi.SyncInfo.ConflictInfo.Type)
            {
                case ConflictType.IO:
                    AddProcessLine("IO conflict at file: " + fi.FullPath);
                    break;
                case ConflictType.UA:
                    AddProcessLine("Access denied to: " + fi.FullPath);
                    break;
                case ConflictType.Unknown:
                    AddProcessLine("Unknown error at file: " + fi.FullPath);
                    break;
            }
        }

        /// <summary>
        /// add process line on dir conflicted
        /// </summary>
        /// <param name="sdi"></param>
        public void OnDirConflicted(MyDirInfo di)
        {
            switch (di.SyncInfo.ConflictInfo.Type)
            {
                case ConflictType.IO:
                    AddProcessLine("IO conflict at folder: " + di.FullPath);
                    break;
                case ConflictType.UA:
                    AddProcessLine("Access denied to: " + di.FullPath);
                    break;
                case ConflictType.Unknown:
                    AddProcessLine("Unknown error at folder: " + di.FullPath);
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
            switch(message.Type)
            {
                case LogType.ERROR:
                    text = "Error:";
                    break;
                case LogType.INFO:
                    text = "Info:";
                    break;
            }
            text += message.Message;
            AddProcessLine(text);
        }

        /// <summary>
        /// hide or show shadows when resizing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkStatisticForm_Resize(object sender, EventArgs e)
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

        public void OnFileFound(MyFileInfo fi)
        {
            Invoke(new Action(() =>
            {
                TreeNodeCollection tnc = treeView1.Nodes;
                TreeNode treeNode;
                for (int i = 0; i < fi.TreePath.Count; i++)
                {
                    treeNode = tnc[fi.TreePath[i].Info.Name];
                    if (treeNode == null)
                        return;

                    tnc = treeNode.Nodes;
                }

                TreeNode tn = new TreeNode(fi.Name);
                tn.ImageIndex = 2;
                tn.SelectedImageIndex = 2;
                tn.Name = fi.Name;
                tnc.Add(tn);
            }));
        }

        public void OnDirFound(MyDirInfo di)
        {
            Invoke(new Action(() =>
            {
                TreeNodeCollection tnc = treeView1.Nodes;
                TreeNode treeNode;
                for (int i = 0; i < di.TreePath.Count; i++)
                {
                    treeNode = tnc[di.TreePath[i].Info.Name];
                    if (treeNode == null)
                        return;

                    tnc = treeNode.Nodes;
                }

                TreeNode tn = new TreeNode(di.Name);
                tn.ImageIndex = 0;
                tn.SelectedImageIndex = 0;
                tn.StateImageIndex = 1;
                tn.Name = di.Name;
                tnc.Add(tn);
            }));
        }

        public TreeNode getTreeNode(MyFileInfo fi)
        {
            TreeNodeCollection tnc = treeView1.Nodes;
            TreeNode treeNode;
            for (int i = 0; i < fi.TreePath.Count; i++)
            {
                treeNode = tnc[fi.TreePath[i].Info.Name];
                if (treeNode == null)
                    return null;

                tnc = treeNode.Nodes;
            }

            return tnc[fi.Name];
        }
        public TreeNode getTreeNode(MyDirInfo di)
        {
            TreeNodeCollection tnc = treeView1.Nodes;
            TreeNode treeNode;
            for (int i = 0; i < di.TreePath.Count; i++)
            {
                treeNode = tnc[di.TreePath[i].Info.Name];
                if (treeNode == null)
                    return null;

                tnc = treeNode.Nodes;
            }

            return tnc[di.Name];
        }
    }
}
