using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace CueSheetGenerator {
	/// <summary>
	/// strategy class for the pathfinder application, aggregates and composes
	/// all helper classes, and exposes functionality to a graphical user interface
	/// </summary>
	class PathfinderStrategy {

		/// <summary>
		/// Event handler for providing UI with status updates
		/// </summary>
		public delegate void updateStatusEventHandler();
		public event updateStatusEventHandler finishedProcessing;
		public event updateStatusEventHandler processedWaypoint;

		string _status = "Ok";
        /// <summary>
        /// internal status of the pathfinder strategy class
        /// </summary>
		public string Status {
			get { return _status; }
		}

        /// <summary>
        /// path resolution constants
        /// </summary>
		public const double TEN_M = 10.0, FIFTEEN_M = 15.0
			, TWENTY_M = 20.0, THIRTY_M = 30.0;
		

		double _waypointSeperation = THIRTY_M;
        /// <summary>
        /// path resolution waypoint seperation
        /// </summary>
		public double WaypointSeperation {
			get { return _waypointSeperation; }
			set { _waypointSeperation = value; }
		}

		StringBuilder _directionsString = null;
		
		public const double METERS_PER_MILE = 1609.344;
		
		WebInterface _web = null;
        /// <summary>
        /// web interface class instance
        /// </summary>
        public WebInterface Web { 
			get { return _web; } 
		}

		TrackPath _path = null;
        /// <summary>
        /// track path instance, contians waypoints and 
        /// related methods for composing a path URL for 
        /// Google static maps
        /// </summary>
		public TrackPath Path {
			get { return _path; }
		}

		List<Location> _locations = null;
        /// <summary>
        /// instance of location class, contains addresses and waypoints 
        /// for each address
        /// </summary>
		public List<Location> Locations {
			get { return _locations; }
		}
		
		DirectionsGenerator _directions = null;
        /// <summary>
        /// instance of directions generator class, contains a list of turns
        /// and methods for generating a list of turns from locations
        /// </summary>
		public DirectionsGenerator Directions {
			get { return _directions; } 
		}

        CacheStrategy _cache = null;
        /// <summary>
        /// instance of cache strategy, contains a cache list, where each
        /// cache contains a red-black tree, and has a name cooresponding 
        /// to the UTM zone of the points it contians
        /// </summary>
        public CacheStrategy Cache {
            get { return _cache; }
        }

		//should not be modified from outside this class
		private int _currentTurn = 0;
        /// <summary>
        /// the current turn the user is viewing
        /// </summary>
		public int CurrentTurn {
			get {
                if (_directions.Turns == null ||_currentTurn >= _directions.Turns.Count)
                    _currentTurn = 0;
                return _currentTurn; }
		}

		Image _image = null;
		string _baseMapUrl = "http://maps.google.com/maps/api/staticmap?size=";
		string _mapSize = "500x500&";
		string _baseGeoUrl = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=";
		FiducialStrategy _fidStrategy = null;
		
		Waypoint _waypointFromMouse = null;
        /// <summary>
        /// the waypoint that cooresponds to the location of the 
        /// mouse pointer on the ride map
        /// </summary>
		public Waypoint WaypointFromMouse {
			get { return _waypointFromMouse; }
		}

        /// <summary>
        /// constructor for pathfinder strategy
        /// </summary>
		public PathfinderStrategy() {
			_path = new TrackPath();
			_web = new WebInterface();
			_cache = new CacheStrategy();
			_fidStrategy = new FiducialStrategy();
			_directionsString = new StringBuilder();
		}

        /// <summary>
        /// destructor, writes caches back out to the filesystem
        /// when the application is closed
        /// </summary>
		~PathfinderStrategy() {
			_cache.writeCachesToFile();
		}

		/// <summary>
		/// returns the image of the ride map
		/// </summary>
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

		/// <summary>
		/// returns the map image at index _currentTurn
		/// </summary>
		public Image getTurnMap(int height, int width) {
			Image image = null;
			TrackPath turnPath = new TrackPath();
			turnPath.Round = false;
			turnPath.Weight = 10;
			turnPath.MapType = _path.MapType;
			string mapSize = width.ToString() + "x" + height.ToString() + "&";
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count > _currentTurn) {
				for(int i = _directions.Turns[_currentTurn].Locs[0].GpxWaypoint.Index;
					i <= _directions.Turns[_currentTurn].Locs[2].GpxWaypoint.Index; i++)
					turnPath.Waypoints.Add(_path.Waypoints[i]);
			}
            turnPath.GeocodeWaypoints = turnPath.Waypoints;
			// download web image
			if (turnPath != null && turnPath.Waypoints.Count > 0) {
				image = _web.downloadImage(_baseMapUrl + mapSize
					+ turnPath.getPathString() + "&sensor=false");
				image = drawWaypoints(image, turnPath);
			}
			return image;
		}

		/// <summary>
		/// increment the current turn index
		/// </summary>
		public void incrementTurn() {
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count - 1 > _currentTurn)
				_currentTurn++;
			else _currentTurn = 0;
		}

		/// <summary>
		/// decriment the current turn index
		/// </summary>
		public void decrementTurn() {
			if (_directions != null && _directions.Turns != null 
				&& 0 < _currentTurn)
				_currentTurn--;
			else if (_directions != null && _directions.Turns != null
				&& _directions.Turns.Count > 0)
				_currentTurn = _directions.Turns.Count - 1;
			else _currentTurn = 0;
		}

		/// <summary>
		/// removes the turn at index _currentTurn from the turns array
		/// </summary>
		public void deleteCurrentTurn() {
			if (_directions != null && _directions.Turns != null 
				&& _directions.Turns.Count != 0) {
				_directions.Turns.RemoveAt(_currentTurn);
				_directions.computeTurnDistances();
				if (_currentTurn > _directions.Turns.Count - 1) 
					_currentTurn = 0;
			}
		}

		/// <summary>
		/// get turn string at the index of _currentTurn
		/// </summary>
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

		private Image drawWaypoints(Image image, TrackPath path) {
			_fidStrategy.processImage((Bitmap)image);
			//if the fiducial strategy class has located the balloons
			//register the image to the UTM locations (UL, LR)
			if (_fidStrategy.MapLocated) {
				_fidStrategy.setCorrespondence(path.UpperLeft, path.LowerRight);
				image = new Bitmap(new Bitmap(image));
				Graphics g = Graphics.FromImage(image);
				SolidBrush brush = new SolidBrush(Color.Green);
				Point pt;
				for (int i = 0; i < path.GeocodeWaypoints.Count; i++) {
					pt = _fidStrategy.getPoint(path.GeocodeWaypoints[i]);
					g.FillEllipse(brush, pt.X - 2, pt.Y - 2, 4, 4);
				}
			}
			return image;
		}

		/// <summary>
		/// if the map has been located i.e. registered, then 
		/// we can return a UTM point given a mouse location on the image
		/// </summary>
		public void getWaypointFromMousePosition(Point pt) {
            if (_fidStrategy.MapLocated && _path.Waypoints.Count > 0)
                _waypointFromMouse = _fidStrategy.getWaypoint(pt);
            else _waypointFromMouse = null;
		}

		/// <summary>
		/// stub for adding points of interest to the map
		/// </summary>
		public void addPointOfInterest(Point p) {}

		/// <summary>
		/// gets a string that represents the list of directions
		/// currently formatted using google maps directions as a model
		/// </summary>
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
					+ "\r\ntotal distance: " + getDistanceInUnits(_path.TotalDistance, units));
			} else if (_locations.Count > 0) {
				_directionsString = new StringBuilder("1) Start at " + _locations[0].Address
					+ "\r\n\r\nEnd at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_path.TotalDistance, units));
			} else {
				if (_path.TotalDistance > 0)
					_directionsString = new StringBuilder("No internet connection.\r\ntotal distance "
						+ getDistanceInUnits(_path.TotalDistance, units));
				else _directionsString = new StringBuilder("No locations, check your input .gpx file");
			}
			string temp = _directionsString.ToString().Replace("Turn null", "Go straight");
			return temp;
		}

		private string getDistanceInUnits(double distance, string units) {
			switch (units) {
				case "Meters": return Math.Round(distance, 1).ToString() + " meters";
				case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString() + " kilometers";
				case "Miles": return Math.Round(distance / METERS_PER_MILE, 1).ToString() + " miles";
				default: return null;
			}
		}

		/// <summary>
		/// write the list of directions out to the filesystem as a comma seperated value file
		/// </summary>
		public void writeCsvFile(string fileName, string units) {
			CsvWriter cw = new CsvWriter();
			if (_locations != null && _locations.Count > 0 && _directions.Turns != null)
				cw.writeCsvFile(fileName, _locations, _directions.Turns, units);
			_image.Save(fileName + ".bmp");
			_status = cw.Status;
		}

		/// <summary>
		/// given an input gpx file, process the file and conver the 
		/// gps waypoints to locations using the google and geonames
		/// reverse geocoding services, then generate a set of directions
		/// </summary>
		public void processInput(string fileName) {
			_currentTurn = 0;
			//parse the gpx file for waypoints
			TrackFileParser parser;
			if (fileName.EndsWith(".gpx")) {
				parser = new GpxParser(fileName, _path);
				_status = parser.Status;
			}
            processWaypoints();
		}

        public void reProcessInput() {
            _path.GeocodeWaypoints.Clear();
            processWaypoints();
        }

        private void processWaypoints() {
            //convert the lat lon coordinates to utm 
            //and prine the path
            _path.processWaypoints(_waypointSeperation);
            //get the reverse geocoded locations
            _locations = new List<Location>();
            //iterate through the waypoints, look it up in the cache, if it is  
            //found, the use it. if it is not then ask google or geonames
            _directions = new DirectionsGenerator();
            Thread t = new Thread(getLocations);
            t.Start();
        }

		bool _exceeded_query_limit = false;
		string _fullGeoUrl = "";
		Location _tempLoc = null;

		//this runs in its own thread, looks up the locations in the 
		//path waypoint list, invokes registered methods when done
		private void getLocations() {
			for (int i = 0; i < _path.GeocodeWaypoints.Count; i++) {
				if (i % 10 == 0) _exceeded_query_limit = false;
                getLocation(_path.GeocodeWaypoints[i], i);
				if(!_cache.CacheHit)
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

		//retrieves the location from either the cache (not implemented),
		//google geocoding API, or geonames.org
		private void getLocation(Waypoint wpt, int i) {
			//hit the cache first
			_tempLoc = _cache.lookup(wpt);
			if (_cache.CacheHit) {
				_locations.Add(_tempLoc);
			} else if (!_cache.CacheHit && !_exceeded_query_limit) {
				_fullGeoUrl = _baseGeoUrl + wpt.Lat + "," + wpt.Lon + "&sensor=false";
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt));
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
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt));
				if (_locations[_locations.Count - 1].Status == Location.SERVERS_OVERLOADED) {
					_locations.RemoveAt(_locations.Count - 1);
					_exceeded_query_limit = false;
					//try again
					getLocation(wpt, i);
				}
			}
			if (!_cache.CacheHit)
				_cache.addToCache(_locations[_locations.Count - 1]);
		}


	}
}
