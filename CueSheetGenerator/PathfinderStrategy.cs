using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CueSheetGenerator {
	class PathfinderStrategy {

		string _statusString = "Ok";

		public const double WAYPOINT_SEPERATION = 10.0;

		public string StatusString { get { return _statusString; } }

		string _directionsString = "";
		
		public const double METERS_PER_MILE = 1609.344;

		WebInterface _web = null;
		internal WebInterface Web { get { return _web; } }
	
		GpxParser _parser = null;
		TrackPath _path = null;

		DirectionsGenerator _directions = null;
		internal DirectionsGenerator Directions { get { return _directions; } }

		Image _image = null;

		string _baseMapUrl = "http://maps.google.com/maps/api/staticmap?size=";
		string _mapSize = "500x500&";
		string _baseGeoUrl = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=";
		List<Location> _locations = null;

		AddressCache _cache = null;

		public PathfinderStrategy() {
			_web = new WebInterface();
			_cache = new AddressCache();

		}

		public Image getMap(int height, int width) {
			_mapSize = width.ToString() + "x" + height.ToString() + "&";
			// Download web image
			if(_path != null && _path.Waypoints.Count > 0)
				_image = _web.downloadImage(_baseMapUrl + _mapSize + _path.getPathString() + "&sensor=false");
			else _image = _web.downloadImage(_baseMapUrl + _mapSize + "&sensor=false");
			return _image;
		}

		public string getDirections(string units) {
			//case for meters, kilometers, and miles
			if (_locations.Count > 0 && _directions.Turns.Count > 0) {
				_directionsString = "1) Start at " + _locations[0].Address + "\r\ngo " 
					+ getDistanceInUnits(_directions.Turns[0].Distance, units)
					+ "\r\nmake a  " + _directions.Turns[0].TurnDirection
					+ " at " + _directions.Turns[0].Locs[2].StreetName + "\r\ntotal: " 
					+ getDistanceInUnits(_directions.Turns[0].Locs[1].GpxWaypoint.Distance, units) 
					+ "\r\n\r\n";

				for (int i = 1; i < _directions.Turns.Count - 1; i++) {
					_directionsString += (i + 1) + ") Turn " + _directions.Turns[i].TurnDirection
						+ " at " + _directions.Turns[i].Locs[2].StreetName + "\r\ngo "
						+ getDistanceInUnits(_directions.Turns[i].Distance, units) + " total: "
						+ getDistanceInUnits(_directions.Turns[i].Locs[1].GpxWaypoint.Distance, units)
						+ "\r\n\r\n";
				}
				int last = _directions.Turns.Count - 1;
				_directionsString += (last + 1) + ") Turn " + _directions.Turns[last].TurnDirection
					+ " at " + _directions.Turns[last].Locs[2].StreetName + "\r\ngo "
					+ getDistanceInUnits(_directions.Turns[last].Distance, units) + ", total: "
					+ getDistanceInUnits(_directions.Turns[last].Locs[1].GpxWaypoint.Distance, units)
					+ "\r\n\r\nEnd at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_directions.TotalDistance, units);
			} else if (_locations.Count > 0)
				_directionsString = "1) Start at " + _locations[0].Address
					+ "\r\n\r\nEnd at " + _locations[_locations.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(_directions.TotalDistance, units);
			else
				if (_directions.TotalDistance > 0)
					_directionsString = "No internet connection.\r\ntotal distance "
						+ getDistanceInUnits(_directions.TotalDistance, units);
				else _directionsString = "No locations, check your input .gpx file";
			return _directionsString;
		}

		private string getDistanceInUnits(double distance, string units) {
			switch (units) {
				case "Meters": return Math.Round(distance, 1).ToString() + " meters";
				case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString() + " kilometers";
				case "Miles": return Math.Round(distance / METERS_PER_MILE, 1).ToString() + " miles";
				default: return null;
			}
		}

		public void processInput(string fileName) {
			_path = new TrackPath();
			//parse the gpx file for waypoints
			_parser = new GpxParser(fileName, ref _path);
			_statusString = _parser.Status;
			_directions = new DirectionsGenerator(_path.Waypoints);
			//if waypoints are within a 5 meters of eachother, remove one of them
			for (int i = 0; i < _path.Waypoints.Count - 1; i++)
				if (Math.Abs(_path.Waypoints[i].Distance
					- _path.Waypoints[i + 1].Distance) < WAYPOINT_SEPERATION) {
					_path.Waypoints.RemoveAt(i+1); i--;
			}
			string fullMapUrl = _baseMapUrl + _mapSize + _path.getPathString() + "&sensor=false";
			//download web image
			_image = _web.downloadImage(fullMapUrl);
			//get the reverse geocoded locations
			_locations = new List<Location>();
			//iterate through the waypoints, look it up in the cache, if it is found, the use it
			//if it is not then ask google or geonames
			
			for (int i = 0; i < _path.Waypoints.Count; i++) {
				if (i % 10 == 0) _exceeded_query_limit = false;
				getLocation(_path.Waypoints[i], i);
			}
			//sanatize input
			for (int i = 0; i < _locations.Count; i++) {
				if (_locations[i].StreetName == "") { _locations.RemoveAt(i); i--; };
			}
			//generate directions
			if (_locations.Count > 0) _directions.generateDirections(_locations);
		}


		bool _exceeded_query_limit = false;
		string _fullGeoUrl = "";
		Location _tempLoc = null;

		void getLocation(Waypoint wpt, int i) {
			//hit the cache first
			//_tempLoc = _cache.lookup(wpt);
			if (_tempLoc != null) _locations.Add(_tempLoc);
			if (_tempLoc == null && !_exceeded_query_limit) {
				_fullGeoUrl = _baseGeoUrl + wpt.Lat + "," + wpt.Lon + "&sensor=false";
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt, i));
				if (_locations[i].Status == Location.OVER_QUERY_LIMIT) {
					_statusString = "Exceeded Google reverse geocoding API request quota";
					_exceeded_query_limit = true;
					getLocation(wpt, i);
				}
			} else {
				//if google cut us off, then try:
				//http://ws.geonames.org/findNearestAddress?
				if (_locations[_locations.Count - 1].Status == Location.OVER_QUERY_LIMIT)
					_locations.RemoveAt(_locations.Count - 1);
				_fullGeoUrl = "http://ws.geonames.org/findNearestAddress?lat=" + wpt.Lat + "&lng=" + wpt.Lon;
				_locations.Add(new Location(_web.downloadWebPage(_fullGeoUrl), wpt, i));
				if (_locations[_locations.Count - 1].Status == Location.SERVERS_OVERLOADED) {
					_locations.RemoveAt(_locations.Count - 1);
					_exceeded_query_limit = false;
					//try again
					getLocation(wpt, i);

				}
			}
			//_cache.addToCache(_locations[_locations.Count - 1]);
		}


	}
}
