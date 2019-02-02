using LandasRLTracker.Properties;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace LandasRLTracker
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlOverview = new System.Windows.Forms.Panel();
            this.lblMmr = new System.Windows.Forms.Label();
            this.lblAddedMmr = new System.Windows.Forms.Label();
            this.lblRankName = new System.Windows.Forms.Label();
            this.lblGamesPlayedCount = new System.Windows.Forms.Label();
            this.lblGamesPlayed = new System.Windows.Forms.Label();
            this.lblRating = new System.Windows.Forms.Label();
            this.lblPlaylistName = new System.Windows.Forms.Label();
            this.picOverviewRank = new System.Windows.Forms.PictureBox();
            this.lblPlaylist = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxOverview = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTrackerLastUpdateValue = new System.Windows.Forms.Label();
            this.lblTrackerLastUpdateTag = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnTrackerReset = new System.Windows.Forms.Button();
            this.lblTrackerGlobalWorstStreakValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalBestStreakValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalCurrentStreakValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalLosesValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalWinsValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalGamesPlayedValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalMmrRatioValue = new System.Windows.Forms.Label();
            this.lblTrackerGlobalWorstStreakTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalBestStreakTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalCurrentStreakTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalLosesTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalWinsTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalGamesPlayedTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalMmrRatioTag = new System.Windows.Forms.Label();
            this.lblTrackerGlobalTag = new System.Windows.Forms.Label();
            this.picTrackerLastgameRank = new System.Windows.Forms.PictureBox();
            this.lblTrackerLastgameRankDiv = new System.Windows.Forms.Label();
            this.lblTrackerLastgameRankValue = new System.Windows.Forms.Label();
            this.lblTrackerLastgameRankTag = new System.Windows.Forms.Label();
            this.lblTrackerLastgamePlaylistValue = new System.Windows.Forms.Label();
            this.lblTrackerLastgamePlaylistTag = new System.Windows.Forms.Label();
            this.lblTrackerLastgameMmrWonLostValue = new System.Windows.Forms.Label();
            this.lblTrackerLastgameMmrWonLostTag = new System.Windows.Forms.Label();
            this.lblTrackerLastgameTag = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridSessionsummary = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridSessiontimeline = new System.Windows.Forms.DataGridView();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblSteamControl = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStreamerFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.landasTwitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeOnLaunchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblVersion = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOverviewRank)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTrackerLastgameRank)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSessionsummary)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSessiontimeline)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(616, 459);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.BackgroundImage = global::LandasRLTracker.Properties.Resources.DFH_Stadium_arena_preview;
            this.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage1.Controls.Add(this.pnlOverview);
            this.tabPage1.Controls.Add(this.lblPlaylist);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.listBoxOverview);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(608, 433);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rank overview";
            // 
            // pnlOverview
            // 
            this.pnlOverview.BackColor = System.Drawing.SystemColors.Control;
            this.pnlOverview.Controls.Add(this.lblMmr);
            this.pnlOverview.Controls.Add(this.lblAddedMmr);
            this.pnlOverview.Controls.Add(this.lblRankName);
            this.pnlOverview.Controls.Add(this.lblGamesPlayedCount);
            this.pnlOverview.Controls.Add(this.lblGamesPlayed);
            this.pnlOverview.Controls.Add(this.lblRating);
            this.pnlOverview.Controls.Add(this.lblPlaylistName);
            this.pnlOverview.Controls.Add(this.picOverviewRank);
            this.pnlOverview.Location = new System.Drawing.Point(196, 45);
            this.pnlOverview.Name = "pnlOverview";
            this.pnlOverview.Size = new System.Drawing.Size(359, 348);
            this.pnlOverview.TabIndex = 4;
            // 
            // lblMmr
            // 
            this.lblMmr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMmr.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMmr.ForeColor = System.Drawing.Color.Black;
            this.lblMmr.Location = new System.Drawing.Point(223, 269);
            this.lblMmr.Name = "lblMmr";
            this.lblMmr.Size = new System.Drawing.Size(67, 34);
            this.lblMmr.TabIndex = 7;
            this.lblMmr.Text = "1234";
            this.lblMmr.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAddedMmr
            // 
            this.lblAddedMmr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAddedMmr.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddedMmr.ForeColor = System.Drawing.Color.Black;
            this.lblAddedMmr.Location = new System.Drawing.Point(284, 272);
            this.lblAddedMmr.Name = "lblAddedMmr";
            this.lblAddedMmr.Size = new System.Drawing.Size(59, 30);
            this.lblAddedMmr.TabIndex = 11;
            this.lblAddedMmr.Text = "(+11)";
            // 
            // lblRankName
            // 
            this.lblRankName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRankName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRankName.ForeColor = System.Drawing.Color.Black;
            this.lblRankName.Location = new System.Drawing.Point(3, 220);
            this.lblRankName.Name = "lblRankName";
            this.lblRankName.Size = new System.Drawing.Size(353, 34);
            this.lblRankName.TabIndex = 10;
            this.lblRankName.Text = "Rank";
            this.lblRankName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGamesPlayedCount
            // 
            this.lblGamesPlayedCount.AutoSize = true;
            this.lblGamesPlayedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGamesPlayedCount.ForeColor = System.Drawing.Color.Black;
            this.lblGamesPlayedCount.Location = new System.Drawing.Point(109, 317);
            this.lblGamesPlayedCount.Name = "lblGamesPlayedCount";
            this.lblGamesPlayedCount.Size = new System.Drawing.Size(43, 24);
            this.lblGamesPlayedCount.TabIndex = 9;
            this.lblGamesPlayedCount.Text = "123";
            this.lblGamesPlayedCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblGamesPlayed
            // 
            this.lblGamesPlayed.AutoSize = true;
            this.lblGamesPlayed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGamesPlayed.ForeColor = System.Drawing.Color.Black;
            this.lblGamesPlayed.Location = new System.Drawing.Point(13, 317);
            this.lblGamesPlayed.Name = "lblGamesPlayed";
            this.lblGamesPlayed.Size = new System.Drawing.Size(136, 24);
            this.lblGamesPlayed.TabIndex = 8;
            this.lblGamesPlayed.Text = "Games played:";
            this.lblGamesPlayed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblRating
            // 
            this.lblRating.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRating.AutoSize = true;
            this.lblRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRating.ForeColor = System.Drawing.Color.Black;
            this.lblRating.Location = new System.Drawing.Point(12, 270);
            this.lblRating.Name = "lblRating";
            this.lblRating.Size = new System.Drawing.Size(265, 37);
            this.lblRating.TabIndex = 6;
            this.lblRating.Text = "skill rating (mmr):";
            this.lblRating.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblPlaylistName
            // 
            this.lblPlaylistName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlaylistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylistName.ForeColor = System.Drawing.Color.Black;
            this.lblPlaylistName.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPlaylistName.Location = new System.Drawing.Point(3, 14);
            this.lblPlaylistName.Name = "lblPlaylistName";
            this.lblPlaylistName.Size = new System.Drawing.Size(353, 43);
            this.lblPlaylistName.TabIndex = 5;
            this.lblPlaylistName.Text = "Playlist";
            this.lblPlaylistName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picOverviewRank
            // 
            this.picOverviewRank.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picOverviewRank.Enabled = false;
            this.picOverviewRank.Image = global::LandasRLTracker.Properties.Resources._19;
            this.picOverviewRank.InitialImage = null;
            this.picOverviewRank.Location = new System.Drawing.Point(69, 65);
            this.picOverviewRank.Name = "picOverviewRank";
            this.picOverviewRank.Size = new System.Drawing.Size(214, 135);
            this.picOverviewRank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOverviewRank.TabIndex = 3;
            this.picOverviewRank.TabStop = false;
            this.picOverviewRank.WaitOnLoad = true;
            // 
            // lblPlaylist
            // 
            this.lblPlaylist.AutoSize = true;
            this.lblPlaylist.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlaylist.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPlaylist.Location = new System.Drawing.Point(38, 119);
            this.lblPlaylist.Name = "lblPlaylist";
            this.lblPlaylist.Size = new System.Drawing.Size(135, 37);
            this.lblPlaylist.TabIndex = 2;
            this.lblPlaylist.Text = "Playlist:";
            this.lblPlaylist.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(399, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // listBoxOverview
            // 
            this.listBoxOverview.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxOverview.FormattingEnabled = true;
            this.listBoxOverview.Location = new System.Drawing.Point(30, 156);
            this.listBoxOverview.Name = "listBoxOverview";
            this.listBoxOverview.Size = new System.Drawing.Size(120, 121);
            this.listBoxOverview.TabIndex = 0;
            this.listBoxOverview.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackgroundImage = global::LandasRLTracker.Properties.Resources.DFH_Stadium_arena_preview;
            this.tabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(608, 433);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tracker";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblTrackerLastUpdateValue);
            this.panel1.Controls.Add(this.lblTrackerLastUpdateTag);
            this.panel1.Location = new System.Drawing.Point(38, 391);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 35);
            this.panel1.TabIndex = 1;
            // 
            // lblTrackerLastUpdateValue
            // 
            this.lblTrackerLastUpdateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastUpdateValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastUpdateValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastUpdateValue.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTrackerLastUpdateValue.Location = new System.Drawing.Point(107, 3);
            this.lblTrackerLastUpdateValue.Name = "lblTrackerLastUpdateValue";
            this.lblTrackerLastUpdateValue.Size = new System.Drawing.Size(164, 26);
            this.lblTrackerLastUpdateValue.TabIndex = 22;
            this.lblTrackerLastUpdateValue.Text = "12/12/12 12:12";
            this.lblTrackerLastUpdateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTrackerLastUpdateTag
            // 
            this.lblTrackerLastUpdateTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastUpdateTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastUpdateTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastUpdateTag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTrackerLastUpdateTag.Location = new System.Drawing.Point(10, 3);
            this.lblTrackerLastUpdateTag.Name = "lblTrackerLastUpdateTag";
            this.lblTrackerLastUpdateTag.Size = new System.Drawing.Size(105, 26);
            this.lblTrackerLastUpdateTag.TabIndex = 21;
            this.lblTrackerLastUpdateTag.Text = "Last update:";
            this.lblTrackerLastUpdateTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(36, 11);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnTrackerReset);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalWorstStreakValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalBestStreakValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalCurrentStreakValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalLosesValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalWinsValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalGamesPlayedValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalMmrRatioValue);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalWorstStreakTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalBestStreakTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalCurrentStreakTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalLosesTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalWinsTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalGamesPlayedTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalMmrRatioTag);
            this.splitContainer1.Panel1.Controls.Add(this.lblTrackerGlobalTag);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.picTrackerLastgameRank);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameRankDiv);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameRankValue);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameRankTag);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgamePlaylistValue);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgamePlaylistTag);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameMmrWonLostValue);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameMmrWonLostTag);
            this.splitContainer1.Panel2.Controls.Add(this.lblTrackerLastgameTag);
            this.splitContainer1.Size = new System.Drawing.Size(533, 374);
            this.splitContainer1.SplitterDistance = 187;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnTrackerReset
            // 
            this.btnTrackerReset.Location = new System.Drawing.Point(390, 14);
            this.btnTrackerReset.Name = "btnTrackerReset";
            this.btnTrackerReset.Size = new System.Drawing.Size(95, 23);
            this.btnTrackerReset.TabIndex = 21;
            this.btnTrackerReset.Text = "Restart session";
            this.btnTrackerReset.UseVisualStyleBackColor = true;
            this.btnTrackerReset.Click += new System.EventHandler(this.BtnTrackerReset_Click);
            // 
            // lblTrackerGlobalWorstStreakValue
            // 
            this.lblTrackerGlobalWorstStreakValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalWorstStreakValue.AutoSize = true;
            this.lblTrackerGlobalWorstStreakValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalWorstStreakValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalWorstStreakValue.Location = new System.Drawing.Point(448, 142);
            this.lblTrackerGlobalWorstStreakValue.Name = "lblTrackerGlobalWorstStreakValue";
            this.lblTrackerGlobalWorstStreakValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalWorstStreakValue.TabIndex = 20;
            this.lblTrackerGlobalWorstStreakValue.Text = "1234";
            this.lblTrackerGlobalWorstStreakValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalBestStreakValue
            // 
            this.lblTrackerGlobalBestStreakValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalBestStreakValue.AutoSize = true;
            this.lblTrackerGlobalBestStreakValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalBestStreakValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalBestStreakValue.Location = new System.Drawing.Point(284, 142);
            this.lblTrackerGlobalBestStreakValue.Name = "lblTrackerGlobalBestStreakValue";
            this.lblTrackerGlobalBestStreakValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalBestStreakValue.TabIndex = 19;
            this.lblTrackerGlobalBestStreakValue.Text = "1234";
            this.lblTrackerGlobalBestStreakValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalCurrentStreakValue
            // 
            this.lblTrackerGlobalCurrentStreakValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalCurrentStreakValue.AutoSize = true;
            this.lblTrackerGlobalCurrentStreakValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalCurrentStreakValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalCurrentStreakValue.Location = new System.Drawing.Point(164, 142);
            this.lblTrackerGlobalCurrentStreakValue.Name = "lblTrackerGlobalCurrentStreakValue";
            this.lblTrackerGlobalCurrentStreakValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalCurrentStreakValue.TabIndex = 18;
            this.lblTrackerGlobalCurrentStreakValue.Text = "1234";
            this.lblTrackerGlobalCurrentStreakValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalLosesValue
            // 
            this.lblTrackerGlobalLosesValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalLosesValue.AutoSize = true;
            this.lblTrackerGlobalLosesValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalLosesValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalLosesValue.Location = new System.Drawing.Point(441, 97);
            this.lblTrackerGlobalLosesValue.Name = "lblTrackerGlobalLosesValue";
            this.lblTrackerGlobalLosesValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalLosesValue.TabIndex = 17;
            this.lblTrackerGlobalLosesValue.Text = "1234";
            this.lblTrackerGlobalLosesValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalWinsValue
            // 
            this.lblTrackerGlobalWinsValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalWinsValue.AutoSize = true;
            this.lblTrackerGlobalWinsValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalWinsValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalWinsValue.Location = new System.Drawing.Point(285, 97);
            this.lblTrackerGlobalWinsValue.Name = "lblTrackerGlobalWinsValue";
            this.lblTrackerGlobalWinsValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalWinsValue.TabIndex = 16;
            this.lblTrackerGlobalWinsValue.Text = "1234";
            this.lblTrackerGlobalWinsValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalGamesPlayedValue
            // 
            this.lblTrackerGlobalGamesPlayedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalGamesPlayedValue.AutoSize = true;
            this.lblTrackerGlobalGamesPlayedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalGamesPlayedValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalGamesPlayedValue.Location = new System.Drawing.Point(144, 97);
            this.lblTrackerGlobalGamesPlayedValue.Name = "lblTrackerGlobalGamesPlayedValue";
            this.lblTrackerGlobalGamesPlayedValue.Size = new System.Drawing.Size(78, 31);
            this.lblTrackerGlobalGamesPlayedValue.TabIndex = 15;
            this.lblTrackerGlobalGamesPlayedValue.Text = "1234";
            this.lblTrackerGlobalGamesPlayedValue.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalMmrRatioValue
            // 
            this.lblTrackerGlobalMmrRatioValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalMmrRatioValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalMmrRatioValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalMmrRatioValue.Location = new System.Drawing.Point(299, 50);
            this.lblTrackerGlobalMmrRatioValue.Name = "lblTrackerGlobalMmrRatioValue";
            this.lblTrackerGlobalMmrRatioValue.Size = new System.Drawing.Size(232, 34);
            this.lblTrackerGlobalMmrRatioValue.TabIndex = 14;
            this.lblTrackerGlobalMmrRatioValue.Text = "+12";
            // 
            // lblTrackerGlobalWorstStreakTag
            // 
            this.lblTrackerGlobalWorstStreakTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalWorstStreakTag.AutoSize = true;
            this.lblTrackerGlobalWorstStreakTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalWorstStreakTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalWorstStreakTag.Location = new System.Drawing.Point(381, 142);
            this.lblTrackerGlobalWorstStreakTag.Name = "lblTrackerGlobalWorstStreakTag";
            this.lblTrackerGlobalWorstStreakTag.Size = new System.Drawing.Size(93, 31);
            this.lblTrackerGlobalWorstStreakTag.TabIndex = 13;
            this.lblTrackerGlobalWorstStreakTag.Text = "Worst:";
            this.lblTrackerGlobalWorstStreakTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalBestStreakTag
            // 
            this.lblTrackerGlobalBestStreakTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalBestStreakTag.AutoSize = true;
            this.lblTrackerGlobalBestStreakTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalBestStreakTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalBestStreakTag.Location = new System.Drawing.Point(234, 142);
            this.lblTrackerGlobalBestStreakTag.Name = "lblTrackerGlobalBestStreakTag";
            this.lblTrackerGlobalBestStreakTag.Size = new System.Drawing.Size(77, 31);
            this.lblTrackerGlobalBestStreakTag.TabIndex = 12;
            this.lblTrackerGlobalBestStreakTag.Text = "Best:";
            this.lblTrackerGlobalBestStreakTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalCurrentStreakTag
            // 
            this.lblTrackerGlobalCurrentStreakTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalCurrentStreakTag.AutoSize = true;
            this.lblTrackerGlobalCurrentStreakTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalCurrentStreakTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalCurrentStreakTag.Location = new System.Drawing.Point(14, 142);
            this.lblTrackerGlobalCurrentStreakTag.Name = "lblTrackerGlobalCurrentStreakTag";
            this.lblTrackerGlobalCurrentStreakTag.Size = new System.Drawing.Size(195, 31);
            this.lblTrackerGlobalCurrentStreakTag.TabIndex = 11;
            this.lblTrackerGlobalCurrentStreakTag.Text = "Current streak:";
            this.lblTrackerGlobalCurrentStreakTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalLosesTag
            // 
            this.lblTrackerGlobalLosesTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalLosesTag.AutoSize = true;
            this.lblTrackerGlobalLosesTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalLosesTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalLosesTag.Location = new System.Drawing.Point(381, 97);
            this.lblTrackerGlobalLosesTag.Name = "lblTrackerGlobalLosesTag";
            this.lblTrackerGlobalLosesTag.Size = new System.Drawing.Size(95, 31);
            this.lblTrackerGlobalLosesTag.TabIndex = 10;
            this.lblTrackerGlobalLosesTag.Text = "Loses:";
            this.lblTrackerGlobalLosesTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalWinsTag
            // 
            this.lblTrackerGlobalWinsTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalWinsTag.AutoSize = true;
            this.lblTrackerGlobalWinsTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalWinsTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalWinsTag.Location = new System.Drawing.Point(234, 97);
            this.lblTrackerGlobalWinsTag.Name = "lblTrackerGlobalWinsTag";
            this.lblTrackerGlobalWinsTag.Size = new System.Drawing.Size(82, 31);
            this.lblTrackerGlobalWinsTag.TabIndex = 9;
            this.lblTrackerGlobalWinsTag.Text = "Wins:";
            this.lblTrackerGlobalWinsTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalGamesPlayedTag
            // 
            this.lblTrackerGlobalGamesPlayedTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalGamesPlayedTag.AutoSize = true;
            this.lblTrackerGlobalGamesPlayedTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalGamesPlayedTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalGamesPlayedTag.Location = new System.Drawing.Point(14, 97);
            this.lblTrackerGlobalGamesPlayedTag.Name = "lblTrackerGlobalGamesPlayedTag";
            this.lblTrackerGlobalGamesPlayedTag.Size = new System.Drawing.Size(196, 31);
            this.lblTrackerGlobalGamesPlayedTag.TabIndex = 8;
            this.lblTrackerGlobalGamesPlayedTag.Text = "Games played:";
            this.lblTrackerGlobalGamesPlayedTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalMmrRatioTag
            // 
            this.lblTrackerGlobalMmrRatioTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalMmrRatioTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalMmrRatioTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalMmrRatioTag.Location = new System.Drawing.Point(-6, 51);
            this.lblTrackerGlobalMmrRatioTag.Name = "lblTrackerGlobalMmrRatioTag";
            this.lblTrackerGlobalMmrRatioTag.Size = new System.Drawing.Size(498, 33);
            this.lblTrackerGlobalMmrRatioTag.TabIndex = 7;
            this.lblTrackerGlobalMmrRatioTag.Text = "MMR ratio:";
            this.lblTrackerGlobalMmrRatioTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerGlobalTag
            // 
            this.lblTrackerGlobalTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerGlobalTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerGlobalTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerGlobalTag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTrackerGlobalTag.Location = new System.Drawing.Point(-2, 3);
            this.lblTrackerGlobalTag.Name = "lblTrackerGlobalTag";
            this.lblTrackerGlobalTag.Size = new System.Drawing.Size(533, 43);
            this.lblTrackerGlobalTag.TabIndex = 6;
            this.lblTrackerGlobalTag.Text = "Global Stats";
            this.lblTrackerGlobalTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picTrackerLastgameRank
            // 
            this.picTrackerLastgameRank.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picTrackerLastgameRank.Image = global::LandasRLTracker.Properties.Resources._0;
            this.picTrackerLastgameRank.Location = new System.Drawing.Point(370, 67);
            this.picTrackerLastgameRank.Name = "picTrackerLastgameRank";
            this.picTrackerLastgameRank.Size = new System.Drawing.Size(100, 80);
            this.picTrackerLastgameRank.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTrackerLastgameRank.TabIndex = 28;
            this.picTrackerLastgameRank.TabStop = false;
            // 
            // lblTrackerLastgameRankDiv
            // 
            this.lblTrackerLastgameRankDiv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameRankDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameRankDiv.ForeColor = System.Drawing.Color.Green;
            this.lblTrackerLastgameRankDiv.Location = new System.Drawing.Point(204, 94);
            this.lblTrackerLastgameRankDiv.Name = "lblTrackerLastgameRankDiv";
            this.lblTrackerLastgameRankDiv.Size = new System.Drawing.Size(179, 34);
            this.lblTrackerLastgameRankDiv.TabIndex = 27;
            this.lblTrackerLastgameRankDiv.Text = "RANK UP!";
            // 
            // lblTrackerLastgameRankValue
            // 
            this.lblTrackerLastgameRankValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameRankValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameRankValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgameRankValue.Location = new System.Drawing.Point(68, 135);
            this.lblTrackerLastgameRankValue.Name = "lblTrackerLastgameRankValue";
            this.lblTrackerLastgameRankValue.Size = new System.Drawing.Size(336, 34);
            this.lblTrackerLastgameRankValue.TabIndex = 26;
            this.lblTrackerLastgameRankValue.Text = "Diamond III Div IV [1190MMR]";
            // 
            // lblTrackerLastgameRankTag
            // 
            this.lblTrackerLastgameRankTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameRankTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameRankTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgameRankTag.Location = new System.Drawing.Point(15, 133);
            this.lblTrackerLastgameRankTag.Name = "lblTrackerLastgameRankTag";
            this.lblTrackerLastgameRankTag.Size = new System.Drawing.Size(61, 33);
            this.lblTrackerLastgameRankTag.TabIndex = 25;
            this.lblTrackerLastgameRankTag.Text = "Rank:";
            this.lblTrackerLastgameRankTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTrackerLastgamePlaylistValue
            // 
            this.lblTrackerLastgamePlaylistValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgamePlaylistValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgamePlaylistValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgamePlaylistValue.Location = new System.Drawing.Point(99, 52);
            this.lblTrackerLastgamePlaylistValue.Name = "lblTrackerLastgamePlaylistValue";
            this.lblTrackerLastgamePlaylistValue.Size = new System.Drawing.Size(232, 34);
            this.lblTrackerLastgamePlaylistValue.TabIndex = 24;
            this.lblTrackerLastgamePlaylistValue.Text = "Standard (3vs3)";
            // 
            // lblTrackerLastgamePlaylistTag
            // 
            this.lblTrackerLastgamePlaylistTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgamePlaylistTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgamePlaylistTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgamePlaylistTag.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrackerLastgamePlaylistTag.Location = new System.Drawing.Point(14, 52);
            this.lblTrackerLastgamePlaylistTag.Name = "lblTrackerLastgamePlaylistTag";
            this.lblTrackerLastgamePlaylistTag.Size = new System.Drawing.Size(95, 33);
            this.lblTrackerLastgamePlaylistTag.TabIndex = 23;
            this.lblTrackerLastgamePlaylistTag.Text = "Playlist:";
            this.lblTrackerLastgamePlaylistTag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTrackerLastgameMmrWonLostValue
            // 
            this.lblTrackerLastgameMmrWonLostValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameMmrWonLostValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameMmrWonLostValue.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgameMmrWonLostValue.Location = new System.Drawing.Point(156, 94);
            this.lblTrackerLastgameMmrWonLostValue.Name = "lblTrackerLastgameMmrWonLostValue";
            this.lblTrackerLastgameMmrWonLostValue.Size = new System.Drawing.Size(179, 34);
            this.lblTrackerLastgameMmrWonLostValue.TabIndex = 22;
            this.lblTrackerLastgameMmrWonLostValue.Text = "+12";
            // 
            // lblTrackerLastgameMmrWonLostTag
            // 
            this.lblTrackerLastgameMmrWonLostTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameMmrWonLostTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameMmrWonLostTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgameMmrWonLostTag.Location = new System.Drawing.Point(16, 92);
            this.lblTrackerLastgameMmrWonLostTag.Name = "lblTrackerLastgameMmrWonLostTag";
            this.lblTrackerLastgameMmrWonLostTag.Size = new System.Drawing.Size(156, 33);
            this.lblTrackerLastgameMmrWonLostTag.TabIndex = 21;
            this.lblTrackerLastgameMmrWonLostTag.Text = "MMR won/lost:";
            this.lblTrackerLastgameMmrWonLostTag.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTrackerLastgameTag
            // 
            this.lblTrackerLastgameTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackerLastgameTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackerLastgameTag.ForeColor = System.Drawing.Color.Black;
            this.lblTrackerLastgameTag.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTrackerLastgameTag.Location = new System.Drawing.Point(-2, 3);
            this.lblTrackerLastgameTag.Name = "lblTrackerLastgameTag";
            this.lblTrackerLastgameTag.Size = new System.Drawing.Size(533, 43);
            this.lblTrackerLastgameTag.TabIndex = 21;
            this.lblTrackerLastgameTag.Text = "Last game stats";
            this.lblTrackerLastgameTag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = global::LandasRLTracker.Properties.Resources.DFH_Stadium_arena_preview;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.dataGridSessionsummary);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(608, 433);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Playlist summary";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridSessionsummary
            // 
            this.dataGridSessionsummary.AllowUserToAddRows = false;
            this.dataGridSessionsummary.AllowUserToDeleteRows = false;
            this.dataGridSessionsummary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridSessionsummary.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridSessionsummary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridSessionsummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSessionsummary.Location = new System.Drawing.Point(4, 12);
            this.dataGridSessionsummary.Name = "dataGridSessionsummary";
            this.dataGridSessionsummary.ReadOnly = true;
            this.dataGridSessionsummary.RowHeadersVisible = false;
            this.dataGridSessionsummary.ShowCellErrors = false;
            this.dataGridSessionsummary.ShowCellToolTips = false;
            this.dataGridSessionsummary.ShowEditingIcon = false;
            this.dataGridSessionsummary.ShowRowErrors = false;
            this.dataGridSessionsummary.Size = new System.Drawing.Size(599, 409);
            this.dataGridSessionsummary.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackgroundImage = global::LandasRLTracker.Properties.Resources.DFH_Stadium_arena_preview;
            this.tabPage4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage4.Controls.Add(this.dataGridSessiontimeline);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(608, 433);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Session timeline";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridSessiontimeline
            // 
            this.dataGridSessiontimeline.AllowUserToAddRows = false;
            this.dataGridSessiontimeline.AllowUserToDeleteRows = false;
            this.dataGridSessiontimeline.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridSessiontimeline.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridSessiontimeline.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridSessiontimeline.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSessiontimeline.Location = new System.Drawing.Point(5, 12);
            this.dataGridSessiontimeline.Name = "dataGridSessiontimeline";
            this.dataGridSessiontimeline.ReadOnly = true;
            this.dataGridSessiontimeline.RowHeadersVisible = false;
            this.dataGridSessiontimeline.ShowCellErrors = false;
            this.dataGridSessiontimeline.ShowCellToolTips = false;
            this.dataGridSessiontimeline.ShowEditingIcon = false;
            this.dataGridSessiontimeline.ShowRowErrors = false;
            this.dataGridSessiontimeline.Size = new System.Drawing.Size(599, 409);
            this.dataGridSessiontimeline.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(330, 28);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(36, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User:";
            // 
            // lblSteamControl
            // 
            this.lblSteamControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSteamControl.BackColor = System.Drawing.Color.Transparent;
            this.lblSteamControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSteamControl.ForeColor = System.Drawing.Color.Green;
            this.lblSteamControl.Location = new System.Drawing.Point(366, 25);
            this.lblSteamControl.Name = "lblSteamControl";
            this.lblSteamControl.Size = new System.Drawing.Size(257, 19);
            this.lblSteamControl.TabIndex = 5;
            this.lblSteamControl.Text = "SteamUser";
            this.lblSteamControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSteamControl.Visible = false;
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 484);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip.Size = new System.Drawing.Size(640, 24);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Margin = new System.Windows.Forms.Padding(12, 3, 0, 2);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(82, 19);
            this.toolStripStatusLabel.Text = "Tracker status:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.linksToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(640, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openStreamerFolderToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openStreamerFolderToolStripMenuItem
            // 
            this.openStreamerFolderToolStripMenuItem.Name = "openStreamerFolderToolStripMenuItem";
            this.openStreamerFolderToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openStreamerFolderToolStripMenuItem.Text = "Open \'StreamKit\' folder";
            this.openStreamerFolderToolStripMenuItem.Click += new System.EventHandler(this.OpenStreamerFolderToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesToolStripMenuItem_Click);
            // 
            // linksToolStripMenuItem
            // 
            this.linksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.githubToolStripMenuItem,
            this.landasTwitterToolStripMenuItem});
            this.linksToolStripMenuItem.Name = "linksToolStripMenuItem";
            this.linksToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.linksToolStripMenuItem.Text = "Links";
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.githubToolStripMenuItem.Text = "Landa\'s Github";
            this.githubToolStripMenuItem.Click += new System.EventHandler(this.GithubToolStripMenuItem_Click);
            // 
            // landasTwitterToolStripMenuItem
            // 
            this.landasTwitterToolStripMenuItem.Name = "landasTwitterToolStripMenuItem";
            this.landasTwitterToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.landasTwitterToolStripMenuItem.Text = "Landa\'s Twitter";
            this.landasTwitterToolStripMenuItem.Click += new System.EventHandler(this.LandasTwitterToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runOnStartupToolStripMenuItem,
            this.minimizeOnLaunchToolStripMenuItem,
            this.minimizeToTrayToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // runOnStartupToolStripMenuItem
            // 
            this.runOnStartupToolStripMenuItem.Checked = global::LandasRLTracker.Properties.Settings.Default.runOnStartupChecked;
            this.runOnStartupToolStripMenuItem.CheckOnClick = true;
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            this.runOnStartupToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.runOnStartupToolStripMenuItem.Text = "Run on startup";
            this.runOnStartupToolStripMenuItem.Click += new System.EventHandler(this.RunOnStartupToolStripMenuItem_Click);
            // 
            // minimizeOnLaunchToolStripMenuItem
            // 
            this.minimizeOnLaunchToolStripMenuItem.Checked = global::LandasRLTracker.Properties.Settings.Default.minimizeOnLaunchChecked;
            this.minimizeOnLaunchToolStripMenuItem.CheckOnClick = true;
            this.minimizeOnLaunchToolStripMenuItem.Name = "minimizeOnLaunchToolStripMenuItem";
            this.minimizeOnLaunchToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.minimizeOnLaunchToolStripMenuItem.Text = "Minimize on launch";
            this.minimizeOnLaunchToolStripMenuItem.Click += new System.EventHandler(this.MinimizeOnLaunchToolStripMenuItem_Click);
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Checked = global::LandasRLTracker.Properties.Settings.Default.minimizeToTrayChecked;
            this.minimizeToTrayToolStripMenuItem.CheckOnClick = true;
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to tray";
            this.minimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.MinimizeToTrayToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.howToToolStripMenuItem.Text = "How to use";
            this.howToToolStripMenuItem.Click += new System.EventHandler(this.HowToToolStripMenuItem_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(581, 490);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblVersion.Size = new System.Drawing.Size(45, 23);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "v1.2.3";
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Landa\'s RL Tracker";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 508);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblSteamControl);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Landa\'s RL Tracker";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlOverview.ResumeLayout(false);
            this.pnlOverview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOverviewRank)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTrackerLastgameRank)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSessionsummary)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSessiontimeline)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBoxOverview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlaylist;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblSteamControl;
        private System.Windows.Forms.PictureBox picOverviewRank;
        private System.Windows.Forms.Panel pnlOverview;
        private System.Windows.Forms.Label lblPlaylistName;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblMmr;
        private System.Windows.Forms.Label lblGamesPlayedCount;
        private System.Windows.Forms.Label lblGamesPlayed;
        private System.Windows.Forms.Label lblRankName;
        private System.Windows.Forms.DataGridView dataGridSessionsummary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblTrackerGlobalTag;
        private System.Windows.Forms.Label lblTrackerGlobalWorstStreakTag;
        private System.Windows.Forms.Label lblTrackerGlobalBestStreakTag;
        private System.Windows.Forms.Label lblTrackerGlobalCurrentStreakTag;
        private System.Windows.Forms.Label lblTrackerGlobalLosesTag;
        private System.Windows.Forms.Label lblTrackerGlobalWinsTag;
        private System.Windows.Forms.Label lblTrackerGlobalGamesPlayedTag;
        private System.Windows.Forms.Label lblTrackerGlobalMmrRatioTag;
        private System.Windows.Forms.Label lblTrackerGlobalMmrRatioValue;
        private System.Windows.Forms.Label lblTrackerGlobalLosesValue;
        private System.Windows.Forms.Label lblTrackerGlobalWinsValue;
        private System.Windows.Forms.Label lblTrackerGlobalGamesPlayedValue;
        private System.Windows.Forms.Label lblTrackerGlobalWorstStreakValue;
        private System.Windows.Forms.Label lblTrackerGlobalBestStreakValue;
        private System.Windows.Forms.Label lblTrackerGlobalCurrentStreakValue;
        private System.Windows.Forms.Label lblTrackerLastgameTag;
        private System.Windows.Forms.Label lblTrackerLastgameMmrWonLostValue;
        private System.Windows.Forms.Label lblTrackerLastgameMmrWonLostTag;
        private System.Windows.Forms.Label lblTrackerLastgamePlaylistValue;
        private System.Windows.Forms.Label lblTrackerLastgamePlaylistTag;
        private System.Windows.Forms.Label lblTrackerLastgameRankValue;
        private System.Windows.Forms.Label lblTrackerLastgameRankTag;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTrackerLastUpdateValue;
        private System.Windows.Forms.Label lblTrackerLastUpdateTag;
        private System.Windows.Forms.Label lblTrackerLastgameRankDiv;
        private System.Windows.Forms.Label lblAddedMmr;
        private System.Windows.Forms.PictureBox picTrackerLastgameRank;
        private System.Windows.Forms.Button btnTrackerReset;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridSessiontimeline;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStreamerFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem landasTwitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runOnStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeOnLaunchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

