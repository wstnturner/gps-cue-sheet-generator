﻿namespace CueSheetGenerator {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapPictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.endTextBox = new System.Windows.Forms.TextBox();
            this.cueSheetListView = new System.Windows.Forms.ListView();
            this.beginTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hTMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kilometersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roadmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satelliteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hybridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathResToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fifteenMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twentyMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtyMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revGeoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.points250ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.points500ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.points750ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.points1000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.points2000ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewElevationProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lookupToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentTurnStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openGpsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveOutputFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.turnPictureBox = new System.Windows.Forms.PictureBox();
            this.poiNametextBox = new System.Windows.Forms.TextBox();
            this.poiNameLabel = new System.Windows.Forms.Label();
            this.poiDescriptionLabel = new System.Windows.Forms.Label();
            this.poiDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.addPoiButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.turnPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.mapPictureBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 537);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ride Map";
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPictureBox.Location = new System.Drawing.Point(3, 16);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(600, 518);
            this.mapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mapPictureBox.TabIndex = 0;
            this.mapPictureBox.TabStop = false;
            this.mapPictureBox.SizeChanged += new System.EventHandler(this.mapPictureBox_SizeChanged);
            this.mapPictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseDoubleClick);
            this.mapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.endTextBox);
            this.groupBox2.Controls.Add(this.cueSheetListView);
            this.groupBox2.Controls.Add(this.beginTextBox);
            this.groupBox2.Location = new System.Drawing.Point(612, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 269);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Directions";
            // 
            // endTextBox
            // 
            this.endTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.endTextBox.Location = new System.Drawing.Point(3, 232);
            this.endTextBox.Multiline = true;
            this.endTextBox.Name = "endTextBox";
            this.endTextBox.ReadOnly = true;
            this.endTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.endTextBox.Size = new System.Drawing.Size(271, 34);
            this.endTextBox.TabIndex = 6;
            // 
            // cueSheetListView
            // 
            this.cueSheetListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cueSheetListView.FullRowSelect = true;
            this.cueSheetListView.GridLines = true;
            this.cueSheetListView.HideSelection = false;
            this.cueSheetListView.Location = new System.Drawing.Point(3, 50);
            this.cueSheetListView.MultiSelect = false;
            this.cueSheetListView.Name = "cueSheetListView";
            this.cueSheetListView.Size = new System.Drawing.Size(271, 182);
            this.cueSheetListView.TabIndex = 4;
            this.cueSheetListView.UseCompatibleStateImageBehavior = false;
            this.cueSheetListView.View = System.Windows.Forms.View.Details;
            this.cueSheetListView.SelectedIndexChanged += new System.EventHandler(this.cueSheetListView_SelectedIndexChanged);
            // 
            // beginTextBox
            // 
            this.beginTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.beginTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.beginTextBox.Location = new System.Drawing.Point(3, 16);
            this.beginTextBox.Multiline = true;
            this.beginTextBox.Name = "beginTextBox";
            this.beginTextBox.ReadOnly = true;
            this.beginTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.beginTextBox.Size = new System.Drawing.Size(271, 34);
            this.beginTextBox.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.viewElevationProfileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(892, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cSVFileToolStripMenuItem,
            this.hTMLFileToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem});
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // cSVFileToolStripMenuItem
            // 
            this.cSVFileToolStripMenuItem.Name = "cSVFileToolStripMenuItem";
            this.cSVFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.cSVFileToolStripMenuItem.Text = "CSV File";
            this.cSVFileToolStripMenuItem.Click += new System.EventHandler(this.cSVFileToolStripMenuItem_Click);
            // 
            // hTMLFileToolStripMenuItem
            // 
            this.hTMLFileToolStripMenuItem.Name = "hTMLFileToolStripMenuItem";
            this.hTMLFileToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.hTMLFileToolStripMenuItem.Text = "HTML File";
            this.hTMLFileToolStripMenuItem.Click += new System.EventHandler(this.hTMLFileToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(108, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitsToolStripMenuItem,
            this.mapTypeToolStripMenuItem,
            this.pathResToolStripMenuItem,
            this.revGeoToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metersToolStripMenuItem,
            this.kilometersToolStripMenuItem,
            this.milesToolStripMenuItem});
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.unitsToolStripMenuItem.Text = "Units";
            // 
            // metersToolStripMenuItem
            // 
            this.metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            this.metersToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.metersToolStripMenuItem.Tag = "Meters";
            this.metersToolStripMenuItem.Text = "Meters";
            this.metersToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // kilometersToolStripMenuItem
            // 
            this.kilometersToolStripMenuItem.Name = "kilometersToolStripMenuItem";
            this.kilometersToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.kilometersToolStripMenuItem.Tag = "Kilometers";
            this.kilometersToolStripMenuItem.Text = "Kilometers";
            this.kilometersToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // milesToolStripMenuItem
            // 
            this.milesToolStripMenuItem.Checked = true;
            this.milesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.milesToolStripMenuItem.Name = "milesToolStripMenuItem";
            this.milesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.milesToolStripMenuItem.Tag = "Miles";
            this.milesToolStripMenuItem.Text = "Miles";
            this.milesToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // mapTypeToolStripMenuItem
            // 
            this.mapTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roadmapToolStripMenuItem,
            this.satelliteToolStripMenuItem,
            this.terrainToolStripMenuItem,
            this.hybridToolStripMenuItem});
            this.mapTypeToolStripMenuItem.Name = "mapTypeToolStripMenuItem";
            this.mapTypeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.mapTypeToolStripMenuItem.Text = "Map Type";
            // 
            // roadmapToolStripMenuItem
            // 
            this.roadmapToolStripMenuItem.Checked = true;
            this.roadmapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.roadmapToolStripMenuItem.Name = "roadmapToolStripMenuItem";
            this.roadmapToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.roadmapToolStripMenuItem.Text = "Roadmap";
            this.roadmapToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // satelliteToolStripMenuItem
            // 
            this.satelliteToolStripMenuItem.Name = "satelliteToolStripMenuItem";
            this.satelliteToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.satelliteToolStripMenuItem.Text = "Satellite";
            this.satelliteToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.terrainToolStripMenuItem.Text = "Terrain";
            this.terrainToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // hybridToolStripMenuItem
            // 
            this.hybridToolStripMenuItem.Name = "hybridToolStripMenuItem";
            this.hybridToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.hybridToolStripMenuItem.Text = "Hybrid";
            this.hybridToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // pathResToolStripMenuItem
            // 
            this.pathResToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tenMToolStripMenuItem,
            this.fifteenMToolStripMenuItem,
            this.twentyMToolStripMenuItem,
            this.thirtyMToolStripMenuItem});
            this.pathResToolStripMenuItem.Name = "pathResToolStripMenuItem";
            this.pathResToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.pathResToolStripMenuItem.Text = "Path Resolution";
            // 
            // tenMToolStripMenuItem
            // 
            this.tenMToolStripMenuItem.Name = "tenMToolStripMenuItem";
            this.tenMToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.tenMToolStripMenuItem.Text = "10m";
            this.tenMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // fifteenMToolStripMenuItem
            // 
            this.fifteenMToolStripMenuItem.Name = "fifteenMToolStripMenuItem";
            this.fifteenMToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.fifteenMToolStripMenuItem.Text = "15m";
            this.fifteenMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // twentyMToolStripMenuItem
            // 
            this.twentyMToolStripMenuItem.Name = "twentyMToolStripMenuItem";
            this.twentyMToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.twentyMToolStripMenuItem.Text = "20m";
            this.twentyMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // thirtyMToolStripMenuItem
            // 
            this.thirtyMToolStripMenuItem.Checked = true;
            this.thirtyMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thirtyMToolStripMenuItem.Name = "thirtyMToolStripMenuItem";
            this.thirtyMToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.thirtyMToolStripMenuItem.Text = "30m";
            this.thirtyMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // revGeoToolStripMenuItem
            // 
            this.revGeoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.points250ToolStripMenuItem,
            this.points500ToolStripMenuItem,
            this.points750ToolStripMenuItem,
            this.points1000ToolStripMenuItem,
            this.points2000ToolStripMenuItem});
            this.revGeoToolStripMenuItem.Name = "revGeoToolStripMenuItem";
            this.revGeoToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.revGeoToolStripMenuItem.Text = "Geocode Points";
            // 
            // points250ToolStripMenuItem
            // 
            this.points250ToolStripMenuItem.Checked = true;
            this.points250ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.points250ToolStripMenuItem.Name = "points250ToolStripMenuItem";
            this.points250ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.points250ToolStripMenuItem.Text = "250 Points";
            this.points250ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points500ToolStripMenuItem
            // 
            this.points500ToolStripMenuItem.Name = "points500ToolStripMenuItem";
            this.points500ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.points500ToolStripMenuItem.Text = "500 Points";
            this.points500ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points750ToolStripMenuItem
            // 
            this.points750ToolStripMenuItem.Name = "points750ToolStripMenuItem";
            this.points750ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.points750ToolStripMenuItem.Text = "750 Points";
            this.points750ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points1000ToolStripMenuItem
            // 
            this.points1000ToolStripMenuItem.Name = "points1000ToolStripMenuItem";
            this.points1000ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.points1000ToolStripMenuItem.Text = "1000 Points";
            this.points1000ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points2000ToolStripMenuItem
            // 
            this.points2000ToolStripMenuItem.Name = "points2000ToolStripMenuItem";
            this.points2000ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.points2000ToolStripMenuItem.Text = "2000 Points";
            this.points2000ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ViewHelpToolStripMenuItem
            // 
            this.ViewHelpToolStripMenuItem.Name = "ViewHelpToolStripMenuItem";
            this.ViewHelpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ViewHelpToolStripMenuItem.Text = "View Help";
            this.ViewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // viewElevationProfileToolStripMenuItem
            // 
            this.viewElevationProfileToolStripMenuItem.Name = "viewElevationProfileToolStripMenuItem";
            this.viewElevationProfileToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.viewElevationProfileToolStripMenuItem.Text = "View Elevation Profile";
            this.viewElevationProfileToolStripMenuItem.Click += new System.EventHandler(this.viewElevationProfileToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lookupToolStripProgressBar,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.currentTurnStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 593);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(892, 23);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(20, 18);
            this.toolStripStatusLabel1.Text = "Ok";
            // 
            // lookupToolStripProgressBar
            // 
            this.lookupToolStripProgressBar.Name = "lookupToolStripProgressBar";
            this.lookupToolStripProgressBar.Size = new System.Drawing.Size(150, 17);
            this.lookupToolStripProgressBar.Step = 1;
            this.lookupToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 18);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(26, 18);
            this.toolStripStatusLabel3.Text = "0, 0";
            // 
            // currentTurnStatusLabel
            // 
            this.currentTurnStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.currentTurnStatusLabel.Name = "currentTurnStatusLabel";
            this.currentTurnStatusLabel.Size = new System.Drawing.Size(0, 18);
            // 
            // openGpsFileDialog
            // 
            this.openGpsFileDialog.Filter = "KML File|*.kml|GPX Files|*.gpx";
            this.openGpsFileDialog.FilterIndex = 2;
            this.openGpsFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openGpxFileDialog_FileOk);
            // 
            // saveOutputFileDialog
            // 
            this.saveOutputFileDialog.Filter = "CSV File|*.csv";
            this.saveOutputFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveOutputFileDialog_FileOk);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(797, 564);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(83, 23);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(708, 564);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(83, 23);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(620, 564);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(79, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.turnPictureBox);
            this.groupBox3.Location = new System.Drawing.Point(612, 298);
            this.groupBox3.MinimumSize = new System.Drawing.Size(277, 263);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 263);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Turn Inspector";
            // 
            // turnPictureBox
            // 
            this.turnPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.turnPictureBox.Location = new System.Drawing.Point(3, 16);
            this.turnPictureBox.Name = "turnPictureBox";
            this.turnPictureBox.Size = new System.Drawing.Size(271, 244);
            this.turnPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.turnPictureBox.TabIndex = 0;
            this.turnPictureBox.TabStop = false;
            this.turnPictureBox.SizeChanged += new System.EventHandler(this.turnPictureBox_SizeChanged);
            // 
            // poiNametextBox
            // 
            this.poiNametextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.poiNametextBox.Enabled = false;
            this.poiNametextBox.Location = new System.Drawing.Point(66, 567);
            this.poiNametextBox.Name = "poiNametextBox";
            this.poiNametextBox.Size = new System.Drawing.Size(127, 20);
            this.poiNametextBox.TabIndex = 7;
            // 
            // poiNameLabel
            // 
            this.poiNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.poiNameLabel.AutoSize = true;
            this.poiNameLabel.Enabled = false;
            this.poiNameLabel.Location = new System.Drawing.Point(4, 570);
            this.poiNameLabel.Name = "poiNameLabel";
            this.poiNameLabel.Size = new System.Drawing.Size(56, 13);
            this.poiNameLabel.TabIndex = 8;
            this.poiNameLabel.Text = "POI Name";
            // 
            // poiDescriptionLabel
            // 
            this.poiDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.poiDescriptionLabel.AutoSize = true;
            this.poiDescriptionLabel.Enabled = false;
            this.poiDescriptionLabel.Location = new System.Drawing.Point(199, 570);
            this.poiDescriptionLabel.Name = "poiDescriptionLabel";
            this.poiDescriptionLabel.Size = new System.Drawing.Size(81, 13);
            this.poiDescriptionLabel.TabIndex = 9;
            this.poiDescriptionLabel.Text = "POI Description";
            // 
            // poiDescriptionTextBox
            // 
            this.poiDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.poiDescriptionTextBox.Enabled = false;
            this.poiDescriptionTextBox.Location = new System.Drawing.Point(286, 567);
            this.poiDescriptionTextBox.Name = "poiDescriptionTextBox";
            this.poiDescriptionTextBox.Size = new System.Drawing.Size(235, 20);
            this.poiDescriptionTextBox.TabIndex = 10;
            // 
            // addPoiButton
            // 
            this.addPoiButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addPoiButton.Enabled = false;
            this.addPoiButton.Location = new System.Drawing.Point(527, 564);
            this.addPoiButton.Name = "addPoiButton";
            this.addPoiButton.Size = new System.Drawing.Size(79, 23);
            this.addPoiButton.TabIndex = 11;
            this.addPoiButton.Text = "Add";
            this.addPoiButton.UseVisualStyleBackColor = true;
            this.addPoiButton.Click += new System.EventHandler(this.addPoiButton_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 616);
            this.Controls.Add(this.addPoiButton);
            this.Controls.Add(this.poiDescriptionTextBox);
            this.Controls.Add(this.poiDescriptionLabel);
            this.Controls.Add(this.poiNameLabel);
            this.Controls.Add(this.poiNametextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(900, 550);
            this.Name = "MainForm";
            this.Tag = "Pathfinder";
            this.Text = "Pathfinder";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.turnPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox mapPictureBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog openGpsFileDialog;
        private System.Windows.Forms.SaveFileDialog saveOutputFileDialog;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ViewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar lookupToolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox turnPictureBox;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kilometersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem milesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel currentTurnStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem mapTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roadmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satelliteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hybridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathResToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tenMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fifteenMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twentyMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revGeoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem points250ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem points500ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem points750ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem points1000ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirtyMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem points2000ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox endTextBox;
        private System.Windows.Forms.TextBox beginTextBox;
        private System.Windows.Forms.ListView cueSheetListView;
        private System.Windows.Forms.TextBox poiNametextBox;
        private System.Windows.Forms.Label poiNameLabel;
        private System.Windows.Forms.Label poiDescriptionLabel;
        private System.Windows.Forms.TextBox poiDescriptionTextBox;
        private System.Windows.Forms.Button addPoiButton;
        private System.Windows.Forms.ToolStripMenuItem cSVFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hTMLFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewElevationProfileToolStripMenuItem;
    }
}

