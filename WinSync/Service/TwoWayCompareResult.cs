using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSync.Service
{
    public class TwoWayCompareResult
    {
        /// <summary>
        /// synchronisation direction
        /// </summary>
        public SyncDirection Direction { get; set; }

        /// <summary>
        /// if destination element should be removed
        /// </summary>
        public bool Remove { get; set; }

        /// <summary>
        /// create TwoWayCompareResult
        /// </summary>
        /// <param name="direction">synchronisation direction</param>
        /// <param name="remove">if destination element should be removed</param>
        public TwoWayCompareResult(SyncDirection direction, bool remove)
        {
            Direction = direction;
            Remove = remove;
        }
    }
}
