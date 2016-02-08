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
            label_conflictsCount.Text = (_l.SyncInfo.ConflictInfos.Count).ToString();

            foreach (ConflictInfo conflictInfo in _l.SyncInfo.ConflictInfos)
            {
                listBox_conflicts.Items.Add($"{(conflictInfo.GetType() == typeof(FileConflictInfo) ? "File" : "Dir")} ({conflictInfo.Type},{conflictInfo.Context}): {conflictInfo.GetAbsolutePath()}");
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
