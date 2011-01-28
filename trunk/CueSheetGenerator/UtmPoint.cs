using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UtmConvert {
    /// <summary>
    /// class utm point, helper class for the utm convert class
    /// </summary>
    public class UtmPoint {
        Point _point;
        /// <summary>
        /// 2D point in space
        /// </summary>
		[CategoryAttribute("UTM Settings")
		, DescriptionAttribute("2D point in space")]
        public Point Point {
            get { return _point; }
            set { _point = value; }
        }

        string _zone = "T10";
        /// <summary>
        /// UTM zone, T10 for the northwest 
        /// </summary>
		[CategoryAttribute("UTM Settings")
		, DescriptionAttribute("UTM zone, T10 for the northwest")]
        public string Zone {
            get { return _zone; }
            set { _zone = value; }
        }

        string _note = "cone";
        /// <summary>
        /// either "cone" or "dummy"
        /// </summary>
		[CategoryAttribute("Waypoint Settings")
		, DescriptionAttribute("either cone or dummy")]
        public string Note {
            get { return _note; }
            set { _note = value; }
        }

        string _handle = "";
        /// <summary>
        /// just a string handle, unique identifier
        /// </summary>
		[CategoryAttribute("Waypoint Settings")
		, DescriptionAttribute("a string handle, unique identifier")]
        public string Handle {
            get { return _handle; }
            set { _handle = value; }
        }

        /// <summary>
        /// default utm point constructor
        /// </summary>
        public UtmPoint() {
            _point = new Point();
        }

        /// <summary>
        /// constructor
        /// </summary>
        public UtmPoint(Point point, string zone, string note, string handle) {
            _point = new Point();
            _point = point;
            _zone = zone;
            _note = note;
            _handle = handle;
        }
    }
}
