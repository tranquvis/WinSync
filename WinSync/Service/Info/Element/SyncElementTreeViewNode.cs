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

        /// <summary>
        /// create TreeNode representing a SyncElement 
        /// and update its visual representation
        /// </summary>
        /// <param name="elementInfo">element info</param>
        public SyncElementTreeViewNode(MyElementInfo elementInfo) : base(elementInfo.Name)
        {
            elementInfo.ElementTreeViewNode = this;
            ElementInfo = elementInfo;
            Name = elementInfo.Name;
            Update();
        }

        /// <summary>
        /// get properties associated with the current SyncElementStatus
        /// </summary>
        protected TreeNodeProperties.StatusProperties TNStatusProp => 
            TreeNodeProperties.GetStatusProperties(ElementInfo.SyncElementInfo.SyncStatus);

        /// <summary>
        /// update visual representation
        /// </summary>
        public abstract void Update();
    }

    /// <summary>
    /// visual tree node properties
    /// </summary>
    public class TreeNodeProperties
    {
        private static readonly StatusProperties[] statusPropertiesList;

        static TreeNodeProperties()
        {
            statusPropertiesList = new StatusProperties[6];
            statusPropertiesList[(int)SyncElementStatus.ElementFound] = new StatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeDetectingStarted] = new StatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.NoChangeFound] = new StatusProperties(Color.Black, 1, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeFound] = new StatusProperties(Color.Blue, 2, 0);
            statusPropertiesList[(int)SyncElementStatus.ChangeApplied] = new StatusProperties(Color.Green, 3, 0);
            statusPropertiesList[(int)SyncElementStatus.Conflicted] = new StatusProperties(Color.Red, 4, 0);
        }

        /// <summary>
        /// get properties associated with SyncElementStatus
        /// </summary>
        /// <param name="syncElementStatus">status</param>
        /// <returns></returns>
        public static StatusProperties GetStatusProperties(SyncElementStatus syncElementStatus)
        {
            return statusPropertiesList[(int)syncElementStatus];
        }

        /// <summary>
        /// properties associated with SyncElementStatus
        /// </summary>
        public class StatusProperties
        {
            public Color TextColor { get; }

            public int FolderImageIndex { get; }
            
            public int FileImageIndex { get; }

            public StatusProperties(Color textColor, int folderImageIndex, int fileImageIndex)
            {
                TextColor = textColor;
                FolderImageIndex = folderImageIndex;
                FileImageIndex = fileImageIndex;
            }
        }
    }
}
