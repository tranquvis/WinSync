using System.Windows.Forms;

namespace WinSync.Controls
{
    public partial class Shadow : UserControl
    {
        /// <summary>
        /// create Shadow for bottom
        /// </summary>
        public Shadow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// create Shadow for bottom
        /// </summary>
        /// <param name="width">shadow width</param>
        public Shadow(int width) : this()
        {
            Width = width;
        }
    }
}
