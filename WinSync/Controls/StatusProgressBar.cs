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
        private static Label HelperLabel => new Label()
        {
            Font = new Font("Microsoft Sans Serif", 8F),
            Margin = new Padding(1, 3, 1, 3),
            Height = 13,
            TextAlign = ContentAlignment.MiddleCenter,
            ForeColor = Color.FromArgb(150, 150, 150)
        };

        private static Label ArrowLabel {
            get
            {
                Label l = HelperLabel;
                l.Text = ">>";
                l.AutoSize = true;
                return l;
            }
        }

        private static Label OrLabel
        {
            get
            {
                Label l = HelperLabel;
                l.Text = "|";
                l.AutoSize = true;
                return l;
            }
        }

        private List<SyncStatus>[] _statuses;
        private List<Label>[] _statusLabels;
        private Label[] _arrowLabels;
        private List<Label>[] _orLabels;

        public int ActivatedPos { get; private set; }
        public int ActivatedPosInGroup { get; private set; }

        private SyncStatus _activatedStatus;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public SyncStatus ActivatedStatus
        {
            get { return _statuses[ActivatedPos][ActivatedPosInGroup]; }
            set
            {
                if (_activatedStatus == value)
                    return;

                _activatedStatus = value;

                int i = 0, gi = 0;
                bool doBreak = false;

                for (; i < _statuses.Length; i++)
                {
                    for (gi = 0; gi < _statuses[i].Count; gi++)
                    {
                        if (_statuses[i][gi] == value)
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
                oldStatusLabel.ForeColor = HelperLabel.ForeColor;
                oldStatusLabel.Font = HelperLabel.Font;

                ActivatedPos = i;
                ActivatedPosInGroup = gi;

                //update new label
                Label statusLabel = _statusLabels[i][gi];
                statusLabel.ForeColor = value.Color;
                Font fo = HelperLabel.Font;
                Font fn = new Font(fo.FontFamily, fo.Size, FontStyle.Bold);
                statusLabel.Font = fn;
            }
        }

        public override Color BackColor
        {
            get { return flowLayoutPanel_statusProgress.BackColor; }
            set { flowLayoutPanel_statusProgress.BackColor = value; }
        }

        public StatusProgressBar()
        {
            InitializeComponent();

            _statuses = SyncStatus.GetAllGrouped();

            _statusLabels = new List<Label>[_statuses.Length];
            _arrowLabels = new Label[_statuses.Length-1];
            _orLabels = new List<Label>[_statuses.Length];

            for (int i = 0; i < _statuses.Length; i++)
            {
                _statusLabels[i] = new List<Label>();
                _orLabels[i] = new List<Label>();

                if (i > 0)
                {
                    //add arrow
                    flowLayoutPanel_statusProgress.Controls.Add(_arrowLabels[i-1] = ArrowLabel);
                }

                int gi = 0;
                foreach(SyncStatus status in _statuses[i])
                {
                    Label statusLabel = HelperLabel;
                    statusLabel.Text = status.Title;
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
}
