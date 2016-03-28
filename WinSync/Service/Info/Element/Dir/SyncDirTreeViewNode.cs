using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncDirTreeViewNode : SyncElementTreeViewNode
    {
        public MyDirInfo DirInfo
        {
            get { return (MyDirInfo)ElementInfo; }
            set { ElementInfo = value; }
        }

        /// <summary>
        /// highest SyncElementStatus of child elements
        /// </summary>
        public SyncElementStatus? ChildStatus { get; set; }

        /// <summary>
        /// reate TreeNode representing a SyncDir 
        /// and update its visual representation
        /// </summary>
        /// <param name="dirInfo">directory info</param>
        public SyncDirTreeViewNode(MyDirInfo dirInfo) : base(dirInfo) {}

        /// <summary>
        /// get child node by name (only the next layer is examined)
        /// </summary>
        /// <param name="name">child node name</param>
        /// <returns>SyncElementTreeViewNode</returns>
        public SyncElementTreeViewNode GetChildNode(string name)
        {
            return (SyncElementTreeViewNode)Nodes[name];
        }

        /// <summary>
        /// update visual representation
        /// </summary>
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
