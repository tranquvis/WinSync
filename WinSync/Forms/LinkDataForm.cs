using System;
using System.IO;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Forms
{
    public partial class LinkDataForm : WinSyncForm
    {
        readonly Link _oldLink;

        /// <summary>
        /// create LinkDataForm for creating or editing a link
        /// </summary>
        /// <param name="link">if null a new link will be created</param>
        public LinkDataForm(Link link)
        {
            InitializeComponent();

            if (link != null)
            {
                _oldLink = link;
            }

            comboBox_direction.DataSource = SyncDirection.NameList;

            if(link != null)
            {
                textBox_title.Text = link.Title;
                textBox_folder1.Text = link.Path1;
                textBox_folder2.Text = link.Path2;
                comboBox_direction.SelectedIndex = link.Direction.Id;
                checkBox_remove.Checked = link.Remove;
                checkBox_identifyDrive1ByLabel.Checked = link.Drive1Label != null;
                checkBox_identifyDrive2ByLabel.Checked = link.Drive2Label != null;
                label_driveLabel1.Text = $"(label: {GetDriveLabelFromPath(link.Path1, checkBox_identifyDrive1ByLabel.Checked)})";
                label_driveLabel2.Text = $"(label: {GetDriveLabelFromPath(link.Path2, checkBox_identifyDrive2ByLabel.Checked)})";
            }
        }

        private void button_folder1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = textBox_folder1.Text;
            fbd.ShowDialog();
            if (fbd.SelectedPath.Length != 0)
                textBox_folder1.Text = fbd.SelectedPath;

            label_driveLabel1.Text = $"(label: {GetDriveLabelFromPath(fbd.SelectedPath, checkBox_identifyDrive1ByLabel.Checked)})";
        }

        private void button_folder2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = textBox_folder2.Text;
            fbd.ShowDialog();
            if (fbd.SelectedPath.Length != 0)
                textBox_folder2.Text = fbd.SelectedPath;

            label_driveLabel2.Text = $"(label: {GetDriveLabelFromPath(fbd.SelectedPath, checkBox_identifyDrive2ByLabel.Checked)})";
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            #region check input
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
            bool identifyDrive1ByLabel = checkBox_identifyDrive1ByLabel.Checked;
            bool identifyDrive2ByLabel = checkBox_identifyDrive2ByLabel.Checked;
            #endregion

            string driveLabel1 = null;
            string driveLabel2 = null;
            if (identifyDrive1ByLabel)
            {
                driveLabel1 = GetDriveLabelFromPath(path1, true);
                if (driveLabel1 == null) return;
            }
            if (identifyDrive1ByLabel)
            {
                driveLabel2 = GetDriveLabelFromPath(path1, true);
                if (driveLabel2 == null) return;
            }

            if (error) return;

            try
            {
                Link link = new Link(title, path1, path2, direction, remove, driveLabel1, driveLabel2);

                if (_oldLink != null)
                {
                    //edit link
                    DataManager.ChangeLink(link, _oldLink.Title);
                }
                else
                {
                    //create new link
                    DataManager.AddLink(link);
                }
                Close();
            }
            catch(BadInputException me)
            {
                me.ShowMsgBox();
            }
        }


        private string GetDriveLabelFromPath(string path, bool showException)
        {
            string label = "";
            try
            {
                label = Helper.GetDriveLabelFromLetter(path[0]);
            }
            catch(Exception e)
            {
                if(showException)
                    MessageBox.Show("The Drive Label could not be loaded! \n" + e.Message, "Drive Label load fail", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return label;
        }
    }
}
