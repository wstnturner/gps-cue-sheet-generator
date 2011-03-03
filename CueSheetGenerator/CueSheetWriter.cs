using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    abstract class CueSheetWriter {
        protected string _status = "Ok";
        /// <summary>
        /// status string
        /// </summary>
        public string Status {
            get { return _status; }
        }

        protected string getDistanceInUnits(double distance, string units) {
            switch (units) {
                case "Meters": return Math.Round(distance, 1).ToString();
                case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString();
                case "Miles": return Math.Round(distance
                    / DirectionsPrinter.METERS_PER_MILE, 1).ToString();
                default: return null;
            }
        }

        public abstract void writeCueSheet(string fileName, string gpxFileName
            , List<Address> locs, List<Turn> turns, string units);
    }
}
