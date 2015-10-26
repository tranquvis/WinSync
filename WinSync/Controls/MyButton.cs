using System.Drawing;
using System.Windows.Forms;

namespace WinSync.Controls
{
    public sealed class MyButton : Button
    {
        public MyButton()
        {
            Height = 27;
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            ForeColor = Color.White;
            BackColor = Color.FromArgb(106,148,255);
            FlatAppearance.MouseOverBackColor = Color.FromArgb(39,66,135);
            Font = new Font("Ubuntu", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Cursor = Cursors.Hand;
        }
    }
}
