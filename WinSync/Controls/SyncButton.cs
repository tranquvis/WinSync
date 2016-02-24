using System.Drawing;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public sealed class SyncButton : Button
    {
        public static Image SyncImage = Properties.Resources.ic_sync_48px;
        public static Image CancelImage = Properties.Resources.ic_close_48px;

        public bool StateSync = true; // true if the sync button is visible

        /// <summary>
        /// create SyncButton, which can switch between sync and cancel buttons
        /// </summary>
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

        /// <summary>
        /// switch button appearance to display sync button
        /// </summary>
        public void SwitchToSync()
        {
            if (StateSync) return;

            BackgroundImage = SyncImage;
            StateSync = true;
            Invalidate();
        }

        /// <summary>
        /// switch button appearance to display cancel button
        /// </summary>
        public void SwitchToCancel()
        {
            if (!StateSync) return;

            BackgroundImage = CancelImage;
            StateSync = false;
            Invalidate();
        }
    }
}
