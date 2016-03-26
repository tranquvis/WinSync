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
            this.button_openDetailForm = new WinSync.Controls.MyButton();
            this.panel_detail_header = new System.Windows.Forms.Panel();
            this.button_pr = new WinSync.Controls.MyButton();
            this.button_sync = new WinSync.Controls.MyButton();
            this.label_detail_title = new System.Windows.Forms.Label();
            this.checkBox_availableSyncsOnly = new System.Windows.Forms.CheckBox();
            this.dataTable = new System.Windows.Forms.TableLayoutPanel();
            this.panel_syncDetail = new System.Windows.Forms.Panel();
            this.button_addLink = new WinSync.Controls.MyButton();
            this.progressBar_total = new WinSync.Controls.MyProgressBar();
            this._contentPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_selSL_info.SuspendLayout();
            this.panel_detail_header.SuspendLayout();
            this.panel_syncDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.Controls.Add(this.panel_syncDetail);
            this._contentPanel.Controls.Add(this.checkBox_availableSyncsOnly);
            this._contentPanel.Controls.Add(this.label_p);
            this._contentPanel.Controls.Add(this.progressBar_total);
            this._contentPanel.Controls.Add(this.flowLayoutPanel1);
            this._contentPanel.Controls.Add(this.button_addLink);
            this._contentPanel.Controls.Add(this.dataTable);
            this._contentPanel.Location = new System.Drawing.Point(5, 30);
            this._contentPanel.Size = new System.Drawing.Size(1051, 655);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(14, 66);
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
            this.label_p.Location = new System.Drawing.Point(251, 11);
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
            this.panel2.Size = new System.Drawing.Size(406, 133);
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
            this.panel_detail_header.Size = new System.Drawing.Size(406, 30);
            this.panel_detail_header.TabIndex = 13;
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
            // checkBox_availableSyncsOnly
            // 
            this.checkBox_availableSyncsOnly.AutoSize = true;
            this.checkBox_availableSyncsOnly.BackColor = System.Drawing.Color.Transparent;
            this.checkBox_availableSyncsOnly.Location = new System.Drawing.Point(8, 43);
            this.checkBox_availableSyncsOnly.Name = "checkBox_availableSyncsOnly";
            this.checkBox_availableSyncsOnly.Size = new System.Drawing.Size(130, 17);
            this.checkBox_availableSyncsOnly.TabIndex = 33;
            this.checkBox_availableSyncsOnly.Text = "executable syncs only";
            this.checkBox_availableSyncsOnly.UseVisualStyleBackColor = false;
            this.checkBox_availableSyncsOnly.CheckedChanged += new System.EventHandler(this.checkBox_availableSyncsOnly_CheckedChanged);
            // 
            // dataTable
            // 
            this.dataTable.AutoSize = true;
            this.dataTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataTable.BackColor = System.Drawing.Color.White;
            this.dataTable.ColumnCount = 1;
            this.dataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dataTable.Location = new System.Drawing.Point(3, 61);
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
            this.panel_syncDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_syncDetail.Controls.Add(this.panel2);
            this.panel_syncDetail.Location = new System.Drawing.Point(625, 66);
            this.panel_syncDetail.Name = "panel_syncDetail";
            this.panel_syncDetail.Size = new System.Drawing.Size(408, 135);
            this.panel_syncDetail.TabIndex = 29;
            this.panel_syncDetail.Visible = false;
            // 
            // button_addLink
            // 
            this.button_addLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.button_addLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_addLink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_addLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_addLink.ForeColor = System.Drawing.Color.White;
            this.button_addLink.Location = new System.Drawing.Point(8, 8);
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
            this.progressBar_total.Location = new System.Drawing.Point(294, 11);
            this.progressBar_total.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar_total.Maximum = 1000;
            this.progressBar_total.Name = "progressBar_total";
            this.progressBar_total.Size = new System.Drawing.Size(739, 16);
            this.progressBar_total.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_total.TabIndex = 5;
            this.progressBar_total.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1061, 690);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(5, 30, 5, 5);
            this.Text = "Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Controls.SetChildIndex(this._contentPanel, 0);
            this._contentPanel.ResumeLayout(false);
            this._contentPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel_selSL_info.ResumeLayout(false);
            this.panel_selSL_info.PerformLayout();
            this.panel_detail_header.ResumeLayout(false);
            this.panel_detail_header.PerformLayout();
            this.panel_syncDetail.ResumeLayout(false);
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
    }
}

