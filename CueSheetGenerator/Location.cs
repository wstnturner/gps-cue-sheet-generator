using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace CueSheetGenerator {
	class Location {
		string _status = "Ok";
		public const string OVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";
		public const string SERVERS_OVERLOADED = "GEONAMES_SERVERS_OVERLOADED";
		public string Status {
			get { return _status; }
		}

		string _notes = "";
		public string Notes {
			get { return _notes; }
			set { _notes = value; }
		}

		Waypoint _gpxWaypoint = null;

		int _waypointIndex = 0;

		public int WaypointIndex {
			get { return _waypointIndex; }
			set { _waypointIndex = value; }
		}
		internal Waypoint GpxWaypoint {
			get { return _gpxWaypoint; }
			set { _gpxWaypoint = value; }
		}
		Waypoint _geoWaypoint = null;

		internal Waypoint GeoWaypoint {
			get { return _geoWaypoint; }
			set { _geoWaypoint = value; }
		}

		string _streetName = "";
		string _address = "";

		public string Address {
			get { return _address; }
			set { _address = value; }
		}
 
		public string StreetName {
			get { return _streetName; }
			set { _streetName = value; }
		}

		string _xml = "";

		public string Xml { get { return _xml; } }

		public Location(string doc, Waypoint gpxWpt, int index) {
			_xml = doc;
			if (_xml.Contains("xml"))
				parseDocument();
			_gpxWaypoint = gpxWpt;
			_waypointIndex = index;
		}

		void parseDocument() {
			XmlDocument doc = new XmlDocument();
			//deal with web authentication issues 
			//i.e. have internet connection but not logged in. 
			try {
				doc.LoadXml(_xml);
				//decide xml source
				XmlNode node = doc.ChildNodes[0];
				foreach (XmlNode n in doc.ChildNodes) {
					if (n.Name == "GeocodeResponse") {
						parseGoogleDocument(n); break;
					} else if (n.Name == "geonames") {
						parseGeoNamesDocument(n); break;
					}
				}
			} catch (Exception e) {
				_status = e.Message;
			}
		}

		void parseGoogleDocument(XmlNode node) {
			foreach (XmlNode n in node) {
				if (n.Name == "result") { node = n; break; } 
				else if (n.InnerText == OVER_QUERY_LIMIT) {
					_status = OVER_QUERY_LIMIT; return;
				}
			}
			//get the address
			foreach (XmlNode n in node.ChildNodes) {
				if (n.Name == "formatted_address") {
					_address = n.FirstChild.InnerText;
					string s = _address;
					//this may not work for foreign addresses
					if (Regex.IsMatch(s.Substring(0, s.IndexOf(" ")), "[0-9]")) {
						int i = s.IndexOf(" ");
						s = s.Substring(i + 1, s.IndexOf(",") - i - 1);
					} else 
						s = s.Substring(0, s.IndexOf(","));
					_streetName = s;
					break;
				}
			}
			//get the lat lon
			_geoWaypoint = new Waypoint();
			foreach (XmlNode n in node.ChildNodes) {
				if (n.Name == "geometry") { node = n; break; }
			}
			foreach (XmlNode n in node) {
				if (n.Name == "location") { node = n; break; }
			}
			foreach (XmlNode n in node.ChildNodes) {
				if (n.Name == "lat")
					_geoWaypoint.Lat = double.Parse(n.InnerText);
				if (n.Name == "lng")
					_geoWaypoint.Lon = double.Parse(n.InnerText);
			}
		}

		void parseGeoNamesDocument(XmlNode node) {
			//get the address
			foreach (XmlNode n in node.ChildNodes) {
				if (n.Name == "address") { node = n; break; }
				else if (n.Name == "status") {
					_status = SERVERS_OVERLOADED;
					return;
				}
			}
			//get the lat lon
			_geoWaypoint = new Waypoint();
			foreach (XmlNode n in node.ChildNodes) {
				if (n.Name == "street")
					_streetName = n.InnerText;
				if (n.Name == "streetNumber")
					_address = n.InnerText + " " + _streetName;
				if (n.Name == "lat")
					_geoWaypoint.Lat = double.Parse(n.InnerText);
				if (n.Name == "lng")
					_geoWaypoint.Lon = double.Parse(n.InnerText);
			}
		}
	}
}
