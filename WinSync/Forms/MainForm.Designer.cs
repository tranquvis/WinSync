using WinSync.Controls;
using WinSync.Data;
namespace WinSync.Forms
{
    partial class MainForm
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label_p = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_selSL_info = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label_timeLeft = new System.Windows.Forms.Label();
            this.label_detail_progress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_detail_status = new System.Windows.Forms.Label();
            this.panel_detail_header = new System.Windows.Forms.Panel();
            this.label_detail_title = new System.Windows.Forms.Label();
            this.panel_content = new System.Windows.Forms.Panel();
            this.checkBox_availableSyncsOnly = new System.Windows.Forms.CheckBox();
            this.dataTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel_syncDetail = new System.Windows.Forms.Panel();
            this.button_minimize = new System.Windows.Forms.Button();
            this.button_close = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_openDetailForm = new WinSync.Controls.MyButton();
            this.button_pr = new WinSync.Controls.MyButton();
            this.button_sync = new WinSync.Controls.MyButton();
            this.button_addLink = new WinSync.Controls.MyButton();
            this.progressBar_total = new WinSync.Controls.MyProgressBar();
            this.shadow1 = new WinSync.Controls.Shadow();
            this.panel2.SuspendLayout();
            this.panel_selSL_info.SuspendLayout();
            this.panel_detail_header.SuspendLayout();
            this.panel_content.SuspendLayout();
            this.panel_syncDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 64);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // label_p
            // 
            this.label_p.AutoSize = true;
            this.label_p.BackColor = System.Drawing.Color.Transparent;
            this.label_p.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_p.Location = new System.Drawing.Point(153, 12);
            this.label_p.Name = "label_p";
            this.label_p.Size = new System.Drawing.Size(34, 13);
            this.label_p.TabIndex = 7;
            this.label_p.Text = "100,0";
            this.label_p.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel_selSL_info);
            this.panel2.Controls.Add(this.button_openDetailForm);
            this.panel2.Controls.Add(this.panel_detail_header);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(408, 142);
            this.panel2.TabIndex = 9;
            // 
            // panel_selSL_info
            // 
            this.panel_selSL_info.Controls.Add(this.label1);
            this.panel_selSL_info.Controls.Add(this.label_timeLeft);
            this.panel_selSL_info.Controls.Add(this.label_detail_progress);
            this.panel_selSL_info.Controls.Add(this.label4);
            this.panel_selSL_info.Controls.Add(this.label3);
            this.panel_selSL_info.Controls.Add(this.label_detail_status);
            this.panel_selSL_info.Location = new System.Drawing.Point(7, 37);
            this.panel_selSL_info.Name = "panel_selSL_info";
            this.panel_selSL_info.Size = new System.Drawing.Size(398, 58);
            this.panel_selSL_info.TabIndex = 19;
            this.panel_selSL_info.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Progress:";
            // 
            // label_timeLeft
            // 
            this.label_timeLeft.AutoSize = true;
            this.label_timeLeft.Location = new System.Drawing.Point(247, 3);
            this.label_timeLeft.Name = "label_timeLeft";
            this.label_timeLeft.Size = new System.Drawing.Size(13, 13);
            this.label_timeLeft.TabIndex = 18;
            this.label_timeLeft.Text = "0";
            // 
            // label_detail_progress
            // 
            this.label_detail_progress.AutoSize = true;
            this.label_detail_progress.Location = new System.Drawing.Point(67, 2);
            this.label_detail_progress.Name = "label_detail_progress";
            this.label_detail_progress.Size = new System.Drawing.Size(13, 13);
            this.label_detail_progress.TabIndex = 10;
            this.label_detail_progress.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(139, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "Remaining Time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Status:";
            // 
            // label_detail_status
            // 
            this.label_detail_status.AutoSize = true;
            this.label_detail_status.Location = new System.Drawing.Point(54, 27);
            this.label_detail_status.Name = "label_detail_status";
            this.label_detail_status.Size = new System.Drawing.Size(0, 13);
            this.label_detail_status.TabIndex = 15;
            // 
            // panel_detail_header
            // 
            this.panel_detail_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_detail_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.panel_detail_header.Controls.Add(this.button_pr);
            this.panel_detail_header.Controls.Add(this.button_sync);
            this.panel_detail_header.Controls.Add(this.label_detail_title);
            this.panel_detail_header.Location = new System.Drawing.Point(0, 0);
            this.panel_detail_header.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_detail_header.Name = "panel_detail_header";
            this.panel_detail_header.Size = new System.Drawing.Size(408, 30);
            this.panel_detail_header.TabIndex = 13;
            // 
            // label_detail_title
            // 
            this.label_detail_title.AutoSize = true;
            this.label_detail_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detail_title.ForeColor = System.Drawing.Color.White;
            this.label_detail_title.Location = new System.Drawing.Point(4, 6);
            this.label_detail_title.Name = "label_detail_title";
            this.label_detail_title.Size = new System.Drawing.Size(41, 15);
            this.label_detail_title.TabIndex = 0;
            this.label_detail_title.Text = "label2";
            // 
            // panel_content
            // 
            this.panel_content.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_content.AutoScroll = true;
            this.panel_content.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel_content.Controls.Add(this.checkBox_availableSyncsOnly);
            this.panel_content.Controls.Add(this.dataTable);
            this.panel_content.Controls.Add(this.flowLayoutPanel1);
            this.panel_content.Controls.Add(this.panel_syncDetail);
            this.panel_content.Controls.Add(this.button_addLink);
            this.panel_content.Controls.Add(this.label_p);
            this.panel_content.Controls.Add(this.progressBar_total);
            this.panel_content.Location = new System.Drawing.Point(8, 42);
            this.panel_content.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_content.Name = "panel_content";
            this.panel_content.Size = new System.Drawing.Size(1045, 638);
            this.panel_content.TabIndex = 10;
            // 
            // checkBox_availableSyncsOnly
            // 
            this.checkBox_availableSyncsOnly.AutoSize = true;
            this.checkBox_availableSyncsOnly.Location = new System.Drawing.Point(6, 41);
            this.checkBox_availableSyncsOnly.Name = "checkBox_availableSyncsOnly";
            this.checkBox_availableSyncsOnly.Size = new System.Drawing.Size(130, 17);
            this.checkBox_availableSyncsOnly.TabIndex = 33;
            this.checkBox_availableSyncsOnly.Text = "executable syncs only";
            this.checkBox_availableSyncsOnly.UseVisualStyleBackColor = true;
            this.checkBox_availableSyncsOnly.CheckedChanged += new System.EventHandler(this.checkBox_availableSyncsOnly_CheckedChanged);
            // 
            // dataTable
            // 
            this.dataTable.AutoSize = true;
            this.dataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataTable.BackColor = System.Drawing.Color.White;
            this.dataTable.ColumnCount = 1;
            this.dataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dataTable.Location = new System.Drawing.Point(6, 64);
            this.dataTable.Name = "dataTable";
            this.dataTable.RowCount = 1;
            this.dataTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dataTable.Size = new System.Drawing.Size(0, 0);
            this.dataTable.TabIndex = 32;
            // 
            // panel_syncDetail
            // 
            this.panel_syncDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_syncDetail.BackColor = System.Drawing.Color.Transparent;
            this.panel_syncDetail.Controls.Add(this.shadow1);
            this.panel_syncDetail.Controls.Add(this.panel2);
            this.panel_syncDetail.Location = new System.Drawing.Point(622, 44);
            this.panel_syncDetail.Name = "panel_syncDetail";
            this.panel_syncDetail.Size = new System.Drawing.Size(408, 158);
            this.panel_syncDetail.TabIndex = 29;
            this.panel_syncDetail.Visible = false;
            // 
            // button_minimize
            // 
            this.button_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_minimize.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_minimize.FlatAppearance.BorderSize = 0;
            this.button_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_minimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.button_minimize.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button_minimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_minimize.Location = new System.Drawing.Point(1000, 4);
            this.button_minimize.Margin = new System.Windows.Forms.Padding(0);
            this.button_minimize.Name = "button_minimize";
            this.button_minimize.Size = new System.Drawing.Size(29, 33);
            this.button_minimize.TabIndex = 13;
            this.button_minimize.Text = "_";
            this.button_minimize.UseVisualStyleBackColor = true;
            this.button_minimize.Click += new System.EventHandler(this.button_minimize_Click);
            // 
            // button_close
            // 
            this.button_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_close.FlatAppearance.BorderSize = 0;
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button_close.ForeColor = System.Drawing.Color.DodgerBlue;
            this.button_close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button_close.Location = new System.Drawing.Point(1029, 4);
            this.button_close.Margin = new System.Windows.Forms.Padding(0);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(27, 33);
            this.button_close.TabIndex = 12;
            this.button_close.Text = "x";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WinSync.Properties.Resources.WinSync_Label;
            this.pictureBox1.Location = new System.Drawing.Point(2, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // button_openDetailForm
            // 
            this.button_openDetailForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(0)))), ((int)(((byte)(23)))));
            this.button_openDetailForm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_openDetailForm.FlatAppearance.BorderSize = 0;
            this.button_openDetailForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(0)))), ((int)(((byte)(17)))));
            this.button_openDetailForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_openDetailForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_openDetailForm.ForeColor = System.Drawing.Color.White;
            this.button_openDetailForm.Location = new System.Drawing.Point(8, 101);
            this.button_openDetailForm.Name = "button_openDetailForm";
            this.button_openDetailForm.Size = new System.Drawing.Size(74, 23);
            this.button_openDetailForm.TabIndex = 16;
            this.button_openDetailForm.Text = "Details";
            this.button_openDetailForm.UseVisualStyleBackColor = false;
            this.button_openDetailForm.Click += new System.EventHandler(this.button_openDetailForm_Click);
            // 
            // button_pr
            // 
            this.button_pr.BackColor = System.Drawing.Color.Transparent;
            this.button_pr.BackgroundImage = global::WinSync.Properties.Resources.ic_pause_white;
            this.button_pr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_pr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_pr.FlatAppearance.BorderSize = 0;
            this.button_pr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.button_pr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_pr.ForeColor = System.Drawing.Color.White;
            this.button_pr.Location = new System.Drawing.Point(346, 0);
            this.button_pr.Margin = new System.Windows.Forms.Padding(0);
            this.button_pr.Name = "button_pr";
            this.button_pr.Size = new System.Drawing.Size(31, 30);
            this.button_pr.TabIndex = 23;
            this.button_pr.UseVisualStyleBackColor = false;
            this.button_pr.Click += new System.EventHandler(this.button_pr_Click);
            // 
            // button_sync
            // 
            this.button_sync.BackColor = System.Drawing.Color.Transparent;
            this.button_sync.BackgroundImage = global::WinSync.Properties.Resources.ic_sync_white;
            this.button_sync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_sync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_sync.FlatAppearance.BorderSize = 0;
            this.button_sync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.button_sync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_sync.ForeColor = System.Drawing.Color.White;
            this.button_sync.Location = new System.Drawing.Point(377, 0);
            this.button_sync.Margin = new System.Windows.Forms.Padding(0);
            this.button_sync.Name = "button_sync";
            this.button_sync.Size = new System.Drawing.Size(31, 30);
            this.button_sync.TabIndex = 22;
            this.button_sync.UseVisualStyleBackColor = false;
            this.button_sync.Click += new System.EventHandler(this.button_sync_Click);
            // 
            // button_addLink
            // 
            this.button_addLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.button_addLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_addLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_addLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_addLink.ForeColor = System.Drawing.Color.White;
            this.button_addLink.Location = new System.Drawing.Point(6, 6);
            this.button_addLink.Name = "button_addLink";
            this.button_addLink.Size = new System.Drawing.Size(75, 28);
            this.button_addLink.TabIndex = 8;
            this.button_addLink.Text = "Add Link";
            this.button_addLink.UseVisualStyleBackColor = false;
            this.button_addLink.Click += new System.EventHandler(this.button_addLink_Click);
            // 
            // progressBar_total
            // 
            this.progressBar_total.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_total.BackColor = System.Drawing.Color.Gainsboro;
            this.progressBar_total.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(0)))), ((int)(((byte)(17)))));
            this.progressBar_total.Location = new System.Drawing.Point(196, 12);
            this.progressBar_total.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar_total.Maximum = 1000;
            this.progressBar_total.Name = "progressBar_total";
            this.progressBar_total.Size = new System.Drawing.Size(843, 16);
            this.progressBar_total.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_total.TabIndex = 5;
            this.progressBar_total.Visible = false;
            // 
            // shadow1
            // 
            this.shadow1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadow1.BackColor = System.Drawing.Color.Transparent;
            this.shadow1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("shadow1.BackgroundImage")));
            this.shadow1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shadow1.Location = new System.Drawing.Point(0, 142);
            this.shadow1.Margin = new System.Windows.Forms.Padding(0);
            this.shadow1.Name = "shadow1";
            this.shadow1.Size = new System.Drawing.Size(408, 10);
            this.shadow1.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1061, 690);
            this.ControlBox = false;
            this.Controls.Add(this.button_minimize);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel_content);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel2.ResumeLayout(false);
            this.panel_selSL_info.ResumeLayout(false);
            this.panel_selSL_info.PerformLayout();
            this.panel_detail_header.ResumeLayout(false);
            this.panel_detail_header.PerformLayout();
            this.panel_content.ResumeLayout(false);
            this.panel_content.PerformLayout();
            this.panel_syncDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyProgressBar progressBar_total;
        private System.Windows.Forms.Label label_p;
        private MyButton button_addLink;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_detail_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_detail_header;
        private System.Windows.Forms.Label label_detail_title;
        private System.Windows.Forms.Panel panel_content;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_minimize;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.Label label_detail_status;
        private System.Windows.Forms.Label label3;
        private MyButton button_openDetailForm;
        private MyButton button_pr;
        private MyButton button_sync;
        private System.Windows.Forms.Panel panel_syncDetail;
        private System.Windows.Forms.Label label_timeLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel dataTable;
        private System.Windows.Forms.CheckBox checkBox_availableSyncsOnly;
        private System.Windows.Forms.Panel panel_selSL_info;
        private Shadow shadow1;
    }
}

