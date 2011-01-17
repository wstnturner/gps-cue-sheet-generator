using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace CueSheetGenerator {
	class PathfinderStrategy {
		public delegate void updateStatusEventHandler();
		public event updateStatusEventHandler finishedProcessing;
		public event updateStatusEventHandler processedWaypoint;

		string _status = "Ok";
		public const double TEN_M = 10.0, FIFTEEN_M = 15.0
			, TWENTY_M = 20.0, THIRTY_M = 30.0;
		double _waypointSeperation = THIRTY_M;
		public double WaypointSeperation {
			get { return _waypointSeperation; }
			set { _waypointSeperation = value; }
		}

		public string Status {
			get { return _status; }
		}

		StringBuilder _directionsString = null;
		public const double METERS_PER_MILE = 1609.344;
		WebInterface _web = null;
		internal WebInterface Web { 
			get { return _web; } 
		}

		GpxParser _parser = null;
		TrackPath _path = null;
		internal TrackPath Path {
			get { return _path; }
		}

		List<Location> _locations = null;
		internal List<Location> Locations {
			get { return _locations; }
		}
		
		DirectionsGenerator _directions = null;
		internal DirectionsGenerator Directions {
			get { return _directions; } 
		}

		Image _image = null;
		string _baseMapUrl = "http://maps.google.com/maps/api/staticmap?size=";
		string _mapSize = "500x500&";
		string _baseGeoUrl = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=";
		AddressCache _cache = null;
		FiducialStrategy _fidStrategy = null;
		Waypoint _waypointFromMouse = null;
		internal Waypoint WaypointFromMouse {
			get { return _waypointFromMouse; }
		}

		public PathfinderStrategy() {
			_path = new TrackPath();
			_web = new WebInterface();
			_cache = new AddressCache();
			_fidStrategy = new FiducialStrategy();
			_directionsString = new StringBuilder();
		}

		public Image getRideMap(int height, int width) {
			_mapSize = width.ToString() + "x" + height.ToString() + "&";
			// download web image
			if (_path != null && _path.Waypoints.Count > 0) {
				_image = _web.downloadImage(_baseMapUrl 
					+ _mapSize + _path.getPathString() + "&sensor=false");
				_image = drawWaypoints(_image, _path);
			} else
				_image = _web.downloadImage(_baseMapUrl + _mapSize + "&sensor=false");
			return _image;
		}

		int _currentTurn = 0;
		public Image getTurnMap(int height, int width) {
			Image image = null;
			TrackPath turnPath = new TrackPath();
			turnPath.Round = false;
			turnPath.Weight = 10;
			string mapSize = width.ToString() + "x" + height.ToString() + "&";
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count > _currentTurn) {
				for(int i = _directions.Turns[_currentTurn].Locs[0].WaypointIndex;
					i <= _directions.Turns[_currentTurn].Locs[2].WaypointIndex; i++)
					turnPath.Waypoints.Add(_path.Waypoints[i]);
			}
			// download web image
			if (turnPath != null && turnPath.Waypoints.Count > 0) {
				image = _web.downloadImage(_baseMapUrl + mapSize 
					+ turnPath.getPathString() + "&sensor=false");
				image = drawWaypoints(image, turnPath);
			}
			return image;
		}

		public void incrementTurn() {
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count - 1 > _currentTurn)
				_currentTurn++;
			else _currentTurn = 0;
		}

		public void decrementTurn() {
			if (_directions != null && _directions.Turns != null 
				&& 0 < _currentTurn)
				_currentTurn--;
			else if (_directions != null && _directions.Turns != null)
				_currentTurn = _directions.Turns.Count - 1;
			else _currentTurn = 0;
		}

		public void deleteCurrentTurn() {
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count != 0) {
				_directions.Turns.RemoveAt(_currentTurn);
				if(_currentTurn > 0) _currentTurn--;
			}
		}

		public string getCurrentTurnString() {
			string turnString = "";
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count != 0) {
				turnString = (_currentTurn + 1).ToString() + ") "
					+ _directions.Turns[_currentTurn].Locs[0].StreetName + " to "
					+ _directions.Turns[_currentTurn].Locs[2].StreetName + ": "
					+ _directions.Turns[_currentTurn].TurnDirection + " "
					+ Math.Round(_directions.Turns[_currentTurn].TurnMagnitude, 1).ToString()
					+ " degrees";
			}
			return turnString;
		}

		public Image drawWaypoints(Image image, TrackPath path) {
			_fidStrategy.processImage((Bitmap)image);
			if (_fidStrategy.MapLocated) {
				_fidStrategy.setCorrespondence(path.UpperLeft, path.LowerRight);
				//Image imag = Image.FromStream(new MemoryStream(imageR));
				image = new Bitmap(new Bitmap(image));
				Graphics g = Graphics.FromImage(image);
				SolidBrush brush = new SolidBrush(Color.Green);
				Point pt;
				for (int i = 0; i < path.Waypoints.Count; i++) {
					pt = _fidStrategy.getPoint(path.Waypoints[i]);
					g.FillEllipse(brush, pt.X - 2, pt.Y - 2, 4, 4);
				}
			}
			return image;
		}

		public void getWaypointFromMousePosition(Point pt) {
			if (_fidStrategy.MapLocated)
				_waypointFromMouse = _fidStrategy.getWaypoint(pt);
		}

		public void addPointOfInterest(Point p) {

		}

		public string getDirections(string units) {
			//case for meters, kilometers, and miles
			if (_locations == null) return "";
			if (_locations.Count > 0 && _directions.Turns != null && _directions.Turns.Count > 0) {
				_directionsString = new StringBuilder(("1) Start at " + _locations[0].Address
					+ "\r\ngo " + getDistanceInUnits(_directions.Turns[0].Distance, units)
					+ "\r\nmake a  " + _directions.Turns[0].TurnDirection
					+ " at " + _directions.Turns[0].Locs[2].StreetName + "\r\ntotal: "
					+ getDistanceInUnits(_directions.Turns[0].Locs[1].GpxWaypoint.Distance, units)
					+ "\r\n\r\n"));
				for (int i = 1; i < _directions.Turns.Count - 1; i++)
					_directionsString.Append((i + 1) + ") Turn " + _directions.Turns[i].TurnDirection
						+ " at " + _directions.Turns[i].Locs[2].StreetName + "\r\ngo "
						+ getDistanceInUnits(_directions.Turns[i].Distance, units) + " total: "
						+ getDistanceInUnits(_directions.Turns[i].Locs[1].GpxWaypoint.Distance, units)
						+ "\r\n\r\n");
				int last = _directions.Turns.Count - 1;
				if (_directions.Turns.Count > 1)
					_directionsString.Append((last + 1) + ") Turn " + _directions.Turns[last].TurnDirection
						+ " at " + _directions.Turns[last].Locs[2].StreetName + "\r\ngo "
						+ getDistanceInUnits(_directions.Turns[last].Distance, units) + ", total: "
						+ getDistanceInUnits(_directions.Turns[last].Locs[1].GpxWaypoint.Distance, units) 
						+ "\r\n\r\n");
				_directionsString.Append("End at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_directions.TotalDistance, units));
			} else if (_locations.Count > 0) {
				_directionsString = new StringBuilder("1) Start at " + _locations[0].Address
					+ "\r\n\r\nEnd at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_directions.TotalDistance, units));
			} else {
				if (_directions.TotalDistance > 0)
					_directionsString = new StringBuilder("No internet connection.\r\ntotal distance "
						+ getDistanceInUnits(_directions.TotalDistance, units));
				else _directionsString = new StringBuilder("No locations, check your input .gpx file");
			}
			return _directionsString.ToString();
		}

		private string getDistanceInUnits(double distance, string units) {
			switch (units) {
				case "Meters": return Math.Round(distance, 1).ToString() + " meters";
				case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString() + " kilometers";
				case "Miles": return Math.Round(distance / METERS_PER_MILE, 1).ToString() + " miles";
				default: return null;
			}
		}

		private string getDistanceUnits(double distance, string units) {
			switch (units) {
				case "Meters": return Math.Round(distance, 1).ToString();
				case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString();
				case "Miles": return Math.Round(distance / METERS_PER_MILE, 1).ToString();
				default: return null;
			}
		}

		string generateCsvFile(string units) {
			StringBuilder csvFile = new StringBuilder();
			//case for meters, kilometers, and miles
			if (_locations != null && _locations.Count > 0 && _directions.Turns != null) {
				csvFile.Append("Start at " + _locations[0].Address + "\r\n");
				csvFile.Append("Interval " + units + ",Total " + units + ",Turn"
				+ ",Degrees,Street,Notes,Latitude,Longitude,Elevation (m)"
				+ ",UTM Zone,Northing,Easting\r\n");
				string notes = "";
				for (int i = 0; i < _directions.Turns.Count; i++) {
					notes = _directions.Turns[i].Locs[0].Notes + "|"
						+ _directions.Turns[i].Locs[1].Notes + "|" + _directions.Turns[i].Locs[2].Notes;
					csvFile.Append(getDistanceUnits(_directions.Turns[i].Distance, units)
						+ "," + getDistanceUnits(_directions.Turns[i].Locs[1].GpxWaypoint.Distance, units)
						+ "," + _directions.Turns[i].TurnDirection + "," + _directions.Turns[i].TurnMagnitude
						+ "," + _directions.Turns[i].Locs[2].StreetName + "," + notes
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Lat
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Lon
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Elevation
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Zone
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Northing
						+ "," + _directions.Turns[i].Locs[1].GpxWaypoint.Easting + "\r\n");
				}
				csvFile.Append("End at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_directions.TotalDistance, units));
			}
			return csvFile.ToString();
		}

		public void writeCsvFile(string fileName, string units) {
			CsvWriter cw = new CsvWriter();
			cw.writeCsvFile(fileName, generateCsvFile(units));
			_status = cw.Status;
		}

		public void processInput(string fileName) {
			//parse the gpx file for waypoints
			_parser = new GpxParser(fileName, ref _path);
			_status = _parser.Status;
			_directions = new DirectionsGenerator(_path.Waypoints);
			//if waypoints are within _waypointSeperation meters of eachother, remove one of them
			for (int i = 0; i < _path.Waypoints.Count - 1; i++) {
				if (Math.Abs(_path.Waypoints[i].Distance
					- _path.Waypoints[i + 1].Distance) < _waypointSeperation) {
					_path.Waypoints.RemoveAt(i + 1); 
					i--;
				}
			}
			//get the reverse geocoded locations
			_locations = new List<Location>();
			//iterate through the waypoints, look it up in the cache, if it is  
			//found, the use it. if it is not then ask google or geonames
			Thread t = new Thread(getLocations);
			t.Start();
		}


		bool _exceeded_query_limit = false;
		string _fullGeoUrl = "";
		Location _tempLoc = null;

		void getLocations() {
			for (int i = 0; i < _path.Waypoints.Count; i++) {
				if (i % 10 == 0) _exceeded_query_limit = false;
				getLocation(_path.Waypoints[i], i);
				Thread.Sleep(20);
				if (processedWaypoint != null)
					processedWaypoint.Invoke();
			}
			//sanatize input
			for (int i = 0; i < _locations.Count; i++) {
				if (_locations[i].StreetName == "") { _locations.RemoveAt(i); i--; };
			}
			//generate directions
			if (_locations.Count > 0) _directions.generateDirections(_locations);
			if (finishedProcessing != null)
				finishedProcessing.Invoke();
		}

		void getLocation(Waypoint wpt, int i) {
			//hit the cache first
			//_tempLoc = _cache.lookup(wpt);
			if (_tempLoc != null) _locations.Add(_tempLoc);
			if (_tempLoc == null && !_exceeded_query_limit) {
				_fullGeoUrl = _baseGeoUrl + wpt.Lat + "," + wpt.Lon + "&sensor=false";
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt, i));
				if (_locations[i].Status == Location.OVER_QUERY_LIMIT) {
					//_status = "Exceeded Google reverse geocoding API request quota";
					_exceeded_query_limit = true;
					getLocation(wpt, i);
				}
			} else {
				//if google cut us off, then try:
				//http://ws.geonames.org/findNearestAddress?
				if (_locations[_locations.Count - 1].Status == Location.OVER_QUERY_LIMIT)
					_locations.RemoveAt(_locations.Count - 1);
				_fullGeoUrl = "http://ws.geonames.org/findNearestAddress?lat=" + wpt.Lat + "&lng=" + wpt.Lon;
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt, i));
				if (_locations[_locations.Count - 1].Status == Location.SERVERS_OVERLOADED) {
					_locations.RemoveAt(_locations.Count - 1);
					_exceeded_query_limit = false;
					//try again
					getLocation(wpt, i);

				}
			}
			//_cache.addToCache(_locations[_locations.Count - 1]);
		}


	}
}
