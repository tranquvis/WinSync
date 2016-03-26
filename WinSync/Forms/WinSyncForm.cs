using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Forms
{
    public class WinSyncForm : tranquvis.Utils.WinFormDesign.CustomDesignForm
    {
        public WinSyncForm()
        {
            IconSrc = Properties.Resources.WinSync_Label;
            WindowBackColor = Color.LightGray;
            ContentBackColor = Color.White;
            CaptionBarHeight = 25;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.DimGray, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        }
    }
}
