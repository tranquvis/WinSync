using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSync.Service;

namespace WinSync.Controls
{
    public partial class StatusProgressBar : UserControl
    {
        //general label design
        private static Label GeneralLabel {
            get {
                return new Label()
                {
                    Font = new Font("Microsoft Sans Serif", 8F),
                    Margin = new Padding(1, 3, 1, 3),
                    Height = 13,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(150, 150, 150)
                };
            }
        }

        //design for arrow-label
        private static Label ArrowLabel {
            get
            {
                Label l = GeneralLabel;
                l.Text = ">>";
                l.AutoSize = true;
                return l;
            }
        }

        //design for or-label
        private static Label OrLabel
        {
            get
            {
                Label l = GeneralLabel;
                l.Text = "|";
                l.AutoSize = true;
                return l;
            }
        }
        
        private List<Label>[] _statusLabels;
        private Label[] _arrowLabels;
        private List<Label>[] _orLabels;

        private string[][] _statusTitles;

        /// <summary>
        /// status-titles in groups
        /// </summary>
        public string[][] StatusTitles
        {
            get { return _statusTitles; }
            set
            {
                _statusTitles = value;
                _statusLabels = new List<Label>[_statusTitles.Length];
                _arrowLabels = new Label[_statusTitles.Length - 1];
                _orLabels = new List<Label>[_statusTitles.Length];

                for (int i = 0; i < _statusTitles.Length; i++)
                {
                    _statusLabels[i] = new List<Label>();
                    _orLabels[i] = new List<Label>();

                    if (i > 0)
                    {
                        //add arrow
                        flowLayoutPanel_statusProgress.Controls.Add(_arrowLabels[i - 1] = ArrowLabel);
                    }

                    for (int gi = 0; gi < _statusTitles[i].Length; gi++)
                    {
                        Label statusLabel = GeneralLabel;
                        statusLabel.Text = _statusTitles[i][gi];
                        statusLabel.AutoSize = true;

                        if (gi > 0)
                        {
                            //add or
                            Label orL = OrLabel;
                            _orLabels[i].Add(orL);
                            flowLayoutPanel_statusProgress.Controls.Add(orL);
                        }

                        _statusLabels[i].Add(statusLabel);
                        flowLayoutPanel_statusProgress.Controls.Add(statusLabel);

                        gi++;
                    }
                }
            }
        }

        public int ActivatedPos { get; private set; } = 0;
        public int ActivatedPosInGroup { get; private set; } = 0;

        private string _activatedStatus;
        
        /// <summary>
        /// activated status title
        /// </summary>
        public string ActivatedStatus
        {
            get { return _activatedStatus == null ? "" : _activatedStatus; }
            set
            {
                if (_activatedStatus == value)
                    return;

                _activatedStatus = value;

                int i = 0, gi = 0;
                bool doBreak = false;

                for (; i < _statusTitles.Length; i++)
                {
                    for (gi = 0; gi < _statusTitles[i].Length; gi++)
                    {
                        if (_statusTitles[i][gi] == value)
                        {
                            doBreak = true;
                            break;
                        }
                    }
                    if (doBreak)
                        break;
                }

                //reset old
                Label oldStatusLabel = _statusLabels[ActivatedPos][ActivatedPosInGroup];
                oldStatusLabel.ForeColor = GeneralLabel.ForeColor;
                oldStatusLabel.Font = GeneralLabel.Font;

                ActivatedPos = i;
                ActivatedPosInGroup = gi;

                //update new label
                Label statusLabel = _statusLabels[i][gi];
                Font fo = GeneralLabel.Font;
                Font fn = new Font(fo.FontFamily, fo.Size, FontStyle.Bold);
                statusLabel.Font = fn;
            }
        }

        /// <summary>
        /// color of activated status title
        /// </summary>
        public Color ActivatedStatusColor
        {
            get { return _statusLabels[ActivatedPos][ActivatedPosInGroup].ForeColor; }
            set { _statusLabels[ActivatedPos][ActivatedPosInGroup].ForeColor = value; }
        }

        public override Color BackColor
        {
            get { return flowLayoutPanel_statusProgress.BackColor; }
            set { flowLayoutPanel_statusProgress.BackColor = value; }
        }

        public StatusProgressBar()
        {
            InitializeComponent();
        }
    }
}
