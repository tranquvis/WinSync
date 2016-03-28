using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncFileTreeViewNode : SyncElementTreeViewNode
    {
        public MyFileInfo FileInfo
        {
            get { return (MyFileInfo)ElementInfo; }
            set { ElementInfo = value; }
        }

        /// <summary>
        /// create TreeNode representing a SyncFile
        /// and update its visual representation
        /// </summary>
        /// <param name="fileInfo">file info</param>
        public SyncFileTreeViewNode(MyFileInfo fileInfo) : base(fileInfo) {}

        /// <summary>
        /// update visual representation
        /// </summary>
        public override void Update()
        {
            if(FileInfo.SyncFileInfo != null)
            {
                var tnp = TNStatusProp;
                ImageIndex = tnp.FileImageIndex;
                SelectedImageIndex = tnp.FileImageIndex;
                ForeColor = tnp.TextColor;
            }
        }
    }
}
