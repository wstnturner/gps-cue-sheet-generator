using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CueSheetGenerator {
    public partial class ElevationProfileDisplay : Form {
        internal ElevationProfileDisplay(List<Location> locs, string units) {
            InitializeComponent();
            elevationProfilePictureBox.Image = ElevationProfiler.getElevationProfile(locs, units);
            this.Width = elevationProfilePictureBox.Image.Width + 5;
            this.Height = elevationProfilePictureBox.Image.Height + 55;
            this.Text = "Elevation Profile Display, distance in: " + units.ToString();
        }

        private void elevationProfilePictureBox_MouseMove(object sender, MouseEventArgs e) {

        }
    }
}
