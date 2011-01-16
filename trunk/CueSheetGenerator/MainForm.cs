using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CueSheetGenerator {
	public partial class MainForm : Form {
		event PathfinderStrategy.updateStatusEventHandler finishedProcessing;
		event PathfinderStrategy.updateStatusEventHandler processedWaypoint;

		PathfinderStrategy _ps = null;
		internal PathfinderStrategy Strategy {
			get { return _ps; }
		}

		public MainForm() {
			InitializeComponent();
			_ps = new PathfinderStrategy();
			updateMap();
			_ps.finishedProcessing += updateDirections;
			finishedProcessing += updateDirections;
			_ps.processedWaypoint += updateProgressBar;
			processedWaypoint += updateProgressBar;
		}

		void updateMap() {
			// show image in picturebox
			mapPictureBox.Image = _ps.getMap(mapPictureBox.Height, mapPictureBox.Width);
			if (mapPictureBox.Image.Height == 1 && mapPictureBox.Image.Width == 1)
				toolStripStatusLabel1.Text = _ps.Web.Status;
		}

		void updateProgressBar() {
			//not ideal, i'd rather use logic to terminate the thread
			//rather than use error handling
			try {
				if (directionsTextBox.InvokeRequired)
					this.Invoke(processedWaypoint);
				else
					lookupToolStripProgressBar.Value = _ps.Locations.Count;
			} catch(Exception e) {
				Thread.CurrentThread.Abort();
			}
		}
		
		public void updateDirections() {
			if (directionsTextBox.InvokeRequired)
				this.Invoke(finishedProcessing);
			else {
				directionsTextBox.Clear();
				directionsTextBox.Text = _ps.getDirections(unitsToolStripComboBox.SelectedItem.ToString());
				toolStripStatusLabel1.Text = _ps.Status;
				toolStripStatusLabel2.Text = "Done";
				lookupToolStripProgressBar.Value = 0;
			}
		}

		private void openGpxFileDialog_FileOk(object sender, CancelEventArgs e) {
			_ps.processInput(openGpxFileDialog.FileName);
			updateMap();
			//initialize the progress bar
			lookupToolStripProgressBar.Maximum = _ps.Path.Waypoints.Count;
			toolStripStatusLabel2.Text = "Processing";
		}

		private void openToolStripMenuItem1_Click(object sender, EventArgs e) {
			openGpxFileDialog.ShowDialog();
		}

		private void turnViewerToolStripMenuItem_Click(object sender, EventArgs e) {
			if (_ps.Directions.Turns != null && _ps.Directions.Turns.Count > 0) {
				TurnViewer tv = new TurnViewer(_ps.Directions.Turns, _ps.Web, this);
				tv.Show();
			}
		}

		private void unitsToolStripComboBox_Click(object sender, EventArgs e) {
			if(_ps.Directions != null && _ps.Directions.Turns != null) updateDirections();
		}

		private void mapPictureBox_SizeChanged(object sender, EventArgs e) {
			if (_ps != null) updateMap();
		}

		private void mapPictureBox_MouseMove(object sender, MouseEventArgs e) {
			if (_ps != null) {
				Point pt = new Point(e.X, e.Y);
				_ps.getWaypointFromMousePosition(pt);
				if (_ps.WaypointFromMouse != null)
					toolStripStatusLabel3.Text = _ps.Path.UpperLeft.Zone + " E "
						+ Math.Round(_ps.WaypointFromMouse.Easting, 2).ToString()
						+ ", " + "N " + Math.Round(_ps.WaypointFromMouse.Northing, 2).ToString();
			}
		}

		private void mapPictureBox_MouseClick(object sender, MouseEventArgs e) {
			//use this event to add notes to locations on the map
			//the note will appear in the csv file output by the program
			_ps.addPointOfInterest(new Point(e.X, e.Y));
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			saveCsvFileDialog.ShowDialog();
		}

		private void saveCsvFileDialog_FileOk(object sender, CancelEventArgs e) {
			_ps.writeCsvFile(saveCsvFileDialog.FileName, unitsToolStripComboBox.SelectedItem.ToString());
			toolStripStatusLabel1.Text = _ps.Status;
		}



	}


}
