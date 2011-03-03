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
        event PathfinderStrategy.updateStatusEventHandler processedLocation;
        event PathfinderStrategy.updateStatusEventHandler enableControlls;

        //private instance of the pathfinder strategy class
        PathfinderStrategy _ps = null;

        /// <summary>
        /// string constants for menu option case structures
        /// </summary>
        public const string METERS = "Meters", KM = "Kilometers", MILES = "Miles";
        string _units = MILES;
        string _currentFileName = null;

        /// <summary>
        /// constructor for the main form
        /// </summary>
        public MainForm(string fileName) {
            InitializeComponent();
            cueSheetListView.SmallImageList = new ImageList();
            //use reflection to add the images in the assembly to the image list
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            string[] resources = thisExe.GetManifestResourceNames();
            System.IO.Stream file = thisExe.GetManifestResourceStream("CueSheetGenerator.left.ico");
            cueSheetListView.SmallImageList.Images.Add(new Bitmap(Image.FromStream(file)));
            file = thisExe.GetManifestResourceStream("CueSheetGenerator.right.ico");
            cueSheetListView.SmallImageList.Images.Add(new Bitmap(Image.FromStream(file)));
            file = thisExe.GetManifestResourceStream("CueSheetGenerator.straight.ico");
            cueSheetListView.SmallImageList.Images.Add(new Bitmap(Image.FromStream(file)));
            file = thisExe.GetManifestResourceStream("CueSheetGenerator.poi.ico");
            cueSheetListView.SmallImageList.Images.Add(new Bitmap(Image.FromStream(file)));


            cueSheetListView.Columns.Add("Turn #", 48);
            cueSheetListView.Columns.Add("Distance", 62);
            cueSheetListView.Columns.Add("Turn", 48);
            cueSheetListView.Columns.Add("Street Name", -2);

            //may need to add a method here to make the columns visible on mac

            _ps = new PathfinderStrategy();
            _ps.finishedProcessing += updateDirections;
            finishedProcessing += updateDirections;
            _ps.finishedProcessing += reEnableControls;
            enableControlls += reEnableControls;
            _ps.processedLocation += updateProgressBar;
            processedLocation += updateProgressBar;
            this.Show();
            if (fileName != null) openGpsFile(fileName);
            else updateRideMap(true);
        }

        /// update the main ride map
        void updateRideMap(bool downloadNew) {
            // show image in picturebox
            mapPictureBox.Image = _ps.getRideMap(downloadNew, mapPictureBox.Height, mapPictureBox.Width);
            if (mapPictureBox.Image == null)
                toolStripStatusLabel1.Text = _ps.Web.Status;
        }

        /// update the turn ins[ector turn map
        void updateTurnMap() {
            /*hightlight current direction text*/
            turnPictureBox.Image = _ps.getTurnMap(turnPictureBox.Height, turnPictureBox.Width);
            if (turnPictureBox.Image == null) {
                toolStripStatusLabel1.Text = _ps.Web.Status;
            }
        }

        /// every time a location is decoded update the progress bar
        void updateProgressBar() {
            //not ideal, i'd rather use logic to terminate the thread
            //rather than use error handling
            try {
                if (beginTextBox.InvokeRequired)
                    this.Invoke(processedLocation);
                else
                    lookupToolStripProgressBar.Value = _ps.Addresses.Count;
            } catch (Exception e) {
                Thread.CurrentThread.Abort(e);
            }
        }

        /// when the directions have been generated update the directions display
        private void updateDirections() {
            if (beginTextBox.InvokeRequired) {
                this.Invoke(finishedProcessing);
            } else {
                toolStripStatusLabel2.Text = "Done,";
                lookupToolStripProgressBar.Value = 0;
                updateRideMap(false);
                currentTurnStatusLabel.Text = _ps.getCurrentTurnString();
                // update startTextBox
                if (_ps.Addresses.Count > 0) {
                    beginTextBox.Text = "Start at " + _ps.Addresses[0].AddressString;
                    // update the cue sheet ListView
                    updateListView();
                    // update endTextBox
                    endTextBox.Text = "End at " + _ps.Addresses[_ps.Addresses.Count - 1].AddressString
                        + "\r\nTotal distance: "
                        + DirectionsPrinter.getDistanceInUnits(_ps.Path.TotalDistance, _units);
                } else {
                    beginTextBox.Text = "Bad input file, check input file for correct format.";
                }
            }
        }


        // set the contents of the cue sheet ListView control
        private void updateListView() {
            if (_ps.Turns == null) return;
            //clear the list view
            cueSheetListView.Items.Clear();
            //add necessary columns to list view
			// moved to MainForm.Designer.cs!
            for (int i = 0; i < _ps.Turns.Count; i++) {
                Turn t = _ps.Turns[i];
                ListViewItem lvi = null;
                if (t.TurnDirection == "left")
                    lvi = new ListViewItem((i + 1).ToString(), 0);
                else if (t.TurnDirection == "right")
                    lvi = new ListViewItem((i + 1).ToString(), 1);
                else if (t.TurnDirection == "straight") 
                    lvi = new ListViewItem((i + 1).ToString(), 2);
                else lvi = new ListViewItem((i + 1).ToString(), 3);
                lvi.SubItems.Add(DirectionsPrinter.getDistanceInUnits(t.Locs[1].GpxLocation.Distance, _units));
                lvi.SubItems.Add(t.TurnDirection);
                lvi.SubItems.Add(t.Locs[2].StreetName);
                cueSheetListView.Items.Add(lvi);
            }
            // highlight the first turn in the display
            if (_ps.Turns.Count > 0)
                highlightCurrentTurn();
        }

        /// the controlls are disabled during processing, re-enable them afterward
        private void reEnableControls() {
            if (beginTextBox.InvokeRequired) {
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
            _currentFileName = fileName;
            //display the file name in the main window text
            if (_ps.Cache.Windows)
                this.Text = this.Tag + ": " + fileName.Remove(0, fileName.LastIndexOf("\\") + 1);
            else this.Text = this.Tag + ": " + fileName.Remove(0, fileName.LastIndexOf("/") + 1);
            _ps.processInput(fileName);
            prepareToDisplayRoute();
        }

        /// get the UI ready to display the route
        private void prepareToDisplayRoute() {
            if (_ps.Path.Locations.Count > 0) {
                //must call this first, because updating the ride map
                //affects the number of geocode locations
                updateRideMap(true);
                //initialize the progress bar
                lookupToolStripProgressBar.Value = 0;
                lookupToolStripProgressBar.Maximum = _ps.Path.GeocodeLocations.Count;
                toolStripStatusLabel2.Text = "Processing,";
                toolStripStatusLabel1.Text = _ps.Status;
                fileToolStripMenuItem.Enabled = false;
                optionsToolStripMenuItem.Enabled = false;
                deleteButton.Enabled = false;
                backButton.Enabled = false;
                nextButton.Enabled = false;
                turnPictureBox.Image = null;
				cueSheetListView.Items.Clear();
                beginTextBox.Clear();
                endTextBox.Clear();
            }
        }

        ////hightlight current direction text
        private void highlightCurrentTurn() {
			if (cueSheetListView.Items.Count > 0) {
            	cueSheetListView.Items[_ps.CurrentTurn].Selected = true;
            	cueSheetListView.EnsureVisible(_ps.CurrentTurn);
            	cueSheetListView.Focus();
			}
        }

        private void enablePoiControlls(bool enable) {
            //enable or disable the controls
            poiNameLabel.Enabled = enable;
            poiNametextBox.Enabled = enable;
            poiDescriptionLabel.Enabled = enable;
            poiDescriptionTextBox.Enabled = enable;
            addPoiButton.Enabled = enable;
            if (enable) {
                poiNameLabel.BackColor = Color.LimeGreen;
                poiDescriptionLabel.BackColor = Color.LimeGreen;
            } else {
                poiNameLabel.BackColor = SystemColors.Control;
                poiDescriptionLabel.BackColor = SystemColors.Control;
            }
        }

        //all event handlers below here
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

        //user changes the size of the map
        private void mapPictureBox_SizeChanged(object sender, EventArgs e) {
            if (_ps != null) updateRideMap(true);
        }

        //user moves the mouse over the ride map
        private void mapPictureBox_MouseMove(object sender, MouseEventArgs e) {
            if (_ps != null) {
                Point pt = new Point(e.X, e.Y);
                _ps.getLocationFromMousePosition(pt);
                if (_ps.LocationFromMouse != null)
                    toolStripStatusLabel3.Text = _ps.Path.UpperLeft.Zone + " E "
                        + Math.Round(_ps.LocationFromMouse.Easting).ToString()
                        + ", " + "N " + Math.Round(_ps.LocationFromMouse.Northing).ToString()
                        + ",";
            }
        }

        Point currentPoiLocation;

        //user clicks on the ride map, used to add points of interest
        private void mapPictureBox_MouseDoubleClick(object sender, MouseEventArgs e) {
            //use this event to add notes to locations on the map
            //the note will appear in the csv file output by the program
            if (_ps.Turns != null) {
                currentPoiLocation = new Point(e.X, e.Y);
                enablePoiControlls(!addPoiButton.Enabled);
            }
        }

        private void addPoiButton_Click(object sender, EventArgs e) {
            _ps.addPointOfInterest(currentPoiLocation, poiNametextBox.Text, poiDescriptionTextBox.Text);
            poiNametextBox.Text = "";
            poiDescriptionTextBox.Text = "";
            enablePoiControlls(!addPoiButton.Enabled);
            updateListView();
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
            highlightCurrentTurn();
            currentTurnStatusLabel.Text = _ps.getCurrentTurnString();
        }

        //user clicked the back button
        private void backButton_Click(object sender, EventArgs e) {
            _ps.decrementTurn();
            // KURT: todo: could also scroll cuesheet automatically; right now if the list of turns
            // in the cue sheet is longer than one screen (i.e. has a vertical scroll bar) and
            // you click next, eventually the highlighted turn in the cue sheet ListView is not
            // visible. Maybe there's a way to scroll the ListView as needed to keep the highlighted
            // turn visible.
            highlightCurrentTurn();
            currentTurnStatusLabel.Text = _ps.getCurrentTurnString();
        }

        //user clicked a turn in the cue sheet ListView
        private void cueSheetListView_SelectedIndexChanged(object sender, EventArgs e) {
            ListView myListView = (ListView)sender;
            if (myListView.SelectedIndices.Count > 0) {
                int index = myListView.SelectedIndices[0];
                //endTextBox.Text = "selected: " + index;
                _ps.CurrentTurn = index;
                updateTurnMap();
                updateRideMap(false);
                currentTurnStatusLabel.Text = _ps.getCurrentTurnString();
            }
        }
        //user clicked the delete button
        private void deleteButton_Click(object sender, EventArgs e) {
            _ps.deleteCurrentTurn();
            updateTurnMap();
            updateRideMap(false);
            if (_ps.Turns != null)
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
            System.Diagnostics.Process.Start("http://gps-cue-sheet-generator.googlecode.com/files/Pathfinder1UserManual.pdf");
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
            updateRideMap(true);
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
                    _ps.LocationSeperation = PathfinderStrategy.TEN_M;
                    break;
                case "15m":
                    fifteenMToolStripMenuItem.Checked = true;
                    _ps.LocationSeperation = PathfinderStrategy.FIFTEEN_M;
                    break;
                case "20m":
                    twentyMToolStripMenuItem.Checked = true;
                    _ps.LocationSeperation = PathfinderStrategy.TWENTY_M;
                    break;
                case "30m":
                    thirtyMToolStripMenuItem.Checked = true;
                    _ps.LocationSeperation = PathfinderStrategy.THIRTY_M;
                    break;
                default:
                    thirtyMToolStripMenuItem.Checked = true;
                    _ps.LocationSeperation = PathfinderStrategy.THIRTY_M;
                    break;
            }
            if (_ps.Path.Locations.Count > 0) {
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
            if (_ps.Path.Locations.Count > 0) {
                _ps.reProcessInput();
                prepareToDisplayRoute();
            }
        }
        #endregion

        private void cSVFileToolStripMenuItem_Click(object sender, EventArgs e) {
            saveCsvFileDialog.Filter = "CSV File|*.csv";
            saveCsvFileDialog.ShowDialog();
        }

        private void hTMLFileToolStripMenuItem_Click(object sender, EventArgs e) {
            saveCsvFileDialog.Filter = "HTML File|*.html";
            saveCsvFileDialog.ShowDialog();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e) {
            copyToClipboard();
        }

        private void copyToClipboard() {
            StringBuilder buffer = new StringBuilder();
            for (int i = 0; i < cueSheetListView.Columns.Count; i++) {
                buffer.Append(cueSheetListView.Columns[i].Text);
                buffer.Append(",");
            }
            buffer.Append("\n");
            for (int i = 0; i < cueSheetListView.Items.Count; i++) {
                for (int j = 0; j < cueSheetListView.Columns.Count; j++) {
                    buffer.Append(cueSheetListView.Items[i].SubItems[j].Text);
                    buffer.Append(",");
                }
                buffer.Append("\n");
            }
            Clipboard.SetText(buffer.ToString());
        }

    }



}
