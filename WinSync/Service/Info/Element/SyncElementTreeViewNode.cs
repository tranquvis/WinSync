using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSync.Service
{
    public abstract class SyncElementTreeViewNode : TreeNode
    {
        public MyElementInfo ElementInfo { get; protected set; }

        public SyncElementTreeViewNode(MyElementInfo elementInfo) : base(elementInfo.Name)
        {
            elementInfo.ElementTreeViewNode = this;
            ElementInfo = elementInfo;
            Name = elementInfo.Name;
            Update();
        }
        
        protected TreeNodeProperties.TreeNodeStatusProperties TNStatusProp => 
            TreeNodeProperties.GetStatusProperties(ElementInfo.SyncElementInfo.SyncStatus);

        public abstract void Update();
    }

    public class TreeNodeProperties
    {
        private static readonly TreeNodeStatusProperties[] statusPropertiesList;

        static TreeNodeProperties()
        {
            statusPropertiesList = new TreeNodeStatusProperties[6];
            statusPropertiesList[(int)SyncElementStatus.ElementFound] = new TreeNodeStatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeDetectingStarted] = new TreeNodeStatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.NoChangeFound] = new TreeNodeStatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeFound] = new TreeNodeStatusProperties(Color.Blue, 2, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeApplied] = new TreeNodeStatusProperties(Color.Green, 3, 0);
            statusPropertiesList[(int)SyncElementStatus.Conflicted] = new TreeNodeStatusProperties(Color.Red, 4, 0);
        }

        public static TreeNodeStatusProperties GetStatusProperties(SyncElementStatus syncElementStatus)
        {
            return statusPropertiesList[(int)syncElementStatus];
        }

        public TreeNodeStatusProperties StatusProperties { get; }

        public class TreeNodeStatusProperties
        {
            public Color TextColor { get; }
            public int FolderImageIndex { get; }
            public int FileImageIndex { get; }

            public TreeNodeStatusProperties(Color textColor, int folderImageIndex, int fileImageIndex)
            {
                TextColor = textColor;
                FolderImageIndex = folderImageIndex;
                FileImageIndex = fileImageIndex;
            }
        }
    }
}
