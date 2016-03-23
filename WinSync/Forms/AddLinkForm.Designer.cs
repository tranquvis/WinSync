using WinSync.Controls;

namespace WinSync.Forms
{
    partial class AddLinkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLinkForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_direction = new System.Windows.Forms.ComboBox();
            this.label_errorTitle = new System.Windows.Forms.Label();
            this.label_errorFolder1 = new System.Windows.Forms.Label();
            this.label_errorFolder2 = new System.Windows.Forms.Label();
            this.button_folder2 = new WinSync.Controls.MyButton();
            this.button_folder1 = new WinSync.Controls.MyButton();
            this.textBox_folder2 = new WinSync.Controls.MyTextBox();
            this.textBox_folder1 = new WinSync.Controls.MyTextBox();
            this.textBox_title = new WinSync.Controls.MyTextBox();
            this.button_cancel = new WinSync.Controls.MyButton();
            this.button_create = new WinSync.Controls.MyButton();
            this.checkBox_remove = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Folder 1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(13, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Folder 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Direction";
            // 
            // comboBox_direction
            // 
            this.comboBox_direction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Location = new System.Drawing.Point(91, 167);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(121, 21);
            this.comboBox_direction.TabIndex = 10;
            // 
            // label_errorTitle
            // 
            this.label_errorTitle.AutoSize = true;
            this.label_errorTitle.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorTitle.Location = new System.Drawing.Point(87, 6);
            this.label_errorTitle.Name = "label_errorTitle";
            this.label_errorTitle.Size = new System.Drawing.Size(0, 13);
            this.label_errorTitle.TabIndex = 19;
            // 
            // label_errorFolder1
            // 
            this.label_errorFolder1.AutoSize = true;
            this.label_errorFolder1.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorFolder1.Location = new System.Drawing.Point(88, 70);
            this.label_errorFolder1.Name = "label_errorFolder1";
            this.label_errorFolder1.Size = new System.Drawing.Size(0, 13);
            this.label_errorFolder1.TabIndex = 20;
            // 
            // label_errorFolder2
            // 
            this.label_errorFolder2.AutoSize = true;
            this.label_errorFolder2.ForeColor = System.Drawing.Color.Firebrick;
            this.label_errorFolder2.Location = new System.Drawing.Point(88, 112);
            this.label_errorFolder2.Name = "label_errorFolder2";
            this.label_errorFolder2.Size = new System.Drawing.Size(0, 13);
            this.label_errorFolder2.TabIndex = 21;
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
            this.button_folder2.Location = new System.Drawing.Point(376, 128);
            this.button_folder2.Name = "button_folder2";
            this.button_folder2.Size = new System.Drawing.Size(27, 22);
            this.button_folder2.TabIndex = 12;
            this.button_folder2.Text = "...";
            this.button_folder2.UseVisualStyleBackColor = false;
            this.button_folder2.Click += new System.EventHandler(this.button_folder2_Click);
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
            this.button_folder1.Location = new System.Drawing.Point(376, 86);
            this.button_folder1.Name = "button_folder1";
            this.button_folder1.Size = new System.Drawing.Size(27, 22);
            this.button_folder1.TabIndex = 11;
            this.button_folder1.Text = "...";
            this.button_folder1.UseVisualStyleBackColor = false;
            this.button_folder1.Click += new System.EventHandler(this.button_folder1_Click);
            // 
            // textBox_folder2
            // 
            this.textBox_folder2.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_folder2.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_folder2.Location = new System.Drawing.Point(91, 128);
            this.textBox_folder2.Name = "textBox_folder2";
            this.textBox_folder2.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_folder2.Size = new System.Drawing.Size(279, 22);
            this.textBox_folder2.TabIndex = 7;
            // 
            // textBox_folder1
            // 
            this.textBox_folder1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_folder1.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_folder1.Location = new System.Drawing.Point(91, 86);
            this.textBox_folder1.Name = "textBox_folder1";
            this.textBox_folder1.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_folder1.Size = new System.Drawing.Size(279, 22);
            this.textBox_folder1.TabIndex = 5;
            // 
            // textBox_title
            // 
            this.textBox_title.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBox_title.FocusBorderColor = System.Drawing.SystemColors.Highlight;
            this.textBox_title.Location = new System.Drawing.Point(91, 43);
            this.textBox_title.Name = "textBox_title";
            this.textBox_title.Padding = new System.Windows.Forms.Padding(3);
            this.textBox_title.Size = new System.Drawing.Size(150, 22);
            this.textBox_title.TabIndex = 3;
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_cancel.ForeColor = System.Drawing.Color.White;
            this.button_cancel.Location = new System.Drawing.Point(12, 252);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 27);
            this.button_cancel.TabIndex = 1;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_create
            // 
            this.button_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.button_create.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_create.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.button_create.ForeColor = System.Drawing.Color.White;
            this.button_create.Location = new System.Drawing.Point(359, 252);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(75, 27);
            this.button_create.TabIndex = 0;
            this.button_create.Text = "Create";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // checkBox_remove
            // 
            this.checkBox_remove.AutoSize = true;
            this.checkBox_remove.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_remove.Location = new System.Drawing.Point(16, 205);
            this.checkBox_remove.Name = "checkBox_remove";
            this.checkBox_remove.Size = new System.Drawing.Size(241, 17);
            this.checkBox_remove.TabIndex = 23;
            this.checkBox_remove.Text = "Remove Files/Folders in Destination Directory";
            this.checkBox_remove.UseVisualStyleBackColor = false;
            // 
            // AddLinkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 287);
            this.Controls.Add(this.checkBox_remove);
            this.Controls.Add(this.label_errorFolder2);
            this.Controls.Add(this.label_errorFolder1);
            this.Controls.Add(this.label_errorTitle);
            this.Controls.Add(this.button_folder2);
            this.Controls.Add(this.button_folder1);
            this.Controls.Add(this.comboBox_direction);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_folder2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_folder1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_create);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddLinkForm";
            this.Text = "Add Link";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyButton button_create;
        private MyButton button_cancel;
        private System.Windows.Forms.Label label1;
        private MyTextBox textBox_title;
        private MyTextBox textBox_folder1;
        private System.Windows.Forms.Label label2;
        private MyTextBox textBox_folder2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_direction;
        private MyButton button_folder1;
        private MyButton button_folder2;
        private System.Windows.Forms.Label label_errorTitle;
        private System.Windows.Forms.Label label_errorFolder1;
        private System.Windows.Forms.Label label_errorFolder2;
        private System.Windows.Forms.CheckBox checkBox_remove;
    }
}