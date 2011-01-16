using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CueSheetGenerator {
	partial class TurnViewer : Form {
		List<Turn> _turns = null;
		WebInterface _web = null;
		TrackPath _path = null;
		int _currentTurn = 0;
		string _baseMapUrl = "http://maps.google.com/maps/api/staticmap?size=";
		string _mapSize = "360x280&";
		MainForm _form = null;

		public TurnViewer(List<Turn> turns, WebInterface web, MainForm form) {
			InitializeComponent();
			_turns = turns;
			_web = web;
			_form = form;
			_path = new TrackPath();
			_path.Round = false;
			_path.Weight = 10;
			updateMap();
		}

		void updateMap() {
			_path.Waypoints.Clear();
			if (_currentTurn == _turns.Count) _currentTurn = 0;
			foreach (Location loc in _turns[_currentTurn].Locs)
				_path.Waypoints.Add(loc.GpxWaypoint);
			string fullMapUrl = _baseMapUrl + _mapSize + _path.getPathString() + "&sensor=false";
			mapPictureBox.Image = _form.Strategy.drawWaypoints(_web.downloadImage(fullMapUrl), _path);
			turnNumberTextBox.Text = (_currentTurn + 1).ToString();
			toolStripStatusLabel1.Text =_turns[_currentTurn].Locs[0].StreetName + " to "
				+ _turns[_currentTurn].Locs[2].StreetName + ": " 
				+ _turns[_currentTurn].TurnDirection + " "
				+ Math.Round(_turns[_currentTurn].TurnMagnitude, 1).ToString() + " degrees";
		}

		private void backButton_Click(object sender, EventArgs e) {
			if (_currentTurn == 0) _currentTurn = _turns.Count - 1;
			else _currentTurn--;
			updateMap();
		}

		private void nextButton_Click(object sender, EventArgs e) {
			if (_currentTurn == _turns.Count - 1) _currentTurn = 0;
			else _currentTurn++;
			updateMap();
		}

		private void turnPictureBox_SizeChanged(object sender, EventArgs e) {
			_mapSize = mapPictureBox.Width.ToString() + "x" + mapPictureBox.Height.ToString() + "&";
			updateMap();
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			if (_turns.Count != 0) {
				_turns.RemoveAt(_currentTurn);
				updateMap();
				_form.Strategy.Directions.computeTurnDistances();
				_form.updateDirections();
			}
		}

	}
}
