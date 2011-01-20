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
			updateRideMap();
			_ps.finishedProcessing += updateDirections;
			finishedProcessing += updateDirections;
			finishedProcessing += reEnableControls;
			_ps.processedWaypoint += updateProgressBar;
			processedWaypoint += updateProgressBar;
		}

		void updateRideMap() {
			// show image in picturebox
			mapPictureBox.Image = _ps.getRideMap(mapPictureBox.Height, mapPictureBox.Width);
			if (mapPictureBox.Image == null)
				toolStripStatusLabel1.Text = _ps.Web.Status;
		}

		void updateTurnMap() {
			turnPictureBox.Image = _ps.getTurnMap(turnPictureBox.Height, turnPictureBox.Width);
			if (turnPictureBox.Image == null)
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
				Thread.CurrentThread.Abort(e);
			}
		}
		
		public void updateDirections() {
			if (directionsTextBox.InvokeRequired) {
				this.Invoke(finishedProcessing);
			} else {
				directionsTextBox.Clear();
				directionsTextBox.Text = _ps.getDirections(_units);               
                toolStripStatusLabel1.Text = _ps.Status;
				toolStripStatusLabel2.Text = "Done,";
				lookupToolStripProgressBar.Value = 0;
				updateTurnMap();
				toolStripStatusLabel4.Text = _ps.getCurrentTurnString();
                /*hightlight current direction text*/
                highlight();
			}


		}

		public void reEnableControls() {
			fileToolStripMenuItem.Enabled = true;
			viewToolStripMenuItem.Enabled = true;
			deleteButton.Enabled = true;
			backButton.Enabled = true;
			nextButton.Enabled = true;
		}

		private void openGpxFileDialog_FileOk(object sender, CancelEventArgs e) {
			_ps.processInput(openGpxFileDialog.FileName);
			turnPictureBox.Image = null;
			directionsTextBox.Clear();
			fileToolStripMenuItem.Enabled = false;
			viewToolStripMenuItem.Enabled = false;
			deleteButton.Enabled = false;
			backButton.Enabled = false;
			nextButton.Enabled = false;
			updateRideMap();
			//initialize the progress bar
			lookupToolStripProgressBar.Maximum = _ps.Path.Waypoints.Count;
			toolStripStatusLabel2.Text = "Processing,";
		}

		private void openToolStripMenuItem1_Click(object sender, EventArgs e) {
			openGpxFileDialog.ShowDialog();
		}

		private void unitsToolStripComboBox_Click(object sender, EventArgs e) {
			if(_ps.Directions != null && _ps.Directions.Turns != null) updateDirections();
		}

		private void mapPictureBox_SizeChanged(object sender, EventArgs e) {
			if (_ps != null) updateRideMap();
		}

		private void mapPictureBox_MouseMove(object sender, MouseEventArgs e) {
			if (_ps != null) {
				Point pt = new Point(e.X, e.Y);
				_ps.getWaypointFromMousePosition(pt);
				if (_ps.WaypointFromMouse != null)
					toolStripStatusLabel3.Text = _ps.Path.UpperLeft.Zone + " E "
						+ Math.Round(_ps.WaypointFromMouse.Easting).ToString()
						+ ", " + "N " + Math.Round(_ps.WaypointFromMouse.Northing).ToString()
						+ ",";
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
			_ps.writeCsvFile(saveCsvFileDialog.FileName, _units);
			toolStripStatusLabel1.Text = _ps.Status;
		}

		public const string METERS = "Meters", KM = "Kilometers", MILES = "Miles";
		string _units = MILES;

		private void unitsToolStripMenuItem_Click(object sender, EventArgs e) {
			metersToolStripMenuItem.Checked = false;
			kilometersToolStripMenuItem.Checked = false;
			milesToolStripMenuItem.Checked = false;
			switch (sender.ToString()) {
				case "Meters":
					metersToolStripMenuItem.Checked = true;
					_units = METERS;
					break;
				case "Kilometers":
					kilometersToolStripMenuItem.Checked = true;
					_units = KM;
					break;
				case "Miles":
					milesToolStripMenuItem.Checked = true;
					_units = MILES;
					break;
				default:
					milesToolStripMenuItem.Checked = true;
					break;
			}
			updateDirections();
		}

		private void turnPictureBox_SizeChanged(object sender, EventArgs e) {
			if (_ps != null) updateTurnMap();
		}

		private void nextButton_Click(object sender, EventArgs e) {
			_ps.incrementTurn();
			updateTurnMap();
			toolStripStatusLabel4.Text = _ps.getCurrentTurnString();
            /*hightlight current direction text*/
            highlight();
            
        }

		private void backButton_Click(object sender, EventArgs e) {
			_ps.decrementTurn();
			updateTurnMap();
			toolStripStatusLabel4.Text = _ps.getCurrentTurnString();
            /*hightlight current direction text*/
            highlight();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			_ps.deleteCurrentTurn();
			updateTurnMap();
			if (_ps.Directions!= null && _ps.Directions.Turns != null)
				updateDirections();
            /*hightlight current direction text*/
            highlight();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			PathfinderAboutBox p = new PathfinderAboutBox();
			p.Show();
		}

		private void mapTypeToolStripMenuItem_Click(object sender, EventArgs e) {
			roadmapToolStripMenuItem.Checked = false;
			satelliteToolStripMenuItem.Checked = false;
			terrainToolStripMenuItem.Checked = false;
			hybridToolStripMenuItem.Checked = false;
			switch (sender.ToString()) {
				case "Roadmap":
					roadmapToolStripMenuItem.Checked = true;
					_ps.Path.MapType = TrackPath.ROADMAP;
					break;
				case "Satellite":
					satelliteToolStripMenuItem.Checked = true;
					_ps.Path.MapType = TrackPath.SATELLITE;
					break;
				case "Terrain":
					terrainToolStripMenuItem.Checked = true;
					_ps.Path.MapType = TrackPath.TERRAIN;
					break;
				case "Hybrid":
					hybridToolStripMenuItem.Checked = true;
					_ps.Path.MapType = TrackPath.HYBRID;
					break;
				default:
					roadmapToolStripMenuItem.Checked = true;
					_ps.Path.MapType = TrackPath.ROADMAP;
					break;
			}
			updateRideMap();
		}

		private void pathResolutionToolStripMenuItem_Click(object sender, EventArgs e) {
			tenMToolStripMenuItem.Checked = false;
			fifteenMToolStripMenuItem.Checked = false;
			twentyMToolStripMenuItem.Checked = false;
			thirtyMToolStripMenuItem.Checked = false;
			switch (sender.ToString()) {
				case "10m":
					tenMToolStripMenuItem.Checked = true;
					_ps.WaypointSeperation = PathfinderStrategy.TEN_M;
					break;
				case "15m":
					fifteenMToolStripMenuItem.Checked = true;
					_ps.WaypointSeperation = PathfinderStrategy.FIFTEEN_M;
					break;
				case "20m":
					twentyMToolStripMenuItem.Checked = true;
					_ps.WaypointSeperation = PathfinderStrategy.TWENTY_M;
					break;
				case "30m":
					thirtyMToolStripMenuItem.Checked = true;
					_ps.WaypointSeperation = PathfinderStrategy.THIRTY_M;
					break;
				default:
					thirtyMToolStripMenuItem.Checked = true;
					_ps.WaypointSeperation = PathfinderStrategy.THIRTY_M;
					break;
			}
		}

		private void revGeoToolStripMenuItem_Click(object sender, EventArgs e) {
			points250ToolStripMenuItem.Checked = false;
			points500ToolStripMenuItem.Checked = false;
			points750ToolStripMenuItem.Checked = false;
			points1000ToolStripMenuItem.Checked = false;
			switch (sender.ToString()) {
				case "250 Points":
					points250ToolStripMenuItem.Checked = true;
					_ps.Path.MaxGpxPoints = TrackPath.REV_GEO_250;
					break;
				case "500 Points":
					points500ToolStripMenuItem.Checked = true;
					_ps.Path.MaxGpxPoints = TrackPath.REV_GEO_500;
					break;
				case "750 Points":
					points750ToolStripMenuItem.Checked = true;
					_ps.Path.MaxGpxPoints = TrackPath.REV_GEO_750;
					break;
				case "1000 Points":
					points1000ToolStripMenuItem.Checked = true;
					_ps.Path.MaxGpxPoints = TrackPath.REV_GEO_1000;
					break;
				default:
					points250ToolStripMenuItem.Checked = true;
					_ps.Path.MaxGpxPoints = TrackPath.REV_GEO_250;
					break;
			}
		}

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /*hightlight current direction text*/
        public void highlight()
        {
            string s = directionsTextBox.Text;    
            int i = s.IndexOf((_ps.CurrentTurn+1).ToString() + ")");
            directionsTextBox.SelectionStart = i;
            directionsTextBox.SelectionLength = s.IndexOf("\r\n\r\n", i) - s.IndexOf((_ps.CurrentTurn + 1).ToString() + ")");
            directionsTextBox.Focus();                         
        }
    

	}


}
