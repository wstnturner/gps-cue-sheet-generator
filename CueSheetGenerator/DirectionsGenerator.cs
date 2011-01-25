using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtmConvert;

namespace CueSheetGenerator {
	class DirectionsGenerator {
		List<Location> _locs = null;
		List<Turn> _turns = null;
		public List<Turn> Turns {
			get { return _turns; }
			set { _turns = value; }
		}
		ConvertLatLonUtm _utmConvert = null;
		double _totalDistance = 0.0;

		public double TotalDistance {
			get { return _totalDistance; }
			set { _totalDistance = value; }
		}


		List<Waypoint> _waypoints = null;

		//initailize the waypoints with UTM data
		//this seems like a reasonable place to put this but it seams
		//to have caused some pain, it may be more appropriate to put this
		//in the PathfinderStrategy class
		public DirectionsGenerator(List<Waypoint> waypoints) {
			_waypoints = waypoints;
			_utmConvert = new ConvertLatLonUtm();
			double radLat = ConvertDegRad.getRadians(_waypoints[0].Lat);
			double radLon = ConvertDegRad.getRadians(_waypoints[0].Lon);
			_utmConvert.convertLatLonToUtm(radLat, radLon);
			_waypoints[0].Easting = _utmConvert.Easting;
			_waypoints[0].Northing = _utmConvert.Northing;
			_waypoints[0].Zone = _utmConvert.Zone;
			_waypoints[0].Distance = 0.0;
			_waypoints[0].setKey();
			for (int i = 1; i < _waypoints.Count; i++) {
				radLat = ConvertDegRad.getRadians(_waypoints[i].Lat);
				radLon = ConvertDegRad.getRadians(_waypoints[i].Lon);
				_utmConvert.convertLatLonToUtm(radLat, radLon);
				_waypoints[i].Easting = _utmConvert.Easting;
				_waypoints[i].Northing = _utmConvert.Northing;
				x1 = _waypoints[i - 1].Easting;
				y1 = _waypoints[i - 1].Northing;
				x2 = _waypoints[i].Easting;
				y2 = _waypoints[i].Northing;
				_waypoints[i].setKey();
				_totalDistance += Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
				_waypoints[i].Zone = _utmConvert.Zone;
				_waypoints[i].Distance = _totalDistance;
			}
		}

		//computes the average of a list of waypoints
		public Waypoint averageWaypoints(List<Waypoint> list) {
			double lat = 0.0, lon = 0.0, east = 0.0
				, north = 0.0, dist = 0.0, ele = 0.0;
			int index = 0;
			foreach (Waypoint wpt in list) {
				lat += wpt.Lat;
				lon += wpt.Lon;
				east += wpt.Easting;
				north += wpt.Northing;
				dist += wpt.Distance;
				ele += wpt.Elevation;
				index += wpt.Index;
			}
			Waypoint temp = new Waypoint();
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

		//generates turn directions given a list of locations
		public void generateDirections(List<Location> locations) {
			List<Waypoint> tempWpts = new List<Waypoint>();
			_locs = locations;
			_turns = new List<Turn>();
			string prevStreet = _locs[0].StreetName;
			for (int i = 1; i < _locs.Count; i++ ) {
				//reject false street changes from intersections
				if (i < _locs.Count - 2 && _locs[i - 1].StreetName != _locs[i].StreetName
					&& (_locs[i - 1].StreetName == _locs[i + 1].StreetName
					|| _locs[i - 1].StreetName == _locs[i + 2].StreetName)) {
					_locs[i].StreetName = _locs[i - 1].StreetName;
					_locs[i + 1].StreetName = _locs[i].StreetName;
				}
				if (prevStreet != _locs[i].StreetName) {
					if (i < _locs.Count - 1) {
						if (i > 1 && _locs[i - 1].StreetName == _locs[i - 2].StreetName) {
							tempWpts.Clear();
							tempWpts.Add(_locs[i - 1].GpxWaypoint);
							tempWpts.Add(_locs[i].GpxWaypoint);
							//average the two middle points
							_locs[i].GpxWaypoint = averageWaypoints(tempWpts);
							_turns.Add(new Turn(_locs[i - 2], _locs[i], _locs[i + 1]));
						} else _turns.Add(new Turn(_locs[i - 1], _locs[i], _locs[i + 1]));
						prevStreet = _locs[i].StreetName;
					} else {
						//if the very last street name differs from the previous one
						_turns.Add(new Turn(_locs[i - 2], _locs[i - 1], _locs[i]));
					}
				}
			}
			//each turn consists of three points, one: the street we are on
			//two: the intersection (more or less), three: the street we are turning on to
			//the turn direction (left or right) is computed as follows: the line between
			//one and two is converted from rectangular to polar as is the line between 
			//two and three. The two angles are examined and a turn direction is determined
			//compute turn directions and distances between turns 
			computeTurnDistances();
			for (int i = 0; i < _turns.Count; i++) computeTurn(i);
		}

		//computes the distance between each turn
		public void computeTurnDistances() {
			if (_turns.Count > 0)
				_turns[0].Distance = _turns[0].Locs[1].GpxWaypoint.Distance;
			for (int i = 1; i < _turns.Count; i++)
				_turns[i].Distance = _turns[i].Locs[1].GpxWaypoint.Distance
					- _turns[i - 1].Locs[1].GpxWaypoint.Distance;
		}

		//compute present heading, future heading, and theta
		double theta1 = 0.0, theta2 = 0.0;
		double x1 = 0.0, y1 = 0.0, x2 = 0.0, y2 = 0.0;
		void computeTurn(int i) {
			x1 = _turns[i].Locs[0].GpxWaypoint.Easting;
			x2 = _turns[i].Locs[1].GpxWaypoint.Easting;
			y1 = _turns[i].Locs[0].GpxWaypoint.Northing;
			y2 = _turns[i].Locs[1].GpxWaypoint.Northing;
			theta1 = calculateTheta(x2 - x1, y2 - y1);
			x1 = _turns[i].Locs[1].GpxWaypoint.Easting;
			x2 = _turns[i].Locs[2].GpxWaypoint.Easting;
			y1 = _turns[i].Locs[1].GpxWaypoint.Northing;
			y2 = _turns[i].Locs[2].GpxWaypoint.Northing;
			theta2 = calculateTheta(x2 - x1, y2 - y1);
			_turns[i].TurnMagnitude = Math.Abs(theta2 - theta1);
			if (_turns[i].TurnMagnitude > 5.0) {
				if (_turns[i].TurnMagnitude > 180.0)
					_turns[i].TurnMagnitude = 360.0 - _turns[i].TurnMagnitude;
				if (theta2 - theta1 < 0.0 && Math.Abs(theta2 - theta1) >= 180.0)
					_turns[i].TurnDirection = "left";
				else if (theta2 - theta1 > 0.0 && Math.Abs(theta2 - theta1) >= 180.0)
					_turns[i].TurnDirection = "right";
				else if (theta2 - theta1 < 0.0 && Math.Abs(theta2 - theta1) < 180.0)
					_turns[i].TurnDirection = "right";
				else if (theta2 - theta1 > 0.0 && Math.Abs(theta2 - theta1) < 180.0)
					_turns[i].TurnDirection = "left";
			} else _turns[i].TurnDirection = "null";
		}

		//calculates the angles of the pre and post turn line segments
		double calculateTheta(double xDelta, double yDelta) {
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

	//helper class Turn
	class Turn {
		Location[] _locs;
		double _distance = 0.0;
		string _turnDirection = "null";
		public string TurnDirection {
			get { return _turnDirection; }
			set { _turnDirection = value; }
		}
		double _turnMagnitude = 0.0;
		public double TurnMagnitude {
			get { return _turnMagnitude; }
			set { _turnMagnitude = value; }
		}
		public double Distance {
			get { return _distance; }
			set { _distance = value; }
		}
		public Location[] Locs {
			get { return _locs; }
			set { _locs = value; }
		}

		public Turn(Location one,
			Location two,
			Location three) {
			_locs = new Location[3];
			_locs[0] = one;
			_locs[1] = two;
			_locs[2] = three;
		}
	}
}
