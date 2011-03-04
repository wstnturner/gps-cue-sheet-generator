namespace CueSheetGenerator {
    partial class ElevationProfileDisplay {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElevationProfileDisplay));
            this.elevationProfilePictureBox = new System.Windows.Forms.PictureBox();
            this.elevationProfilePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.elevationProfilePictureBox)).BeginInit();
            this.elevationProfilePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // elevationProfilePictureBox
            // 
            this.elevationProfilePictureBox.Location = new System.Drawing.Point(3, 3);
            this.elevationProfilePictureBox.Name = "elevationProfilePictureBox";
            this.elevationProfilePictureBox.Size = new System.Drawing.Size(100, 50);
            this.elevationProfilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.elevationProfilePictureBox.TabIndex = 0;
            this.elevationProfilePictureBox.TabStop = false;
            this.elevationProfilePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.elevationProfilePictureBox_MouseMove);
            // 
            // elevationProfilePanel
            // 
            this.elevationProfilePanel.AutoScroll = true;
            this.elevationProfilePanel.AutoSize = true;
            this.elevationProfilePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.elevationProfilePanel.Controls.Add(this.elevationProfilePictureBox);
            this.elevationProfilePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elevationProfilePanel.Location = new System.Drawing.Point(0, 0);
            this.elevationProfilePanel.Name = "elevationProfilePanel";
            this.elevationProfilePanel.Size = new System.Drawing.Size(292, 266);
            this.elevationProfilePanel.TabIndex = 1;
            // 
            // ElevationProfileDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.elevationProfilePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ElevationProfileDisplay";
            this.Text = "Elevation Profile Display";
            ((System.ComponentModel.ISupportInitialize)(this.elevationProfilePictureBox)).EndInit();
            this.elevationProfilePanel.ResumeLayout(false);
            this.elevationProfilePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox elevationProfilePictureBox;
        private System.Windows.Forms.Panel elevationProfilePanel;
    }
}