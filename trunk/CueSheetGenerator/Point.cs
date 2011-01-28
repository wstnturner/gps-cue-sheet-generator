using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UtmConvert {
    /// <summary>
    /// class point, double precision point class
    /// </summary>
    public class Point {
        double _x = 0;
		//Summary:
		//X coordinate of the UTM point
		[CategoryAttribute("UTM Point Settings")
		, DescriptionAttribute("X coordinate of the UTM point")]
        public double X {
            get { return _x; }
            set { _x = value; }
        }

        double _y = 0;
		//Summary:
		//Y coordinate of the UTM point
		[CategoryAttribute("UTM Point Settings")
		, DescriptionAttribute("Y coordinate of the UTM point")]
        public double Y {
            get { return _y; }
            set { _y = value; }
        }

        //default constructor
        public Point() { }

        //native constructor
        public Point(double x, double y) {
            _x = x;
            _y = y;
        }

        //constructor overload as int
        public Point(int x, int y) {
            _x = (double)x;
            _y = (double)y;
        }

        //constructor overload as float
        public Point(float x, float y) {
            _x = (double)x;
            _y = (double)y;
        }

        //constructor overload as string
        public Point(string x, string y) {
            double.TryParse(x, out _x);
            double.TryParse(y, out _y);
        }
    }
}
