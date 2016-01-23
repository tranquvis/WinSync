using WinSync.Data;
using WinSync.Service;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Forms
{
    public partial class LinkStatisticForm : Form, ISyncListener
    {
        readonly Link _l;
        bool _initFlag;
        bool updateStatsAsyncRunning;

        /// <summary>
        /// create a LinkStatisticsForm that displays all details of a synchronisation process
        /// </summary>
        /// <param name="l">link that contains the synchronisation information</param>
        public LinkStatisticForm(Link l)
        {
            if (l.SyncInfo == null)
                Close();
            
            _l = l;
            InitializeComponent();

            label_title.Text = _l.Title;
            label_folder1.Text = _l.Path1;
            label_folder2.Text = _l.Path2;
            label_direction.Text = _l.Direction.ToString();

            _l.SyncInfo.Listener = this;

            UpdateStatsAsync();
            StartUpdateRoutine();
        }
        
        public void StartUpdateRoutine()
        {
            Task.Run(async () =>
            {
                while (!IsDisposed)
                {
                    if (!updateStatsAsyncRunning && _l.IsRunning())
                        Invoke(new Action(() => { UpdateStatsAsync(); }));

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

        /// <summary>
        /// on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkStatisticForm_Load(object sender, EventArgs e)
        {
            label_title.Text = _l.Title;
            label_folder1.Text = _l.Path1;
            label_folder2.Text = _l.Path2;
            label_direction.Text = _l.Direction.ToString();

            _l.SyncInfo.Listener = this;
            listBox_syncInfo.Items.Add("...");
            UpdateStatsAsync();
        }
        
        private void LinkStatisticForm_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                _l.Sync();
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
        public void OnDetectingFileStarted(string path)
        {
            AddProcessLine("Start detecting file:" + path);
        }

        /// <summary>
        /// add process line on file change detected
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileChangeDetected(SyncFileInfo sfi)
        {
            AddProcessLine("File change detected:" + sfi.Path);
        }

        /// <summary>
        /// add process line on file synced
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileSynced(SyncFileInfo sfi)
        {
            if(sfi.Remove)
                AddProcessLine("File deleted:" + sfi.Path);
            else
                AddProcessLine("File copied:" + sfi.Path);
        }

        public void OnDirSynced(SyncDirInfo sdi)
        {
            if (sdi.Remove)
                AddProcessLine("Directory removed:" + sdi.Path);
            else
                AddProcessLine("Directory created:" + sdi.Path);
        }

        /// <summary>
        /// add process line on file conflicted
        /// </summary>
        /// <param name="sfi"></param>
        public void OnFileConflicted(SyncFileInfo sfi)
        {
            switch (sfi.ConflictInfo.Type)
            {
                case ConflictType.IO:
                    AddProcessLine("IO conflict at file: " + sfi.Path);
                    break;
                case ConflictType.UA:
                    AddProcessLine("Access denied to: " + sfi.Path);
                    break;
                case ConflictType.Unknown:
                    AddProcessLine("Unknown error at file: " + sfi.Path);
                    break;
            }
        }

        /// <summary>
        /// add process line on dir conflicted
        /// </summary>
        /// <param name="sdi"></param>
        public void OnDirConflicted(SyncDirInfo sdi)
        {
            switch (sdi.ConflictInfo.Type)
            {
                case ConflictType.IO:
                    AddProcessLine("IO conflict at folder: " + sdi.Path);
                    break;
                case ConflictType.UA:
                    AddProcessLine("Access denied to: " + sdi.Path);
                    break;
                case ConflictType.Unknown:
                    AddProcessLine("Unknown error at folder: " + sdi.Path);
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
    }
}
