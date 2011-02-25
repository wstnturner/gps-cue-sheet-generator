using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    static class POIGenerator {

        public static PointOfInterest createPointOfInterest(Location loc, List<Address> addresses) {
            Address ca = findNearestAddress(loc, addresses);
            //more options exist, for doing this
            PointOfInterest p = new PointOfInterest(loc, ca, ca, ca);
            p.TurnDirection = "POI";
            p.Locs[2].StreetName = "description of POI";
            return p;
        }

        static Address findNearestAddress(Location loc, List<Address> addresses) {
            double minDistance = double.PositiveInfinity;
            Address closestAddress = null;
            double currentDistance = 0.0;
            double x1 = 0.0, y1 = 0.0;
            double x2 = loc.Easting, y2 = loc.Northing;
            foreach(Address a in addresses) {
                x1 = a.GpxLocation.Easting;
                y1 = a.GpxLocation.Northing;
                currentDistance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                if(currentDistance < minDistance) {
                    minDistance = currentDistance;
                    closestAddress = a;
                }
            }
            return new Address(closestAddress);
        }

        public static int addPOIToTurnList(PointOfInterest poi, List<Turn> turns) {
            int index = 0;
            while (index < turns.Count && 
                poi.Locs[1].GpxLocation.Distance 
                > turns[index].Locs[1].GpxLocation.Distance) 
                index++;
            turns.Insert(index, (Turn)poi);
            return index;
        }

    }
}
