using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace tranquvis.Utils.WinFormDesign
{
    public class CustomDesignForm : Form
    {
        private Brush _windowBrush;
        private Brush _contentBrush;
        private Color _windowBackColor;
        private Color _contentBackColor;

        private Image _iconSrc;
        private int _iconHeight;
        private int _iconWidth;
        private Padding _iconMargin;

        private Button _buttonMinimize;
        private Button _buttonClose;
        private Button _buttonMaximize;
        private Button _buttonDownsize;

        private Button[][] _windowControls;

        private Label _labelTitle;

        protected readonly Panel _contentPanel;

        protected int _formControlWidth;
        protected int _formControlHeight;

        /// <summary>
        /// create Form with custom design
        /// </summary>
        public CustomDesignForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts

            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            MinimumSize = new Size(200, 100);

            //default settings
            WindowBackColor = Color.Gray;
            ContentBackColor = Color.White;
            FormControlHoverColor = Color.DimGray;
            IconMargin  = new Padding(0, 0, 0, 2);
            //---------------

            _labelTitle = FTitleLTempl;
            Controls.Add(_contentPanel = ContentPanelTempl);
        }

        /// <summary>
        /// Override/Extend this method to init layout properties. <para />
        /// (this will be called after initalizeComponent)
        /// </summary>
        public virtual void InitCustomLayout()
        {
            _formControlWidth = CaptionBarHeight + FormBorderWidth;
            _formControlHeight = CaptionBarHeight + FormBorderWidth;
            CreateFormControls();

            UpdateIconSize();

            _contentPanel.Top = CaptionBarHeight + FormBorderWidth;
            _contentPanel.Left = FormBorderWidth;
            _contentPanel.Size = new Size(ClientSize.Width - FormBorderWidth * 2, ClientSize.Height - FormBorderWidth * 2 - CaptionBarHeight);

            Padding = new Padding(FormBorderWidth, FormBorderWidth + CaptionBarHeight, FormBorderWidth, FormBorderWidth);

            //display title label
            _labelTitle.Left = FormBorderWidth + IconMargin.Left + _iconWidth + IconMargin.Right;
            _labelTitle.Top = FormBorderWidth + (CaptionBarHeight - _labelTitle.Height) / 2;
            Controls.Add(_labelTitle);
        }

        /// <summary>
        /// background color of window frame
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color WindowBackColor
        {
            get { return _windowBackColor; }
            set
            {
                _windowBackColor = value;
                _windowBrush = new SolidBrush(_windowBackColor);
            }
        }

        /// <summary>
        /// background color of the content area
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color ContentBackColor
        {
            get { return _contentBackColor; }
            set
            {
                _contentBackColor = value;
                _contentBrush = new SolidBrush(_contentBackColor);
            }
        }

        /// <summary>
        /// title of form, that will be displayed in caption-bar
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string FormTitle {
            get { return _labelTitle.Text; }
            set
            {
                _labelTitle.Text = value;
            }
        }

        /// <summary>
        /// height of caption bar
        /// note taht the caption bar is under the top form border
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int CaptionBarHeight { get; set; } = 30;

        /// <summary>
        /// width of form border
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public int FormBorderWidth { get; set; } = 5;

        /// <summary>
        /// source of icon in caption-bar or null if no icon should be displayed
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Image IconSrc
        {
            get { return _iconSrc; }
            set
            {
                _iconSrc = value;
                UpdateIconSize();
            }
        }

        /// <summary>
        /// icon margin
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Padding IconMargin
        {
            get { return _iconMargin; }
            set
            {
                _iconMargin = value;
                UpdateIconSize();
            }
        }

        /// <summary>
        /// hover color of the form controls
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color FormControlHoverColor { get; set; }

        /// <summary>
        /// if a drop shadow should be painted to the window
        /// </summary>
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public bool WindowDropShadow { get; set; } = true;
        
        /// <summary>
        /// is the same as ContentBackColor. <para />
        /// Setting has no effect! (necessary for VS Form-Designer)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("use ContentBackColor instead", false)]
        public new Color BackColor
        {
            get { return ContentBackColor; }
            set { }
        }

        /// <summary>
        /// form border style <para />
        /// Is always set to none! (necessary for VS Form-Designer)
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        #region control templates
        /// <summary>
        /// form-control-template for minimize-button <para />
        /// override this to change minimize-button style
        /// </summary>
        protected virtual Button FCBMinimizeTempl
        {
            get
            {
                Button b = new Button();
                b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = FormControlHoverColor;
                b.FlatStyle = FlatStyle.Flat;
                b.ForeColor = Color.Black;
                b.BackColor = Color.Transparent;
                b.ImeMode = ImeMode.NoControl;
                b.Margin = new Padding(0);
                b.UseVisualStyleBackColor = true;
                b.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold);
                b.Text = "_";
                return b;
            }
        }

        /// <summary>
        /// form-control-template for close-button <para />
        /// override this to change close-button style
        /// </summary>
        protected virtual Button FCBCloseTempl
        {
            get
            {
                Button b = new Button();
                b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = FormControlHoverColor;
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.ImeMode = ImeMode.NoControl;
                b.UseVisualStyleBackColor = true;

                Bitmap bm = new Bitmap(11, 11);
                Graphics g = Graphics.FromImage(bm);
                Pen pen = new Pen(Brushes.Black, 1);
                g.DrawLine(pen, 1, 1, 10, 10);
                g.DrawLine(pen, 1, 10, 10, 1);
                b.Image = bm;
                b.ImageAlign = ContentAlignment.MiddleCenter;

                return b;
            }
        }

        /// <summary>
        /// form-control-template for maximize-button <para />
        /// override this to change maximize-button style
        /// </summary>
        protected virtual Button FCBMaximizeTempl
        {
            get
            {
                Button b = new Button();
                b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = FormControlHoverColor;
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.ImeMode = ImeMode.NoControl;
                b.Margin = new Padding(0);
                b.UseVisualStyleBackColor = true;
                
                Bitmap bm = new Bitmap(12, 12);
                Graphics g = Graphics.FromImage(bm);
                g.DrawRectangle(new Pen(Brushes.Black, 1), 1, 3, 10, 8);
                b.Image = bm;
                b.ImageAlign = ContentAlignment.MiddleCenter;

                return b;
            }
        }

        /// <summary>
        /// form-control-template for downsize-button <para />
        /// override this to change downsize-button style
        /// </summary>
        protected virtual Button FCBDownsizeTempl
        {
            get
            {
                Button b = new Button();
                b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                b.FlatAppearance.BorderSize = 0;
                b.FlatAppearance.MouseOverBackColor = FormControlHoverColor;
                b.FlatStyle = FlatStyle.Flat;
                b.BackColor = Color.Transparent;
                b.ImeMode = ImeMode.NoControl;
                b.Margin = new Padding(0);
                b.UseVisualStyleBackColor = true;
                
                Bitmap bm = new Bitmap(14, 13);
                Graphics g = Graphics.FromImage(bm);
                Pen pen = new Pen(Brushes.Black, 1);
                g.DrawRectangle(pen, 1, 4, 9, 8);
                g.DrawLines(pen, new Point[] { new Point(4, 4), new Point(4, 1), new Point(13, 1), new Point(13, 9), new Point(10, 9)});
                b.Image = bm;
                b.ImageAlign = ContentAlignment.MiddleCenter;
                
                return b;
            }
        }

        /// <summary>
        /// template for form-title-label <para />
        /// override this to change its style
        /// </summary>
        protected virtual Label FTitleLTempl
        {
            get
            {
                Label l = new Label();
                l.Padding = new Padding(6, 0, 0, 0);
                l.Font = new Font("Microsoft Sans Serif", 8);
                l.Height = 14;
                l.ForeColor = Color.Black;
                l.BackColor = Color.Transparent;
                return l;
            }
        }

        /// <summary>
        /// template for _contentPanel <para />
        /// override this to change its style
        /// </summary>
        protected virtual Panel ContentPanelTempl
        {
            get
            {
                Panel p = new Panel();
                p.BackColor = Color.Transparent;
                p.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
                p.Padding = new Padding(5);
                return p;
            }
        }
        #endregion

        /// <summary>
        /// create form controls for minimizing and closing form
        /// </summary>
        private void CreateFormControls()
        {
            #region init buttons
            //close button
            _buttonClose = FCBCloseTempl;
            _buttonClose.Click += delegate { Close(); };

            if (MaximizeBox)
            {
                //maximize button
                _buttonMaximize = FCBMaximizeTempl;
                if (WindowState == FormWindowState.Maximized)
                    _buttonMaximize.Visible = false;
                _buttonMaximize.Click += delegate
                {
                    WindowState = FormWindowState.Maximized;
                    _buttonMaximize.Visible = false;
                    if (_buttonDownsize != null)
                        _buttonDownsize.Visible = true;
                };

                //downsize button
                _buttonDownsize = FCBDownsizeTempl;
                if (WindowState == FormWindowState.Normal)
                    _buttonDownsize.Visible = false;
                _buttonDownsize.Click += delegate
                {
                    WindowState = FormWindowState.Normal;
                    _buttonDownsize.Visible = false;
                    if (_buttonDownsize != null)
                        _buttonMaximize.Visible = true;
                };
            }

            //minimize button
            if (MinimizeBox)
            {
                _buttonMinimize = FCBMinimizeTempl;
                _buttonMinimize.Click += delegate { WindowState = FormWindowState.Minimized; };
            }
            #endregion

            //define button positions
            _windowControls = new Button[][]
            {
                new Button[] { _buttonClose },
                new Button[] { _buttonMaximize, _buttonDownsize },
                new Button[] { _buttonMinimize }
            };

            //position controls and add them to form
            Size controlSize = new Size(_formControlWidth, _formControlHeight);
            int posX = ClientSize.Width;

            foreach (Button[] group in _windowControls)
            {
                posX -= controlSize.Width;

                foreach (Button b in group)
                {
                    if (b == null)
                        continue;

                    b.Size = controlSize;
                    b.Location = new Point(posX, 0);
                    Controls.Add(b);
                }
            }
        }

        /// <summary>
        /// update the size of the icon, depending on the image-source, caption-bar-height and icon margin 
        /// </summary>
        private void UpdateIconSize()
        {
            if (IconSrc == null || IconMargin == null)
                return;
            _iconHeight = CaptionBarHeight - IconMargin.Top - IconMargin.Bottom;
            _iconWidth = (int)((float)_iconSrc.Width / _iconSrc.Height * _iconHeight);
        }

        /// <summary>
        /// necessary for initialising custom layout in VS Designer <para />
        /// Do not call this method outside the Designer!
        /// </summary>
        /// <param name="performLayout"></param>
        public new void ResumeLayout(bool performLayout)
        {
            //init custom layout after InitializeComponent
            InitCustomLayout();
            base.ResumeLayout(performLayout);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                if(!WindowDropShadow)
                    return base.CreateParams;

                //draw window shadow
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #region window management
        protected override void OnPaint(PaintEventArgs e)
        {
            //draw window borders
            e.Graphics.FillRectangle(_windowBrush, BorderTopR);
            e.Graphics.FillRectangle(_windowBrush, BorderLeftR);
            e.Graphics.FillRectangle(_windowBrush, BorderRightR);
            e.Graphics.FillRectangle(_windowBrush, BorderBottomR);

            //draw caption-bar
            e.Graphics.FillRectangle(_windowBrush, CaptionBarR);

            //draw content area
            e.Graphics.FillRectangle(_contentBrush, ContentR);

            //draw logo
            if (IconSrc != null)
                e.Graphics.DrawImage(IconSrc, FormBorderWidth + IconMargin.Left, FormBorderWidth + IconMargin.Top, _iconWidth, _iconHeight);
        }

        Rectangle ContentR { get { return new Rectangle(FormBorderWidth, CaptionBarHeight + FormBorderWidth, ClientSize.Width - 2 * FormBorderWidth, ClientSize.Height - CaptionBarHeight - 2 * FormBorderWidth); } }

        Rectangle CaptionBarR { get { return new Rectangle(FormBorderWidth, FormBorderWidth, ClientSize.Width - 2 * FormBorderWidth, CaptionBarHeight); } }

        Rectangle BorderTopR { get { return new Rectangle(0, 0, ClientSize.Width, FormBorderWidth); } }
        Rectangle BorderLeftR { get { return new Rectangle(0, 0, FormBorderWidth, ClientSize.Height); } }
        Rectangle BorderBottomR { get { return new Rectangle(0, ClientSize.Height - FormBorderWidth, ClientSize.Width, FormBorderWidth); } }
        Rectangle BorderRightR { get { return new Rectangle(ClientSize.Width - FormBorderWidth, 0, FormBorderWidth, ClientSize.Height); } }

        Rectangle CornerTopLeftR { get { return new Rectangle(0, 0, FormBorderWidth, FormBorderWidth); } }
        Rectangle CornerTopRightR { get { return new Rectangle(ClientSize.Width - FormBorderWidth, 0, FormBorderWidth, FormBorderWidth); } }
        Rectangle CornerBottomLeftR { get { return new Rectangle(0, ClientSize.Height - FormBorderWidth, FormBorderWidth, FormBorderWidth); } }
        Rectangle CornerBottomRightR { get { return new Rectangle(ClientSize.Width - FormBorderWidth, ClientSize.Height - FormBorderWidth, FormBorderWidth, FormBorderWidth); } }
        
        private const int
            HT_CAPTION = 2,
            HT_LEFT = 10,
            HT_RIGHT = 11,
            HT_TOP = 12,
            HT_TOPLEFT = 13,
            HT_TOPRIGHT = 14,
            HT_BOTTOM = 15,
            HT_BOTTOMLEFT = 16,
            HT_BOTTOMRIGHT = 17;

        /// <summary>
        /// make windows resizeable and moveable
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84)
            {
                Point cursor = PointToClient(Cursor.Position);

                if (CaptionBarR.Contains(cursor)) message.Result = (IntPtr)HT_CAPTION;

                else if (CornerTopLeftR.Contains(cursor)) message.Result = (IntPtr)HT_TOPLEFT;
                else if (CornerTopRightR.Contains(cursor)) message.Result = (IntPtr)HT_TOPRIGHT;
                else if (CornerBottomLeftR.Contains(cursor)) message.Result = (IntPtr)HT_BOTTOMLEFT;
                else if (CornerBottomRightR.Contains(cursor)) message.Result = (IntPtr)HT_BOTTOMRIGHT;

                else if (BorderTopR.Contains(cursor)) message.Result = (IntPtr)HT_TOP;
                else if (BorderLeftR.Contains(cursor)) message.Result = (IntPtr)HT_LEFT;
                else if (BorderRightR.Contains(cursor)) message.Result = (IntPtr)HT_RIGHT;
                else if (BorderBottomR.Contains(cursor)) message.Result = (IntPtr)HT_BOTTOM;
            }
        }
        #endregion
    }
}
