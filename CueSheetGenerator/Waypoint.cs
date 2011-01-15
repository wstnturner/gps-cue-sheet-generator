using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
	class Waypoint {
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

		public Waypoint() { }

		public Waypoint(double lat, double lon) {
			_lat = lat;
			_lon = lon;
		}
	}
}
