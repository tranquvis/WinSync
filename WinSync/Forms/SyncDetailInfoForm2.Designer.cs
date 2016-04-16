using WinSync.Controls;
using WinSync.Service;

namespace WinSync.Forms
{
    partial class SyncDetailInfoForm2
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncDetailInfoForm2));
            this.panel_header = new System.Windows.Forms.Panel();
            this.button_pr = new WinSync.Controls.MyButton();
            this.button_sync = new WinSync.Controls.MyButton();
            this.label_title = new System.Windows.Forms.Label();
            this.splitContainer_left = new System.Windows.Forms.SplitContainer();
            this.tabControl_left1 = new System.Windows.Forms.TabControl();
            this.tabPage_linkInfo = new System.Windows.Forms.TabPage();
            this.panel_linkInfo = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_link_folder1Title = new System.Windows.Forms.Label();
            this.label_link_folder1 = new System.Windows.Forms.Label();
            this.label_link_folder2 = new System.Windows.Forms.Label();
            this.label_link_folder2Title = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label_link_directionTitle = new System.Windows.Forms.Label();
            this.label_link_direction = new System.Windows.Forms.Label();
            this.tabPage_syncInfo = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.statusProgressBar1 = new WinSync.Controls.StatusProgressBar();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label_syst_progressTitle = new System.Windows.Forms.Label();
            this.label_syst_progress = new System.Windows.Forms.Label();
            this.label_syst_runningTasks = new System.Windows.Forms.Label();
            this.label_syst_runningTasksTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label_syst_leftTimeTitle = new System.Windows.Forms.Label();
            this.label_syst_leftTime = new System.Windows.Forms.Label();
            this.label_syst_totalTimeTitle = new System.Windows.Forms.Label();
            this.label_syst_totalTime = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel_fetchFD = new System.Windows.Forms.TableLayoutPanel();
            this.label_fetchFD_filesFoundTitle = new System.Windows.Forms.Label();
            this.label_fetchFD_filesFound = new System.Windows.Forms.Label();
            this.label_fetchFD_foldersFoundTitle = new System.Windows.Forms.Label();
            this.label_fetchFD_foldersFound = new System.Windows.Forms.Label();
            this.panel_detectCh = new System.Windows.Forms.TableLayoutPanel();
            this.label_detectCh_changesDetectedTitle = new System.Windows.Forms.Label();
            this.label_detectCh_changesDetected = new System.Windows.Forms.Label();
            this.label_detectCh_FDDoneTitle = new System.Windows.Forms.Label();
            this.label_detectCh_FDDone = new System.Windows.Forms.Label();
            this.panel_detectCh_chTypes = new System.Windows.Forms.TableLayoutPanel();
            this.label_detectCh_filesToRemoveTitle = new System.Windows.Forms.Label();
            this.label_detectCh_filesToRemove = new System.Windows.Forms.Label();
            this.label_detectCh_foldersToRemoveTitle = new System.Windows.Forms.Label();
            this.label_detectCh_foldersToRemove = new System.Windows.Forms.Label();
            this.label_detectCh_filesToCopyTitle = new System.Windows.Forms.Label();
            this.label_detectCh_filesToCopy = new System.Windows.Forms.Label();
            this.label_detectCh_foldersToCreateTitle = new System.Windows.Forms.Label();
            this.label_detectCh_foldersToCreate = new System.Windows.Forms.Label();
            this.panel_crDirs = new System.Windows.Forms.TableLayoutPanel();
            this.label_crDirs_dirsCreatedTitle = new System.Windows.Forms.Label();
            this.label_crDirs_dirsCreated = new System.Windows.Forms.Label();
            this.panel_applyCh_speed = new System.Windows.Forms.TableLayoutPanel();
            this.label_applyCh_speed_currentTitle = new System.Windows.Forms.Label();
            this.label_applyCh_speed_current = new System.Windows.Forms.Label();
            this.label_applyCh_speed_averageTitle = new System.Windows.Forms.Label();
            this.label_applyCh_speed_average = new System.Windows.Forms.Label();
            this.panel_applyCh_syncedFiles = new System.Windows.Forms.TableLayoutPanel();
            this.label_applyCh_removedFilesSizeTitle = new System.Windows.Forms.Label();
            this.label_applyCh_copiedFilesCount = new System.Windows.Forms.Label();
            this.label_applyCh_copiedFilesCountTitle = new System.Windows.Forms.Label();
            this.label_applyCh_copiedFilesSizeTitle = new System.Windows.Forms.Label();
            this.label_applyCh_removedFilesCountTitle = new System.Windows.Forms.Label();
            this.label_applyCh_copiedFilesSize = new System.Windows.Forms.Label();
            this.label_applyCh_removedFilesCount = new System.Windows.Forms.Label();
            this.label_applyCh_removedFilesSize = new System.Windows.Forms.Label();
            this.panel_remDirs = new System.Windows.Forms.TableLayoutPanel();
            this.label_remDirs_foldersRemovedTitle = new System.Windows.Forms.Label();
            this.label_remDirs_foldersRemoved = new System.Windows.Forms.Label();
            this.tabPage_syncElementInfo = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel_sei = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_nameTitle = new System.Windows.Forms.Label();
            this.label_sei_name = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_typeTitle = new System.Windows.Forms.Label();
            this.label_sei_type = new System.Windows.Forms.Label();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_startTimeTitle = new System.Windows.Forms.Label();
            this.label_sei_startTime = new System.Windows.Forms.Label();
            this.label_sei_endTime = new System.Windows.Forms.Label();
            this.label_sei_endTimeTitle = new System.Windows.Forms.Label();
            this.label_sei_duration = new System.Windows.Forms.Label();
            this.label_sei_durationTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_syncStatusTitle = new System.Windows.Forms.Label();
            this.label_sei_syncStatus = new System.Windows.Forms.Label();
            this.label_sei_infoTitle = new System.Windows.Forms.Label();
            this.textBox_sei_info = new System.Windows.Forms.TextBox();
            this.label_sei_syncExecInfoTitle = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_path1Title = new System.Windows.Forms.Label();
            this.label_sei_path1 = new System.Windows.Forms.Label();
            this.label_sei_path2 = new System.Windows.Forms.Label();
            this.label_sei_path2Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.label_sei_directionTitle = new System.Windows.Forms.Label();
            this.label_sei_direction = new System.Windows.Forms.Label();
            this.label_sei_remove = new System.Windows.Forms.Label();
            this.label_sei_removeTitle = new System.Windows.Forms.Label();
            this.button_refresh = new WinSync.Controls.MyButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.listBox_log = new System.Windows.Forms.ListBox();
            this.progressBar = new WinSync.Controls.AnimatedProgressBar();
            this.imageList_nodeElements = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer_main = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel_left = new System.Windows.Forms.TableLayoutPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this._contentPanel.SuspendLayout();
            this.panel_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).BeginInit();
            this.splitContainer_left.Panel1.SuspendLayout();
            this.splitContainer_left.Panel2.SuspendLayout();
            this.splitContainer_left.SuspendLayout();
            this.tabControl_left1.SuspendLayout();
            this.tabPage_linkInfo.SuspendLayout();
            this.panel_linkInfo.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage_syncInfo.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.panel_fetchFD.SuspendLayout();
            this.panel_detectCh.SuspendLayout();
            this.panel_detectCh_chTypes.SuspendLayout();
            this.panel_crDirs.SuspendLayout();
            this.panel_applyCh_speed.SuspendLayout();
            this.panel_applyCh_syncedFiles.SuspendLayout();
            this.panel_remDirs.SuspendLayout();
            this.tabPage_syncElementInfo.SuspendLayout();
            this.tableLayoutPanel_sei.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).BeginInit();
            this.splitContainer_main.Panel1.SuspendLayout();
            this.splitContainer_main.Panel2.SuspendLayout();
            this.splitContainer_main.SuspendLayout();
            this.tableLayoutPanel_left.SuspendLayout();
            this.SuspendLayout();
            // 
            // _contentPanel
            // 
            this._contentPanel.BackColor = System.Drawing.Color.LightGray;
            this._contentPanel.Controls.Add(this.splitContainer_main);
            this._contentPanel.Location = new System.Drawing.Point(5, 30);
            this._contentPanel.Size = new System.Drawing.Size(795, 550);
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
            this.panel_header.Margin = new System.Windows.Forms.Padding(0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(492, 34);
            this.panel_header.TabIndex = 13;
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
            this.button_pr.Location = new System.Drawing.Point(418, 0);
            this.button_pr.Margin = new System.Windows.Forms.Padding(0);
            this.button_pr.Name = "button_pr";
            this.button_pr.Size = new System.Drawing.Size(38, 34);
            this.button_pr.TabIndex = 21;
            this.button_pr.UseVisualStyleBackColor = false;
            this.button_pr.Visible = false;
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
            this.button_sync.Location = new System.Drawing.Point(456, 0);
            this.button_sync.Margin = new System.Windows.Forms.Padding(0);
            this.button_sync.Name = "button_sync";
            this.button_sync.Size = new System.Drawing.Size(39, 34);
            this.button_sync.TabIndex = 1;
            this.button_sync.UseVisualStyleBackColor = false;
            this.button_sync.Click += new System.EventHandler(this.button_sync_Click);
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
            // splitContainer_left
            // 
            this.splitContainer_left.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_left.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_left.Location = new System.Drawing.Point(0, 54);
            this.splitContainer_left.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer_left.Name = "splitContainer_left";
            this.splitContainer_left.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer_left.Panel1
            // 
            this.splitContainer_left.Panel1.Controls.Add(this.tabControl_left1);
            this.splitContainer_left.Panel1MinSize = 50;
            // 
            // splitContainer_left.Panel2
            // 
            this.splitContainer_left.Panel2.Controls.Add(this.panel6);
            this.splitContainer_left.Panel2MinSize = 23;
            this.splitContainer_left.Size = new System.Drawing.Size(492, 494);
            this.splitContainer_left.SplitterDistance = 305;
            this.splitContainer_left.TabIndex = 31;
            // 
            // tabControl_left1
            // 
            this.tabControl_left1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_left1.Controls.Add(this.tabPage_linkInfo);
            this.tabControl_left1.Controls.Add(this.tabPage_syncInfo);
            this.tabControl_left1.Controls.Add(this.tabPage_syncElementInfo);
            this.tabControl_left1.Location = new System.Drawing.Point(0, 0);
            this.tabControl_left1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl_left1.Name = "tabControl_left1";
            this.tabControl_left1.SelectedIndex = 0;
            this.tabControl_left1.Size = new System.Drawing.Size(495, 305);
            this.tabControl_left1.TabIndex = 30;
            // 
            // tabPage_linkInfo
            // 
            this.tabPage_linkInfo.Controls.Add(this.panel_linkInfo);
            this.tabPage_linkInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPage_linkInfo.Name = "tabPage_linkInfo";
            this.tabPage_linkInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_linkInfo.Size = new System.Drawing.Size(487, 279);
            this.tabPage_linkInfo.TabIndex = 0;
            this.tabPage_linkInfo.Text = "Link Info";
            this.tabPage_linkInfo.UseVisualStyleBackColor = true;
            // 
            // panel_linkInfo
            // 
            this.panel_linkInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_linkInfo.Controls.Add(this.flowLayoutPanel2);
            this.panel_linkInfo.Location = new System.Drawing.Point(8, 8);
            this.panel_linkInfo.Margin = new System.Windows.Forms.Padding(5);
            this.panel_linkInfo.Name = "panel_linkInfo";
            this.panel_linkInfo.Size = new System.Drawing.Size(473, 263);
            this.panel_linkInfo.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.Controls.Add(this.panel5);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(473, 263);
            this.flowLayoutPanel2.TabIndex = 23;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.label_link_folder1Title);
            this.panel4.Controls.Add(this.label_link_folder1);
            this.panel4.Controls.Add(this.label_link_folder2);
            this.panel4.Controls.Add(this.label_link_folder2Title);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(84, 39);
            this.panel4.TabIndex = 21;
            // 
            // label_link_folder1Title
            // 
            this.label_link_folder1Title.Location = new System.Drawing.Point(3, 0);
            this.label_link_folder1Title.Name = "label_link_folder1Title";
            this.label_link_folder1Title.Size = new System.Drawing.Size(60, 16);
            this.label_link_folder1Title.TabIndex = 15;
            this.label_link_folder1Title.Text = "Folder 1:";
            // 
            // label_link_folder1
            // 
            this.label_link_folder1.AutoSize = true;
            this.label_link_folder1.Location = new System.Drawing.Point(69, 0);
            this.label_link_folder1.Name = "label_link_folder1";
            this.label_link_folder1.Size = new System.Drawing.Size(12, 13);
            this.label_link_folder1.TabIndex = 16;
            this.label_link_folder1.Text = "s";
            // 
            // label_link_folder2
            // 
            this.label_link_folder2.AutoSize = true;
            this.label_link_folder2.Location = new System.Drawing.Point(69, 23);
            this.label_link_folder2.Name = "label_link_folder2";
            this.label_link_folder2.Size = new System.Drawing.Size(12, 13);
            this.label_link_folder2.TabIndex = 18;
            this.label_link_folder2.Text = "s";
            // 
            // label_link_folder2Title
            // 
            this.label_link_folder2Title.Location = new System.Drawing.Point(3, 23);
            this.label_link_folder2Title.Name = "label_link_folder2Title";
            this.label_link_folder2Title.Size = new System.Drawing.Size(60, 16);
            this.label_link_folder2Title.TabIndex = 17;
            this.label_link_folder2Title.Text = "Folder 2:";
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.Controls.Add(this.label_link_directionTitle);
            this.panel5.Controls.Add(this.label_link_direction);
            this.panel5.Location = new System.Drawing.Point(100, 3);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(84, 16);
            this.panel5.TabIndex = 22;
            // 
            // label_link_directionTitle
            // 
            this.label_link_directionTitle.Location = new System.Drawing.Point(3, 0);
            this.label_link_directionTitle.Name = "label_link_directionTitle";
            this.label_link_directionTitle.Size = new System.Drawing.Size(60, 16);
            this.label_link_directionTitle.TabIndex = 19;
            this.label_link_directionTitle.Text = "Direction:";
            // 
            // label_link_direction
            // 
            this.label_link_direction.AutoSize = true;
            this.label_link_direction.Location = new System.Drawing.Point(69, 0);
            this.label_link_direction.Name = "label_link_direction";
            this.label_link_direction.Size = new System.Drawing.Size(12, 13);
            this.label_link_direction.TabIndex = 20;
            this.label_link_direction.Text = "s";
            // 
            // tabPage_syncInfo
            // 
            this.tabPage_syncInfo.Controls.Add(this.tableLayoutPanel5);
            this.tabPage_syncInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPage_syncInfo.Name = "tabPage_syncInfo";
            this.tabPage_syncInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_syncInfo.Size = new System.Drawing.Size(487, 279);
            this.tabPage_syncInfo.TabIndex = 1;
            this.tabPage_syncInfo.Text = "Sync Info";
            this.tabPage_syncInfo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.statusProgressBar1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel6, 0, 2);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(8, 6);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(473, 267);
            this.tableLayoutPanel5.TabIndex = 57;
            // 
            // statusProgressBar1
            // 
            this.statusProgressBar1.ActivatedStatusColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.statusProgressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusProgressBar1.AutoSize = true;
            this.statusProgressBar1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.statusProgressBar1.Location = new System.Drawing.Point(0, 44);
            this.statusProgressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.statusProgressBar1.Name = "statusProgressBar1";
            this.statusProgressBar1.Size = new System.Drawing.Size(473, 44);
            this.statusProgressBar1.StatusTitles = null;
            this.statusProgressBar1.TabIndex = 53;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.tableLayoutPanel4);
            this.flowLayoutPanel3.Controls.Add(this.tableLayoutPanel3);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(318, 38);
            this.flowLayoutPanel3.TabIndex = 56;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.label_syst_progressTitle, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label_syst_progress, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label_syst_runningTasks, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label_syst_runningTasksTitle, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(134, 32);
            this.tableLayoutPanel4.TabIndex = 55;
            // 
            // label_syst_progressTitle
            // 
            this.label_syst_progressTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_syst_progressTitle.Location = new System.Drawing.Point(3, 0);
            this.label_syst_progressTitle.Name = "label_syst_progressTitle";
            this.label_syst_progressTitle.Size = new System.Drawing.Size(90, 16);
            this.label_syst_progressTitle.TabIndex = 33;
            this.label_syst_progressTitle.Text = "Progress:";
            // 
            // label_syst_progress
            // 
            this.label_syst_progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_syst_progress.AutoSize = true;
            this.label_syst_progress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_syst_progress.Location = new System.Drawing.Point(99, 0);
            this.label_syst_progress.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_syst_progress.Name = "label_syst_progress";
            this.label_syst_progress.Size = new System.Drawing.Size(15, 16);
            this.label_syst_progress.TabIndex = 34;
            this.label_syst_progress.Text = "0";
            this.label_syst_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_syst_runningTasks
            // 
            this.label_syst_runningTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_syst_runningTasks.AutoSize = true;
            this.label_syst_runningTasks.Location = new System.Drawing.Point(99, 16);
            this.label_syst_runningTasks.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_syst_runningTasks.Name = "label_syst_runningTasks";
            this.label_syst_runningTasks.Size = new System.Drawing.Size(13, 16);
            this.label_syst_runningTasks.TabIndex = 53;
            this.label_syst_runningTasks.Text = "0";
            this.label_syst_runningTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_syst_runningTasksTitle
            // 
            this.label_syst_runningTasksTitle.Location = new System.Drawing.Point(3, 16);
            this.label_syst_runningTasksTitle.Name = "label_syst_runningTasksTitle";
            this.label_syst_runningTasksTitle.Size = new System.Drawing.Size(90, 16);
            this.label_syst_runningTasksTitle.TabIndex = 52;
            this.label_syst_runningTasksTitle.Text = "Running Tasks:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.label_syst_leftTimeTitle, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label_syst_leftTime, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.label_syst_totalTimeTitle, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label_syst_totalTime, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(143, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(172, 32);
            this.tableLayoutPanel3.TabIndex = 54;
            // 
            // label_syst_leftTimeTitle
            // 
            this.label_syst_leftTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_syst_leftTimeTitle.Location = new System.Drawing.Point(3, 16);
            this.label_syst_leftTimeTitle.Name = "label_syst_leftTimeTitle";
            this.label_syst_leftTimeTitle.Size = new System.Drawing.Size(130, 16);
            this.label_syst_leftTimeTitle.TabIndex = 52;
            this.label_syst_leftTimeTitle.Text = "Left Time (estimated):";
            // 
            // label_syst_leftTime
            // 
            this.label_syst_leftTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_syst_leftTime.AutoSize = true;
            this.label_syst_leftTime.Location = new System.Drawing.Point(139, 16);
            this.label_syst_leftTime.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_syst_leftTime.Name = "label_syst_leftTime";
            this.label_syst_leftTime.Size = new System.Drawing.Size(13, 16);
            this.label_syst_leftTime.TabIndex = 53;
            this.label_syst_leftTime.Text = "0";
            this.label_syst_leftTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_syst_totalTimeTitle
            // 
            this.label_syst_totalTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_syst_totalTimeTitle.Location = new System.Drawing.Point(3, 0);
            this.label_syst_totalTimeTitle.Name = "label_syst_totalTimeTitle";
            this.label_syst_totalTimeTitle.Size = new System.Drawing.Size(130, 16);
            this.label_syst_totalTimeTitle.TabIndex = 50;
            this.label_syst_totalTimeTitle.Text = "Total Time:";
            // 
            // label_syst_totalTime
            // 
            this.label_syst_totalTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_syst_totalTime.AutoSize = true;
            this.label_syst_totalTime.Location = new System.Drawing.Point(139, 0);
            this.label_syst_totalTime.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_syst_totalTime.Name = "label_syst_totalTime";
            this.label_syst_totalTime.Size = new System.Drawing.Size(13, 16);
            this.label_syst_totalTime.TabIndex = 51;
            this.label_syst_totalTime.Text = "0";
            this.label_syst_totalTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.panel_fetchFD);
            this.flowLayoutPanel6.Controls.Add(this.panel_detectCh);
            this.flowLayoutPanel6.Controls.Add(this.panel_detectCh_chTypes);
            this.flowLayoutPanel6.Controls.Add(this.panel_crDirs);
            this.flowLayoutPanel6.Controls.Add(this.panel_applyCh_speed);
            this.flowLayoutPanel6.Controls.Add(this.panel_applyCh_syncedFiles);
            this.flowLayoutPanel6.Controls.Add(this.panel_remDirs);
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 88);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(357, 208);
            this.flowLayoutPanel6.TabIndex = 52;
            // 
            // panel_fetchFD
            // 
            this.panel_fetchFD.AutoSize = true;
            this.panel_fetchFD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_fetchFD.ColumnCount = 2;
            this.panel_fetchFD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_fetchFD.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_fetchFD.Controls.Add(this.label_fetchFD_filesFoundTitle, 0, 0);
            this.panel_fetchFD.Controls.Add(this.label_fetchFD_filesFound, 1, 0);
            this.panel_fetchFD.Controls.Add(this.label_fetchFD_foldersFoundTitle, 0, 1);
            this.panel_fetchFD.Controls.Add(this.label_fetchFD_foldersFound, 1, 1);
            this.panel_fetchFD.Location = new System.Drawing.Point(3, 3);
            this.panel_fetchFD.Name = "panel_fetchFD";
            this.panel_fetchFD.RowCount = 2;
            this.panel_fetchFD.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fetchFD.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_fetchFD.Size = new System.Drawing.Size(162, 32);
            this.panel_fetchFD.TabIndex = 53;
            this.panel_fetchFD.Visible = false;
            // 
            // label_fetchFD_filesFoundTitle
            // 
            this.label_fetchFD_filesFoundTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fetchFD_filesFoundTitle.Location = new System.Drawing.Point(3, 0);
            this.label_fetchFD_filesFoundTitle.Name = "label_fetchFD_filesFoundTitle";
            this.label_fetchFD_filesFoundTitle.Size = new System.Drawing.Size(120, 16);
            this.label_fetchFD_filesFoundTitle.TabIndex = 41;
            this.label_fetchFD_filesFoundTitle.Text = "Files Found:";
            // 
            // label_fetchFD_filesFound
            // 
            this.label_fetchFD_filesFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_fetchFD_filesFound.AutoSize = true;
            this.label_fetchFD_filesFound.Location = new System.Drawing.Point(129, 0);
            this.label_fetchFD_filesFound.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_fetchFD_filesFound.Name = "label_fetchFD_filesFound";
            this.label_fetchFD_filesFound.Size = new System.Drawing.Size(13, 16);
            this.label_fetchFD_filesFound.TabIndex = 42;
            this.label_fetchFD_filesFound.Text = "0";
            this.label_fetchFD_filesFound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_fetchFD_foldersFoundTitle
            // 
            this.label_fetchFD_foldersFoundTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fetchFD_foldersFoundTitle.Location = new System.Drawing.Point(3, 16);
            this.label_fetchFD_foldersFoundTitle.Name = "label_fetchFD_foldersFoundTitle";
            this.label_fetchFD_foldersFoundTitle.Size = new System.Drawing.Size(120, 16);
            this.label_fetchFD_foldersFoundTitle.TabIndex = 46;
            this.label_fetchFD_foldersFoundTitle.Text = "Folders Found:";
            // 
            // label_fetchFD_foldersFound
            // 
            this.label_fetchFD_foldersFound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_fetchFD_foldersFound.AutoSize = true;
            this.label_fetchFD_foldersFound.Location = new System.Drawing.Point(129, 16);
            this.label_fetchFD_foldersFound.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_fetchFD_foldersFound.Name = "label_fetchFD_foldersFound";
            this.label_fetchFD_foldersFound.Size = new System.Drawing.Size(13, 16);
            this.label_fetchFD_foldersFound.TabIndex = 47;
            this.label_fetchFD_foldersFound.Text = "0";
            this.label_fetchFD_foldersFound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_detectCh
            // 
            this.panel_detectCh.AutoSize = true;
            this.panel_detectCh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_detectCh.ColumnCount = 2;
            this.panel_detectCh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_detectCh.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_detectCh.Controls.Add(this.label_detectCh_changesDetectedTitle, 0, 0);
            this.panel_detectCh.Controls.Add(this.label_detectCh_changesDetected, 1, 0);
            this.panel_detectCh.Controls.Add(this.label_detectCh_FDDoneTitle, 0, 1);
            this.panel_detectCh.Controls.Add(this.label_detectCh_FDDone, 1, 1);
            this.panel_detectCh.Location = new System.Drawing.Point(171, 3);
            this.panel_detectCh.Name = "panel_detectCh";
            this.panel_detectCh.RowCount = 2;
            this.panel_detectCh.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh.Size = new System.Drawing.Size(162, 32);
            this.panel_detectCh.TabIndex = 54;
            this.panel_detectCh.Visible = false;
            // 
            // label_detectCh_changesDetectedTitle
            // 
            this.label_detectCh_changesDetectedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_changesDetectedTitle.Location = new System.Drawing.Point(3, 0);
            this.label_detectCh_changesDetectedTitle.Name = "label_detectCh_changesDetectedTitle";
            this.label_detectCh_changesDetectedTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_changesDetectedTitle.TabIndex = 41;
            this.label_detectCh_changesDetectedTitle.Text = "Changes Detected:";
            // 
            // label_detectCh_changesDetected
            // 
            this.label_detectCh_changesDetected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_changesDetected.AutoSize = true;
            this.label_detectCh_changesDetected.Location = new System.Drawing.Point(129, 0);
            this.label_detectCh_changesDetected.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_changesDetected.Name = "label_detectCh_changesDetected";
            this.label_detectCh_changesDetected.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_changesDetected.TabIndex = 42;
            this.label_detectCh_changesDetected.Text = "0";
            this.label_detectCh_changesDetected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_detectCh_FDDoneTitle
            // 
            this.label_detectCh_FDDoneTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_FDDoneTitle.Location = new System.Drawing.Point(3, 16);
            this.label_detectCh_FDDoneTitle.Name = "label_detectCh_FDDoneTitle";
            this.label_detectCh_FDDoneTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_FDDoneTitle.TabIndex = 46;
            this.label_detectCh_FDDoneTitle.Text = "Files/Folders Done:";
            // 
            // label_detectCh_FDDone
            // 
            this.label_detectCh_FDDone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_FDDone.AutoSize = true;
            this.label_detectCh_FDDone.Location = new System.Drawing.Point(129, 16);
            this.label_detectCh_FDDone.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_FDDone.Name = "label_detectCh_FDDone";
            this.label_detectCh_FDDone.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_FDDone.TabIndex = 47;
            this.label_detectCh_FDDone.Text = "0";
            this.label_detectCh_FDDone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_detectCh_chTypes
            // 
            this.panel_detectCh_chTypes.AutoSize = true;
            this.panel_detectCh_chTypes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_detectCh_chTypes.ColumnCount = 2;
            this.panel_detectCh_chTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_detectCh_chTypes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_filesToRemoveTitle, 0, 0);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_filesToRemove, 1, 0);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_foldersToRemoveTitle, 0, 1);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_foldersToRemove, 1, 1);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_filesToCopyTitle, 0, 2);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_filesToCopy, 1, 2);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_foldersToCreateTitle, 0, 3);
            this.panel_detectCh_chTypes.Controls.Add(this.label_detectCh_foldersToCreate, 1, 3);
            this.panel_detectCh_chTypes.Location = new System.Drawing.Point(3, 41);
            this.panel_detectCh_chTypes.Name = "panel_detectCh_chTypes";
            this.panel_detectCh_chTypes.RowCount = 4;
            this.panel_detectCh_chTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh_chTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh_chTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh_chTypes.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_detectCh_chTypes.Size = new System.Drawing.Size(162, 64);
            this.panel_detectCh_chTypes.TabIndex = 55;
            this.panel_detectCh_chTypes.Visible = false;
            // 
            // label_detectCh_filesToRemoveTitle
            // 
            this.label_detectCh_filesToRemoveTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_filesToRemoveTitle.Location = new System.Drawing.Point(3, 0);
            this.label_detectCh_filesToRemoveTitle.Name = "label_detectCh_filesToRemoveTitle";
            this.label_detectCh_filesToRemoveTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_filesToRemoveTitle.TabIndex = 41;
            this.label_detectCh_filesToRemoveTitle.Text = "Files to Remove:";
            // 
            // label_detectCh_filesToRemove
            // 
            this.label_detectCh_filesToRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_filesToRemove.AutoSize = true;
            this.label_detectCh_filesToRemove.Location = new System.Drawing.Point(129, 0);
            this.label_detectCh_filesToRemove.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_filesToRemove.Name = "label_detectCh_filesToRemove";
            this.label_detectCh_filesToRemove.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_filesToRemove.TabIndex = 42;
            this.label_detectCh_filesToRemove.Text = "0";
            this.label_detectCh_filesToRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_detectCh_foldersToRemoveTitle
            // 
            this.label_detectCh_foldersToRemoveTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_foldersToRemoveTitle.Location = new System.Drawing.Point(3, 16);
            this.label_detectCh_foldersToRemoveTitle.Name = "label_detectCh_foldersToRemoveTitle";
            this.label_detectCh_foldersToRemoveTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_foldersToRemoveTitle.TabIndex = 46;
            this.label_detectCh_foldersToRemoveTitle.Text = "Folders to Remove:";
            // 
            // label_detectCh_foldersToRemove
            // 
            this.label_detectCh_foldersToRemove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_foldersToRemove.AutoSize = true;
            this.label_detectCh_foldersToRemove.Location = new System.Drawing.Point(129, 16);
            this.label_detectCh_foldersToRemove.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_foldersToRemove.Name = "label_detectCh_foldersToRemove";
            this.label_detectCh_foldersToRemove.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_foldersToRemove.TabIndex = 47;
            this.label_detectCh_foldersToRemove.Text = "0";
            this.label_detectCh_foldersToRemove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_detectCh_filesToCopyTitle
            // 
            this.label_detectCh_filesToCopyTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_filesToCopyTitle.Location = new System.Drawing.Point(3, 32);
            this.label_detectCh_filesToCopyTitle.Name = "label_detectCh_filesToCopyTitle";
            this.label_detectCh_filesToCopyTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_filesToCopyTitle.TabIndex = 48;
            this.label_detectCh_filesToCopyTitle.Text = "Files to Copy:";
            // 
            // label_detectCh_filesToCopy
            // 
            this.label_detectCh_filesToCopy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_filesToCopy.AutoSize = true;
            this.label_detectCh_filesToCopy.Location = new System.Drawing.Point(129, 32);
            this.label_detectCh_filesToCopy.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_filesToCopy.Name = "label_detectCh_filesToCopy";
            this.label_detectCh_filesToCopy.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_filesToCopy.TabIndex = 49;
            this.label_detectCh_filesToCopy.Text = "0";
            this.label_detectCh_filesToCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_detectCh_foldersToCreateTitle
            // 
            this.label_detectCh_foldersToCreateTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_detectCh_foldersToCreateTitle.Location = new System.Drawing.Point(3, 48);
            this.label_detectCh_foldersToCreateTitle.Name = "label_detectCh_foldersToCreateTitle";
            this.label_detectCh_foldersToCreateTitle.Size = new System.Drawing.Size(120, 16);
            this.label_detectCh_foldersToCreateTitle.TabIndex = 50;
            this.label_detectCh_foldersToCreateTitle.Text = "Folders to Create:";
            // 
            // label_detectCh_foldersToCreate
            // 
            this.label_detectCh_foldersToCreate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_detectCh_foldersToCreate.AutoSize = true;
            this.label_detectCh_foldersToCreate.Location = new System.Drawing.Point(129, 48);
            this.label_detectCh_foldersToCreate.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_detectCh_foldersToCreate.Name = "label_detectCh_foldersToCreate";
            this.label_detectCh_foldersToCreate.Size = new System.Drawing.Size(13, 16);
            this.label_detectCh_foldersToCreate.TabIndex = 51;
            this.label_detectCh_foldersToCreate.Text = "0";
            this.label_detectCh_foldersToCreate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_crDirs
            // 
            this.panel_crDirs.AutoSize = true;
            this.panel_crDirs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_crDirs.ColumnCount = 2;
            this.panel_crDirs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_crDirs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_crDirs.Controls.Add(this.label_crDirs_dirsCreatedTitle, 0, 0);
            this.panel_crDirs.Controls.Add(this.label_crDirs_dirsCreated, 1, 0);
            this.panel_crDirs.Location = new System.Drawing.Point(171, 41);
            this.panel_crDirs.Name = "panel_crDirs";
            this.panel_crDirs.RowCount = 1;
            this.panel_crDirs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_crDirs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.panel_crDirs.Size = new System.Drawing.Size(183, 16);
            this.panel_crDirs.TabIndex = 56;
            this.panel_crDirs.Visible = false;
            // 
            // label_crDirs_dirsCreatedTitle
            // 
            this.label_crDirs_dirsCreatedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_crDirs_dirsCreatedTitle.Location = new System.Drawing.Point(3, 0);
            this.label_crDirs_dirsCreatedTitle.Name = "label_crDirs_dirsCreatedTitle";
            this.label_crDirs_dirsCreatedTitle.Size = new System.Drawing.Size(120, 16);
            this.label_crDirs_dirsCreatedTitle.TabIndex = 41;
            this.label_crDirs_dirsCreatedTitle.Text = "Folders Created:";
            // 
            // label_crDirs_dirsCreated
            // 
            this.label_crDirs_dirsCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_crDirs_dirsCreated.AutoSize = true;
            this.label_crDirs_dirsCreated.Location = new System.Drawing.Point(129, 0);
            this.label_crDirs_dirsCreated.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_crDirs_dirsCreated.Name = "label_crDirs_dirsCreated";
            this.label_crDirs_dirsCreated.Size = new System.Drawing.Size(34, 16);
            this.label_crDirs_dirsCreated.TabIndex = 42;
            this.label_crDirs_dirsCreated.Text = "0 of 0";
            this.label_crDirs_dirsCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_applyCh_speed
            // 
            this.panel_applyCh_speed.AutoSize = true;
            this.panel_applyCh_speed.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_applyCh_speed.ColumnCount = 2;
            this.panel_applyCh_speed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_applyCh_speed.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_applyCh_speed.Controls.Add(this.label_applyCh_speed_currentTitle, 0, 0);
            this.panel_applyCh_speed.Controls.Add(this.label_applyCh_speed_current, 1, 0);
            this.panel_applyCh_speed.Controls.Add(this.label_applyCh_speed_averageTitle, 0, 1);
            this.panel_applyCh_speed.Controls.Add(this.label_applyCh_speed_average, 1, 1);
            this.panel_applyCh_speed.Location = new System.Drawing.Point(3, 111);
            this.panel_applyCh_speed.Name = "panel_applyCh_speed";
            this.panel_applyCh_speed.RowCount = 2;
            this.panel_applyCh_speed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_applyCh_speed.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_applyCh_speed.Size = new System.Drawing.Size(159, 32);
            this.panel_applyCh_speed.TabIndex = 53;
            this.panel_applyCh_speed.Visible = false;
            // 
            // label_applyCh_speed_currentTitle
            // 
            this.label_applyCh_speed_currentTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_speed_currentTitle.Location = new System.Drawing.Point(3, 0);
            this.label_applyCh_speed_currentTitle.Name = "label_applyCh_speed_currentTitle";
            this.label_applyCh_speed_currentTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_speed_currentTitle.TabIndex = 41;
            this.label_applyCh_speed_currentTitle.Text = "Current Speed:";
            // 
            // label_applyCh_speed_current
            // 
            this.label_applyCh_speed_current.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_speed_current.AutoSize = true;
            this.label_applyCh_speed_current.Location = new System.Drawing.Point(129, 0);
            this.label_applyCh_speed_current.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_speed_current.Name = "label_applyCh_speed_current";
            this.label_applyCh_speed_current.Size = new System.Drawing.Size(10, 16);
            this.label_applyCh_speed_current.TabIndex = 42;
            this.label_applyCh_speed_current.Text = " ";
            this.label_applyCh_speed_current.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_applyCh_speed_averageTitle
            // 
            this.label_applyCh_speed_averageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_speed_averageTitle.Location = new System.Drawing.Point(3, 16);
            this.label_applyCh_speed_averageTitle.Name = "label_applyCh_speed_averageTitle";
            this.label_applyCh_speed_averageTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_speed_averageTitle.TabIndex = 46;
            this.label_applyCh_speed_averageTitle.Text = "Average Speed:";
            // 
            // label_applyCh_speed_average
            // 
            this.label_applyCh_speed_average.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_speed_average.AutoSize = true;
            this.label_applyCh_speed_average.Location = new System.Drawing.Point(129, 16);
            this.label_applyCh_speed_average.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_speed_average.Name = "label_applyCh_speed_average";
            this.label_applyCh_speed_average.Size = new System.Drawing.Size(10, 16);
            this.label_applyCh_speed_average.TabIndex = 47;
            this.label_applyCh_speed_average.Text = " ";
            this.label_applyCh_speed_average.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_applyCh_syncedFiles
            // 
            this.panel_applyCh_syncedFiles.AutoSize = true;
            this.panel_applyCh_syncedFiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_applyCh_syncedFiles.ColumnCount = 2;
            this.panel_applyCh_syncedFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_applyCh_syncedFiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_removedFilesSizeTitle, 0, 3);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_copiedFilesCount, 1, 0);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_copiedFilesCountTitle, 0, 0);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_copiedFilesSizeTitle, 0, 1);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_removedFilesCountTitle, 0, 2);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_copiedFilesSize, 1, 1);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_removedFilesCount, 1, 2);
            this.panel_applyCh_syncedFiles.Controls.Add(this.label_applyCh_removedFilesSize, 1, 3);
            this.panel_applyCh_syncedFiles.Location = new System.Drawing.Point(168, 111);
            this.panel_applyCh_syncedFiles.Name = "panel_applyCh_syncedFiles";
            this.panel_applyCh_syncedFiles.RowCount = 4;
            this.panel_applyCh_syncedFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_applyCh_syncedFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_applyCh_syncedFiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.panel_applyCh_syncedFiles.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_applyCh_syncedFiles.Size = new System.Drawing.Size(159, 72);
            this.panel_applyCh_syncedFiles.TabIndex = 52;
            this.panel_applyCh_syncedFiles.Visible = false;
            // 
            // label_applyCh_removedFilesSizeTitle
            // 
            this.label_applyCh_removedFilesSizeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_removedFilesSizeTitle.Location = new System.Drawing.Point(3, 56);
            this.label_applyCh_removedFilesSizeTitle.Name = "label_applyCh_removedFilesSizeTitle";
            this.label_applyCh_removedFilesSizeTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_removedFilesSizeTitle.TabIndex = 41;
            this.label_applyCh_removedFilesSizeTitle.Text = "Removed Files Size:";
            // 
            // label_applyCh_copiedFilesCount
            // 
            this.label_applyCh_copiedFilesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_copiedFilesCount.AutoSize = true;
            this.label_applyCh_copiedFilesCount.Location = new System.Drawing.Point(129, 0);
            this.label_applyCh_copiedFilesCount.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_copiedFilesCount.Name = "label_applyCh_copiedFilesCount";
            this.label_applyCh_copiedFilesCount.Size = new System.Drawing.Size(10, 16);
            this.label_applyCh_copiedFilesCount.TabIndex = 40;
            this.label_applyCh_copiedFilesCount.Text = " ";
            this.label_applyCh_copiedFilesCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_applyCh_copiedFilesCountTitle
            // 
            this.label_applyCh_copiedFilesCountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_copiedFilesCountTitle.Location = new System.Drawing.Point(3, 0);
            this.label_applyCh_copiedFilesCountTitle.Name = "label_applyCh_copiedFilesCountTitle";
            this.label_applyCh_copiedFilesCountTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_copiedFilesCountTitle.TabIndex = 39;
            this.label_applyCh_copiedFilesCountTitle.Text = "Copied Files:";
            // 
            // label_applyCh_copiedFilesSizeTitle
            // 
            this.label_applyCh_copiedFilesSizeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_copiedFilesSizeTitle.Location = new System.Drawing.Point(3, 16);
            this.label_applyCh_copiedFilesSizeTitle.Name = "label_applyCh_copiedFilesSizeTitle";
            this.label_applyCh_copiedFilesSizeTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_copiedFilesSizeTitle.TabIndex = 37;
            this.label_applyCh_copiedFilesSizeTitle.Text = "Copied Files Size:";
            // 
            // label_applyCh_removedFilesCountTitle
            // 
            this.label_applyCh_removedFilesCountTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_applyCh_removedFilesCountTitle.Location = new System.Drawing.Point(3, 36);
            this.label_applyCh_removedFilesCountTitle.Name = "label_applyCh_removedFilesCountTitle";
            this.label_applyCh_removedFilesCountTitle.Size = new System.Drawing.Size(120, 16);
            this.label_applyCh_removedFilesCountTitle.TabIndex = 42;
            this.label_applyCh_removedFilesCountTitle.Text = "Removed Files:";
            // 
            // label_applyCh_copiedFilesSize
            // 
            this.label_applyCh_copiedFilesSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_copiedFilesSize.AutoSize = true;
            this.label_applyCh_copiedFilesSize.Location = new System.Drawing.Point(129, 16);
            this.label_applyCh_copiedFilesSize.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_copiedFilesSize.Name = "label_applyCh_copiedFilesSize";
            this.label_applyCh_copiedFilesSize.Size = new System.Drawing.Size(10, 20);
            this.label_applyCh_copiedFilesSize.TabIndex = 38;
            this.label_applyCh_copiedFilesSize.Text = " ";
            this.label_applyCh_copiedFilesSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_applyCh_removedFilesCount
            // 
            this.label_applyCh_removedFilesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_removedFilesCount.AutoSize = true;
            this.label_applyCh_removedFilesCount.Location = new System.Drawing.Point(129, 36);
            this.label_applyCh_removedFilesCount.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_removedFilesCount.Name = "label_applyCh_removedFilesCount";
            this.label_applyCh_removedFilesCount.Size = new System.Drawing.Size(10, 20);
            this.label_applyCh_removedFilesCount.TabIndex = 43;
            this.label_applyCh_removedFilesCount.Text = " ";
            this.label_applyCh_removedFilesCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_applyCh_removedFilesSize
            // 
            this.label_applyCh_removedFilesSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_applyCh_removedFilesSize.AutoSize = true;
            this.label_applyCh_removedFilesSize.Location = new System.Drawing.Point(129, 56);
            this.label_applyCh_removedFilesSize.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_applyCh_removedFilesSize.Name = "label_applyCh_removedFilesSize";
            this.label_applyCh_removedFilesSize.Size = new System.Drawing.Size(10, 16);
            this.label_applyCh_removedFilesSize.TabIndex = 44;
            this.label_applyCh_removedFilesSize.Text = " ";
            this.label_applyCh_removedFilesSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel_remDirs
            // 
            this.panel_remDirs.AutoSize = true;
            this.panel_remDirs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel_remDirs.ColumnCount = 2;
            this.panel_remDirs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_remDirs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panel_remDirs.Controls.Add(this.label_remDirs_foldersRemovedTitle, 0, 0);
            this.panel_remDirs.Controls.Add(this.label_remDirs_foldersRemoved, 1, 0);
            this.panel_remDirs.Location = new System.Drawing.Point(3, 189);
            this.panel_remDirs.Name = "panel_remDirs";
            this.panel_remDirs.RowCount = 1;
            this.panel_remDirs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.panel_remDirs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.panel_remDirs.Size = new System.Drawing.Size(183, 16);
            this.panel_remDirs.TabIndex = 57;
            this.panel_remDirs.Visible = false;
            // 
            // label_remDirs_foldersRemovedTitle
            // 
            this.label_remDirs_foldersRemovedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_remDirs_foldersRemovedTitle.Location = new System.Drawing.Point(3, 0);
            this.label_remDirs_foldersRemovedTitle.Name = "label_remDirs_foldersRemovedTitle";
            this.label_remDirs_foldersRemovedTitle.Size = new System.Drawing.Size(120, 16);
            this.label_remDirs_foldersRemovedTitle.TabIndex = 41;
            this.label_remDirs_foldersRemovedTitle.Text = "Folders Removed:";
            // 
            // label_remDirs_foldersRemoved
            // 
            this.label_remDirs_foldersRemoved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_remDirs_foldersRemoved.AutoSize = true;
            this.label_remDirs_foldersRemoved.Location = new System.Drawing.Point(129, 0);
            this.label_remDirs_foldersRemoved.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_remDirs_foldersRemoved.Name = "label_remDirs_foldersRemoved";
            this.label_remDirs_foldersRemoved.Size = new System.Drawing.Size(34, 16);
            this.label_remDirs_foldersRemoved.TabIndex = 42;
            this.label_remDirs_foldersRemoved.Text = "0 of 0";
            this.label_remDirs_foldersRemoved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage_syncElementInfo
            // 
            this.tabPage_syncElementInfo.Controls.Add(this.tableLayoutPanel_sei);
            this.tabPage_syncElementInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPage_syncElementInfo.Name = "tabPage_syncElementInfo";
            this.tabPage_syncElementInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_syncElementInfo.Size = new System.Drawing.Size(487, 279);
            this.tabPage_syncElementInfo.TabIndex = 2;
            this.tabPage_syncElementInfo.Text = "Sync Element Info";
            this.tabPage_syncElementInfo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel_sei
            // 
            this.tableLayoutPanel_sei.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_sei.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_sei.ColumnCount = 1;
            this.tableLayoutPanel_sei.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_sei.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel_sei.Controls.Add(this.flowLayoutPanel9, 0, 3);
            this.tableLayoutPanel_sei.Controls.Add(this.label_sei_syncExecInfoTitle, 0, 2);
            this.tableLayoutPanel_sei.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel_sei.Controls.Add(this.button_refresh, 0, 4);
            this.tableLayoutPanel_sei.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel_sei.Name = "tableLayoutPanel_sei";
            this.tableLayoutPanel_sei.RowCount = 5;
            this.tableLayoutPanel_sei.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_sei.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_sei.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_sei.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_sei.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_sei.Size = new System.Drawing.Size(475, 267);
            this.tableLayoutPanel_sei.TabIndex = 61;
            this.tableLayoutPanel_sei.Visible = false;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel5.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(184, 21);
            this.flowLayoutPanel5.TabIndex = 59;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label_sei_nameTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_sei_name, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(84, 15);
            this.tableLayoutPanel1.TabIndex = 55;
            // 
            // label_sei_nameTitle
            // 
            this.label_sei_nameTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_nameTitle.AutoSize = true;
            this.label_sei_nameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_nameTitle.Location = new System.Drawing.Point(3, 0);
            this.label_sei_nameTitle.Name = "label_sei_nameTitle";
            this.label_sei_nameTitle.Size = new System.Drawing.Size(44, 15);
            this.label_sei_nameTitle.TabIndex = 33;
            this.label_sei_nameTitle.Text = "Name:";
            this.label_sei_nameTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_name
            // 
            this.label_sei_name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_name.AutoSize = true;
            this.label_sei_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_name.Location = new System.Drawing.Point(53, 0);
            this.label_sei_name.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_name.Name = "label_sei_name";
            this.label_sei_name.Size = new System.Drawing.Size(11, 15);
            this.label_sei_name.TabIndex = 34;
            this.label_sei_name.Text = " ";
            this.label_sei_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label_sei_typeTitle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_sei_type, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(93, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(88, 15);
            this.tableLayoutPanel2.TabIndex = 54;
            // 
            // label_sei_typeTitle
            // 
            this.label_sei_typeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_typeTitle.AutoSize = true;
            this.label_sei_typeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_typeTitle.Location = new System.Drawing.Point(3, 0);
            this.label_sei_typeTitle.Name = "label_sei_typeTitle";
            this.label_sei_typeTitle.Size = new System.Drawing.Size(36, 15);
            this.label_sei_typeTitle.TabIndex = 50;
            this.label_sei_typeTitle.Text = "Type:";
            this.label_sei_typeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_type
            // 
            this.label_sei_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_type.AutoSize = true;
            this.label_sei_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_type.Location = new System.Drawing.Point(45, 0);
            this.label_sei_type.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_type.Name = "label_sei_type";
            this.label_sei_type.Size = new System.Drawing.Size(23, 15);
            this.label_sei_type.TabIndex = 51;
            this.label_sei_type.Text = "File";
            this.label_sei_type.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel9.AutoSize = true;
            this.flowLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel9.Controls.Add(this.tableLayoutPanel7);
            this.flowLayoutPanel9.Controls.Add(this.tableLayoutPanel9);
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 97);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(469, 125);
            this.flowLayoutPanel9.TabIndex = 60;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.AutoSize = true;
            this.tableLayoutPanel7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.label_sei_startTimeTitle, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label_sei_startTime, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label_sei_endTime, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.label_sei_endTimeTitle, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label_sei_duration, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.label_sei_durationTitle, 0, 2);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 3;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(105, 45);
            this.tableLayoutPanel7.TabIndex = 55;
            // 
            // label_sei_startTimeTitle
            // 
            this.label_sei_startTimeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_startTimeTitle.AutoSize = true;
            this.label_sei_startTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_startTimeTitle.Location = new System.Drawing.Point(3, 0);
            this.label_sei_startTimeTitle.Name = "label_sei_startTimeTitle";
            this.label_sei_startTimeTitle.Size = new System.Drawing.Size(66, 15);
            this.label_sei_startTimeTitle.TabIndex = 33;
            this.label_sei_startTimeTitle.Text = "Start Time:";
            this.label_sei_startTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_startTime
            // 
            this.label_sei_startTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_startTime.AutoSize = true;
            this.label_sei_startTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_startTime.Location = new System.Drawing.Point(75, 0);
            this.label_sei_startTime.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_startTime.Name = "label_sei_startTime";
            this.label_sei_startTime.Size = new System.Drawing.Size(10, 15);
            this.label_sei_startTime.TabIndex = 34;
            this.label_sei_startTime.Text = " ";
            this.label_sei_startTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_endTime
            // 
            this.label_sei_endTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_endTime.AutoSize = true;
            this.label_sei_endTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_endTime.Location = new System.Drawing.Point(75, 15);
            this.label_sei_endTime.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_endTime.Name = "label_sei_endTime";
            this.label_sei_endTime.Size = new System.Drawing.Size(10, 15);
            this.label_sei_endTime.TabIndex = 36;
            this.label_sei_endTime.Text = " ";
            this.label_sei_endTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_endTimeTitle
            // 
            this.label_sei_endTimeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_endTimeTitle.AutoSize = true;
            this.label_sei_endTimeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_endTimeTitle.Location = new System.Drawing.Point(3, 15);
            this.label_sei_endTimeTitle.Name = "label_sei_endTimeTitle";
            this.label_sei_endTimeTitle.Size = new System.Drawing.Size(63, 15);
            this.label_sei_endTimeTitle.TabIndex = 35;
            this.label_sei_endTimeTitle.Text = "End Time:";
            this.label_sei_endTimeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_duration
            // 
            this.label_sei_duration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_duration.AutoSize = true;
            this.label_sei_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_duration.Location = new System.Drawing.Point(75, 30);
            this.label_sei_duration.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_duration.Name = "label_sei_duration";
            this.label_sei_duration.Size = new System.Drawing.Size(10, 15);
            this.label_sei_duration.TabIndex = 38;
            this.label_sei_duration.Text = " ";
            this.label_sei_duration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_durationTitle
            // 
            this.label_sei_durationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_durationTitle.AutoSize = true;
            this.label_sei_durationTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_durationTitle.Location = new System.Drawing.Point(3, 30);
            this.label_sei_durationTitle.Name = "label_sei_durationTitle";
            this.label_sei_durationTitle.Size = new System.Drawing.Size(57, 15);
            this.label_sei_durationTitle.TabIndex = 37;
            this.label_sei_durationTitle.Text = "Duration:";
            this.label_sei_durationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.AutoSize = true;
            this.tableLayoutPanel9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.label_sei_syncStatusTitle, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.label_sei_syncStatus, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.label_sei_infoTitle, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.textBox_sei_info, 1, 1);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 54);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(469, 68);
            this.tableLayoutPanel9.TabIndex = 61;
            // 
            // label_sei_syncStatusTitle
            // 
            this.label_sei_syncStatusTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_syncStatusTitle.AutoSize = true;
            this.label_sei_syncStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_syncStatusTitle.Location = new System.Drawing.Point(3, 0);
            this.label_sei_syncStatusTitle.Name = "label_sei_syncStatusTitle";
            this.label_sei_syncStatusTitle.Size = new System.Drawing.Size(44, 15);
            this.label_sei_syncStatusTitle.TabIndex = 33;
            this.label_sei_syncStatusTitle.Text = "Status:";
            this.label_sei_syncStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_syncStatus
            // 
            this.label_sei_syncStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_syncStatus.AutoSize = true;
            this.label_sei_syncStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_syncStatus.Location = new System.Drawing.Point(53, 0);
            this.label_sei_syncStatus.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_syncStatus.Name = "label_sei_syncStatus";
            this.label_sei_syncStatus.Size = new System.Drawing.Size(10, 15);
            this.label_sei_syncStatus.TabIndex = 34;
            this.label_sei_syncStatus.Text = " ";
            this.label_sei_syncStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_infoTitle
            // 
            this.label_sei_infoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_infoTitle.AutoSize = true;
            this.label_sei_infoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_infoTitle.Location = new System.Drawing.Point(3, 15);
            this.label_sei_infoTitle.Name = "label_sei_infoTitle";
            this.label_sei_infoTitle.Size = new System.Drawing.Size(30, 53);
            this.label_sei_infoTitle.TabIndex = 35;
            this.label_sei_infoTitle.Text = "Info:";
            this.label_sei_infoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_sei_info
            // 
            this.textBox_sei_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_sei_info.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBox_sei_info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_sei_info.Location = new System.Drawing.Point(53, 18);
            this.textBox_sei_info.Multiline = true;
            this.textBox_sei_info.Name = "textBox_sei_info";
            this.textBox_sei_info.ReadOnly = true;
            this.textBox_sei_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_sei_info.Size = new System.Drawing.Size(413, 47);
            this.textBox_sei_info.TabIndex = 36;
            this.textBox_sei_info.Text = " ";
            // 
            // label_sei_syncExecInfoTitle
            // 
            this.label_sei_syncExecInfoTitle.AutoSize = true;
            this.label_sei_syncExecInfoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label_sei_syncExecInfoTitle.Location = new System.Drawing.Point(3, 79);
            this.label_sei_syncExecInfoTitle.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label_sei_syncExecInfoTitle.Name = "label_sei_syncExecInfoTitle";
            this.label_sei_syncExecInfoTitle.Size = new System.Drawing.Size(132, 15);
            this.label_sei_syncExecInfoTitle.TabIndex = 61;
            this.label_sei_syncExecInfoTitle.Text = "Sync Execution Info";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel6);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel8);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(194, 36);
            this.flowLayoutPanel1.TabIndex = 62;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.AutoSize = true;
            this.tableLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.label_sei_path1Title, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label_sei_path1, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label_sei_path2, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label_sei_path2Title, 0, 1);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(84, 30);
            this.tableLayoutPanel6.TabIndex = 55;
            // 
            // label_sei_path1Title
            // 
            this.label_sei_path1Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_path1Title.AutoSize = true;
            this.label_sei_path1Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_path1Title.Location = new System.Drawing.Point(3, 0);
            this.label_sei_path1Title.Name = "label_sei_path1Title";
            this.label_sei_path1Title.Size = new System.Drawing.Size(45, 15);
            this.label_sei_path1Title.TabIndex = 33;
            this.label_sei_path1Title.Text = "Path 1:";
            this.label_sei_path1Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_path1
            // 
            this.label_sei_path1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_path1.AutoSize = true;
            this.label_sei_path1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_path1.Location = new System.Drawing.Point(54, 0);
            this.label_sei_path1.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_path1.Name = "label_sei_path1";
            this.label_sei_path1.Size = new System.Drawing.Size(10, 15);
            this.label_sei_path1.TabIndex = 34;
            this.label_sei_path1.Text = " ";
            this.label_sei_path1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_path2
            // 
            this.label_sei_path2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_path2.AutoSize = true;
            this.label_sei_path2.Location = new System.Drawing.Point(54, 15);
            this.label_sei_path2.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_path2.Name = "label_sei_path2";
            this.label_sei_path2.Size = new System.Drawing.Size(10, 15);
            this.label_sei_path2.TabIndex = 51;
            this.label_sei_path2.Text = " ";
            this.label_sei_path2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_path2Title
            // 
            this.label_sei_path2Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_path2Title.AutoSize = true;
            this.label_sei_path2Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_path2Title.Location = new System.Drawing.Point(3, 15);
            this.label_sei_path2Title.Name = "label_sei_path2Title";
            this.label_sei_path2Title.Size = new System.Drawing.Size(45, 15);
            this.label_sei_path2Title.TabIndex = 50;
            this.label_sei_path2Title.Text = "Path 2:";
            this.label_sei_path2Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.AutoSize = true;
            this.tableLayoutPanel8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.label_sei_directionTitle, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.label_sei_direction, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.label_sei_remove, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.label_sei_removeTitle, 0, 1);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(93, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(98, 30);
            this.tableLayoutPanel8.TabIndex = 55;
            // 
            // label_sei_directionTitle
            // 
            this.label_sei_directionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_directionTitle.AutoSize = true;
            this.label_sei_directionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_directionTitle.Location = new System.Drawing.Point(3, 0);
            this.label_sei_directionTitle.Name = "label_sei_directionTitle";
            this.label_sei_directionTitle.Size = new System.Drawing.Size(59, 15);
            this.label_sei_directionTitle.TabIndex = 33;
            this.label_sei_directionTitle.Text = "Direction:";
            this.label_sei_directionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_direction
            // 
            this.label_sei_direction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_direction.AutoSize = true;
            this.label_sei_direction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_direction.Location = new System.Drawing.Point(68, 0);
            this.label_sei_direction.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_direction.Name = "label_sei_direction";
            this.label_sei_direction.Size = new System.Drawing.Size(10, 15);
            this.label_sei_direction.TabIndex = 34;
            this.label_sei_direction.Text = " ";
            this.label_sei_direction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_remove
            // 
            this.label_sei_remove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_remove.AutoSize = true;
            this.label_sei_remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label_sei_remove.Location = new System.Drawing.Point(68, 15);
            this.label_sei_remove.Margin = new System.Windows.Forms.Padding(3, 0, 20, 0);
            this.label_sei_remove.Name = "label_sei_remove";
            this.label_sei_remove.Size = new System.Drawing.Size(10, 15);
            this.label_sei_remove.TabIndex = 36;
            this.label_sei_remove.Text = " ";
            this.label_sei_remove.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_sei_removeTitle
            // 
            this.label_sei_removeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sei_removeTitle.AutoSize = true;
            this.label_sei_removeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sei_removeTitle.Location = new System.Drawing.Point(3, 15);
            this.label_sei_removeTitle.Name = "label_sei_removeTitle";
            this.label_sei_removeTitle.Size = new System.Drawing.Size(56, 15);
            this.label_sei_removeTitle.TabIndex = 35;
            this.label_sei_removeTitle.Text = "Remove:";
            this.label_sei_removeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_refresh
            // 
            this.button_refresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.button_refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_refresh.FlatAppearance.BorderSize = 0;
            this.button_refresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(66)))), ((int)(((byte)(135)))));
            this.button_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_refresh.ForeColor = System.Drawing.Color.White;
            this.button_refresh.Location = new System.Drawing.Point(3, 228);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(88, 24);
            this.button_refresh.TabIndex = 63;
            this.button_refresh.Text = "refresh";
            this.button_refresh.UseVisualStyleBackColor = false;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.label32);
            this.panel6.Controls.Add(this.listBox_log);
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(492, 185);
            this.panel6.TabIndex = 0;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label32.Location = new System.Drawing.Point(3, 3);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(32, 17);
            this.label32.TabIndex = 20;
            this.label32.Text = "Log";
            // 
            // listBox_log
            // 
            this.listBox_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_log.BackColor = System.Drawing.SystemColors.Window;
            this.listBox_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox_log.FormattingEnabled = true;
            this.listBox_log.HorizontalScrollbar = true;
            this.listBox_log.Location = new System.Drawing.Point(3, 29);
            this.listBox_log.Name = "listBox_log";
            this.listBox_log.Size = new System.Drawing.Size(486, 145);
            this.listBox_log.TabIndex = 19;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.BackColor = System.Drawing.Color.White;
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(67)))), ((int)(((byte)(102)))));
            this.progressBar.Location = new System.Drawing.Point(0, 34);
            this.progressBar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(492, 10);
            this.progressBar.TabIndex = 22;
            // 
            // imageList_nodeElements
            // 
            this.imageList_nodeElements.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_nodeElements.ImageStream")));
            this.imageList_nodeElements.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_nodeElements.Images.SetKeyName(0, "ic_file_black_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(1, "ic_folder_black_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(2, "ic_folder_blue_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(3, "ic_folder_green_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(4, "ic_folder_red_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(5, "ic_folder_outline_black_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(6, "ic_folder_outline_blue_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(7, "ic_folder_outline_green_24px-01.png");
            this.imageList_nodeElements.Images.SetKeyName(8, "ic_folder_outline_red_24px-01.png");
            // 
            // splitContainer_main
            // 
            this.splitContainer_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer_main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer_main.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_main.Name = "splitContainer_main";
            // 
            // splitContainer_main.Panel1
            // 
            this.splitContainer_main.Panel1.Controls.Add(this.tableLayoutPanel_left);
            // 
            // splitContainer_main.Panel2
            // 
            this.splitContainer_main.Panel2.Controls.Add(this.treeView1);
            this.splitContainer_main.Size = new System.Drawing.Size(795, 550);
            this.splitContainer_main.SplitterDistance = 494;
            this.splitContainer_main.TabIndex = 25;
            // 
            // tableLayoutPanel_left
            // 
            this.tableLayoutPanel_left.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_left.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel_left.ColumnCount = 1;
            this.tableLayoutPanel_left.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_left.Controls.Add(this.panel_header, 0, 0);
            this.tableLayoutPanel_left.Controls.Add(this.progressBar, 0, 1);
            this.tableLayoutPanel_left.Controls.Add(this.splitContainer_left, 0, 2);
            this.tableLayoutPanel_left.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_left.Name = "tableLayoutPanel_left";
            this.tableLayoutPanel_left.RowCount = 3;
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_left.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_left.Size = new System.Drawing.Size(492, 548);
            this.tableLayoutPanel_left.TabIndex = 22;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.LightGray;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList_nodeElements;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(295, 548);
            this.treeView1.TabIndex = 24;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // SyncDetailInfoForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 585);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncDetailInfoForm2";
            this.Padding = new System.Windows.Forms.Padding(5, 30, 5, 5);
            this.Text = "Sync Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncDetailInfoForm2_FormClosing);
            this.Resize += new System.EventHandler(this.SyncDetailInfoForm2_Resize);
            this.Controls.SetChildIndex(this._contentPanel, 0);
            this._contentPanel.ResumeLayout(false);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.splitContainer_left.Panel1.ResumeLayout(false);
            this.splitContainer_left.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_left)).EndInit();
            this.splitContainer_left.ResumeLayout(false);
            this.tabControl_left1.ResumeLayout(false);
            this.tabPage_linkInfo.ResumeLayout(false);
            this.panel_linkInfo.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage_syncInfo.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.panel_fetchFD.ResumeLayout(false);
            this.panel_fetchFD.PerformLayout();
            this.panel_detectCh.ResumeLayout(false);
            this.panel_detectCh.PerformLayout();
            this.panel_detectCh_chTypes.ResumeLayout(false);
            this.panel_detectCh_chTypes.PerformLayout();
            this.panel_crDirs.ResumeLayout(false);
            this.panel_crDirs.PerformLayout();
            this.panel_applyCh_speed.ResumeLayout(false);
            this.panel_applyCh_speed.PerformLayout();
            this.panel_applyCh_syncedFiles.ResumeLayout(false);
            this.panel_applyCh_syncedFiles.PerformLayout();
            this.panel_remDirs.ResumeLayout(false);
            this.panel_remDirs.PerformLayout();
            this.tabPage_syncElementInfo.ResumeLayout(false);
            this.tableLayoutPanel_sei.ResumeLayout(false);
            this.tableLayoutPanel_sei.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.splitContainer_main.Panel1.ResumeLayout(false);
            this.splitContainer_main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_main)).EndInit();
            this.splitContainer_main.ResumeLayout(false);
            this.tableLayoutPanel_left.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Label label_title;
        private MyButton button_sync;
        private MyButton button_pr;
        private AnimatedProgressBar progressBar;
        private System.Windows.Forms.ImageList imageList_nodeElements;
        private System.Windows.Forms.SplitContainer splitContainer_main;
        private System.Windows.Forms.SplitContainer splitContainer_left;
        private System.Windows.Forms.Panel panel_linkInfo;
        private System.Windows.Forms.Label label_link_directionTitle;
        private System.Windows.Forms.Label label_link_folder2;
        private System.Windows.Forms.Label label_link_folder2Title;
        private System.Windows.Forms.Label label_link_folder1;
        private System.Windows.Forms.Label label_link_folder1Title;
        private System.Windows.Forms.Label label_link_direction;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ListBox listBox_log;
        private System.Windows.Forms.TabControl tabControl_left1;
        private System.Windows.Forms.TabPage tabPage_linkInfo;
        private System.Windows.Forms.TabPage tabPage_syncInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private StatusProgressBar statusProgressBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label_syst_progressTitle;
        private System.Windows.Forms.Label label_syst_progress;
        private System.Windows.Forms.Label label_syst_runningTasks;
        private System.Windows.Forms.Label label_syst_runningTasksTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label_syst_leftTimeTitle;
        private System.Windows.Forms.Label label_syst_leftTime;
        private System.Windows.Forms.Label label_syst_totalTimeTitle;
        private System.Windows.Forms.Label label_syst_totalTime;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel panel_applyCh_speed;
        private System.Windows.Forms.Label label_applyCh_speed_currentTitle;
        private System.Windows.Forms.Label label_applyCh_speed_current;
        private System.Windows.Forms.Label label_applyCh_speed_averageTitle;
        private System.Windows.Forms.Label label_applyCh_speed_average;
        private System.Windows.Forms.TableLayoutPanel panel_applyCh_syncedFiles;
        private System.Windows.Forms.Label label_applyCh_copiedFilesCount;
        private System.Windows.Forms.Label label_applyCh_copiedFilesCountTitle;
        private System.Windows.Forms.Label label_applyCh_copiedFilesSizeTitle;
        private System.Windows.Forms.Label label_applyCh_copiedFilesSize;
        private System.Windows.Forms.TableLayoutPanel panel_fetchFD;
        private System.Windows.Forms.Label label_fetchFD_filesFoundTitle;
        private System.Windows.Forms.Label label_fetchFD_filesFound;
        private System.Windows.Forms.Label label_fetchFD_foldersFoundTitle;
        private System.Windows.Forms.Label label_fetchFD_foldersFound;
        private System.Windows.Forms.TableLayoutPanel panel_detectCh;
        private System.Windows.Forms.Label label_detectCh_changesDetectedTitle;
        private System.Windows.Forms.Label label_detectCh_changesDetected;
        private System.Windows.Forms.Label label_detectCh_FDDoneTitle;
        private System.Windows.Forms.Label label_detectCh_FDDone;
        private System.Windows.Forms.TableLayoutPanel panel_detectCh_chTypes;
        private System.Windows.Forms.Label label_detectCh_filesToRemoveTitle;
        private System.Windows.Forms.Label label_detectCh_filesToRemove;
        private System.Windows.Forms.Label label_detectCh_foldersToRemove;
        private System.Windows.Forms.Label label_detectCh_foldersToRemoveTitle;
        private System.Windows.Forms.Label label_detectCh_foldersToCreateTitle;
        private System.Windows.Forms.Label label_detectCh_filesToCopyTitle;
        private System.Windows.Forms.Label label_detectCh_filesToCopy;
        private System.Windows.Forms.Label label_detectCh_foldersToCreate;
        private System.Windows.Forms.TableLayoutPanel panel_crDirs;
        private System.Windows.Forms.Label label_crDirs_dirsCreatedTitle;
        private System.Windows.Forms.Label label_crDirs_dirsCreated;
        private System.Windows.Forms.TableLayoutPanel panel_remDirs;
        private System.Windows.Forms.Label label_remDirs_foldersRemovedTitle;
        private System.Windows.Forms.Label label_remDirs_foldersRemoved;
        private System.Windows.Forms.Label label_applyCh_removedFilesSizeTitle;
        private System.Windows.Forms.Label label_applyCh_removedFilesCountTitle;
        private System.Windows.Forms.Label label_applyCh_removedFilesCount;
        private System.Windows.Forms.Label label_applyCh_removedFilesSize;
        private System.Windows.Forms.TabPage tabPage_syncElementInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label_sei_path1Title;
        private System.Windows.Forms.Label label_sei_path1;
        private System.Windows.Forms.Label label_sei_path2Title;
        private System.Windows.Forms.Label label_sei_path2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_sei_nameTitle;
        private System.Windows.Forms.Label label_sei_name;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label_sei_typeTitle;
        private System.Windows.Forms.Label label_sei_type;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label label_sei_directionTitle;
        private System.Windows.Forms.Label label_sei_direction;
        private System.Windows.Forms.Label label_sei_remove;
        private System.Windows.Forms.Label label_sei_removeTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label label_sei_startTimeTitle;
        private System.Windows.Forms.Label label_sei_startTime;
        private System.Windows.Forms.Label label_sei_endTime;
        private System.Windows.Forms.Label label_sei_endTimeTitle;
        private System.Windows.Forms.Label label_sei_duration;
        private System.Windows.Forms.Label label_sei_durationTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label_sei_syncStatusTitle;
        private System.Windows.Forms.Label label_sei_syncStatus;
        private System.Windows.Forms.Label label_sei_infoTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_sei;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.Label label_sei_syncExecInfoTitle;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyButton button_refresh;
        private System.Windows.Forms.TextBox textBox_sei_info;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_left;
    }
}