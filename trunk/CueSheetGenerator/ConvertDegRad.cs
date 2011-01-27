using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtmConvert {
	/// <summary>
	///Converts radians to degrees and vice-versa, takes a viriaty of input formats
	/// </summary>
    public static class ConvertDegRad {
		//this class is really overkill for what we are trying to do
		//all we need is a basic radian to degree converter and vice versa 
		//but this is code I am reusing from another project that works with
		//lots of strings, so what the hell.

		public delegate void updateErrorEventHandler(Exception error);
		public static event updateErrorEventHandler updateErrorEvent;

        static string _status = "ok";
        public static string Status {
            get { return ConvertDegRad._status; }
        }
		 
        // for dd mm ss format
        public static double getRadians(string degrees
            , string minutes, string seconds, char direction) {
            _status = "ok";
            try {
				if (direction == 'S' || direction == 'W')
                    return -(Math.PI / 180) * (double.Parse(degrees)
                        + double.Parse(minutes) / 60 + double.Parse(seconds) / 3600);
                return (Math.PI / 180) * (double.Parse(degrees)
                    + double.Parse(minutes) / 60 + double.Parse(seconds) / 3600);
            } catch (Exception e) {
				if (updateErrorEvent != null)
					updateErrorEvent.Invoke(e);
                _status = "not a number: " + e.Message;
                return -1;
            }
        }
        //for dd mm format
        public static double getRadians(string degrees
			, string minutes, char direction) {
            _status = "ok";
            try {
				if (direction == 'S' || direction == 'W')
                    return -(Math.PI / 180) * (double.Parse(degrees)
                        + double.Parse(minutes) / 60);
                return (Math.PI / 180)*(double.Parse(degrees)
                    + double.Parse(minutes) / 60);
            } catch (Exception e) {
				if (updateErrorEvent != null)
					updateErrorEvent.Invoke(e);
                _status = "not a number: " + e.Message;
                return -1;
            }
        }
        //for dd format
		public static double getRadians(string degrees, char direction) {
            _status = "ok";
            try {
				if (direction == 'S' || direction == 'W')
                    return -(Math.PI / 180) * (double.Parse(degrees));
                return (Math.PI / 180)*(double.Parse(degrees));
            } catch (Exception e) {
				if (updateErrorEvent != null)
					updateErrorEvent.Invoke(e);
                _status = "not a number: " + e.Message;
                return -1;
            }
        }

		//general purpose degree to radian conversion
		public static double getRadians(double degrees) {
			return (Math.PI / 180) * degrees;
		}

        public static double getDegrees(double rad) {
            return rad / (Math.PI / 180);
        }

        public static string getDegreesMin(double rad) {
            string dd_ff = (rad / (Math.PI / 180)).ToString();
            string mm_ff = (double.Parse(dd_ff.Substring(dd_ff.IndexOf(".")
                , dd_ff.Length - dd_ff.IndexOf("."))) * 60).ToString();
            dd_ff = dd_ff.Substring(0, dd_ff.IndexOf("."));
            return dd_ff + " " + mm_ff;
        }

        public static string getDegreesMinSec(double rad) {
            string dd_ff = (rad / (Math.PI / 180)).ToString();
            string mm_ff = (double.Parse(dd_ff.Substring(dd_ff.IndexOf(".")
                , dd_ff.Length - dd_ff.IndexOf("."))) * 60).ToString();
            string ss_ff = (double.Parse(mm_ff.Substring(mm_ff.IndexOf(".")
                , mm_ff.Length - mm_ff.IndexOf("."))) * 60).ToString();
            dd_ff = dd_ff.Substring(0, dd_ff.IndexOf("."));
            mm_ff = mm_ff.Substring(0, mm_ff.IndexOf("."));
            return dd_ff + " " + mm_ff + " " + ss_ff;
        }

    }
}
