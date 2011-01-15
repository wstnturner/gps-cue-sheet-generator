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
		const int MAX_MAP_POINTS = 90;
		const int MAX_GPX_POINTS = 500;
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

		public TrackPath() {
			_waypoints = new List<Waypoint>();
		}

		void preProcessPath() {
			double divisor = _waypoints.Count / (double)MAX_GPX_POINTS;
			if (divisor > 1) {
				List<Waypoint> temp = new List<Waypoint>();
				for (int i = 0; i < MAX_GPX_POINTS; i++)
					temp.Add(_waypoints[(int)((double)i * divisor)]);
				_waypoints = temp;
			}
			divisor = _waypoints.Count / MAX_MAP_POINTS;
			if (_pathWaypoints == null && divisor > 1) {
				_pathWaypoints = new List<Waypoint>();
				for (int i = 0; i < MAX_MAP_POINTS; i++)
					_pathWaypoints.Add(_waypoints[(int)((double)i * divisor)]);
			} else if (_pathWaypoints == null && divisor < 1) {
				_pathWaypoints = _waypoints;
			}
		}

		public string getPathString() {
			preProcessPath();
			string pathString = "path=";
			pathString += "color:" + _color;
			pathString += "|weight:" + _weight;
			if (_round) {
				foreach (Waypoint wpt in _pathWaypoints) {
					pathString += "|" + Math.Round(wpt.Lat, 4);
					pathString += "," + Math.Round(wpt.Lon, 4);
				}
			} else {
				foreach (Waypoint wpt in _pathWaypoints) {
					pathString += "|" + wpt.Lat;
					pathString += "," + wpt.Lon;
				}
			}
			return pathString;
		}

	}
}
