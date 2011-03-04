using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    static class DirectionsPrinter {


        public const double METERS_PER_MILE = 1609.344;

        /// <summary>
        /// gets a string that represents the list of directions
        /// currently formatted using google maps directions as a model
        /// </summary>
        public static string getDirections(string units, List<Address> locations, List<Turn> turns, TrackPath path) {
            //case for meters, kilometers, and miles
            if (locations == null) return "";
            StringBuilder _directionsString = null;
            if (locations.Count > 0 && turns != null && turns.Count > 0) {
                _directionsString = new StringBuilder(("1) Start at " + locations[0].AddressString
                    + "\r\ngo " + getDistanceInUnits(turns[0].Distance, units)
                    + "\r\nmake a  " + turns[0].TurnDirection
                    + " at " + turns[0].Locs[2].StreetName + "\r\ntotal: "
                    + getDistanceInUnits(turns[0].Locs[1].GpxLocation.Distance, units)
                    + "\r\n\r\n"));
                for (int i = 1; i < turns.Count - 1; i++)
                    _directionsString.Append((i + 1) + ") Turn " + turns[i].TurnDirection
                        + " at " + turns[i].Locs[2].StreetName + "\r\ngo "
                        + getDistanceInUnits(turns[i].Distance, units) + " total: "
                        + getDistanceInUnits(turns[i].Locs[1].GpxLocation.Distance, units)
                        + "\r\n\r\n");
                int last = turns.Count - 1;
                if (turns.Count > 1)
                    _directionsString.Append((last + 1) + ") Turn " + turns[last].TurnDirection
                        + " at " + turns[last].Locs[2].StreetName + "\r\ngo "
                        + getDistanceInUnits(turns[last].Distance, units) + ", total: "
                        + getDistanceInUnits(turns[last].Locs[1].GpxLocation.Distance, units)
                        + "\r\n\r\n");
                _directionsString.Append("End at " + locations[locations.Count - 1].AddressString
                    + "\r\ntotal distance: " + getDistanceInUnits(path.TotalDistance, units));
            } else if (locations.Count > 0) {
                _directionsString = new StringBuilder("1) Start at " + locations[0].AddressString
                    + "\r\n\r\nEnd at " + locations[locations.Count - 1].AddressString
                    + "\r\ntotal distance: " + getDistanceInUnits(path.TotalDistance, units));
            } else {
                if (path.TotalDistance > 0)
                    _directionsString = new StringBuilder("No internet connection.\r\ntotal distance "
                        + getDistanceInUnits(path.TotalDistance, units));
                else _directionsString = new StringBuilder("No locations, check your input .gpx file");
            }
            string temp = _directionsString.ToString().Replace("Turn null", "Go straight");
            return temp;
        }


        public static string getDistanceInUnits(double distance, string units) {
            switch (units) {
                case "Meters": return Math.Round(distance, 1).ToString() + " m";
                case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString() + " km";
                case "Miles": return Math.Round(distance / METERS_PER_MILE, 1).ToString() + " mi";
                default: return null;
            }
        }

        public static string getDistanceGivenUnits(double distance, string units) {
            switch (units) {
                case "Meters": return Math.Round(distance, 1).ToString();
                case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString();
                case "Miles": return Math.Round(distance
                    / DirectionsPrinter.METERS_PER_MILE, 1).ToString();
                default: return null;
            }
        }

    }
}
