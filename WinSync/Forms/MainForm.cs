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
        SyncLink _selectedLink;
        readonly List<LinkRow> _linkRows = new List<LinkRow>();
        readonly List<SyncDetailInfoForm2> _statForms = new List<SyncDetailInfoForm2>();
        bool _updatingSyncInfoRunning;
        bool _updatingSyncLinkInfoRunning;

        public MainForm()
        {
            InitializeComponent();
            
            DataManager.LinkChanged += OnLinkDataChanged;
            DataManager.LoadLinks();

            StartUpdateRoutine();
        }

        void StartUpdateRoutine()
        {
            Task.Run(async () =>
            {
                while (!IsDisposed)
                {
                    if (!_updatingSyncInfoRunning && DataManager.AnySyncRunning)
                        Invoke(new Action(() => { StartUpdatingSyncInfo(); }));
                    if (!_updatingSyncLinkInfoRunning && _selectedLink?.SyncInfo != null)
                        Invoke(new Action(() => { StartUpdatingSelectedSyncLinkInfo(); }));

                    await Task.Delay(500);
                }
            });
        }

        /// <summary>
        /// start synchronisation of link and update button
        /// </summary>
        /// <param name="l"></param>
        void StartSync(SyncLink l)
        {
            label_p.Visible = true;
            label_p.Text = @"0";
            progressBar_total.Visible = true;
            progressBar_total.Value = 0;
            
            l.Sync();
            
            StartUpdatingSyncInfo();
            if (_selectedLink == l)
                StartUpdatingSelectedSyncLinkInfo();
        }

        /// <summary>
        /// cancel synchronisation of link
        /// </summary>
        /// <param name="l"></param>
        void CancelSync(SyncLink l)
        {
            l.CancelSync();
        }

        /// <summary>
        /// start updating general synchronisation info async, 
        /// until all synchronisation have finished
        /// </summary>
        async void StartUpdatingSyncInfo()
        {
            if (_updatingSyncInfoRunning)
                return;
            _updatingSyncInfoRunning = true;

            progressBar_total.Visible = true;
            
            while (DataManager.AnySyncRunning)
            {
                UpdateSyncInfo();
                await Task.Delay(200);
            }
            UpdateSyncInfo();

            _updatingSyncInfoRunning = false;

            progressBar_total.Visible = false;
            label_p.Visible = false;
        }

        /// <summary>
        /// update general synchronisation info
        /// </summary>
        void UpdateSyncInfo()
        {
            //update total progress info
            float tp = DataManager.GetTotalProgress();
            progressBar_total.Value = (int)(tp * 10);
            label_p.Text = $"{tp:0.0}%";

            //update each sync data of link
            foreach (SyncLink l in DataManager.Links)
            {
                LinkRow linkRow = GetLinkRow(l);

                if (l.SyncInfo == null)
                    continue;

                linkRow.UpdateSyncData();
            }
        }

        /// <summary>
        /// start updating synchronisation info of selected link async, until no link is selected
        /// </summary>
        async void StartUpdatingSelectedSyncLinkInfo()
        {
            if (_updatingSyncLinkInfoRunning)
                return;
            _updatingSyncLinkInfoRunning = true;

            panel_syncDetail.Visible = true;

            while (_selectedLink != null)
            {
                UpdateSelectedSyncLinkInfo();
                await Task.Delay(300);
            }

            _updatingSyncLinkInfoRunning = false;
            panel_syncDetail.Visible = false;
        }

        /// <summary>
        /// update synchronisation info of selected link
        /// </summary>
        void UpdateSelectedSyncLinkInfo()
        {
            label_detail_title.Text = _selectedLink.Title;

            if (_selectedLink.SyncInfo != null)
            {
                panel_selSL_info.Visible = true;
                label_detail_progress.Text = $"{_selectedLink.SyncInfo.SyncProgress:0.0}%";
                label_detail_status.Text = _selectedLink.SyncInfo.Status.Title;
                label_timeLeft.Text = _selectedLink.SyncInfo.TimeRemainingEst.ToString(@"hh\:mm\:ss");
                panel_detail_header.BackColor = _selectedLink.SyncInfo.Status.Color;
            }
            else
            {
                //reset info
                panel_selSL_info.Visible = false;
                panel_detail_header.BackColor = Color.Gray;
            }

            if (_selectedLink.IsRunning)
            {
                button_sync.BackgroundImage = Properties.Resources.ic_cancel_white;
                button_pr.Visible = true;
                button_pr.BackgroundImage = _selectedLink.SyncInfo.Paused ? Properties.Resources.ic_play_white : Properties.Resources.ic_pause_white;
            }
            else
            {
                button_sync.BackgroundImage = Properties.Resources.ic_sync_white;
                button_pr.Visible = false;
            }
        }

        /// <summary>
        /// open SyncDetailInfoForm of selected link
        /// </summary>
        void OpenSyncDetailInfoFormInBackground()
        {
            if (_selectedLink == null) return;

            SyncDetailInfoForm2 f = new SyncDetailInfoForm2(_selectedLink, this);

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

        #region LinkRow management
        LinkRow GetLinkRow(SyncLink l)
        {
            return _linkRows.FirstOrDefault(x => x.Link == l);
        }

        void AddLinkRow(SyncLink link)
        {
            LinkRow ll = new LinkRow(link);
            ll.LinkDeletionRequested += delegate { DataManager.RemoveLink(link.Title); };
            ll.EditLinkRequested += delegate { new EditLinkForm(link).ShowDialog(); };
            ll.LinkRowSelected += delegate { SelectLinkRow(link); };
            ll.SyncStartRequested += delegate { StartSync(link); };
            ll.SyncCancellationRequested += delegate { CancelSync(link); };

            _linkRows.Add(ll);
            dataTable.Controls.Add(ll, 0, _linkRows.Count);
        }

        void RemoveLinkRow(SyncLink link)
        {
            for (int i = 0; i < _linkRows.Count; i++)
            {
                if (_linkRows[i].Link == link)
                {
                    _linkRows.RemoveAt(i);
                    dataTable.Controls.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// select LinkRow and unselect others
        /// start updating detail stats afterwards
        /// </summary>
        /// <param name="l">associated SyncLink</param>
        void SelectLinkRow(SyncLink l)
        {
            //deselect others
            foreach (LinkRow lr in _linkRows.Where(x => x.Link != l))
                lr.Selected = false;

            _selectedLink = l;

            StartUpdatingSelectedSyncLinkInfo();
        }
        #endregion

        #region event handler

        private void OnLinkDataChanged(int changeType, SyncLink l)
        {
            switch (changeType)
            {
                case 0:
                    //Link added
                    AddLinkRow(l);
                    break;
                case 1:
                    //Link changed
                    GetLinkRow(l).UpdateData();
                    break;
                case 2:
                    //Link removed
                    RemoveLinkRow(l);
                    break;
            }
        }

        /// <summary>
        /// add link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_addLink_Click(object sender, EventArgs e)
        {
            AddLinkForm form = new AddLinkForm();
            form.Show();
        }

        /// <summary>
        /// on resume/pause button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_pr_Click(object sender, EventArgs e)
        {
            if (_selectedLink.SyncInfo == null)
                return;

            if (_selectedLink.SyncInfo.Paused)
            {
                _selectedLink.ResumeSync();
            }
            else
            {
                _selectedLink.PauseSync();
            }
            UpdateSelectedSyncLinkInfo();
        }

        /// <summary>
        /// on synchronisation button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_sync_Click(object sender, EventArgs e)
        {
            if (!_selectedLink.IsRunning)
            {
                StartSync(_selectedLink);
            }
            else
            {
                CancelSync(_selectedLink);
            }
        }

        /// <summary>
        /// open SyncDetailInfoForm in new thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_openDetailForm_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(OpenSyncDetailInfoFormInBackground);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// close all SyncDetailInfoForms on main form closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (SyncDetailInfoForm2 f in _statForms)
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
        
        private void checkBox_availableSyncsOnly_CheckedChanged(object sender, EventArgs e)
        {
            foreach(LinkRow lr in _linkRows)
            {
                lr.Visible = checkBox_availableSyncsOnly.Checked ? lr.Link.IsExecutable() : true;
            }
        }
        #endregion

        #region window management
        /// <summary>
        /// Caption bar height
        /// </summary>
        private const int CaptionHeight = 40;

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
        #endregion
    }
}
 