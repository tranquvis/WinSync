using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Controls
{
    public partial class LinkRow : UserControl
    {
        private static Color MyBackColor = Color.WhiteSmoke;
        private static Color MySelectBackColor = Color.LightGray;

        public SyncLink Link { get; private set; }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                inner.BackColor = value ? MySelectBackColor : MyBackColor;
            }
        }

        #region addable event handler
        /// <summary>
        /// occurs, when the deletion of the link has been requested
        /// </summary>
        public event EventHandler LinkDeletionRequested;

        /// <summary>
        /// occurs, when editing the link has been requested
        /// </summary>
        public event EventHandler EditLinkRequested;

        /// <summary>
        /// occurs, when the link has been selected
        /// </summary>
        public event EventHandler LinkRowSelected;

        /// <summary>
        /// occurs, when the synchronisation-start of the link has been requested
        /// </summary>
        public event EventHandler SyncStartRequested;

        /// <summary>
        /// occurs, when the cancellation of the link has been requested
        /// </summary>
        public event EventHandler SyncCancellationRequested;
        #endregion

        /// <summary>
        /// create a LinkRow that displays information and controls for a Link and its synchronisation
        /// </summary>
        public LinkRow(SyncLink link)
        {
            InitializeComponent();

            Link = link;
            UpdateData();

            //add on-click-listener to inner panel and its children
            inner.MouseClick += OnInnerClick;
            foreach (Control c in inner.Controls)
            {
                c.MouseClick += OnInnerClick;
            }
        }

        /// <summary>
        /// update form to meet the link data
        /// </summary>
        public void UpdateData()
        {
            label_title.Text = Link.Title;
            label_path1.Text = Link.Path1;
            label_path2.Text = Link.Path2;

            //update direction image
            if (Link.Direction == SyncDirection.To1)
                pictureBox_direction.Image = Properties.Resources.ic_up;
            else if (Link.Direction == SyncDirection.To2)
                pictureBox_direction.Image = Properties.Resources.ic_down;
            else if (Link.Direction == SyncDirection.TwoWay)
                pictureBox_direction.Image = Properties.Resources.ic_two_way;
        }

        /// <summary>
        /// update form to meet the link synchronisation data
        /// </summary>
        public void UpdateSyncData()
        {
            BackColor = Link.SyncInfo.Status.Color;

            if (Link.SyncInfo == null)
            {
                syncButton.SwitchToSync();
                return;
            }

            //update result image
            if(Link.SyncInfo.Status == SyncStatus.Finished || Link.SyncInfo.Status == SyncStatus.Aborted)
                pictureBox_result.Image = Properties.Resources.ic_check_green_72px;
            else if(Link.SyncInfo.Status == SyncStatus.Conflicted)
                pictureBox_result.Image = Properties.Resources.ic_error_outline_red_72px;
            else
                pictureBox_result.Image = null;

            if (Link.IsRunning)
            {
                //update progress bar
                progressBar.Visible = true;
                progressBar.Value = (int)Link.SyncInfo.SyncProgress;

                if (Link.SyncInfo.Status == SyncStatus.ApplyingFileChanges)
                    progressBar.Style = ProgressBarStyle.Blocks;
                else
                {
                    if (Link.SyncInfo.Paused)
                        progressBar.MarqueeAnimationSpeed = 0;
                    else
                    {
                        if (progressBar.MarqueeAnimationSpeed == 0)
                            progressBar.MarqueeAnimationSpeed = 30;
                        progressBar.Style = ProgressBarStyle.Marquee;
                    }
                }

                //update sync button
                syncButton.SwitchToCancel();
            }
            else
            {
                progressBar.Visible = false;
                syncButton.SwitchToSync();
            }
        }

        /// <summary>
        /// show context menu for editing and deleting link on right click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInnerClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    ContextMenu m = new ContextMenu();

                    MenuItem openFolder1 = new MenuItem("Open Folder 1 in Explorer");
                    openFolder1.Click += (o, args) => OpenInExplorer(Link.Path1);
                    m.MenuItems.Add(openFolder1);

                    MenuItem openFolder2 = new MenuItem("Open Folder 2 in Explorer");
                    openFolder2.Click += (o, args) => OpenInExplorer(Link.Path2);
                    m.MenuItems.Add(openFolder2);

                    MenuItem delete = new MenuItem("Delete Link");
                    delete.Click += LinkDeletionRequested;
                    m.MenuItems.Add(delete);

                    MenuItem edit = new MenuItem("Edit Link");
                    edit.Click += EditLinkRequested;
                    m.MenuItems.Add(edit);

                    m.Show((Control) sender, new Point(e.X, e.Y));
                    break;
                case MouseButtons.Left:
                    Selected = true;
                    LinkRowSelected(this, EventArgs.Empty);
                    break;
            }
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            if (syncButton.StateSync)
                SyncStartRequested(sender, e);
            else
                SyncCancellationRequested(sender, e);
        }

        /// <summary>
        /// open directory path in explorer
        /// </summary>
        /// <param name="path"></param>
        private void OpenInExplorer(string path)
        {
            Process.Start(path);
        }
    }
}
