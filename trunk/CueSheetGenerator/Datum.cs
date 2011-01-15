using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtmConvert {
	public class Datum {
		string _name = "";

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		// meters (a)
		double _equatorialRadius = 0;

		public double EquatorialRadius {
			get { return _equatorialRadius; }
			set { _equatorialRadius = value; }
		}

		public double a {
			get { return _equatorialRadius; }
		}

		// meters (b)
		double _polarRadius = 0;

		public double PolarRadius {
			get { return _polarRadius; }
			set { _polarRadius = value; }
		}

		public double b {
			get { return _polarRadius; }
		}

		// (a-b)/a
		double _flattening = 0;

		public double Flattening {
			get { return _flattening; }
			set { _flattening = value; }
		}

		string _use = "";

		public string Use {
			get { return _use; }
			set { _use = value; }
		}

		public Datum() {
			_name = "NAD83/WGS84";
			_equatorialRadius = 6378137.0;
			_polarRadius = 6356752.3142;
			_flattening = (_equatorialRadius - _polarRadius) / _equatorialRadius;
			_use = "Global";
		}

		public Datum(string name, double equatorialRadius
			, double polarRadius, double flattening, string use) {
			_name = name;
			_equatorialRadius = equatorialRadius;
			_polarRadius = polarRadius;
			_flattening = flattening;
			_use = use;
		}
	}
}
