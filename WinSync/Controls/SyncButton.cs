using System.Drawing;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public sealed class SyncButton : Button
    {
        public static Image SyncImage = Properties.Resources.ic_sync_48px;
        public static Image CancelImage = Properties.Resources.ic_close_48px;

        public bool StateSync = true; // true if the sync button is visible

        public SyncButton()
        {
            BackColor = Color.White;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseOverBackColor = Color.White;
            BackgroundImage = SyncImage;
            BackgroundImageLayout = ImageLayout.Zoom;
            FlatStyle = FlatStyle.Flat;
            Cursor = Cursors.Hand;
        }

        public void SwitchToSync()
        {
            BackgroundImage = SyncImage;
            StateSync = true;
            Invalidate();
        }

        public void SwitchToCancel()
        {
            BackgroundImage = CancelImage;
            StateSync = false;
            Invalidate();
        }
    }
}
