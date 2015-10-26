using System;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Forms
{
    public partial class SyncConflictRetryDialog : Form
    {
        private Link _l;
        public SyncConflictRetryDialog(Link l)
        {
            _l = l;
            InitializeComponent();

            label_linkname.Text = _l.Title;
            label_conflictsCount.Text = (_l.SyncInfo.ConflictDirs.Count + _l.SyncInfo.ConflictFiles.Count).ToString();

            foreach (SyncDirInfo conflictDir in _l.SyncInfo.ConflictDirs)
            {
                listBox_conflicts.Items.Add($"Dir ({conflictDir.ConflictInfo.Type},{conflictDir.ConflictInfo.Context}): {conflictDir.ConflictInfo.GetAbsolutePath()}");
            }

            foreach (SyncFileInfo conflictFile in _l.SyncInfo.ConflictFiles)
            {
                listBox_conflicts.Items.Add($"File ({conflictFile.ConflictInfo.Type},{conflictFile.ConflictInfo.Context}): {conflictFile.ConflictInfo.GetAbsolutePath()}");
            }
        }

        private void button_yes_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_no_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
