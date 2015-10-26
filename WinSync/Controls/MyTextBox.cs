using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public sealed class MyTextBox : Panel
    {
        public TextBox TextBox;

        public override Color BackColor
        {
            get { return TextBox.BackColor; }
            set { TextBox.BackColor = value; }
        }

        public override Color ForeColor
        {
            get { return TextBox.ForeColor; }
            set { TextBox.ForeColor = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color BorderColor { get; set; } = SystemColors.ActiveBorder;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Color FocusBorderColor { get; set; } = SystemColors.Highlight;

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get { return TextBox.Text; }
            set { TextBox.Text = value; }
        }

        public MyTextBox()
        {
            Padding = new Padding(3);
            Height = 22;
            DoubleBuffered = true;

            TextBox = new TextBox
            {
                AutoSize = false,
                Font = new Font("Ubuntu", 9F, FontStyle.Regular, GraphicsUnit.Point, 0),
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill
            };

            TextBox.Enter += EditBox_Refresh;
            TextBox.Leave += EditBox_Refresh;
            TextBox.Resize += EditBox_Refresh;
            Controls.Add(TextBox);
        }

        private void EditBox_Refresh(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Window);
            using (Pen borderPen = new Pen(TextBox.Focused ? FocusBorderColor : BorderColor))
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// Set the Border Colors and Invalidate 
        /// </summary>
        /// <param name="c">Border Color</param>
        /// <param name="focus">Border Color in focus: nullable</param>
        public void SetBorderColor(Color c, Color? focus)
        {
            BorderColor = c;
            FocusBorderColor = focus ?? c;
            Invalidate();
        }

        /// <summary>
        /// Change the Style of the textbox to show bad input
        /// </summary>
        public void SetBadInputState()
        {
            BorderColor = Color.Firebrick;
            Invalidate();
        }

        /// <summary>
        /// Set the Border Color to the init value
        /// </summary>
        public void RestoreBorderColor()
        {
            BorderColor = SystemColors.ActiveBorder;
            FocusBorderColor = SystemColors.Highlight;
            Invalidate();
        }
    }
}
