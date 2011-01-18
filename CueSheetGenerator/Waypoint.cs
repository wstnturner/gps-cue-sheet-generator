using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
	//waypoint class, contains lat lon, UTM point and key
	class Waypoint : IComparable {
		double _lat = 0, _lon = 0;
		string _zone = "T10";

		public string Zone {
			get { return _zone; }
			set { _zone = value; }
		}

		public double Lat {
			get { return _lat; }
			set { _lat = value; }
		}

		public double Lon {
			get { return _lon; }
			set { _lon = value; }
		}

		private double _elevation = 0.0;
		public double Elevation {
			get { return _elevation; }
			set { _elevation = value; }
		}

		double _distance = 0.0;

		public double Distance {
			get { return _distance; }
			set { _distance = value; }
		}

		double _northing = 0, _easting = 0;

		public double Northing {
			get { return _northing; }
			set { _northing = value; }
		}

		public double Easting {
			get { return _easting; }
			set { _easting = value; }
		}

		private string _key = null;

		public long Key {
			get { return long.Parse(_key); }
		}

		//the key which to sort the waypoints by
		public void setKey() {
			_key = ((int)(_northing / 10.0)).ToString() 
				+ ((int)(_easting / 10.0)).ToString();
		}

		//implements IComparable 
		public int CompareTo(object obj) {
			Waypoint otherwaypoint = obj as Waypoint;
			if (otherwaypoint != null)
				return this.Key.CompareTo(otherwaypoint.Key);
			else
				throw new ArgumentException("Object is not a Waypoint");
		}

		public Waypoint() { }

		public Waypoint(double lat, double lon) {
			_lat = lat;
			_lon = lon;
		}
	}
}
