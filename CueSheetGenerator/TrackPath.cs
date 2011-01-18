using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
	class TrackPath {
		//path style
		//path weight in pixels
		int _weight = 5;

		public int Weight {
			get { return _weight; }
			set { _weight = value; }
		}
		//path color
		string _color = "0x0000ff";

		public string Color {
			get { return _color; }
			set { _color = value; }
		}

		public const string ROADMAP = "roadmap", SATELLITE = "satellite"
			, TERRAIN = "terrain", HYBRID = "hybrid";
		string _mapType = ROADMAP;
		public string MapType {
			get { return _mapType; }
			set { _mapType = value; }
		}

		const string MarkersString1 = "&markers=color:0xff00ee|size:tiny|";
		const string MarkersString2 = "&markers=color:0xff00dd|size:tiny|";

		const int MAX_MAP_POINTS = 90;
		
		public const int REV_GEO_250 = 250, REV_GEO_500 = 500
			, REV_GEO_750 = 750, REV_GEO_1000 = 1000;
		int _maxGpxPoints = REV_GEO_250;
		public int MaxGpxPoints {
			get { return _maxGpxPoints; }
			set { _maxGpxPoints = value; }
		}


		bool _round = true;

		public bool Round {
			get { return _round; }
			set { _round = value; }
		}

		List<Waypoint> _waypoints = null;
		internal List<Waypoint> Waypoints {
			get { return _waypoints; }
			set { _waypoints = value; }
		}

		List<Waypoint> _pathWaypoints = null;
		List<Waypoint> _sortedWaypoints = null;

		internal Waypoint UpperLeft {
			get { return _sortedWaypoints[_sortedWaypoints.Count - 1]; }
		}

		internal Waypoint LowerRight {
			get { return _sortedWaypoints[0]; }
		}

		public TrackPath() {
			_waypoints = new List<Waypoint>();
			_pathWaypoints = new List<Waypoint>();
			_sortedWaypoints = new List<Waypoint>();
		}

		public void resetPath() {
			_waypoints = new List<Waypoint>();
			_pathWaypoints = new List<Waypoint>();
			_sortedWaypoints = new List<Waypoint>();
		}

		void preProcessPath() {
			//prune the set of waypoints to reverse geocode based on the number
			//of input waypoints and the _maxGpxPoints
			double divisor = _waypoints.Count / (double)_maxGpxPoints;
			if (divisor > 1) {
				List<Waypoint> temp = new List<Waypoint>();
				for (int i = 0; i < _maxGpxPoints; i++)
					temp.Add(_waypoints[(int)((double)i * divisor)]);
				_waypoints = temp;
			}
			if (_sortedWaypoints.Count <= 3) {
				//sort the waypoint in order to add ballons
				_sortedWaypoints.Clear();
				foreach (Waypoint w in _waypoints)
					_sortedWaypoints.Add(w);
				_sortedWaypoints.Sort();
			}
			//use an even smaller number of waypoints in the set of  
			//path waypoints to restrict URL size
			divisor = _waypoints.Count / (double)MAX_MAP_POINTS;
			if (divisor > 1) {
				_pathWaypoints.Clear();
				for (int i = 0; i < MAX_MAP_POINTS; i++)
					_pathWaypoints.Add(_waypoints[(int)((double)i * divisor)]);
			} else if (divisor < 1)
				_pathWaypoints = _waypoints;
		}

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
			return pathString.ToString();
		}

	}
}
