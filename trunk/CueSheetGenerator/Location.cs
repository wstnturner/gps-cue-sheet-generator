using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace CueSheetGenerator {
    /// <summary>
    /// class location, contains location information such as
    /// waypoint, address, and helper functions to parse the
    /// returned xml from google or geonames
    /// </summary>
	class Location {
        /// <summary>
        /// returned by google server if you exceed 2500 requests per 24 hours
        /// or if you make too frequent of requests, set 20ms deleay between requests
        /// </summary>
		public const string OVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";
        /// <summary>
        /// returned by geonames.org if their servers are busy, simply try the request
        /// again, geonames is slow to begin with, if this is being returned the lookup
        /// will be slower still
        /// </summary>
		public const string SERVERS_OVERLOADED = "GEONAMES_SERVERS_OVERLOADED";

		string _status = "Ok";
        /// <summary>
        /// status string for location class, can return over query limit
        /// or servers overloaded
        /// </summary>
		public string Status {
			get { return _status; }
		}

		string _notes = "";
        /// stores any user definded notes, may use this field instead of
        /// the point of interest class
		public string Notes {
			get { return _notes; }
			set { _notes = value; }
		}

		Waypoint _gpxWaypoint = null;
        /// the waypoint parsed in from the GPX file
		public Waypoint GpxWaypoint {
			get { return _gpxWaypoint; }
			set { _gpxWaypoint = value; }
		}

		Waypoint _geoWaypoint = null;
        /// waypoint parsed from the reverse geocoded xml
        /// returned by google or geonames
		public Waypoint GeoWaypoint {
			get { return _geoWaypoint; }
			set { _geoWaypoint = value; }
		}

		string _address = "";
        /// the full street address of the reverse geocoded location
		public string Address {
			get { return _address; }
			set { _address = value; }
		}

		string _streetName = "";
        /// the street name for the location
		public string StreetName {
			get { return _streetName; }
			set { _streetName = value; }
		}

		string _xml = "";
        /// xml returned by google or geonames
		public string Xml { 
			get { return _xml; }
		}

        /// <summary>
        /// constructor called when a waypoint has been reverse geocoded
        /// from a web service (not from the cache)
        /// </summary>
		public Location(string doc, Waypoint gpxWpt) {
			_xml = doc;
			if (_xml.Contains("xml"))
				parseDocument();
			_gpxWaypoint = gpxWpt;
		}

        /// <summary>
        /// called by the cache strategy class because there are no waypoints 
        /// stored in the cache, just keys, street names, and addresses
        /// </summary>
		public Location(string address, string streetName) {
			_address = address;
			_streetName = streetName;
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
