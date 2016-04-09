using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{

    /// <summary>
    /// visual tree node properties
    /// </summary>
    public class TreeNodeProperties
    {
        private static readonly StatusProperties[] statusPropertiesList;

        static TreeNodeProperties()
        {
            statusPropertiesList = new StatusProperties[6];

            statusPropertiesList[(int)SyncElementStatus.ElementFound] = new StatusProperties(
                Color.Black, 1, 5, 0);

            statusPropertiesList[(int)SyncElementStatus.ChangeDetectingStarted] = new StatusProperties(
                Color.Black, 1, 5, 0);

            statusPropertiesList[(int)SyncElementStatus.NoChangeFound] = new StatusProperties(
                Color.Black, 1, 5, 0);

            statusPropertiesList[(int)SyncElementStatus.ChangeFound] = new StatusProperties(
                Color.FromArgb(48, 109, 176), 2, 6, 0);

            statusPropertiesList[(int)SyncElementStatus.ChangeApplied] = new StatusProperties(
                Color.FromArgb(8, 127, 46), 3, 7, 0);

            statusPropertiesList[(int)SyncElementStatus.Conflicted] = new StatusProperties(
                Color.FromArgb(169, 36, 28), 4, 8, 0);
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
            public int ExpandedFolderImageIndex { get; }
            public int FileImageIndex { get; }

            public StatusProperties(Color textColor, int folderImageIndex, int expandedFolderImageIndex, int fileImageIndex)
            {
                TextColor = textColor;
                FolderImageIndex = folderImageIndex;
                ExpandedFolderImageIndex = expandedFolderImageIndex;
                FileImageIndex = fileImageIndex;
            }
        }
    }
}
