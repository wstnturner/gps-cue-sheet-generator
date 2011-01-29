using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    /// <summary>
    /// class waypoint, contains lat lon, UTM point and key
    /// </summary>
	class Waypoint : IComparable {

		double _lat = 0;
        /// <summary>
        /// latitude of waypoint
        /// </summary>
		public double Lat {
			get { return _lat; }
			set { _lat = value; }
		}

		double _lon = 0;
        /// <summary>
        /// longitude of waypoint
        /// </summary>
		public double Lon {
			get { return _lon; }
			set { _lon = value; }
		}

		private double _elevation = 0.0;
        /// <summary>
        /// elevation of waypoint
        /// </summary>
		public double Elevation {
			get { return _elevation; }
			set { _elevation = value; }
		}
	
		string _zone = "T10";
        /// <summary>
        /// utm zone of waypoint
        /// </summary>
		public string Zone {
			get { return _zone; }
			set { _zone = value; }
		}

		double _northing = 0;
        /// <summary>
        /// northing of waypoint
        /// </summary>
		public double Northing {
			get { return _northing; }
			set { _northing = value; }
		}

		double _easting = 0;
        /// <summary>
        /// easting of waypoint
        /// </summary>
		public double Easting {
			get { return _easting; }
			set { _easting = value; }
		}

		double _distance = 0.0;
        /// <summary>
        /// distance of waypoint from the start of the path
        /// </summary>
		public double Distance {
			get { return _distance; }
			set { _distance = value; }
		}

		private long _key = 0;
        /// <summary>
        /// key, composed of northing and easting, each divided by 10
        /// implicit fuzzy cacheing because waypoints looked up by this
        /// key will be rounded to the nearest 10 meters
        /// </summary>
		public long Key {
			get { return _key; }
		}

		int _index = 0;
        /// <summary>
        /// index of waypoint in waypoint list, this is used because
        /// not all waypoints get reverse geocoded (the set of locations 
        /// is not onto the set of waypoints) so when drawing turns, the 
        /// all waypoints will be used to construct the path
        /// </summary>
		public int Index {
			get { return _index; }
			set { _index = value; }
		}

        /// <summary>
        /// default constructor
        /// </summary>
		public Waypoint() { }

        /// <summary>
        /// overloaded constructor
        /// </summary>
		public Waypoint(double lat, double lon) {
			_lat = lat;
			_lon = lon;
		}

        /// <summary>
        /// set the key which to sort the waypoints by
        /// </summary>
		public void setKey() {
			_key = long.Parse(((int)(_northing / 10.0)).ToString() 
				+ ((int)(_easting / 10.0)).ToString());
		}

        /// <summary>
        /// implements IComparable
        /// </summary> 
		public int CompareTo(object obj) {
			Waypoint otherwaypoint = obj as Waypoint;
			if (otherwaypoint != null)
				return this.Key.CompareTo(otherwaypoint.Key);
			else
				throw new ArgumentException("Object is not a Waypoint");
		}

	}
}
