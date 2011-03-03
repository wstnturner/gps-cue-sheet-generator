using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtmConvert;

namespace CueSheetGenerator {
    /// <summary>
    /// track path contains the locations from the GPX file
    /// it also provides functions for generating Google static maps
    /// URLs for drawing paths on maps
    /// see: http://code.google.com/apis/maps/documentation/staticmaps/
    /// </summary>
    class TrackPath {
        int _weight = 5;
        /// <summary>
        /// path weight in pixels
        /// </summary>
        public int Weight {
            get { return _weight; }
            set { _weight = value; }
        }

        string _color = "0x0000ff";
        /// <summary>
        /// path color
        /// </summary>
        public string Color {
            get { return _color; }
            set { _color = value; }
        }

        public const string ROADMAP = "roadmap", SATELLITE = "satellite"
            , TERRAIN = "terrain", HYBRID = "hybrid";
        string _mapType = ROADMAP;
        /// <summary>
        /// map type, roadmap, satellite, terrain, or hybrid
        /// </summary>
        public string MapType {
            get { return _mapType; }
            set { _mapType = value; }
        }

        const string MarkersString1 = "&markers=color:0xff00ee|size:tiny|";
        const string MarkersString2 = "&markers=color:0xff00dd|size:tiny|";

        const int MAX_MAP_POINTS = 90;

        public const int REV_GEO_250 = 250, REV_GEO_500 = 500
            , REV_GEO_750 = 750, REV_GEO_1000 = 1000, REV_GEO_2000 = 2000;
        int _maxGpxPoints = REV_GEO_250;
        /// <summary>
        /// number of locations to reverse geocode
        /// </summary>
        public int MaxGpxPoints {
            get { return _maxGpxPoints; }
            set { _maxGpxPoints = value; }
        }

        bool _round = true;
        /// <summary>
        /// round the lat lon coordinates to 4 decimal places
        /// </summary>
        public bool Round {
            get { return _round; }
            set { _round = value; }
        }

        List<Location> _locations = null;
        /// <summary>
        /// list of locations read in from the gpx file
        /// </summary>
        public List<Location> Locations {
            get { return _locations; }
            set { _locations = value; }
        }

        List<Location> _geocodeLocations = null;
        /// <summary>
        /// list of locations to reverse geocode
        /// </summary>
        public List<Location> GeocodeLocations {
            get { return _geocodeLocations; }
            set { _geocodeLocations = value; }
        }

        List<Location> _pathLocations = null;
        List<Location> _sortedLocations = null;
        List<Location> _spacedOutPoints = null;

        /// <summary>
        /// 1st locating UTM point
        /// </summary>
        public Location UpperLeft {
            get { return _sortedLocations[_sortedLocations.Count - 1]; }
        }


        /// <summary>
        /// 2nd locating UTM point
        /// </summary>
        public Location LowerRight {
            get { return _sortedLocations[0]; }
        }

        ConvertLatLonUtm _utmConvert = null;

        double _totalDistance = 0.0;
        /// <summary>
        /// total distance traveled
        /// </summary>
        public double TotalDistance {
            get { return _totalDistance; }
            set { _totalDistance = value; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TrackPath() {
            _locations = new List<Location>();
            _geocodeLocations = new List<Location>();
            _pathLocations = new List<Location>();
            _sortedLocations = new List<Location>();
            _spacedOutPoints = new List<Location>();
        }

        /// <summary>
        /// reset the ride path, clear out the location lists
        /// </summary>
        public void resetPath() {
            _locations.Clear();
            _geocodeLocations.Clear();
            _pathLocations.Clear();
            _sortedLocations.Clear();
            _totalDistance = 0.0;
        }

        double x1 = 0.0, y1 = 0.0, x2 = 0.0, y2 = 0.0;

        /// <summary>
        /// iterate through the stored locations and convert the lat lon position
        /// utm coordinates, mark their distances, and thin them out by distance
        /// </summary>
        public void processLocations(double distance) {
            _totalDistance = 0.0;
            if (_locations != null && _locations.Count > 0) {
                _utmConvert = new ConvertLatLonUtm();
                double radLat = ConvertDegRad.getRadians(_locations[0].Lat);
                double radLon = ConvertDegRad.getRadians(_locations[0].Lon);
                _utmConvert.convertLatLonToUtm(radLat, radLon);
                _locations[0].Easting = _utmConvert.Easting;
                _locations[0].Northing = _utmConvert.Northing;
                _locations[0].Zone = _utmConvert.Zone;
                _locations[0].Distance = 0.0;
                _locations[0].setKey();
                for (int i = 1; i < _locations.Count; i++) {
                    radLat = ConvertDegRad.getRadians(_locations[i].Lat);
                    radLon = ConvertDegRad.getRadians(_locations[i].Lon);
                    _utmConvert.convertLatLonToUtm(radLat, radLon);
                    _locations[i].Easting = _utmConvert.Easting;
                    _locations[i].Northing = _utmConvert.Northing;
                    x1 = _locations[i - 1].Easting;
                    y1 = _locations[i - 1].Northing;
                    x2 = _locations[i].Easting;
                    y2 = _locations[i].Northing;
                    _locations[i].setKey();
                    _totalDistance += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    _locations[i].Zone = _utmConvert.Zone;
                    _locations[i].Distance = _totalDistance;
                }
                //set the index of each location
                for (int i = 0; i < _locations.Count; i++)
                    _locations[i].Index = i;
                //if locations are within _locationSeperation meters of eachother, remove one of them
                int current = 0;
                _spacedOutPoints.Clear();
                _spacedOutPoints.Add(_locations[current]);
                for (int i = 1; i < _locations.Count; i++) {
                    if (Math.Abs(_locations[current].Distance
                        - _locations[i].Distance) > distance) {
                        _spacedOutPoints.Add(_locations[i]);
                        current = i;
                    }
                }

                //clear out the list of locations to geocode
                _geocodeLocations.Clear();
                //call douglass reduction
                List<Location> temp = DouglasReduction(_locations, 4.0);
                List<int> locationsToKeep = new List<int>();
                locationsToKeep.Add(0);
                int j = 0;
                for (int i = 1; i < temp.Count - 1; i++) {
                    j = findSecondPrevious(temp[i]);
                    if (j >= temp[i - 1].Index)
                        locationsToKeep.Add(j);
                    locationsToKeep.Add(findFirstPrevious(temp[i]));
                    if (j < temp[i - 1].Index)
                        locationsToKeep.Add(temp[i].Index);
                    locationsToKeep.Add(findFirstNext(temp[i]));
                    j = findSecondNext(temp[i]);
                    if (j < temp[i + 1].Index) 
                        locationsToKeep.Add(j);
                    j = findFirstNext(temp[i]);
                    while (i < temp.Count && temp[i].Index < j) i++;
                }
                locationsToKeep.Add(temp[temp.Count - 1].Index);
                int k = 0;
                while (k < locationsToKeep.Count - 1) {
                    if (locationsToKeep[k] >= locationsToKeep[k + 1])
                        locationsToKeep.RemoveAt(k + 1);
                    else 
                        k++;
                }
                    
                for (int i = 0; i < locationsToKeep.Count; i++) {
                    _geocodeLocations.Add(_locations[locationsToKeep[i]]);
                }
            }
        }

        int findFirstPrevious(Location loc) {
            int i = 1;
            while (i < _spacedOutPoints.Count && loc.Index > _spacedOutPoints[i].Index) i++;
            return _spacedOutPoints[i-1].Index;
        }

        int findSecondPrevious(Location loc) {
            int i = 2;
            while (i < _spacedOutPoints.Count && loc.Index > _spacedOutPoints[i].Index) i++;
            return _spacedOutPoints[i - 2].Index;
        }

        int findFirstNext(Location loc) {
            int i = _spacedOutPoints.Count-2;
            while (i >= 0 && loc.Index < _spacedOutPoints[i].Index) i--;
            return _spacedOutPoints[i+1].Index;
        }

        int findSecondNext(Location loc) {
            int i = _spacedOutPoints.Count - 3;
            while (i >= 0 && loc.Index < _spacedOutPoints[i].Index) i--;
            return _spacedOutPoints[i + 2].Index;
        }

        /// <summary>
        /// Uses the Douglas Peucker algorithm to reduce the number of points.
        /// </summary>
        /// <param name="Points">The points.</param>
        /// <param name="epsilon">The epsilon.</param> vv
        /// <returns></returns>
        public static List<Location> DouglasReduction(List<Location> Points, Double epsilon) {
            if (Points == null || Points.Count < 3)
                return Points;
            Int32 firstPoint = 0;
            Int32 lastPoint = Points.Count - 1;
            List<Int32> pointIndexsToKeep = new List<Int32>();
            //Add the first and last index to the keepers
            pointIndexsToKeep.Add(firstPoint);
            pointIndexsToKeep.Add(lastPoint);
            //The first and the last point cannot be the same
            while (Points[firstPoint].Equals(Points[lastPoint])) {
                lastPoint--;
            }

            DouglasReduction(Points, firstPoint, lastPoint, epsilon, pointIndexsToKeep);
            List<Location> returnPoints = new List<Location>();
            pointIndexsToKeep.Sort();
            foreach (Int32 index in pointIndexsToKeep) {
                returnPoints.Add(Points[index]);
            }
            return returnPoints;
        }

        /// <summary>
        /// Douglases the peucker reduction.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="firstPoint">The first point.</param>
        /// <param name="lastPoint">The last point.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <param name="pointIndexsToKeep">The point index to keep.</param>
        private static void DouglasReduction(List<Location> points
            , Int32 firstPoint, Int32 lastPoint, Double epsilon,
            List<Int32> pointIndexsToKeep) {
            Double maxDistance = 0;
            Int32 indexFarthest = 0;
            for (Int32 index = firstPoint; index < lastPoint; index++) {
                Double distance = PerpendicularDistance(points[firstPoint], points[lastPoint], points[index]);
                if (distance > maxDistance) {
                    maxDistance = distance;
                    indexFarthest = index;
                }
            }
            if (maxDistance > epsilon && indexFarthest != 0) {
                //Add the largest point that exceeds the tolerance
                pointIndexsToKeep.Add(indexFarthest);
                DouglasReduction(points, firstPoint, indexFarthest, epsilon, pointIndexsToKeep);
                DouglasReduction(points, indexFarthest, lastPoint, epsilon, pointIndexsToKeep);
            }
        }

        /// <summary>
        /// The distance of a point from a line made from point1 and point2.
        /// </summary>
        /// <param name="pt1">The PT1.</param>
        /// <param name="pt2">The PT2.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        public static Double PerpendicularDistance(Location Point1, Location Point2, Location Point) {
            Double area = Math.Abs(.5 * (Point1.Easting * Point2.Northing + Point2.Easting *
            Point.Northing + Point.Easting * Point1.Northing - Point2.Easting * Point1.Northing - Point.Easting *
            Point2.Northing - Point1.Easting * Point.Northing));
            Double bottom = Math.Sqrt(Math.Pow(Point1.Easting - Point2.Easting, 2) +
            Math.Pow(Point1.Northing - Point2.Northing, 2));
            Double height = area / bottom * 2;
            return height;
        }

        void preProcessPath() {
            //prune the set of locations to reverse geocode based on the number
            //of input locations and the _maxGpxPoints
            double divisor = _geocodeLocations.Count / (double)_maxGpxPoints;
            if (divisor > 1) {
                List<Location> temp = new List<Location>();
                for (int i = 0; i < _maxGpxPoints; i++)
                    temp.Add(_geocodeLocations[(int)((double)i * divisor)]);
                _geocodeLocations = temp;
            }
            if (_sortedLocations.Count <= 4) {
                //sort the location in order to add ballons
                _sortedLocations.Clear();
                foreach (Location w in _locations)
                    _sortedLocations.Add(w);
                _sortedLocations.Sort();
            }
            //use an even smaller number of locations in the set of  
            //path locations to restrict URL size
            int pointsInPath = MAX_MAP_POINTS;
            if (!_round) pointsInPath -= 50;
            divisor = _geocodeLocations.Count / (double)pointsInPath;
            if (divisor > 1) {
                _pathLocations.Clear();
                for (int i = 0; i < pointsInPath; i++)
                    _pathLocations.Add(_geocodeLocations[(int)((double)i * divisor)]);
            } else if (divisor <= 1) {
                _pathLocations.Clear();
                foreach (Location w in _geocodeLocations)
                _pathLocations.Add(w);
            }
        }


        /// <summary>
        /// returns a string that represents the URL of the google static maps path
        /// </summary>
        public string getPathUrlString() {
            //maybe this is the right place for this
            preProcessPath();
            StringBuilder pathString = new StringBuilder();
            pathString.Append("path=" + "color:" + _color + "|weight:" + _weight);
            if (_round) {
                foreach (Location wpt in _pathLocations)
                    pathString.Append("|" + Math.Round(wpt.Lat, 4)
                        + "," + Math.Round(wpt.Lon, 4));
            } else {
                foreach (Location wpt in _pathLocations)
                    pathString.Append("|" + wpt.Lat + "," + wpt.Lon);
            }
            pathString.Append("&maptype=" + _mapType);
            //draw the fiducial markers
            pathString.Append(MarkersString1
                + _sortedLocations[0].Lat + "," + _sortedLocations[0].Lon
                + MarkersString2 + _sortedLocations[_sortedLocations.Count - 1].Lat
                + "," + _sortedLocations[_sortedLocations.Count - 1].Lon);
            return pathString.ToString();
        }

    }
}
