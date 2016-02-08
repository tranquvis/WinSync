using System.Drawing;

namespace WinSync.Service
{
    public class SyncState
    {
        public static SyncState DetectingChanges = new SyncState(Color.FromArgb(73, 175, 230), "detecting changes");
        public static SyncState CreatingFolders = new SyncState(Color.FromArgb(64, 152, 230), "creating folders");
        public static SyncState ApplyingFileChanges = new SyncState(Color.FromArgb(31, 118, 194), "applying file changes");
        public static SyncState RemoveRedundantDirs = new SyncState(Color.FromArgb(31, 118, 150), "remove redundant directories");
        public static SyncState Finished = new SyncState(Color.FromArgb(0, 136, 255), "finished");
        public static SyncState Aborted = new SyncState(Color.FromArgb(9, 64, 112), "aborted");

        private SyncState(Color color, string title)
        {
            Color = color;
            Title = title;
        }

        public Color Color { get; }
        public string Title { get; }
    }
}
