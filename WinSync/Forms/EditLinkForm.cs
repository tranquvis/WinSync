using System;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Forms
{
    public partial class EditLinkForm : Form
    {
        readonly string _oldName;
        Link _link;

        public EditLinkForm(Link link)
        {
            InitializeComponent();
            _link = link;

            _oldName = link.Title;

            comboBox_direction.DataSource = SyncDirection.NameList;
            comboBox_direction.SelectedIndex = link.Direction.Id;

            textBox_title.Text = link.Title;
            textBox_folder1.Text = link.Path1;
            textBox_folder2.Text = link.Path2;
            checkBox_remove.Checked = link.Remove;
        }

        private void button_folder1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath.Length != 0)
                textBox_folder1.Text = fbd.SelectedPath;
        }

        private void button_folder2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath.Length != 0)
                textBox_folder2.Text = fbd.SelectedPath;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            bool error = false;
            string title = textBox_title.Text;
            if (title.Length == 0)
            {
                textBox_title.SetBadInputState();
                label_errorTitle.Text = @"the title must not be empty";
                error = true;
            }
            else
            {
                textBox_title.RestoreBorderColor();
                label_errorTitle.Text = "";
            }

            string path1 = textBox_folder1.Text;
            if (path1.Length == 0)
            {
                textBox_folder1.SetBadInputState();
                label_errorFolder1.Text = @"Folder 1 must not be empty";
                error = true;
            }
            else
            {
                textBox_folder1.RestoreBorderColor();
                label_errorFolder1.Text = "";
            }

            string path2 = textBox_folder2.Text;
            if (path2.Length == 0)
            {
                textBox_folder2.SetBadInputState();
                label_errorFolder2.Text = @"Folder 2 must not be empty";
                error = true;
            }
            else
            {
                textBox_folder2.RestoreBorderColor();
                label_errorFolder2.Text = "";
            }

            SyncDirection direction = SyncDirection.FromValue(comboBox_direction.SelectedIndex);

            bool remove = checkBox_remove.Checked;

            if (error) return;

            _link.Title = title;
            _link.Path1 = path1;
            _link.Path2 = path2;
            _link.Direction = direction;
            _link.Remove = remove;

            try
            {
                DataManager.UpdateLink(_link, _oldName);
                Close();
            }
            catch(MyException me)
            {
                me.ShowMsgBox();
            }
        }
    }
}
