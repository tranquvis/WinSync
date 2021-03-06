﻿using WinSync.Controls;

namespace WinSync.Forms
{
    partial class LinkDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkDataForm));
            this.label_errorTitle = new System.Windows.Forms.Label();
            this.textBox_title = new WinSync.Controls.MyTextBox();
            this.checkBox_remove = new System.Windows.Forms.CheckBox();
            this.button_save = new WinSync.Controls.MyButton();
            this.button_cancel = new WinSync.Controls.MyButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_errorFolder2 = new System.Windows.Forms.Label();
            this.textBox_folder1 = new WinSync.Controls.MyTextBox();
            this.label_errorFolder1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_folder2 = new WinSync.Controls.MyButton();
            this.textBox_folder2 = new WinSync.Controls.MyTextBox();
            this.button_folder1 = new WinSync.Controls.MyButton();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_direction = new System.Windows.Forms.ComboBox();
            this.checkBox_identifyDrive1ByLabel = new System.Windows.Forms.CheckBox();
            this.label_driveLabel1 = new System.Windows.Forms.Label();
            this.label_driveLabel2 = new System.Windows.Forms.Label();
            this.checkBox_identifyDrive2ByLabel = new System.Windows.Forms.CheckBox();
            this._contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this.label_driveLabel2);
            this._contentPanel.Controls.Add(this.checkBox_identifyDrive2ByLabel);
            this._contentPanel.Controls.Add(this.label_driveLabel1);
            this._contentPanel.Controls.Add(this.checkBox_identifyDrive1ByLabel);
            this._contentPanel.Controls.Add(this.textBox_title);
            this._contentPanel.Controls.Add(this.checkBox_remove);
            this._contentPanel.Controls.Add(this.button_save);
            this._contentPanel.Controls.Add(this.button_cancel);
            this._contentPanel.Controls.Add(this.label1);
            this._contentPanel.Controls.Add(this.label2);
            this._contentPanel.Controls.Add(this.label_errorFolder2);
            this._contentPanel.Controls.Add(this.textBox_folder1);
            this._contentPanel.Controls.Add(this.label_errorFolder1);
            this._contentPanel.Controls.Add(this.label3);
            this._contentPanel.Controls.Add(this.button_folder2);
            this._contentPanel.Controls.Add(this.textBox_folder2);
            this._contentPanel.Controls.Add(this.button_folder1);
            this._contentPanel.Controls.Add(this.label4);
            this._contentPanel.Controls.Add(this.comboBox_direction);
            this._contentPanel.Controls.Add(this.label_errorTitle);
            this._contentPanel.Location = new System.Drawing.Point(5, 30);
            this._contentPanel.Size = new System.Drawing.Size(420, 308);
            // 
            // label_errorTitle
            // 
            this.label_errorTitle.AutoSize = true;
            this.label_errorTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorTitle.Location = new System.Drawing.Point(86, 12);
            this.label_errorTitle.Name = "label_errorTitle";
            this.label_errorTitle.Size = new System.Drawing.Size(0, 13);
            this.label_errorTitle.TabIndex = 18;
            // 
            // textBox_title
            // 
            this.textBox_title.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_title.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_title.Location = new System.Drawing.Point(80, 23);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_title.Size = new System.Drawing.Size(150, 22);
            this.textBox_title.TabIndex = 33;
            // 
            // checkBox_remove
            // 
            this.checkBox_remove.AutoSize = true;
            this.checkBox_remove.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_remove.Location = new System.Drawing.Point(11, 226);
            this.checkBox_remove.Name = "checkBox_remove";
            this.checkBox_remove.Size = new System.Drawing.Size(241, 17);
            this.checkBox_remove.TabIndex = 44;
            this.checkBox_remove.Text = "Remove Files/Folders in Destination Directory";
            this.checkBox_remove.UseVisualStyleBackColor = false;
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.button_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_save.ForeColor = System.Drawing.Color.White;
            this.button_save.Location = new System.Drawing.Point(337, 268);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 27);
            this.button_save.TabIndex = 30;
            this.button_save.Text = "Save";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_cancel.ForeColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(8, 268);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 27);
            this.button_cancel.TabIndex = 31;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Folder 1";
            // 
            // label_errorFolder2
            // 
            this.label_errorFolder2.AutoSize = true;
            this.label_errorFolder2.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorFolder2.Location = new System.Drawing.Point(84, 112);
            this.label_errorFolder2.Name = "label_errorFolder2";
            this.label_errorFolder2.Size = new System.Drawing.Size(0, 13);
            this.label_errorFolder2.TabIndex = 43;
            // 
            // textBox_folder1
            // 
            this.textBox_folder1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_folder1.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_folder1.Location = new System.Drawing.Point(80, 67);
            this.textBox_folder1.Name = "textBox_folder1";
            this.textBox_folder1.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_folder1.Size = new System.Drawing.Size(279, 22);
            this.textBox_folder1.TabIndex = 35;
            // 
            // label_errorFolder1
            // 
            this.label_errorFolder1.AutoSize = true;
            this.label_errorFolder1.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorFolder1.Location = new System.Drawing.Point(82, 56);
            this.label_errorFolder1.Name = "label_errorFolder1";
            this.label_errorFolder1.Size = new System.Drawing.Size(0, 13);
            this.label_errorFolder1.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(9, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Folder 2";
            // 
            // button_folder2
            // 
            this.button_folder2.BackColor = System.Drawing.Color.DarkGray;
            this.button_folder2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_folder2.FlatAppearance.BorderSize = 0;
            this.button_folder2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button_folder2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_folder2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_folder2.ForeColor = System.Drawing.Color.White;
            this.button_folder2.Location = new System.Drawing.Point(367, 123);
            this.button_folder2.Name = "button_folder2";
            this.button_folder2.Size = new System.Drawing.Size(27, 22);
            this.button_folder2.TabIndex = 41;
            this.button_folder2.Text = "...";
            this.button_folder2.UseVisualStyleBackColor = false;
            this.button_folder2.Click += new System.EventHandler(this.button_folder2_Click);
            // 
            // textBox_folder2
            // 
            this.textBox_folder2.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_folder2.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_folder2.Location = new System.Drawing.Point(82, 123);
            this.textBox_folder2.Name = "textBox_folder2";
            this.textBox_folder2.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_folder2.Size = new System.Drawing.Size(279, 22);
            this.textBox_folder2.TabIndex = 37;
            // 
            // button_folder1
            // 
            this.button_folder1.BackColor = System.Drawing.Color.DarkGray;
            this.button_folder1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_folder1.FlatAppearance.BorderSize = 0;
            this.button_folder1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button_folder1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_folder1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_folder1.ForeColor = System.Drawing.Color.White;
            this.button_folder1.Location = new System.Drawing.Point(365, 67);
            this.button_folder1.Name = "button_folder1";
            this.button_folder1.Size = new System.Drawing.Size(27, 22);
            this.button_folder1.TabIndex = 40;
            this.button_folder1.Text = "...";
            this.button_folder1.UseVisualStyleBackColor = false;
            this.button_folder1.Click += new System.EventHandler(this.button_folder1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Direction";
            // 
            // comboBox_direction
            // 
            this.comboBox_direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Location = new System.Drawing.Point(81, 181);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(121, 21);
            this.comboBox_direction.TabIndex = 39;
            // 
            // checkBox_identifyDrive1ByLabel
            // 
            this.checkBox_identifyDrive1ByLabel.AutoSize = true;
            this.checkBox_identifyDrive1ByLabel.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_identifyDrive1ByLabel.Location = new System.Drawing.Point(100, 95);
            this.checkBox_identifyDrive1ByLabel.Name = "checkBox_identifyDrive1ByLabel";
            this.checkBox_identifyDrive1ByLabel.Size = new System.Drawing.Size(140, 17);
            this.checkBox_identifyDrive1ByLabel.TabIndex = 45;
            this.checkBox_identifyDrive1ByLabel.Text = "Identify Drive by its label";
            this.checkBox_identifyDrive1ByLabel.UseVisualStyleBackColor = false;
            // 
            // label_driveLabel1
            // 
            this.label_driveLabel1.AutoSize = true;
            this.label_driveLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.label_driveLabel1.Location = new System.Drawing.Point(247, 96);
            this.label_driveLabel1.Name = "label_driveLabel1";
            this.label_driveLabel1.Size = new System.Drawing.Size(41, 13);
            this.label_driveLabel1.TabIndex = 46;
            this.label_driveLabel1.Text = "(label: )";
            // 
            // label_driveLabel2
            // 
            this.label_driveLabel2.AutoSize = true;
            this.label_driveLabel2.ForeColor = System.Drawing.Color.DimGray;
            this.label_driveLabel2.Location = new System.Drawing.Point(247, 152);
            this.label_driveLabel2.Name = "label_driveLabel2";
            this.label_driveLabel2.Size = new System.Drawing.Size(41, 13);
            this.label_driveLabel2.TabIndex = 48;
            this.label_driveLabel2.Text = "(label: )";
            // 
            // checkBox_identifyDrive2ByLabel
            // 
            this.checkBox_identifyDrive2ByLabel.AutoSize = true;
            this.checkBox_identifyDrive2ByLabel.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_identifyDrive2ByLabel.Location = new System.Drawing.Point(100, 151);
            this.checkBox_identifyDrive2ByLabel.Name = "checkBox_identifyDrive2ByLabel";
            this.checkBox_identifyDrive2ByLabel.Size = new System.Drawing.Size(140, 17);
            this.checkBox_identifyDrive2ByLabel.TabIndex = 47;
            this.checkBox_identifyDrive2ByLabel.Text = "Identify Drive by its label";
            this.checkBox_identifyDrive2ByLabel.UseVisualStyleBackColor = false;
            // 
            // LinkDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 343);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkDataForm";
            this.Padding = new System.Windows.Forms.Padding(5, 30, 5, 5);
            this.Text = "Edit Link";
            this.Controls.SetChildIndex(this._contentPanel, 0);
            this._contentPanel.ResumeLayout(false);
            this._contentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_errorTitle;
        private MyTextBox textBox_title;
        private System.Windows.Forms.CheckBox checkBox_remove;
        private MyButton button_save;
        private MyButton button_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_errorFolder2;
        private MyTextBox textBox_folder1;
        private System.Windows.Forms.Label label_errorFolder1;
        private System.Windows.Forms.Label label3;
        private MyButton button_folder2;
        private MyTextBox textBox_folder2;
        private MyButton button_folder1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_direction;
        private System.Windows.Forms.CheckBox checkBox_identifyDrive1ByLabel;
        private System.Windows.Forms.Label label_driveLabel1;
        private System.Windows.Forms.Label label_driveLabel2;
        private System.Windows.Forms.CheckBox checkBox_identifyDrive2ByLabel;
    }
}