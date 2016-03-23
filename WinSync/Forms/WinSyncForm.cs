using System;
using System.Collections.Generic;
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
            WindowBackColor = System.Drawing.Color.LightGray;
            ContentBackColor = System.Drawing.Color.White;
        }
    }
}
