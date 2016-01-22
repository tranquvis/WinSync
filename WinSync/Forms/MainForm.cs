using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSync.Controls;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Forms
{
    public partial class MainForm : Form
    {
        private readonly List<LinkStatisticForm> _statForms = new List<LinkStatisticForm>();

        /// <summary>
        /// Caption bar height
        /// </summary>
        private const int CaptionHeight = 40;

        private readonly List<LinkLine> _linkLines = new List<LinkLine>(); 
        private List<Link> _links;

        /// <summary>
        /// store links that have already finished to make sure that finish operations are not done multiple times
        /// </summary>
        private readonly List<Link> _syncFinsihedFlags = new List<Link>();

        private int _currentPos = -1;

        public Link ActLink => _links.Count > 0 && _currentPos > -1 ? _links[_currentPos] : null;
        public LinkLine ActLinkLine => _linkLines.Count > 0 && _currentPos > -1 ? _linkLines[_currentPos] : null;

        public MainForm()
        {
            InitializeComponent();
            FillData();
            UpdateDetailStatsAsync();
            UpdateStatsAsync();
        }

        public void SetLinks(List<Link> links)
        {
            _links = new List<Link>();
            foreach (Link link in links)
            {
                AddLink(link);
            }
        }

        public void AddLink(Link link)
        {
            _links.Add(link);
            LinkLine ll = new LinkLine(link);
            ll.DeleteEventHandler = delegate { Delete(_linkLines.IndexOf(ll)); };
            ll.EditEventHandler = delegate { Edit(_linkLines.IndexOf(ll)); };
            ll.SelectEventHandler = delegate { LineSelect(_linkLines.IndexOf(ll)); };
            ll.SyncEventHandler = delegate { Sync(_linkLines.IndexOf(ll)); };

            _linkLines.Add(ll);
            dataTable.Controls.Add(ll, 0, _linkLines.Count);
        }
        
        public void RemoveLink(int pos)
        {
            _links.RemoveAt(pos);
            _linkLines.RemoveAt(pos);
            dataTable.Controls.RemoveAt(pos);
        }

        /// <summary>
        /// query data from file and fill table
        /// </summary>
        private void FillData()
        {
            //fetch links from file
            DataManager.CreateDataFileIfNotExist();
            SetLinks(DataManager.GetLinkList());
            dataTable.Controls.Add(new Shadow(dataTable.Width), 0, _linkLines.Count+1);
        }

        /// <summary>
        /// update synchronisation info of selected link in form
        /// </summary>
        private void UpdateDetails()
        {
            if (ActLink.SyncInfo == null)
            {
                panel_syncDetail.Visible = false;
                return;
            }

            label_detail_title.Text = ActLink.Title;
            label_detail_progress.Text = $"{ActLink.SyncInfo.Progress:0.00}%";
            label_detail_status.Text = ActLink.SyncInfo.State.Title;
            panel_detail_header.BackColor = ActLink.SyncInfo.State.Color;
            panel_syncDetail.Visible = true;

            if (!ActLink.IsRunning())
            {
                button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
                button_pr.Visible = false;
            }
            else
            {
                button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;

                button_pr.Visible = true;
                button_pr.BackgroundImage = ActLink.SyncInfo.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
            }

            label_detail_title.Text = ActLink.Title;
            label_detail_progress.Text = $"{ActLink.SyncInfo.Progress:0.0}%";
            label_detail_status.Text = ActLink.SyncInfo.State.Title;
            label_timeLeft.Text = ActLink.SyncInfo.TimeRemainingEst.ToString(@"hh\:mm\:ss");
            panel_detail_header.BackColor = ActLink.SyncInfo.State.Color;
            panel_syncDetail.Visible = true;
        }

        /// <summary>
        /// add link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_addLink_Click(object sender, EventArgs e)
        {
            AddLinkForm form = new AddLinkForm(this);
            form.Show();
        }
        
        /// <summary>
        /// on resume/pause button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_pr_Click(object sender, EventArgs e)
        {
            if (ActLink.SyncInfo.Paused)
            {
                ActLink.ResumeSync();
            }
            else
            {
                ActLink.PauseSync();
            }
            UpdateDetails();
        }

        /// <summary>
        /// on synchronisation button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sync_Click(object sender, EventArgs e)
        {
            if (!ActLink.IsRunning())
            {
                Sync(_currentPos);
            }
            else
            {
                Cancel(_currentPos);
            }
        }

        /// <summary>
        /// open synchronisation detail form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_openDetailForm_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(OpenStatFormInBackground);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// select line and unselect others
        /// start updating detail stats afterwards
        /// </summary>
        /// <param name="pos"></param>
        private void LineSelect(int pos)
        {
            _currentPos = pos;
            //deselect others
            for (int i = 0; i < _linkLines.Count; i++)
            {
                if (i != pos)
                    _linkLines[i].Selected = false;
            }

            UpdateDetailStatsAsync();
        }

        /// <summary>
        /// open Edit Link Form
        /// </summary>
        /// <param name="pos"></param>
        private void Edit(int pos)
        {
            EditLinkForm form = new EditLinkForm(_links[pos]);
            form.ShowDialog();
            _linkLines[pos].UpdateData();
        }

        /// <summary>
        /// delete link from data
        /// </summary>
        /// <param name="pos"></param>
        private void Delete(int pos)
        {
            DataManager.DeleteLink(_links[pos].Title);
            RemoveLink(pos);
        }

        /// <summary>
        /// start synchronisation of link and update button
        /// </summary>
        /// <param name="pos">link position in _links</param>
        private void Sync(int pos)
        {
            _syncFinsihedFlags.Remove(_links[pos]);

            _linkLines[pos].SyncB.SwitchToCancel();
            _linkLines[pos].InfoIcon = -1;

            ProgressBar pb = _linkLines[pos].ProgressBar;
            pb.Visible = true;
            pb.Value = 0;

            label_p.Visible = true;
            label_p.Text = @"0";
            progressBar_total.Visible = true;
            progressBar_total.Value = 0;

            bool first = !SyncRunning();

            _links[pos].Sync();

            //Start UpdateStatsAsync asynchronous Method when this is the first synchronisation process
            if (first)
                UpdateStatsAsync();

            if (_currentPos == pos)
            {
                UpdateDetailStatsAsync();
            }
        }

        /// <summary>
        /// cancel synchronisation of link and update button
        /// </summary>
        /// <param name="pos">link pos in _links</param>
        private void Cancel(int pos)
        {
            _linkLines[pos].SyncB.SwitchToSync();

            _links[pos].CancelSync();

            _linkLines[pos].ProgressBar.Visible = false;

            if (!SyncRunning())
            {
                progressBar_total.Visible = false;
                label_p.Visible = false;
            }
            UpdateDetails();
        }

        /// <summary>
        /// check if any synchronisation is running
        /// </summary>
        /// <returns></returns>
        public bool SyncRunning()
        {
            return _links.Any(l => l.IsRunning());
        }

        /// <summary>
        /// start updating synchronisation overview statistics every 100 ms while any synchronisation is running
        /// </summary>
        public async void UpdateStatsAsync()
        {
            progressBar_total.Visible = true;
            shadow_progressbar.Visible = true;

            //loop once more after all synchronisations finished
            bool lastLoop = false;
            while (SyncRunning() || lastLoop)
            {
                float tp = GetTotalProgress();
                progressBar_total.Value = (int) (tp*10);
                label_p.Text = $"{tp:0.0}%";

                //check each link
                for (int i = 0; i < _links.Count; i++)
                {
                    Link l = _links[i];
                    LinkLine linkLine = _linkLines[i];

                    if (l.SyncInfo == null)
                        continue;


                    linkLine.StatusColor = l.SyncInfo.State.Color;

                    if (l.IsRunning())
                    {
                        if (!linkLine.SyncB.StateSync)
                        {
                            linkLine.SyncB.SwitchToCancel();
                        }

                        linkLine.ProgressBar.Visible = true;

                        if (l.SyncInfo.State == SyncState.ApplyingFileChanges)
                            linkLine.ProgressBar.Style = ProgressBarStyle.Blocks;
                        else
                        {
                            if (l.SyncInfo.Paused)
                                linkLine.ProgressBar.MarqueeAnimationSpeed = 0;
                            else
                            {
                                if(linkLine.ProgressBar.MarqueeAnimationSpeed == 0)
                                    linkLine.ProgressBar.MarqueeAnimationSpeed = 30;
                                linkLine.ProgressBar.Style = ProgressBarStyle.Marquee;
                            }
                        }
                    }
                    else
                    {
                        if (l.SyncInfo == null || !l.SyncInfo.Finished || _syncFinsihedFlags.Contains(l)) continue;
                        _syncFinsihedFlags.Add(l);

                        //--- on synchronisation finished ---
                        linkLine.ProgressBar.Visible = false;

                        linkLine.SyncB.SwitchToSync();
                        linkLine.InfoIcon = l.SyncInfo.Conflicted ? 1 : 0;

                        //show dialog if conflicts occurred to ask for retrying
                        if (l.SyncInfo != null && l.SyncInfo.Finished && l.SyncInfo.Conflicted)
                        {
                            DialogResult result = new SyncConflictRetryDialog(l).ShowDialog();
                            if (result == DialogResult.Yes)
                                Sync(i);
                        }
                    }
                }
                await Task.Delay(100);
                if (!SyncRunning()) lastLoop = true;
            }

            progressBar_total.Visible = false;
            shadow_progressbar.Visible = false;
            label_p.Visible = false;
        }

        /// <summary>
        /// start updating synchronisation details of selected link every 100 ms while a link is selected
        /// </summary>
        public async void UpdateDetailStatsAsync()
        {

            if (ActLink?.SyncInfo != null)
                panel_syncDetail.Visible = true;

            while (ActLink?.SyncInfo != null)
            {
                label_detail_title.Text = ActLink.Title;
                label_detail_progress.Text = $"{ActLink.SyncInfo.Progress:0.0}%";
                label_detail_status.Text = ActLink.SyncInfo.State.Title;

                label_timeLeft.Text = ActLink.SyncInfo.TimeRemainingEst.ToString(@"hh\:mm\:ss");

                panel_detail_header.BackColor = ActLink.SyncInfo.State.Color;

                if (_currentPos >= 0)
                {
                    if (ActLink.IsRunning())
                    {
                        ActLinkLine.Progress = ActLink.SyncInfo.Progress;

                        label_detail_progress.Text = $"{ActLink.SyncInfo.Progress:0.0}%";
                        label_detail_status.Text = ActLink.SyncInfo.State.Title;

                        button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;

                        button_pr.Visible = true;
                        button_pr.BackgroundImage = ActLink.SyncInfo.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
                    }
                    else
                    {
                        button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
                        button_pr.Visible = false;
                    }
                }

                await Task.Delay(300);
            }

            panel_syncDetail.Visible = false;
        }

        /// <summary>
        /// get total progress of all syncs
        /// </summary>
        /// <returns></returns>
        private float GetTotalProgress()
        {
            float total = 0;
            float p = 0;
            
            foreach (Link l in _links)
            {
                if (!l.SyncInfo.Running) continue;
                total += l.SyncInfo.TotalSize;
                p += l.SyncInfo.SizeApplied;
            }

            return total > 0 ? 100f / total * p : 0f;
        }

        /// <summary>
        /// open Link Statistic Form of selected link in new thread
        /// </summary>
        private void OpenStatFormInBackground()
        {
            if (ActLink == null) return;

            LinkStatisticForm f = new LinkStatisticForm(ActLink);

            f.FormClosed += delegate
            {
                f.Dispose();
                _statForms.Remove(f);
            };

            if (!f.IsDisposed)
            {
                Application.Run(f);
                _statForms.Add(f);
            }
        }

        /// <summary>
        /// close all link statistic forms on main form closin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (LinkStatisticForm f in _statForms)
            {
                try
                {
                    f.Invoke(new Action(() =>
                        f.Close()
                    ));
                }
                catch (InvalidOperationException) { }
            }
        }

        /// <summary>
        /// on form close button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// on form minimize button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        
        /// <summary>
        /// make window moveable
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {  // Trap WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = PointToClient(pos);
                if (pos.Y < CaptionHeight)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}
 