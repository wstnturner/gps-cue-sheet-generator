using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtmConvert {
    /// <summary>
    ///	a set of utm points
    /// </summary>
    public class UtmPointSet {
        List<UtmPoint> _points;
        public List<UtmPoint> Points {
            get { return _points; }
        }

        string _locationName = "";
        public string LocationName {
            set { _locationName = value; }
            get { return _locationName; }
        }

        string _datum = "NAD83/WGS84";
        public string Datum {
            get { return _datum; }
            set { _datum = value; }
        }

        public UtmPointSet() {
            _points = new List<UtmPoint>();
        }

        public UtmPointSet(string locationName
            , List<UtmPoint> points, string datum) {
            _locationName = locationName;
            _points = new List<UtmPoint>(points);
            _datum = datum;
        }

    }
}
