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
}
