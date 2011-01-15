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
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.turnViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unitsToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.howDoIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openGpxFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveCsvFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mapPictureBox);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(532, 520);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Ride Map";
			// 
			// mapPictureBox
			// 
			this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapPictureBox.Location = new System.Drawing.Point(3, 16);
			this.mapPictureBox.Name = "mapPictureBox";
			this.mapPictureBox.Size = new System.Drawing.Size(526, 501);
			this.mapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.mapPictureBox.TabIndex = 0;
			this.mapPictureBox.TabStop = false;
			this.mapPictureBox.SizeChanged += new System.EventHandler(this.mapPictureBox_SizeChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.directionsTextBox);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(256, 520);
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
			this.directionsTextBox.Location = new System.Drawing.Point(3, 16);
			this.directionsTextBox.Multiline = true;
			this.directionsTextBox.Name = "directionsTextBox";
			this.directionsTextBox.ReadOnly = true;
			this.directionsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.directionsTextBox.Size = new System.Drawing.Size(250, 501);
			this.directionsTextBox.TabIndex = 0;
			this.directionsTextBox.WordWrap = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(792, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem});
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.openToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem1
			// 
			this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
			this.openToolStripMenuItem1.Size = new System.Drawing.Size(111, 22);
			this.openToolStripMenuItem1.Text = "Open";
			this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.turnViewerToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.unitsToolStripComboBox});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// turnViewerToolStripMenuItem
			// 
			this.turnViewerToolStripMenuItem.Name = "turnViewerToolStripMenuItem";
			this.turnViewerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.turnViewerToolStripMenuItem.Text = "Turn Viewer";
			this.turnViewerToolStripMenuItem.Click += new System.EventHandler(this.turnViewerToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// unitsToolStripComboBox
			// 
			this.unitsToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.unitsToolStripComboBox.Items.AddRange(new object[] {
            "Meters",
            "Kilometers",
            "Miles"});
			this.unitsToolStripComboBox.Name = "unitsToolStripComboBox";
			this.unitsToolStripComboBox.Size = new System.Drawing.Size(121, 150);
			this.unitsToolStripComboBox.Text = "Miles";
			this.unitsToolStripComboBox.Click += new System.EventHandler(this.unitsToolStripComboBox_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howDoIToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// howDoIToolStripMenuItem
			// 
			this.howDoIToolStripMenuItem.Name = "howDoIToolStripMenuItem";
			this.howDoIToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.howDoIToolStripMenuItem.Text = "How do I";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 544);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(792, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(20, 17);
			this.toolStripStatusLabel1.Text = "Ok";
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
			this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
			this.splitContainer1.Size = new System.Drawing.Size(792, 520);
			this.splitContainer1.SplitterDistance = 532;
			this.splitContainer1.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 566);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Pathfinder";
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
			this.splitContainer1.ResumeLayout(false);
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
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem turnViewerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem howDoIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox unitsToolStripComboBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
	}
}

