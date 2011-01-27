namespace CueSheetGenerator {
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
            this.directionsTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lookupToolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.openGpxFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveCsvFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.turnPictureBox = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.turnPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mapPictureBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ride Map";
            // 
            // mapPictureBox
            // 
            this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapPictureBox.Location = new System.Drawing.Point(3, 17);
            this.mapPictureBox.Name = "mapPictureBox";
            this.mapPictureBox.Size = new System.Drawing.Size(614, 456);
            this.mapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mapPictureBox.TabIndex = 0;
            this.mapPictureBox.TabStop = false;
            this.mapPictureBox.SizeChanged += new System.EventHandler(this.mapPictureBox_SizeChanged);
            this.mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseClick);
            this.mapPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapPictureBox_MouseMove);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.directionsTextBox);
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 198);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Directions";
            // 
            // directionsTextBox
            // 
            this.directionsTextBox.AcceptsReturn = true;
            this.directionsTextBox.AcceptsTab = true;
            this.directionsTextBox.BackColor = System.Drawing.Color.White;
            this.directionsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directionsTextBox.Location = new System.Drawing.Point(3, 17);
            this.directionsTextBox.Multiline = true;
            this.directionsTextBox.Name = "directionsTextBox";
            this.directionsTextBox.ReadOnly = true;
            this.directionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.directionsTextBox.Size = new System.Drawing.Size(293, 178);
            this.directionsTextBox.TabIndex = 0;
            this.directionsTextBox.WordWrap = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(924, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.unitsToolStripMenuItem,
            this.mapTypeToolStripMenuItem,
            this.pathResToolStripMenuItem,
            this.revGeoToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Enabled = false;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.metersToolStripMenuItem,
            this.kilometersToolStripMenuItem,
            this.milesToolStripMenuItem});
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.unitsToolStripMenuItem.Text = "Units";
            // 
            // metersToolStripMenuItem
            // 
            this.metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            this.metersToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.metersToolStripMenuItem.Tag = "Meters";
            this.metersToolStripMenuItem.Text = "Meters";
            this.metersToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // kilometersToolStripMenuItem
            // 
            this.kilometersToolStripMenuItem.Name = "kilometersToolStripMenuItem";
            this.kilometersToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.kilometersToolStripMenuItem.Tag = "Kilometers";
            this.kilometersToolStripMenuItem.Text = "Kilometers";
            this.kilometersToolStripMenuItem.Click += new System.EventHandler(this.unitsToolStripMenuItem_Click);
            // 
            // milesToolStripMenuItem
            // 
            this.milesToolStripMenuItem.Checked = true;
            this.milesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.milesToolStripMenuItem.Name = "milesToolStripMenuItem";
            this.milesToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
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
            this.mapTypeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.mapTypeToolStripMenuItem.Text = "Map Type";
            // 
            // roadmapToolStripMenuItem
            // 
            this.roadmapToolStripMenuItem.Checked = true;
            this.roadmapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.roadmapToolStripMenuItem.Name = "roadmapToolStripMenuItem";
            this.roadmapToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.roadmapToolStripMenuItem.Text = "Roadmap";
            this.roadmapToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // satelliteToolStripMenuItem
            // 
            this.satelliteToolStripMenuItem.Name = "satelliteToolStripMenuItem";
            this.satelliteToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.satelliteToolStripMenuItem.Text = "Satellite";
            this.satelliteToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.terrainToolStripMenuItem.Text = "Terrain";
            this.terrainToolStripMenuItem.Click += new System.EventHandler(this.mapTypeToolStripMenuItem_Click);
            // 
            // hybridToolStripMenuItem
            // 
            this.hybridToolStripMenuItem.Name = "hybridToolStripMenuItem";
            this.hybridToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
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
            this.pathResToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.pathResToolStripMenuItem.Text = "Path Resolution";
            // 
            // tenMToolStripMenuItem
            // 
            this.tenMToolStripMenuItem.Name = "tenMToolStripMenuItem";
            this.tenMToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.tenMToolStripMenuItem.Text = "10m";
            this.tenMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // fifteenMToolStripMenuItem
            // 
            this.fifteenMToolStripMenuItem.Name = "fifteenMToolStripMenuItem";
            this.fifteenMToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.fifteenMToolStripMenuItem.Text = "15m";
            this.fifteenMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // twentyMToolStripMenuItem
            // 
            this.twentyMToolStripMenuItem.Name = "twentyMToolStripMenuItem";
            this.twentyMToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.twentyMToolStripMenuItem.Text = "20m";
            this.twentyMToolStripMenuItem.Click += new System.EventHandler(this.pathResolutionToolStripMenuItem_Click);
            // 
            // thirtyMToolStripMenuItem
            // 
            this.thirtyMToolStripMenuItem.Checked = true;
            this.thirtyMToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thirtyMToolStripMenuItem.Name = "thirtyMToolStripMenuItem";
            this.thirtyMToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
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
            this.revGeoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.revGeoToolStripMenuItem.Text = "Geocode Points";
            // 
            // points250ToolStripMenuItem
            // 
            this.points250ToolStripMenuItem.Checked = true;
            this.points250ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.points250ToolStripMenuItem.Name = "points250ToolStripMenuItem";
            this.points250ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.points250ToolStripMenuItem.Text = "250 Points";
            this.points250ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points500ToolStripMenuItem
            // 
            this.points500ToolStripMenuItem.Name = "points500ToolStripMenuItem";
            this.points500ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.points500ToolStripMenuItem.Text = "500 Points";
            this.points500ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points750ToolStripMenuItem
            // 
            this.points750ToolStripMenuItem.Name = "points750ToolStripMenuItem";
            this.points750ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.points750ToolStripMenuItem.Text = "750 Points";
            this.points750ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points1000ToolStripMenuItem
            // 
            this.points1000ToolStripMenuItem.Name = "points1000ToolStripMenuItem";
            this.points1000ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.points1000ToolStripMenuItem.Text = "1000 Points";
            this.points1000ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // points2000ToolStripMenuItem
            // 
            this.points2000ToolStripMenuItem.Name = "points2000ToolStripMenuItem";
            this.points2000ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.points2000ToolStripMenuItem.Text = "2000 Points";
            this.points2000ToolStripMenuItem.Click += new System.EventHandler(this.revGeoToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // ViewHelpToolStripMenuItem
            // 
            this.ViewHelpToolStripMenuItem.Name = "ViewHelpToolStripMenuItem";
            this.ViewHelpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ViewHelpToolStripMenuItem.Text = "View Help";
            this.ViewHelpToolStripMenuItem.Click += new System.EventHandler(this.howDoIToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lookupToolStripProgressBar,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 500);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(924, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabel1.Text = "Ok";
            // 
            // lookupToolStripProgressBar
            // 
            this.lookupToolStripProgressBar.Name = "lookupToolStripProgressBar";
            this.lookupToolStripProgressBar.Size = new System.Drawing.Size(175, 16);
            this.lookupToolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(28, 17);
            this.toolStripStatusLabel3.Text = "0, 0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 17);
            // 
            // openGpxFileDialog
            // 
            this.openGpxFileDialog.Filter = "GPX Files|*.gpx";
            this.openGpxFileDialog.FilterIndex = 2;
            this.openGpxFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openGpxFileDialog_FileOk);
            // 
            // saveCsvFileDialog
            // 
            this.saveCsvFileDialog.Filter = "CSV File|*.csv";
            this.saveCsvFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveCsvFileDialog_FileOk);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.nextButton);
            this.splitContainer1.Panel2.Controls.Add(this.backButton);
            this.splitContainer1.Panel2.Controls.Add(this.deleteButton);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(924, 476);
            this.splitContainer1.SplitterDistance = 620;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 4;
            // 
            // nextButton
            // 
            this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(218, 452);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(66, 21);
            this.nextButton.TabIndex = 6;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(145, 452);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(66, 21);
            this.backButton.TabIndex = 5;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(66, 452);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(66, 21);
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
            this.groupBox3.Location = new System.Drawing.Point(3, 204);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 243);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Turn Inspector";
            // 
            // turnPictureBox
            // 
            this.turnPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.turnPictureBox.Location = new System.Drawing.Point(3, 17);
            this.turnPictureBox.Name = "turnPictureBox";
            this.turnPictureBox.Size = new System.Drawing.Size(286, 223);
            this.turnPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.turnPictureBox.TabIndex = 0;
            this.turnPictureBox.TabStop = false;
            this.turnPictureBox.SizeChanged += new System.EventHandler(this.turnPictureBox_SizeChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 522);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
		private System.Windows.Forms.OpenFileDialog openGpxFileDialog;
		private System.Windows.Forms.SaveFileDialog saveCsvFileDialog;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.TextBox directionsTextBox;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewHelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
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
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
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
	}
}

