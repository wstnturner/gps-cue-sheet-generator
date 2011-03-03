using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace CueSheetGenerator {
    /// <summary>
    /// strategy class for the pathfinder application, aggregates and composes
    /// all helper classes, and exposes functionality to a graphical user interface
    /// </summary>
    class PathfinderStrategy {

        /// <summary>
        /// Event handler for providing UI with status updates
        /// </summary>
        public delegate void updateStatusEventHandler();
        public event updateStatusEventHandler finishedProcessing;
        public event updateStatusEventHandler processedWaypoint;

        string _status = "Ok";
        /// <summary>
        /// internal status of the pathfinder strategy class
        /// </summary>
        public string Status {
            get { return _status; }
        }

        /// <summary>
        /// path resolution constants
        /// </summary>
        public const double TEN_M = 10.0, FIFTEEN_M = 15.0
            , TWENTY_M = 20.0, THIRTY_M = 30.0;


        double _waypointSeperation = THIRTY_M;
        /// <summary>
        /// path resolution waypoint seperation
        /// </summary>
        public double WaypointSeperation {
            get { return _waypointSeperation; }
            set { _waypointSeperation = value; }
        }

        WebInterface _web = null;
        /// <summary>
        /// web interface class instance
        /// </summary>
        public WebInterface Web {
            get { return _web; }
        }

        TrackPath _path = null;
        /// <summary>
        /// track path instance, contians waypoints and 
        /// related methods for composing a path URL for 
        /// Google static maps
        /// </summary>
        public TrackPath Path {
            get { return _path; }
        }

        /// <summary>
        /// instance of location class, contains addresses and waypoints 
        /// for each address
        /// </summary>
        List<Address> _addresses = null;
        public List<Address> Locations {
            get { return _addresses; }
        }

        CacheStrategy _cache = null;
        /// <summary>
        /// instance of cache strategy, contains a cache list, where each
        /// cache contains a red-black tree, and has a name cooresponding 
        /// to the UTM zone of the points it contians
        /// </summary>
        public CacheStrategy Cache {
            get { return _cache; }
        }

        //should not be modified from outside this class
        private int _currentTurn = 0;
        /// <summary>
        /// the current turn the user is viewing
        /// </summary>
        public int CurrentTurn {
            get {
                if (_turns == null || _currentTurn >= _turns.Count)
                    _currentTurn = 0;
                return _currentTurn;
            }
            set { _currentTurn = value; }
        }

        Image _mapImage = null;
        Image _drawnOnMapImage = null;

        string _baseMapUrl = "http://maps.google.com/maps/api/staticmap?size=";
        string _mapSize = "500x500&";
        string _baseGeoUrl = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=";
        
        FiducialStrategy _rideMapFid = null;
        FiducialStrategy _turnMapFid = null;

        Location _waypointFromMouse = null;
        /// <summary>
        /// the waypoint that cooresponds to the location of the 
        /// mouse pointer on the ride map
        /// </summary>
        public Location WaypointFromMouse {
            get { return _waypointFromMouse; }
        }

        private MapPainter _rideMpaPainter = null;
        private MapPainter _turnMapPainter = null;

        //list of points of interest
        List<PointOfInterest> _pois = null;

        List<Turn> _turns = null;
        public List<Turn> Turns {
            get { return _turns; }
            set { _turns = value; }
        }

        /// <summary>
        /// constructor for pathfinder strategy
        /// </summary>
        public PathfinderStrategy() {
            _path = new TrackPath();
            _web = new WebInterface();
            _cache = new CacheStrategy();
            _rideMapFid = new FiducialStrategy();
            _turnMapFid = new FiducialStrategy();
            _rideMpaPainter = new MapPainter(_rideMapFid);
            _turnMapPainter = new MapPainter(_turnMapFid);
        }

        /// <summary>
        /// destructor, writes caches back out to the filesystem
        /// when the application is closed
        /// </summary>
        ~PathfinderStrategy() {
            _cache.writeCachesToFile();
        }


        /// <summary>
        /// returns the image of the ride map
        /// </summary>
        public Image getRideMap(bool downloadNew, int height, int width) {
            Image mapImage = null;
            _mapSize = width.ToString() + "x" + height.ToString() + "&";
            // download web image
            if (_path != null && _path.Waypoints.Count > 0) {
                if (downloadNew) {
                    _mapImage = _web.downloadImage(_baseMapUrl + _mapSize
                        + _path.getPathUrlString() + "&sensor=false");
                    _mapImage = new Bitmap(_mapImage);
                    _rideMapFid.processImage((Bitmap)_mapImage);
                    if (_rideMapFid.MapLocated)
                        _rideMapFid.setCorrespondence(_path.UpperLeft, _path.LowerRight);
                }
                mapImage = new Bitmap(_mapImage);
                drawnOnRideMap(ref mapImage, _path);
            } else
                mapImage = _web.downloadImage(_baseMapUrl + _mapSize + "&sensor=false");
            _drawnOnMapImage = mapImage;
            return mapImage;
        }

        List<Image> _turnImages = null;

        /// <summary>
        /// returns the map image at index _currentTurn
        /// </summary>
        public Image getTurnMap(int height, int width) {
            Image image = null;
            TrackPath turnPath = new TrackPath();
            turnPath.Round = false;
            turnPath.Weight = 10;
            turnPath.MapType = _path.MapType;
            string mapSize = width.ToString() + "x" + height.ToString() + "&";
            if (_turns != null && _turns.Count > _currentTurn) {
                for (int i = _turns[_currentTurn].Locs[0].GpxLocation.Index;
                    i <= _turns[_currentTurn].Locs[2].GpxLocation.Index; i++)
                    turnPath.Waypoints.Add(_path.Waypoints[i]);
            }
            turnPath.GeocodeWaypoints = turnPath.Waypoints;
            // download web image
            if (turnPath != null && turnPath.Waypoints.Count > 0) {
                //incedentally, getting the path string sorts the waypoints
                string turnPathUrl = turnPath.getPathUrlString();
                if (_turnImages[_currentTurn] == null) {
                    image = _web.downloadImage(_baseMapUrl + mapSize
                        + turnPathUrl + "&sensor=false");
                    image = new Bitmap(image);
                    _turnImages[_currentTurn] = image;
                } else {
                    image = _turnImages[_currentTurn];
                }
                drawOnTurnMap(ref image, turnPath);
            }
            return image;
        }

        /// <summary>
        /// increment the current turn index
        /// </summary>
        public void incrementTurn() {
            if (_turns != null && _turns.Count - 1 > _currentTurn)
                _currentTurn++;
            else _currentTurn = 0;
        }

        /// <summary>
        /// decriment the current turn index
        /// </summary>
        public void decrementTurn() {
            if (_turns != null && 0 < _currentTurn)
                _currentTurn--;
            else if (_turns != null && _turns.Count > 0)
                _currentTurn = _turns.Count - 1;
            else _currentTurn = 0;
        }

        /// <summary>
        /// removes the turn at index _currentTurn from the turns array
        /// </summary>
        public void deleteCurrentTurn() {
            if (_turns != null && _turns.Count != 0) {
                for (int i = 0; i < _pois.Count; i++) {
                    if (_pois[i].Notes == _turns[_currentTurn].Notes)
                        _pois.RemoveAt(i);
                }
                _turns.RemoveAt(_currentTurn);
                _turnImages.RemoveAt(_currentTurn);
                DirectionsGenerator.computeTurnDistances(_turns);
                if (_currentTurn > _turns.Count - 1)
                    _currentTurn = 0;
            }
        }

        /// <summary>
        /// get turn string at the index of _currentTurn
        /// </summary>
        public string getCurrentTurnString() {
            string turnString = "";
            if (_turns != null && _turns.Count != 0) {
                turnString = (_currentTurn + 1).ToString() + ") "
                    + _turns[_currentTurn].Locs[0].StreetName + " to "
                    + _turns[_currentTurn].Locs[2].StreetName + ": "
                    + _turns[_currentTurn].TurnDirection + " "
                    + Math.Round(_turns[_currentTurn].TurnMagnitude, 1).ToString()
                    + " degrees";
            }
            return turnString;
        }

        /// <summary>
        /// if the map has been located i.e. registered, then 
        /// we can return a UTM point given a mouse location on the image
        /// </summary>
        public void getWaypointFromMousePosition(Point pt) {
            if (_rideMapFid.MapLocated && _path.Waypoints.Count > 0)
                _waypointFromMouse = _rideMapFid.getWaypoint(pt);
            else _waypointFromMouse = null;
        }

        /// <summary>
        /// stub for adding points of interest to the map
        /// </summary>
        public void addPointOfInterest(Point p, string name, string description) {
            if (_turns != null) {
                Location loc = _rideMapFid.getWaypoint(p);
                PointOfInterest poi = POIGenerator.createPointOfInterest(loc, _addresses);
                poi.Name = name;
                poi.Notes = description;
                _pois.Add(poi);
                int i = POIGenerator.addPOIToTurnList(poi, _turns);
                _turnImages.Insert(i, null);
                _currentTurn = i;
            }
        }


        /// <summary>
        /// write the list of directions out to the filesystem as a comma seperated value file
        /// </summary>
        public void writeCsvFile(string fileName, string units) {
            CsvWriter cw = new CsvWriter();
            if (_addresses != null && _addresses.Count > 0 && _turns != null)
                cw.writeCueSheet(fileName, _addresses, _turns, units);
            _drawnOnMapImage.Save(fileName + ".bmp");
            _status = cw.Status;
        }

        /// <summary>
        /// given an input gpx file, process the file and conver the 
        /// gps waypoints to locations using the google and geonames
        /// reverse geocoding services, then generate a set of directions
        /// </summary>
        public void processInput(string fileName) {
            _currentTurn = 0;
            _path.resetPath();
            //parse the gpx file for waypoints
            TrackFileParser parser;
            if (fileName.EndsWith(".gpx")) {
                parser = new GpxParser(fileName, _path);
                _status = parser.Status;
            }
            processWaypoints();
        }

        /// <summary>
        /// reprocess the waypoints initially read in from file,
        /// called when the user changes the path resolution
        /// </summary>
        public void reProcessInput() {
            processWaypoints();
        }

        private void processWaypoints() {
            //convert the lat lon coordinates to utm 
            //and prune the path
            _path.processWaypoints(_waypointSeperation);
            //get the reverse geocoded locations
            _addresses = new List<Address>();
            //iterate through the waypoints, look it up in the cache, if it is  
            //found, the use it. if it is not then ask google or geonames
            Thread t = new Thread(getLocations);
            t.Start();
        }

        bool _exceeded_query_limit = false;
        string _fullGeoUrl = "";
        Address _tempLoc = null;

        //this runs in its own thread, looks up the locations in the 
        //path waypoint list, invokes registered methods when done
        private void getLocations() {
            for (int i = 0; i < _path.GeocodeWaypoints.Count; i++) {
                if (i % 10 == 0) _exceeded_query_limit = false;
                getLocation(_path.GeocodeWaypoints[i], i);
                if (!_cache.CacheHit)
                    Thread.Sleep(20);
                if (processedWaypoint != null)
                    processedWaypoint.Invoke();
            }
            //sanatize input
            for (int i = 0; i < _addresses.Count; i++) {
                if (_addresses[i].StreetName == "") { _addresses.RemoveAt(i); i--; };
            }
            //generate directions
            if (_addresses.Count > 0) {
                _pois = new List<PointOfInterest>();
                _turns = DirectionsGenerator.generateDirections(_addresses);
                _turnImages = new List<Image>(_turns.Count);
                for (int i = 0; i < _turns.Count; i++)
                    _turnImages.Add(null);
            }
            if (finishedProcessing != null)
                finishedProcessing.Invoke();
        }

        //retrieves the location from either the cache,
        //google geocoding API, or geonames.org
        private void getLocation(Location wpt, int i) {
            //hit the cache first
            _tempLoc = _cache.lookup(wpt);
            if (_cache.CacheHit) {
                _addresses.Add(_tempLoc);
            } else if (!_cache.CacheHit && !_exceeded_query_limit) {
                _fullGeoUrl = _baseGeoUrl + wpt.Lat + "," + wpt.Lon + "&sensor=false";
                _addresses.Add(new Address(_web.downloadWebPage(_fullGeoUrl), wpt));
                if (_addresses[i].Status == Address.OVER_QUERY_LIMIT) {
                    //_status = "Exceeded Google reverse geocoding API request quota";
                    _exceeded_query_limit = true;
                    getLocation(wpt, i);
                }
            } else {
                //if google cut us off, then try:
                //http://ws.geonames.org/findNearestAddress?
                if (_addresses[_addresses.Count - 1].Status == Address.OVER_QUERY_LIMIT)
                    _addresses.RemoveAt(_addresses.Count - 1);
                _fullGeoUrl = "http://ws.geonames.org/findNearestAddress?lat=" + wpt.Lat + "&lng=" + wpt.Lon;
                _addresses.Add(new Address(_web.downloadWebPage(_fullGeoUrl), wpt));
                if (_addresses[_addresses.Count - 1].Status == Address.SERVERS_OVERLOADED) {
                    _addresses.RemoveAt(_addresses.Count - 1);
                    _exceeded_query_limit = false;
                    //try again
                    getLocation(wpt, i);
                }
            }
            if (!_cache.CacheHit)
                _cache.addToCache(_addresses[_addresses.Count - 1]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        /// <returns>return image of the points drawn on ride map</returns>
        private void drawnOnRideMap(ref Image image, TrackPath path) {
            if (_rideMapFid.MapLocated) {
                _rideMpaPainter.drawWaypoints(ref image, path.GeocodeWaypoints.ToArray());
                if (_turns != null && _turns.Count > 0) {
                    _rideMpaPainter.drawTurn(ref image, _turns[_currentTurn]);
                }
                Location begin = path.GeocodeWaypoints[0];
                Location end = path.GeocodeWaypoints[path.GeocodeWaypoints.Count - 1];
                _rideMpaPainter.drawBeginAndEndPoints(ref image, begin, end);
                if (_pois != null)
                    _rideMpaPainter.drawPointsOfInterest(ref image, _pois.ToArray());
            }
        }

        /// <summary>
        /// Draw waypoints, begin and end points onto the turn inspector
        /// </summary>
        /// <param name="image">image of the turn map</param>
        /// <param name="path">current turn</param>
        /// <returns>image with points drawn on the map</returns>
        private void drawOnTurnMap(ref Image image, TrackPath path) {
            _turnMapFid.processImage((Bitmap)image);
            //if the fiducial strategy class has located the balloons 
            //register the image to the UTM locations (UL, LR) _pd is our instance of PathDrawer 
            if (_turnMapFid.MapLocated) {
                _turnMapFid.setCorrespondence(path.UpperLeft, path.LowerRight);
                _turnMapPainter.drawWaypoints(ref image, path.GeocodeWaypoints.ToArray());
                Location begin = path.GeocodeWaypoints[0];
                Location end = path.GeocodeWaypoints[path.GeocodeWaypoints.Count - 1];
                _turnMapPainter.drawBeginAndEndPoints(ref image, begin, end);
            }
        }
    }

}
