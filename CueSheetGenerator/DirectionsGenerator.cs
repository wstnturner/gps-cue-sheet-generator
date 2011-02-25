using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtmConvert;

namespace CueSheetGenerator {
    /// <summary>
    /// generates a set of turn directions.
    /// </summary>
    static class DirectionsGenerator {

        /// <summary>
        /// constructor for directions generator
        /// </summary>

        /// <summary>
        ///computes the average of a list of waypoints
        /// </summary>
        public static Location averageWaypoints(List<Location> list) {
            double lat = 0.0, lon = 0.0, east = 0.0
                , north = 0.0, dist = 0.0, ele = 0.0;
            int index = 0;
            foreach (Location wpt in list) {
                lat += wpt.Lat;
                lon += wpt.Lon;
                east += wpt.Easting;
                north += wpt.Northing;
                dist += wpt.Distance;
                ele += wpt.Elevation;
                index += wpt.Index;
            }
            Location temp = new Location();
            temp.Lat = lat / (double)(list.Count);
            temp.Lon = lon / (double)(list.Count);
            temp.Easting = east / (double)(list.Count);
            temp.Northing = north / (double)(list.Count);
            temp.Distance = dist / (double)(list.Count);
            temp.Elevation = ele / (double)(list.Count);
            temp.Index = (int)(Math.Round((double)index / (double)(list.Count)));
            temp.Zone = list[0].Zone;
            return temp;
        }

        /// <summary>
        /// generates turn directions given a list of locations
        /// </summary>
        public static List<Turn> generateDirections(List<Address> locs) {
            List<Location> tempWpts = new List<Location>();
            List<Turn> turns = new List<Turn>();
            string prevStreet = locs[0].StreetName;
            for (int i = 1; i < locs.Count; i++) {
                //reject false street changes from intersections
                if (i < locs.Count - 2 && locs[i - 1].StreetName != locs[i].StreetName
                    && (locs[i - 1].StreetName == locs[i + 1].StreetName
                    || locs[i - 1].StreetName == locs[i + 2].StreetName)) {
                    locs[i].StreetName = locs[i - 1].StreetName;
                    locs[i + 1].StreetName = locs[i].StreetName;
                }
                if (prevStreet != locs[i].StreetName) {
                    if (i < locs.Count - 1) {
                        if (i > 1 && locs[i - 1].StreetName == locs[i - 2].StreetName) {
                            tempWpts.Clear();
                            tempWpts.Add(locs[i - 1].GpxLocation);
                            tempWpts.Add(locs[i].GpxLocation);
                            //average the two middle points
                            locs[i].GpxLocation = averageWaypoints(tempWpts);
                            turns.Add(new Turn(locs[i - 2], locs[i], locs[i + 1]));
                        } else turns.Add(new Turn(locs[i - 1], locs[i], locs[i + 1]));
                        prevStreet = locs[i].StreetName;
                    } else {
                        //if the very last street name differs from the previous one
                        turns.Add(new Turn(locs[i - 2], locs[i - 1], locs[i]));
                    }
                }
            }
            //each turn consists of three points, one: the street we are on
            //two: the intersection (more or less), three: the street we are turning on to
            //the turn direction (left or right) is computed as follows: the line between
            //one and two is converted from rectangular to polar as is the line between 
            //two and three. The two angles are examined and a turn direction is determined
            //compute turn directions and distances between turns 
            computeTurnDistances(turns);
            for (int i = 0; i < turns.Count; i++) computeTurn(turns[i]);
            return turns;
        }

        /// <summary>
        /// computes the distance between each turn
        /// </summary>
        public static void computeTurnDistances(List<Turn> turns) {
            if (turns.Count > 0)
                turns[0].Distance = turns[0].Locs[1].GpxLocation.Distance;
            for (int i = 1; i < turns.Count; i++)
                turns[i].Distance = turns[i].Locs[1].GpxLocation.Distance
                    - turns[i - 1].Locs[1].GpxLocation.Distance;
        }

        //compute present heading, future heading, and theta

        public static void computeTurn(Turn turn) {
            double theta1 = 0.0, theta2 = 0.0;
            double x1 = 0.0, y1 = 0.0, x2 = 0.0, y2 = 0.0;
            x1 = turn.Locs[0].GpxLocation.Easting;
            x2 = turn.Locs[1].GpxLocation.Easting;
            y1 = turn.Locs[0].GpxLocation.Northing;
            y2 = turn.Locs[1].GpxLocation.Northing;
            theta1 = calculateTheta(x2 - x1, y2 - y1);
            x1 = turn.Locs[1].GpxLocation.Easting;
            x2 = turn.Locs[2].GpxLocation.Easting;
            y1 = turn.Locs[1].GpxLocation.Northing;
            y2 = turn.Locs[2].GpxLocation.Northing;
            theta2 = calculateTheta(x2 - x1, y2 - y1);
            turn.TurnMagnitude = Math.Abs(theta2 - theta1);
            if (turn.TurnMagnitude > 5.0) {
                if (turn.TurnMagnitude > 180.0)
                    turn.TurnMagnitude = 360.0 - turn.TurnMagnitude;
                if (theta2 - theta1 < 0.0 && Math.Abs(theta2 - theta1) >= 180.0)
                    turn.TurnDirection = "left";
                else if (theta2 - theta1 > 0.0 && Math.Abs(theta2 - theta1) >= 180.0)
                    turn.TurnDirection = "right";
                else if (theta2 - theta1 < 0.0 && Math.Abs(theta2 - theta1) < 180.0)
                    turn.TurnDirection = "right";
                else if (theta2 - theta1 > 0.0 && Math.Abs(theta2 - theta1) < 180.0)
                    turn.TurnDirection = "left";
            } else turn.TurnDirection = "null";
        }

        //calculates the angles of the pre and post turn line segments
        public static double calculateTheta(double xDelta, double yDelta) {
            double theta = 0.0;
            if (xDelta == 0.0 && yDelta > 0.0) theta = 90.0;
            else if (xDelta == 0.0 && yDelta < 0.0) theta = 270.0;
            else {
                theta = ConvertDegRad.getDegrees(Math.Atan(yDelta / xDelta));
                if (xDelta < 0.0 && yDelta > 0.0) theta = 180.0 + theta;
                else if (xDelta < 0.0 && yDelta < 0.0) theta = 180.0 + theta;
                else if (xDelta > 0.0 && yDelta < 0.0) theta = 360.0 + theta;
            }
            return theta;
        }
    }
}
