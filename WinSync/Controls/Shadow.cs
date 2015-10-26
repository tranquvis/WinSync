using System.Windows.Forms;

namespace WinSync.Controls
{
    public partial class Shadow : UserControl
    {
        public Shadow()
        {
            InitializeComponent();
        }
        
        public Shadow(int width) : this()
        {
            Width = width;
        }
    }
}
