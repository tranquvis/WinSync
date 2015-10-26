using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Controls
{
    public partial class LinkLine : UserControl
    {
        private static Color MyBackColor = Color.WhiteSmoke;
        private static Color MySelectBackColor = Color.LightGray;

        /// <summary>
        /// create a LinkLine that displays link info and controls
        /// </summary>
        public LinkLine(Link link)
        {
            InitializeComponent();

            Link = link;
            UpdateData();

            //add on click listener to inner panel and children
            inner.MouseClick += OnInnerClick;
            foreach (Control c in inner.Controls)
            {
                c.MouseClick += OnInnerClick;
            }
        }

        public SyncButton SyncB => syncButton;
        public Label TitleL => label_title;
        public Label Path1L => label_path1;
        public Label Path2L => label_path2;
        public PictureBox DirectionP => pictureBox_direction;
        public ProgressBar ProgressBar => progressBar;

        public Color StatusColor
        {
            get { return BackColor; }
            set { BackColor = value; }
        }

        public void UpdateData()
        {
            Title = Link.Title;
            Path1 = Link.Path1;
            Path2 = Link.Path2;
            Direction = Link.Direction;
        }

        public Link Link { get; }

        public string Title
        {
            set { TitleL.Text = value; }
        }
        public string Path1
        {
            set { Path1L.Text = value; }
        }

        public string Path2
        {
            set { Path2L.Text = value; }
        }
        
        public SyncDirection Direction
        {
            set
            {
                if (value == SyncDirection.To1)
                    DirectionP.Image = Properties.Resources.ic_up;
                else if (value == SyncDirection.To2)
                    DirectionP.Image = Properties.Resources.ic_down;
                else if (value == SyncDirection.TwoWay)
                    DirectionP.Image = Properties.Resources.ic_two_way;
            }
        }
        
        /// <summary>
        /// Set info icon
        /// 0 -> success, 1 -> error, -1 -> no info
        /// </summary>
        public int InfoIcon
        {
            set
            {
                switch (value)
                {
                    case -1:
                        pictureBox_result.Image = null;
                        break;
                    case 0:
                        pictureBox_result.Image = Properties.Resources.ic_check_green_72px;
                        break;
                    case 1:
                        pictureBox_result.Image = Properties.Resources.ic_error_outline_red_72px;
                        break;
                }
            }
        }

        /// <summary>
        /// progress in percent
        /// </summary>
        public float Progress
        {
            get { return ProgressBar.Value * 100.0f / ProgressBar.Maximum; }
            set { ProgressBar.Value = (int) (value * ProgressBar.Maximum / 100.0f); }
        }

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

        public EventHandler DeleteEventHandler { private get; set; } = null;
        public EventHandler EditEventHandler { private get; set; } = null;
        public Action SelectEventHandler { private get; set; } = null;
        public EventHandler SyncEventHandler { private get; set; } = null;


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
                    if (DeleteEventHandler != null)
                        delete.Click += DeleteEventHandler;
                    m.MenuItems.Add(delete);

                    MenuItem edit = new MenuItem("Edit Link");
                    if (EditEventHandler != null)
                        edit.Click += EditEventHandler;
                    m.MenuItems.Add(edit);

                    m.Show((Control) sender, new Point(e.X, e.Y));
                    break;
                case MouseButtons.Left:
                    Selected = true;
                    SelectEventHandler?.Invoke();
                    break;
            }
        }

        private void syncButton_Click(object sender, EventArgs e)
        {
            SyncEventHandler?.Invoke(sender, e);
        }

        private void OpenInExplorer(string path)
        {
            Process.Start(path);
        }
    }
}
