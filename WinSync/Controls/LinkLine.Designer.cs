namespace WinSync.Controls
{
    partial class LinkLine
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkLine));
            this.label_path1 = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.label_path2 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.inner = new System.Windows.Forms.Panel();
            this.pictureBox_direction = new System.Windows.Forms.PictureBox();
            this.pictureBox_result = new System.Windows.Forms.PictureBox();
            this.syncButton = new WinSync.Controls.SyncButton();
            this.inner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_direction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).BeginInit();
            this.SuspendLayout();
            // 
            // label_path1
            // 
            this.label_path1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_path1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label_path1.Location = new System.Drawing.Point(147, 5);
            this.label_path1.Margin = new System.Windows.Forms.Padding(3);
            this.label_path1.Name = "label_path1";
            this.label_path1.Size = new System.Drawing.Size(299, 13);
            this.label_path1.TabIndex = 4;
            this.label_path1.Text = "path1";
            // 
            // label_title
            // 
            this.label_title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_title.Location = new System.Drawing.Point(41, 5);
            this.label_title.Margin = new System.Windows.Forms.Padding(3);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(100, 30);
            this.label_title.TabIndex = 3;
            this.label_title.Text = "title";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_path2
            // 
            this.label_path2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_path2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label_path2.Location = new System.Drawing.Point(147, 21);
            this.label_path2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label_path2.Name = "label_path2";
            this.label_path2.Size = new System.Drawing.Size(299, 13);
            this.label_path2.TabIndex = 1;
            this.label_path2.Text = "path2";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.progressBar.ForeColor = System.Drawing.Color.DimGray;
            this.progressBar.Location = new System.Drawing.Point(38, 38);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar.MarqueeAnimationSpeed = 30;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(493, 5);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 1;
            // 
            // inner
            // 
            this.inner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.inner.Controls.Add(this.pictureBox_direction);
            this.inner.Controls.Add(this.pictureBox_result);
            this.inner.Controls.Add(this.progressBar);
            this.inner.Controls.Add(this.syncButton);
            this.inner.Controls.Add(this.label_path2);
            this.inner.Controls.Add(this.label_title);
            this.inner.Controls.Add(this.label_path1);
            this.inner.Location = new System.Drawing.Point(2, 2);
            this.inner.Margin = new System.Windows.Forms.Padding(2);
            this.inner.Name = "inner";
            this.inner.Size = new System.Drawing.Size(531, 43);
            this.inner.TabIndex = 3;
            // 
            // pictureBox_direction
            // 
            this.pictureBox_direction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_direction.Location = new System.Drawing.Point(457, 8);
            this.pictureBox_direction.Margin = new System.Windows.Forms.Padding(8);
            this.pictureBox_direction.Name = "pictureBox_direction";
            this.pictureBox_direction.Size = new System.Drawing.Size(23, 22);
            this.pictureBox_direction.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_direction.TabIndex = 6;
            this.pictureBox_direction.TabStop = false;
            // 
            // pictureBox_result
            // 
            this.pictureBox_result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_result.Location = new System.Drawing.Point(491, 6);
            this.pictureBox_result.Name = "pictureBox_result";
            this.pictureBox_result.Size = new System.Drawing.Size(37, 29);
            this.pictureBox_result.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_result.TabIndex = 5;
            this.pictureBox_result.TabStop = false;
            // 
            // syncButton
            // 
            this.syncButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.syncButton.BackColor = System.Drawing.Color.Transparent;
            this.syncButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("syncButton.BackgroundImage")));
            this.syncButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.syncButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.syncButton.FlatAppearance.BorderSize = 0;
            this.syncButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.syncButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.syncButton.Location = new System.Drawing.Point(0, 0);
            this.syncButton.Margin = new System.Windows.Forms.Padding(0);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(38, 43);
            this.syncButton.TabIndex = 2;
            this.syncButton.UseVisualStyleBackColor = false;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // LinkLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.inner);
            this.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.Name = "LinkLine";
            this.Size = new System.Drawing.Size(535, 47);
            this.inner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_direction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_result)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label_path2;
        private System.Windows.Forms.Label label_path1;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel inner;
        private SyncButton syncButton;
        private System.Windows.Forms.PictureBox pictureBox_result;
        private System.Windows.Forms.PictureBox pictureBox_direction;
    }
}
