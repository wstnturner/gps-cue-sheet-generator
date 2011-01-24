using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
	//waypoint class, contains lat lon, UTM point and key
	class Waypoint : IComparable {

		double _lat = 0;
		public double Lat {
			get { return _lat; }
			set { _lat = value; }
		}

		double _lon = 0;
		public double Lon {
			get { return _lon; }
			set { _lon = value; }
		}

		private double _elevation = 0.0;
		public double Elevation {
			get { return _elevation; }
			set { _elevation = value; }
		}
	
		string _zone = "T10";
		public string Zone {
			get { return _zone; }
			set { _zone = value; }
		}

		double _northing = 0;
		public double Northing {
			get { return _northing; }
			set { _northing = value; }
		}

		double _easting = 0;
		public double Easting {
			get { return _easting; }
			set { _easting = value; }
		}

		double _distance = 0.0;
		public double Distance {
			get { return _distance; }
			set { _distance = value; }
		}

		private long _key = 0;
		public long Key {
			get { return _key; }
		}

		int _index = 0;
		public int Index {
			get { return _index; }
			set { _index = value; }
		}

		public Waypoint() { }

		public Waypoint(double lat, double lon) {
			_lat = lat;
			_lon = lon;
		}

		/*
		//deserialization constructor.
		public Waypoint(SerializationInfo info, StreamingContext ctxt) {
			//get the values from info and assign them to the appropriate properties
			_lat = (double)info.GetValue("lat", typeof(double));
			_lon = (double)info.GetValue("lon", typeof(double));
			_elevation = (double)info.GetValue("elevation", typeof(double));
			_zone = (string)info.GetValue("zone", typeof(string));
			_northing = (double)info.GetValue("northing", typeof(double));
			_easting = (double)info.GetValue("easting", typeof(double));
			_distance = (double)info.GetValue("distance", typeof(double));
			_key = (long)info.GetValue("key", typeof(long));
		}

		//serialization function.
		public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
			info.AddValue("lat", _lat);
			info.AddValue("lon", _lon);
			info.AddValue("elevation", _elevation);
			info.AddValue("zone", _zone);
			info.AddValue("northing", _northing);
			info.AddValue("easting", _easting);
			info.AddValue("distance", _distance);
			info.AddValue("key", _key);
		}
		*/

		//the key which to sort the waypoints by
		public void setKey() {
			_key = long.Parse(((int)(_northing / 10.0)).ToString() 
				+ ((int)(_easting / 10.0)).ToString());
		}

		//implements IComparable 
		public int CompareTo(object obj) {
			Waypoint otherwaypoint = obj as Waypoint;
			if (otherwaypoint != null)
				return this.Key.CompareTo(otherwaypoint.Key);
			else
				throw new ArgumentException("Object is not a Waypoint");
		}

	}
}
