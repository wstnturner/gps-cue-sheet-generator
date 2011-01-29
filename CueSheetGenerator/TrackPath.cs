using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtmConvert;

namespace CueSheetGenerator {
    /// <summary>
    /// track path contains the waypoints from the GPX file
    /// it also provids functions for generating Google static maps
    /// URLs for drawing paths on maps
    /// see: http://code.google.com/apis/maps/documentation/staticmaps/
    /// </summary>
	class TrackPath {
		int _weight = 5;
        /// <summary>
        /// path weight in pixels
        /// </summary>
		public int Weight {
			get { return _weight; }
			set { _weight = value; }
		}

		string _color = "0x0000ff";
        /// <summary>
        /// path color
        /// </summary>
		public string Color {
			get { return _color; }
			set { _color = value; }
		}

		public const string ROADMAP = "roadmap", SATELLITE = "satellite"
			, TERRAIN = "terrain", HYBRID = "hybrid";
		string _mapType = ROADMAP;
        /// <summary>
        /// map type, roadmap, satellite, terrain, or hybrid
        /// </summary>
		public string MapType {
			get { return _mapType; }
			set { _mapType = value; }
		}

		const string MarkersString1 = "&markers=color:0xff00ee|size:tiny|";
		const string MarkersString2 = "&markers=color:0xff00dd|size:tiny|";
        const string StartMarkersString = "&markers=color:0x00ff18|label:S|";
        const string StopMarkersString = "&markers=color:0x00f9ff|label:F|";
		const int MAX_MAP_POINTS = 85;

		public const int REV_GEO_250 = 250, REV_GEO_500 = 500
			, REV_GEO_750 = 750, REV_GEO_1000 = 1000, REV_GEO_2000 = 2000;
		int _maxGpxPoints = REV_GEO_250;
        /// <summary>
        /// number of waypoints to reverse geocode
        /// </summary>
		public int MaxGpxPoints {
			get { return _maxGpxPoints; }
			set { _maxGpxPoints = value; }
		}

		bool _round = true;
        /// <summary>
        /// round the lat lon coordinates to 4 decimal places
        /// </summary>
		public bool Round {
			get { return _round; }
			set { _round = value; }
		}

		List<Waypoint> _waypoints = null;
        /// <summary>
        /// list of waypoints read in from the gpx file
        /// </summary>
		public List<Waypoint> Waypoints {
			get { return _waypoints; }
			set { _waypoints = value; }
		}

        List<Waypoint> _geocodeWaypoints = null;
        /// <summary>
        /// list of waypoints to reverse geocode
        /// </summary>
        public List<Waypoint> GeocodeWaypoints {
            get { return _geocodeWaypoints; }
            set { _geocodeWaypoints = value; }
        }

		List<Waypoint> _pathWaypoints = null;
		List<Waypoint> _sortedWaypoints = null;
		
        /// <summary>
        /// 1st locating UTM point
        /// </summary>
		public Waypoint UpperLeft {
			get { return _sortedWaypoints[_sortedWaypoints.Count - 1]; }
		}

		
        /// <summary>
        /// 2nd locating UTM point
        /// </summary>
		public Waypoint LowerRight {
			get { return _sortedWaypoints[0]; }
		}

		ConvertLatLonUtm _utmConvert = null;

		double _totalDistance = 0.0;
        /// <summary>
        /// total distance traveled
        /// </summary>
		public double TotalDistance {
			get { return _totalDistance; }
			set { _totalDistance = value; }
		}

        /// <summary>
        /// constructor
        /// </summary>
		public TrackPath() {
			_waypoints = new List<Waypoint>();
            _geocodeWaypoints = new List<Waypoint>();
			_pathWaypoints = new List<Waypoint>();
			_sortedWaypoints = new List<Waypoint>();
		}

        /// <summary>
        /// reset the ride path, clear out the waypoint lists
        /// </summary>
		public void resetPath() {
            _waypoints.Clear();
            _geocodeWaypoints.Clear();
			_pathWaypoints.Clear();
            _sortedWaypoints.Clear();
			_totalDistance = 0.0;
		}

		double x1 = 0.0, y1 = 0.0, x2 = 0.0, y2 = 0.0;

        /// <summary>
        /// iterate through the stored waypoints and convert the lat lon position
        /// utm coordinates, mark their distances, and thin them out by distance
        /// </summary>
		public void processWaypoints(double distance) {
            _totalDistance = 0.0;
            _geocodeWaypoints.Clear();
            if (_waypoints != null && _waypoints.Count > 0) {
                _utmConvert = new ConvertLatLonUtm();
                double radLat = ConvertDegRad.getRadians(_waypoints[0].Lat);
                double radLon = ConvertDegRad.getRadians(_waypoints[0].Lon);
                _utmConvert.convertLatLonToUtm(radLat, radLon);
                _waypoints[0].Easting = _utmConvert.Easting;
                _waypoints[0].Northing = _utmConvert.Northing;
                _waypoints[0].Zone = _utmConvert.Zone;
                _waypoints[0].Distance = 0.0;
                _waypoints[0].setKey();
                for (int i = 1; i < _waypoints.Count; i++) {
                    radLat = ConvertDegRad.getRadians(_waypoints[i].Lat);
                    radLon = ConvertDegRad.getRadians(_waypoints[i].Lon);
                    _utmConvert.convertLatLonToUtm(radLat, radLon);
                    _waypoints[i].Easting = _utmConvert.Easting;
                    _waypoints[i].Northing = _utmConvert.Northing;
                    x1 = _waypoints[i - 1].Easting;
                    y1 = _waypoints[i - 1].Northing;
                    x2 = _waypoints[i].Easting;
                    y2 = _waypoints[i].Northing;
                    _waypoints[i].setKey();
                    _totalDistance += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    _waypoints[i].Zone = _utmConvert.Zone;
                    _waypoints[i].Distance = _totalDistance;
                }
                //set the index of each waypoint
                for (int i = 0; i < _waypoints.Count; i++)
                    _waypoints[i].Index = i;
                //if waypoints are within _waypointSeperation meters of eachother, remove one of them
                int current = 0;
                _geocodeWaypoints.Add(_waypoints[current]);
                for (int i = 1; i < _waypoints.Count; i++) {
                    if (Math.Abs(_waypoints[current].Distance
                        - _waypoints[i].Distance) > distance) {
                        _geocodeWaypoints.Add(_waypoints[i]);
                        current = i;
                    }
                }
            }
		}

		void preProcessPath() {
			//prune the set of waypoints to reverse geocode based on the number
			//of input waypoints and the _maxGpxPoints
            double divisor = _geocodeWaypoints.Count / (double)_maxGpxPoints;
			if (divisor > 1) {
				List<Waypoint> temp = new List<Waypoint>();
				for (int i = 0; i < _maxGpxPoints; i++)
                    temp.Add(_geocodeWaypoints[(int)((double)i * divisor)]);
                _geocodeWaypoints = temp;
			}
			if (_sortedWaypoints.Count <= 4) {
				//sort the waypoint in order to add ballons
				_sortedWaypoints.Clear();
				foreach (Waypoint w in _waypoints)
					_sortedWaypoints.Add(w);
				_sortedWaypoints.Sort();
			}
			//use an even smaller number of waypoints in the set of  
			//path waypoints to restrict URL size
            divisor = _geocodeWaypoints.Count / (double)MAX_MAP_POINTS;
			if (divisor > 1) {
				_pathWaypoints.Clear();
				for (int i = 0; i < MAX_MAP_POINTS; i++)
                    _pathWaypoints.Add(_geocodeWaypoints[(int)((double)i * divisor)]);
			} else if (divisor < 1)
                _pathWaypoints = _geocodeWaypoints;
		}

		
        /// <summary>
        /// returns a string that represents the URL of the google static maps path
        /// </summary>
		public string getPathString() {
			preProcessPath();
			StringBuilder pathString = new StringBuilder();
			pathString.Append("path=" + "color:" + _color +"|weight:" + _weight);
			if (_round) {
				foreach (Waypoint wpt in _pathWaypoints)
					pathString.Append("|" + Math.Round(wpt.Lat, 4) 
						+ "," + Math.Round(wpt.Lon, 4));
			} else {
				foreach (Waypoint wpt in _pathWaypoints)
					pathString.Append("|" + wpt.Lat + "," + wpt.Lon);
			}
			pathString.Append("&maptype=" + _mapType + MarkersString1
				+ _sortedWaypoints[0].Lat + "," + _sortedWaypoints[0].Lon
				+ MarkersString2 + _sortedWaypoints[_sortedWaypoints.Count-1].Lat 
				+ "," + _sortedWaypoints[_sortedWaypoints.Count-1].Lon);
            //add the start and end points
            pathString.Append(StartMarkersString
                + _pathWaypoints[0].Lat + "," + _pathWaypoints[0].Lon
                + StopMarkersString + _pathWaypoints[_pathWaypoints.Count - 1].Lat
                + "," + _pathWaypoints[_pathWaypoints.Count - 1].Lon);
			return pathString.ToString();
		}

	}
}
