using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class SyncDirTreeViewNode : SyncElementTreeViewNode
    {
        private bool _isExpanded;

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
        public SyncDirTreeViewNode(MyDirInfo dirInfo) : base(dirInfo) { }

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
        /// get child-dir-nodes (not recursively)
        /// </summary>
        public IEnumerable<SyncDirTreeViewNode> ChildDirNodes
        {
            get
            {
                foreach (SyncElementTreeViewNode node in Nodes)
                    if (node.GetType() == typeof(SyncDirTreeViewNode))
                        yield return (SyncDirTreeViewNode)node;
            }
        }

        /// <summary>
        /// load all child nodes (not recursively)
        /// </summary>
        public void LoadChildNodes()
        {
            Nodes.Clear();
            foreach (DirTree dir in DirInfo.DirTreeInfo.Dirs)
            {
                dir.Info.DirTreeViewNode = new SyncDirTreeViewNode(dir.Info);
                Nodes.Add(dir.Info.DirTreeViewNode);
            }

            foreach (MyFileInfo file in DirInfo.DirTreeInfo.Files)
            {
                file.FileTreeViewNode = new SyncFileTreeViewNode(file);
                Nodes.Add(file.FileTreeViewNode);
            }
        }

        /// <summary>
        /// update visual representation
        /// </summary>
        public override void Update()
        {
            var tnp = TNStatusProp;
            ForeColor = tnp.TextColor;
            ImageIndex = IsExpanded ? tnp.ExpandedFolderImageIndex : tnp.FolderImageIndex;
            SelectedImageIndex = ImageIndex;

            if (ChildStatus != null)
            {
                var tnpc = TreeNodeProperties.GetStatusProperties(ChildStatus.Value);

                if (tnpc.FolderImageIndex > ImageIndex)
                {
                    ImageIndex = IsExpanded ? tnpc.ExpandedFolderImageIndex : tnpc.FolderImageIndex;
                    SelectedImageIndex = ImageIndex;
                }
            }
        }

        /// <summary>
        /// toggle between expanded and collapsed state
        /// </summary>
        public new void Toggle()
        {
            if (IsExpanded) Collapse();
            else Expand();
        }

        /// <summary>
        /// expand treenode and load child elements
        /// </summary>
        public new void Expand()
        {
            _isExpanded = true;
            LoadChildNodes();
            base.Expand();
            Update();
        }

        /// <summary>
        /// collapse treenode
        /// </summary>
        public new void Collapse()
        {
            _isExpanded = false;
            base.Collapse();
            Update();
        }

        /// <summary>
        /// if the treenode is expanded
        /// </summary>
        public new bool IsExpanded
        {
            get { return _isExpanded; }
        }
    }
}
