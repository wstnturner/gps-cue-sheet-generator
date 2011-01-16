namespace CueSheetGenerator {
	partial class TurnViewer {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TurnViewer));
			this.turnMapGroupBox = new System.Windows.Forms.GroupBox();
			this.mapPictureBox = new System.Windows.Forms.PictureBox();
			this.backButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.turnNumberTextBox = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.deleteButton = new System.Windows.Forms.Button();
			this.turnMapGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// turnMapGroupBox
			// 
			this.turnMapGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.turnMapGroupBox.Controls.Add(this.mapPictureBox);
			this.turnMapGroupBox.Location = new System.Drawing.Point(12, 41);
			this.turnMapGroupBox.Name = "turnMapGroupBox";
			this.turnMapGroupBox.Size = new System.Drawing.Size(368, 300);
			this.turnMapGroupBox.TabIndex = 0;
			this.turnMapGroupBox.TabStop = false;
			this.turnMapGroupBox.Text = "Turn Map";
			// 
			// mapPictureBox
			// 
			this.mapPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapPictureBox.Location = new System.Drawing.Point(3, 16);
			this.mapPictureBox.Name = "mapPictureBox";
			this.mapPictureBox.Size = new System.Drawing.Size(362, 281);
			this.mapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.mapPictureBox.TabIndex = 0;
			this.mapPictureBox.TabStop = false;
			this.mapPictureBox.SizeChanged += new System.EventHandler(this.turnPictureBox_SizeChanged);
			// 
			// backButton
			// 
			this.backButton.Location = new System.Drawing.Point(95, 12);
			this.backButton.Name = "backButton";
			this.backButton.Size = new System.Drawing.Size(82, 23);
			this.backButton.TabIndex = 1;
			this.backButton.Text = "<< Back";
			this.backButton.UseVisualStyleBackColor = true;
			this.backButton.Click += new System.EventHandler(this.backButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(183, 12);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(82, 23);
			this.nextButton.TabIndex = 2;
			this.nextButton.Text = "Next >>";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
			// 
			// turnNumberTextBox
			// 
			this.turnNumberTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.turnNumberTextBox.Location = new System.Drawing.Point(316, 12);
			this.turnNumberTextBox.Name = "turnNumberTextBox";
			this.turnNumberTextBox.Size = new System.Drawing.Size(64, 23);
			this.turnNumberTextBox.TabIndex = 3;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 344);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(392, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(109, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(271, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Turn #";
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(12, 12);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(62, 23);
			this.deleteButton.TabIndex = 6;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			// 
			// TurnViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(392, 366);
			this.Controls.Add(this.deleteButton);
			this.Controls.Add(this.backButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.turnNumberTextBox);
			this.Controls.Add(this.turnMapGroupBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TurnViewer";
			this.Text = "Turn Viewer";
			this.turnMapGroupBox.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mapPictureBox)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox turnMapGroupBox;
		private System.Windows.Forms.PictureBox mapPictureBox;
		private System.Windows.Forms.Button backButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.TextBox turnNumberTextBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Button deleteButton;
	}
}