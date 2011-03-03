using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CueSheetGenerator {
    /// <summary>
    /// class address, contains location information such as
    /// location, address, and helper functions to parse the
    /// returned xml from google or geonames
    /// </summary>
    class Address : Location{

        string _status = "Ok";
        /// <summary>
        /// status string for location class, can return over query limit
        /// or servers overloaded
        /// </summary>
        public string Status {
            get { return _status; }
        }

        Location _gpxLocation = null;
        /// the location parsed in from the GPX file
        public Location GpxLocation {
            get { return _gpxLocation; }
            set { _gpxLocation = value; }
        }

        /// location parsed from the reverse geocoded xml
        /// returned by google or geonames
        Location _geocodedLocation = null;
        public Location GeocodedLocation {
            get { return _geocodedLocation; }
            set { _geocodedLocation = value; }
        }

        string _address = "";
        /// the full street address of the reverse geocoded location
        public string AddressString {
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
        /// constructor called when a location has been reverse geocoded
        /// from a web service (not from the cache)
        /// </summary>
        public Address(string doc, Location gpxLoc) {
            _xml = doc;
            if (_xml.Contains("xml"))
                parseDocument();
            _gpxLocation = gpxLoc;
            Easting = gpxLoc.Easting;
            Northing = gpxLoc.Northing;
        }

        /// <summary>
        /// called by the cache strategy class because there are no locations 
        /// stored in the cache, just keys, street names, and addresses
        /// </summary>
        public Address(string address, string streetName) {
            _address = address;
            _streetName = streetName;
        }

        /// <summary>
        /// constructor called when a deep copy is required
        /// </summary>
        public Address(Address a) {
            _address = a._address;
            _geocodedLocation = a._geocodedLocation;
            _gpxLocation = a._gpxLocation;
            _status = a._status;
            _streetName = a._streetName;
            _xml = a._xml;
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
                        _status = GoogleXml.parse(n, ref _address, ref _streetName, ref _geocodedLocation); break;
                    } else if (n.Name == "geonames") {
                        _status = GeonamesXml.parse(n, ref _address, ref _streetName, ref _geocodedLocation); break;
                    }
                }
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }
}
