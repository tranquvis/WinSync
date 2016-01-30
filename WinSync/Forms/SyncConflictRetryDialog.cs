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

            foreach (MyDirInfo conflictDir in _l.SyncInfo.ConflictDirs)
            {
                listBox_conflicts.Items.Add($"Dir ({conflictDir.SyncInfo.ConflictInfo.Type},{conflictDir.SyncInfo.ConflictInfo.Context}): {conflictDir.SyncInfo.ConflictInfo.GetAbsolutePath()}");
            }

            foreach (MyFileInfo conflictFile in _l.SyncInfo.ConflictFiles)
            {
                listBox_conflicts.Items.Add($"File ({conflictFile.SyncInfo.ConflictInfo.Type},{conflictFile.SyncInfo.ConflictInfo.Context}): {conflictFile.SyncInfo.ConflictInfo.GetAbsolutePath()}");
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
