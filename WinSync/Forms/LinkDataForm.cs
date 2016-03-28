﻿using System;
using System.Windows.Forms;
using WinSync.Data;
using WinSync.Service;

namespace WinSync.Forms
{
    public partial class LinkDataForm : WinSyncForm
    {
        readonly Link _oldLink;
        Link _newLink;

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
                _newLink = link.Clone();
            }

            comboBox_direction.DataSource = SyncDirection.NameList;

            if(link != null)
            {
                textBox_title.Text = link.Title;
                textBox_folder1.Text = link.Path1;
                textBox_folder2.Text = link.Path2;
                comboBox_direction.SelectedIndex = link.Direction.Id;
                checkBox_remove.Checked = link.Remove;
            }
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
            #endregion

            if (error) return;


            try
            {
                if (_oldLink != null)
                {
                    //edit link
                    _newLink.Title = title;
                    _newLink.Path1 = path1;
                    _newLink.Path2 = path2;
                    _newLink.Direction = direction;
                    _newLink.Remove = remove;
                    DataManager.ChangeLink(_newLink, _oldLink.Title);
                }
                else
                {
                    //create new link
                    _newLink = new Link(title, path1, path2, direction, remove);
                    DataManager.AddLink(_newLink);
                }
                Close();
            }
            catch(BadInputException me)
            {
                me.ShowMsgBox();
            }
        }
    }
}