using WinSync.Controls;

namespace WinSync.Forms
{
    partial class LinkStatisticForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkStatisticForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_averageSpeed = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.shadow_bottom2 = new System.Windows.Forms.Panel();
            this.shadow_top2 = new System.Windows.Forms.Panel();
            this.label_totalTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_speed = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_syncedFilesCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_syncedFilesSize = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_detail_status = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_detail_progress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox_syncInfo = new System.Windows.Forms.ListBox();
            this.panel_header = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_folder1 = new System.Windows.Forms.Label();
            this.label_folder2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_direction = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel0 = new System.Windows.Forms.Panel();
            this.shadow_top1 = new System.Windows.Forms.Panel();
            this.shadow_bottom1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.progressBar = new WinSync.Controls.MyProgressBar();
            this.button_pr = new WinSync.Controls.MyButton();
            this.button_sync = new WinSync.Controls.MyButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel_header.SuspendLayout();
            this.panel0.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.panel1.Controls.Add(this.label_averageSpeed);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.shadow_bottom2);
            this.panel1.Controls.Add(this.shadow_top2);
            this.panel1.Controls.Add(this.label_totalTime);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label_speed);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label_syncedFilesCount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label_syncedFilesSize);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label_detail_status);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label_detail_progress);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(0, 90);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 479);
            this.panel1.TabIndex = 10;
            // 
            // label_averageSpeed
            // 
            this.label_averageSpeed.AutoSize = true;
            this.label_averageSpeed.Location = new System.Drawing.Point(381, 87);
            this.label_averageSpeed.Name = "label_averageSpeed";
            this.label_averageSpeed.Size = new System.Drawing.Size(0, 13);
            this.label_averageSpeed.TabIndex = 30;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(282, 85);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "Average Speed:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(3, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Sync Statistics";
            // 
            // shadow_bottom2
            // 
            this.shadow_bottom2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadow_bottom2.BackgroundImage = global::WinSync.Properties.Resources.shadow_bottom_d50;
            this.shadow_bottom2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shadow_bottom2.Location = new System.Drawing.Point(0, 456);
            this.shadow_bottom2.Margin = new System.Windows.Forms.Padding(0);
            this.shadow_bottom2.Name = "shadow_bottom2";
            this.shadow_bottom2.Size = new System.Drawing.Size(541, 6);
            this.shadow_bottom2.TabIndex = 27;
            // 
            // shadow_top2
            // 
            this.shadow_top2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadow_top2.BackgroundImage = global::WinSync.Properties.Resources.shadow_top_d50;
            this.shadow_top2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shadow_top2.Location = new System.Drawing.Point(0, 138);
            this.shadow_top2.Name = "shadow_top2";
            this.shadow_top2.Size = new System.Drawing.Size(541, 10);
            this.shadow_top2.TabIndex = 23;
            // 
            // label_totalTime
            // 
            this.label_totalTime.AutoSize = true;
            this.label_totalTime.Location = new System.Drawing.Point(356, 61);
            this.label_totalTime.Name = "label_totalTime";
            this.label_totalTime.Size = new System.Drawing.Size(0, 13);
            this.label_totalTime.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(282, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 15);
            this.label10.TabIndex = 24;
            this.label10.Text = "Total Time:";
            // 
            // label_speed
            // 
            this.label_speed.AutoSize = true;
            this.label_speed.Location = new System.Drawing.Point(334, 36);
            this.label_speed.Name = "label_speed";
            this.label_speed.Size = new System.Drawing.Size(0, 13);
            this.label_speed.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(282, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Speed:";
            // 
            // label_syncedFilesCount
            // 
            this.label_syncedFilesCount.AutoSize = true;
            this.label_syncedFilesCount.Location = new System.Drawing.Point(120, 111);
            this.label_syncedFilesCount.Name = "label_syncedFilesCount";
            this.label_syncedFilesCount.Size = new System.Drawing.Size(0, 13);
            this.label_syncedFilesCount.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Synced files count:";
            // 
            // label_syncedFilesSize
            // 
            this.label_syncedFilesSize.AutoSize = true;
            this.label_syncedFilesSize.Location = new System.Drawing.Point(112, 87);
            this.label_syncedFilesSize.Name = "label_syncedFilesSize";
            this.label_syncedFilesSize.Size = new System.Drawing.Size(0, 13);
            this.label_syncedFilesSize.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 18;
            this.label7.Text = "Synced files size:";
            // 
            // label_detail_status
            // 
            this.label_detail_status.AutoSize = true;
            this.label_detail_status.Location = new System.Drawing.Point(50, 61);
            this.label_detail_status.Name = "label_detail_status";
            this.label_detail_status.Size = new System.Drawing.Size(12, 13);
            this.label_detail_status.TabIndex = 15;
            this.label_detail_status.Text = "s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Status:";
            // 
            // label_detail_progress
            // 
            this.label_detail_progress.AutoSize = true;
            this.label_detail_progress.Location = new System.Drawing.Point(64, 36);
            this.label_detail_progress.Name = "label_detail_progress";
            this.label_detail_progress.Size = new System.Drawing.Size(13, 13);
            this.label_detail_progress.TabIndex = 10;
            this.label_detail_progress.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Progress:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.listBox_syncInfo);
            this.panel2.Location = new System.Drawing.Point(0, 148);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(541, 308);
            this.panel2.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Process";
            // 
            // listBox_syncInfo
            // 
            this.listBox_syncInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_syncInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.listBox_syncInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_syncInfo.FormattingEnabled = true;
            this.listBox_syncInfo.Location = new System.Drawing.Point(6, 32);
            this.listBox_syncInfo.Name = "listBox_syncInfo";
            this.listBox_syncInfo.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox_syncInfo.Size = new System.Drawing.Size(535, 260);
            this.listBox_syncInfo.TabIndex = 17;
            // 
            // panel_header
            // 
            this.panel_header.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(175)))), ((int)(((byte)(230)))));
            this.panel_header.Controls.Add(this.button_pr);
            this.panel_header.Controls.Add(this.button_sync);
            this.panel_header.Controls.Add(this.label_title);
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(541, 34);
            this.panel_header.TabIndex = 13;
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(5, 6);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(46, 18);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Folder 1:";
            // 
            // label_folder1
            // 
            this.label_folder1.AutoSize = true;
            this.label_folder1.Location = new System.Drawing.Point(62, 14);
            this.label_folder1.Name = "label_folder1";
            this.label_folder1.Size = new System.Drawing.Size(12, 13);
            this.label_folder1.TabIndex = 16;
            this.label_folder1.Text = "s";
            // 
            // label_folder2
            // 
            this.label_folder2.AutoSize = true;
            this.label_folder2.Location = new System.Drawing.Point(62, 37);
            this.label_folder2.Name = "label_folder2";
            this.label_folder2.Size = new System.Drawing.Size(12, 13);
            this.label_folder2.TabIndex = 18;
            this.label_folder2.Text = "s";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Folder 2:";
            // 
            // label_direction
            // 
            this.label_direction.AutoSize = true;
            this.label_direction.Location = new System.Drawing.Point(62, 59);
            this.label_direction.Name = "label_direction";
            this.label_direction.Size = new System.Drawing.Size(12, 13);
            this.label_direction.TabIndex = 20;
            this.label_direction.Text = "s";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Richtung:";
            // 
            // panel0
            // 
            this.panel0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel0.BackColor = System.Drawing.Color.Transparent;
            this.panel0.Controls.Add(this.shadow_top1);
            this.panel0.Controls.Add(this.panel1);
            this.panel0.Controls.Add(this.label2);
            this.panel0.Controls.Add(this.label_direction);
            this.panel0.Controls.Add(this.label_folder1);
            this.panel0.Controls.Add(this.label6);
            this.panel0.Controls.Add(this.label5);
            this.panel0.Controls.Add(this.label_folder2);
            this.panel0.ForeColor = System.Drawing.Color.Black;
            this.panel0.Location = new System.Drawing.Point(0, 65);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(541, 585);
            this.panel0.TabIndex = 21;
            // 
            // shadow_top1
            // 
            this.shadow_top1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadow_top1.BackgroundImage = global::WinSync.Properties.Resources.shadow_top_d50;
            this.shadow_top1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shadow_top1.Location = new System.Drawing.Point(0, 80);
            this.shadow_top1.Name = "shadow_top1";
            this.shadow_top1.Size = new System.Drawing.Size(541, 10);
            this.shadow_top1.TabIndex = 22;
            // 
            // shadow_bottom1
            // 
            this.shadow_bottom1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shadow_bottom1.BackgroundImage = global::WinSync.Properties.Resources.shadow_bottom_d50;
            this.shadow_bottom1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.shadow_bottom1.Location = new System.Drawing.Point(0, 634);
            this.shadow_bottom1.Margin = new System.Windows.Forms.Padding(0);
            this.shadow_bottom1.Name = "shadow_bottom1";
            this.shadow_bottom1.Size = new System.Drawing.Size(541, 6);
            this.shadow_bottom1.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackgroundImage = global::WinSync.Properties.Resources.shadow_bottom_d50;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 5);
            this.panel3.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.Location = new System.Drawing.Point(3, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 17);
            this.label12.TabIndex = 29;
            this.label12.Text = "Link Info";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.Color.Gainsboro;
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.progressBar.Location = new System.Drawing.Point(0, 34);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(541, 10);
            this.progressBar.TabIndex = 22;
            // 
            // button_pr
            // 
            this.button_pr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_pr.BackColor = System.Drawing.Color.Transparent;
            this.button_pr.BackgroundImage = global::WinSync.Properties.Resources.ic_pause_white;
            this.button_pr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_pr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_pr.FlatAppearance.BorderSize = 0;
            this.button_pr.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.button_pr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_pr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_pr.ForeColor = System.Drawing.Color.White;
            this.button_pr.Location = new System.Drawing.Point(467, 0);
            this.button_pr.Margin = new System.Windows.Forms.Padding(0);
            this.button_pr.Name = "button_pr";
            this.button_pr.Size = new System.Drawing.Size(38, 34);
            this.button_pr.TabIndex = 21;
            this.button_pr.UseVisualStyleBackColor = false;
            this.button_pr.Click += new System.EventHandler(this.button_pr_Click);
            // 
            // button_sync
            // 
            this.button_sync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sync.BackColor = System.Drawing.Color.Transparent;
            this.button_sync.BackgroundImage = global::WinSync.Properties.Resources.ic_sync_white;
            this.button_sync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_sync.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_sync.FlatAppearance.BorderSize = 0;
            this.button_sync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.button_sync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_sync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_sync.ForeColor = System.Drawing.Color.White;
            this.button_sync.Location = new System.Drawing.Point(505, 0);
            this.button_sync.Margin = new System.Windows.Forms.Padding(0);
            this.button_sync.Name = "button_sync";
            this.button_sync.Size = new System.Drawing.Size(39, 34);
            this.button_sync.TabIndex = 1;
            this.button_sync.UseVisualStyleBackColor = false;
            this.button_sync.Click += new System.EventHandler(this.button_sync_Click);
            // 
            // LinkStatisticForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(541, 647);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.shadow_bottom1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panel_header);
            this.Controls.Add(this.panel0);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LinkStatisticForm";
            this.Text = "Sync Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LinkStatisticForm_FormClosing);
            this.Load += new System.EventHandler(this.LinkStatisticForm_Load);
            this.Resize += new System.EventHandler(this.LinkStatisticForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.panel0.ResumeLayout(false);
            this.panel0.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox_syncInfo;
        private System.Windows.Forms.Label label_detail_status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_detail_progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_folder1;
        private System.Windows.Forms.Label label_folder2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_direction;
        private System.Windows.Forms.Label label6;
        private MyButton button_sync;
        private System.Windows.Forms.Label label_syncedFilesCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_syncedFilesSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_speed;
        private System.Windows.Forms.Label label9;
        private MyButton button_pr;
        private System.Windows.Forms.Panel panel0;
        private MyProgressBar progressBar;
        private System.Windows.Forms.Label label_totalTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel shadow_top2;
        private System.Windows.Forms.Panel shadow_top1;
        private System.Windows.Forms.Panel shadow_bottom2;
        private System.Windows.Forms.Panel shadow_bottom1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_averageSpeed;
        private System.Windows.Forms.Label label14;
    }
}