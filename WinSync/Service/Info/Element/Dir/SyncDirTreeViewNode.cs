using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncDirTreeViewNode : SyncElementTreeViewNode
    {
        public MyDirInfo DirInfo => (MyDirInfo)ElementInfo;

        public SyncElementStatus? ChildStatus { get; set; }

        public SyncDirTreeViewNode(MyDirInfo dirInfo) : base(dirInfo)
        {}
        
        public SyncElementTreeViewNode GetChildNode(string name)
        {
            return (SyncElementTreeViewNode)Nodes[name];
        }

        public override void Update()
        {
            if(DirInfo.SyncDirInfo != null)
            {
                var tnp = TNStatusProp;
                ForeColor = tnp.TextColor;
                ImageIndex = tnp.FolderImageIndex;
                SelectedImageIndex = tnp.FolderImageIndex;
            }
            
            if (ChildStatus != null)
            {
                var tnpc = TreeNodeProperties.GetStatusProperties(ChildStatus.Value);

                if(tnpc.FolderImageIndex > ImageIndex)
                {
                    ImageIndex = tnpc.FolderImageIndex;
                    SelectedImageIndex = tnpc.FolderImageIndex;
                }
            }
        }
    }
}
