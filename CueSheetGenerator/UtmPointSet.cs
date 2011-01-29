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
        /// <summary>
        ///	the utm point list
        /// </summary>
        public List<UtmPoint> Points {
            get { return _points; }
        }

        string _locationName = "";
        /// <summary>
        ///	area of operation, e.g. Seattle Center etc.
        /// </summary>
        public string LocationName {
            set { _locationName = value; }
            get { return _locationName; }
        }

        string _datum = "NAD83/WGS84";
        /// <summary>
        ///	the datum used for this location
        /// </summary>
        public string Datum {
            get { return _datum; }
            set { _datum = value; }
        }

        /// <summary>
        ///	default constructor
        /// </summary>
        public UtmPointSet() {
            _points = new List<UtmPoint>();
        }

        /// <summary>
        ///	constructor, given location name, a set of points, and a datum
        /// </summary>
        public UtmPointSet(string locationName
            , List<UtmPoint> points, string datum) {
            _locationName = locationName;
            _points = new List<UtmPoint>(points);
            _datum = datum;
        }

    }
}
