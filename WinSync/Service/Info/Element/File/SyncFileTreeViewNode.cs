using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncFileTreeViewNode : SyncElementTreeViewNode
    {
        public MyFileInfo FileInfo => (MyFileInfo)ElementInfo;

        public SyncFileTreeViewNode(MyFileInfo fileInfo) : base(fileInfo)
        {}

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
