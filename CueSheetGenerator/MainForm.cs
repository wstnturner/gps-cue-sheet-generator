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
    /// <summary>
    /// main form class, Pathfinder GUI
    /// </summary>
    public partial class MainForm : Form {
        event PathfinderStrategy.updateStatusEventHandler finishedProcessing;
        event PathfinderStrategy.updateStatusEventHandler processedWaypoint;
        event PathfinderStrategy.updateStatusEventHandler enableControlls;
        bool _osx = false;

        //private instance of the pathfinder strategy class
        PathfinderStrategy _ps = null;

        /// <summary>
        /// string constants for menu option case structures
        /// </summary>
        public const string METERS = "Meters", KM = "Kilometers", MILES = "Miles";
        string _units = MILES;

        /// <summary>
        /// constructor for the main form
        /// </summary>
        public MainForm(string fileName) {
            InitializeComponent();
            _ps = new PathfinderStrategy();
            _osx = Environment.OSVersion.VersionString.Contains("Unix 10");
            if (_osx) {
                openToolStripMenuItem.Enabled = false;
                toolStripStatusLabel4.Text = "Use drag and drop to open gpx files.";
            }
            _ps.finishedProcessing += updateDirections;
            finishedProcessing += updateDirections;
            _ps.finishedProcessing += reEnableControls;
            enableControlls += reEnableControls;
            _ps.processedWaypoint += updateProgressBar;
            processedWaypoint += updateProgressBar;
            this.Show();
            if (fileName != null) openGpsFile(fileName);
            else updateRideMap();
        }

        /// update the main ride map
        void updateRideMap() {
            // show image in picturebox
            mapPictureBox.Image = _ps.getRideMap(mapPictureBox.Height, mapPictureBox.Width);
            if (mapPictureBox.Image == null)
                toolStripStatusLabel1.Text = _ps.Web.Status;
        }

        /// update the turn ins[ector turn map
        void updateTurnMap() {
            /*hightlight current direction text*/
            highlight();
            turnPictureBox.Image = _ps.getTurnMap(turnPictureBox.Height, turnPictureBox.Width);
            if (turnPictureBox.Image == null) {
                toolStripStatusLabel1.Text = _ps.Web.Status;
            }
        }

        /// every time a waypoint is decoded update the progress bar
        void updateProgressBar() {
            //not ideal, i'd rather use logic to terminate the thread
            //rather than use error handling
            try {
                if (directionsTextBox.InvokeRequired)
                    this.Invoke(processedWaypoint);
                else
                    lookupToolStripProgressBar.Value = _ps.Locations.Count;
            } catch (Exception e) {
                Thread.CurrentThread.Abort(e);
            }
        }

        /// when the directions have been generated update the directions display
        private void updateDirections() {
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
            }
        }

        /// the controlls are disabled during processing, re-enable them afterward
        private void reEnableControls() {
            if (directionsTextBox.InvokeRequired) {
                this.Invoke(enableControlls);
            } else {
                fileToolStripMenuItem.Enabled = true;
                optionsToolStripMenuItem.Enabled = true;
                deleteButton.Enabled = true;
                backButton.Enabled = true;
                nextButton.Enabled = true;
            }
        }

        /// open a GPS file form the filesystem (GPX or KML)
        private void openGpsFile(string fileName) {
            //display the file name in the main window text
            if (_ps.Cache.Windows)
                this.Text = this.Tag + ": " + fileName.Remove(0, fileName.LastIndexOf("\\") + 1);
            else this.Text = this.Tag + ": " + fileName.Remove(0, fileName.LastIndexOf("/") + 1);
            _ps.processInput(fileName);
            prepareToDisplayRoute();
        }

        /// get the UI ready to display the route
        private void prepareToDisplayRoute() {
            if (_ps.Path.Waypoints.Count > 0) {
                //initialize the progress bar
                lookupToolStripProgressBar.Value = 0;
                lookupToolStripProgressBar.Maximum = _ps.Path.MaxGpxPoints;
                toolStripStatusLabel2.Text = "Processing,";
                fileToolStripMenuItem.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;
                deleteButton.Enabled = false;
                backButton.Enabled = false;
                nextButton.Enabled = false;
                turnPictureBox.Image = null;
                directionsTextBox.Clear();
                updateRideMap();
            }
        }

        //hightlight current direction text
        private void highlight() {
            if (_ps.Directions != null) {
                string s = directionsTextBox.Text;
                int i = s.IndexOf((_ps.CurrentTurn + 1).ToString() + ")");
                if (i > -1) {
                    directionsTextBox.Focus();
                    directionsTextBox.SelectionStart = i;
                    directionsTextBox.SelectionLength = s.IndexOf("\r\n\r\n", i) - i;
                    directionsTextBox.ScrollToCaret();
                }
            }
        }

        //all event handlers bolow here
        #region event_handlers
        //user drags an object onto the program surface
        private void MainForm_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        //user drops an object onto the program surface
        private void MainForm_DragDrop(object sender, DragEventArgs e) {
            Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (a != null && fileToolStripMenuItem.Enabled) {
                string s = a.GetValue(0).ToString();
                if (s.EndsWith(".gpx"))
                    openGpsFile(s);
            }
        }

        //user opens a GPX file (clicks OK)
        private void openGpxFileDialog_FileOk(object sender, CancelEventArgs e) {
            openGpsFile(openGpxFileDialog.FileName);
        }

        //user clicks the File->Open menu control 
        private void openToolStripMenuItem1_Click(object sender, EventArgs e) {
            openGpxFileDialog.ShowDialog();
        }

        //user clicks the File->Save menu control
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveCsvFileDialog.ShowDialog();
        }

        //user changes the size of the map
        private void mapPictureBox_SizeChanged(object sender, EventArgs e) {
            if (_ps != null) updateRideMap();
        }

        //user moves the mouse over the ride map
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

        //user clicks on the ride map, used to add points of interest
        private void mapPictureBox_MouseClick(object sender, MouseEventArgs e) {
            //use this event to add notes to locations on the map
            //the note will appear in the csv file output by the program
            _ps.addPointOfInterest(new Point(e.X, e.Y));
        }

        //user saves a CSV file (clicks save in the dialogue)
        private void saveCsvFileDialog_FileOk(object sender, CancelEventArgs e) {
            _ps.writeCsvFile(saveCsvFileDialog.FileName, _units);
            toolStripStatusLabel1.Text = _ps.Status;
        }

        //user changes the units option (Meters, Kilometers, Miles)
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

        //user changes the size of the turn map picture box
        private void turnPictureBox_SizeChanged(object sender, EventArgs e) {
            if (_ps != null) updateTurnMap();
        }

        //user clicked the next button
        private void nextButton_Click(object sender, EventArgs e) {
            _ps.incrementTurn();
            updateTurnMap();
            toolStripStatusLabel4.Text = _ps.getCurrentTurnString();
        }

        //user clicked the back button
        private void backButton_Click(object sender, EventArgs e) {
            _ps.decrementTurn();
            updateTurnMap();
            toolStripStatusLabel4.Text = _ps.getCurrentTurnString();
        }

        //user clicked the delete button
        private void deleteButton_Click(object sender, EventArgs e) {
            _ps.deleteCurrentTurn();
            updateTurnMap();
            if (_ps.Directions != null && _ps.Directions.Turns != null)
                updateDirections();
        }

        //user clicked the Help->About menu item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            PathfinderAboutBox p = new PathfinderAboutBox();
            p.Show();
        }

        //user clicked the File->Exit menu item
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        //user clicked the Help->View Help menu item
        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://dl.dropbox.com/u/18878030/pathfinder_v1/user_documentation/PathfinderUserManual.pdf");
        }

        //user changed the Options->Map Type menu item
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
            updateTurnMap();
        }

        //user changed the Options->Path Resolution menu item
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
            if (_ps.Path.Waypoints.Count > 0) {
                _ps.reProcessInput();
                prepareToDisplayRoute();
            }
        }

        //user changed the Options->Geocode Points menu item
        private void revGeoToolStripMenuItem_Click(object sender, EventArgs e) {
            points250ToolStripMenuItem.Checked = false;
            points500ToolStripMenuItem.Checked = false;
            points750ToolStripMenuItem.Checked = false;
            points1000ToolStripMenuItem.Checked = false;
            points2000ToolStripMenuItem.Checked = false;
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
                case "2000 Points":
                    points2000ToolStripMenuItem.Checked = true;
                    _ps.Path.MaxGpxPoints = TrackPath.REV_GEO_2000;
                    break;
                default:
                    points250ToolStripMenuItem.Checked = true;
                    _ps.Path.MaxGpxPoints = TrackPath.REV_GEO_250;
                    break;
            }
            if (_ps.Path.Waypoints.Count > 0) {
                _ps.reProcessInput();
                prepareToDisplayRoute();
            }
        }
        #endregion

    }

}
