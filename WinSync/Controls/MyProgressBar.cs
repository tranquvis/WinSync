using System.Drawing;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public sealed class MyProgressBar : ProgressBar
    {
        public MyProgressBar()
        {
            Margin = new Padding(5, 5, 5, 5);
            BackColor = Color.White;
            ForeColor = Color.FromArgb(0,100,100);
            Maximum = 1000;

            SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// custom paint progress bar to change its color
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;
            int w = rec.Width;
            rec.Width = (int)(rec.Width * ((double)Value / Maximum));

            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);

            e.Graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, w, rec.Height);
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), 0, 0, rec.Width, rec.Height);
        }
    }
}
