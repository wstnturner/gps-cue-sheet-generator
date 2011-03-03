using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace CueSheetGenerator {

    static class geoNamesReverseGeocoder {
        const string BASE_URL = "http://ws.geonames.org/findNearestAddress?lat=";

        public static Address getAddress(Location loc, WebInterface web) {
            string fullGeoUrl = BASE_URL + loc.Lat + "&lng=" + loc.Lon;
            return new Address(web.downloadWebPage(fullGeoUrl), loc);
        }
    }

    static class googleReverseGeocoder {
        const string BASE_URL = "http://maps.googleapis.com/maps/api/geocode/xml?latlng=";

        public static Address getAddress(Location loc, WebInterface web) {
            string fullGeoUrl = BASE_URL + loc.Lat + "," + loc.Lon + "&sensor=false";
            return new Address(web.downloadWebPage(fullGeoUrl), loc);
        }
    }

    static class GoogleXml {
        /// <summary>
        /// returned by google server if you exceed 2500 requests per 24 hours
        /// or if you make too frequent of requests, set 20ms deleay between requests
        /// </summary>
        public const string OVER_QUERY_LIMIT = "OVER_QUERY_LIMIT";

        public static string parse(XmlNode node, ref string address, ref string streetName, ref Location geoLoc) {
            foreach (XmlNode n in node) {
                if (n.Name == "result") { node = n; break; } else if (n.InnerText == OVER_QUERY_LIMIT) {
                    return OVER_QUERY_LIMIT;
                }
            }
            //get the address
            foreach (XmlNode n in node.ChildNodes) {
                if (n.Name == "formatted_address") {
                    address = n.FirstChild.InnerText;
                    string s = address;
                    //this may not work for foreign addresses
                    if (Regex.IsMatch(s.Substring(0, s.IndexOf(" ")), "[0-9]")) {
                        int i = s.IndexOf(" ");
                        s = s.Substring(i + 1, s.IndexOf(",") - i - 1);
                    } else
                        s = s.Substring(0, s.IndexOf(","));
                    streetName = s;
                    break;
                }
            }
            ////get the lat lon
            geoLoc = new Location();
            foreach (XmlNode n in node.ChildNodes) {
                if (n.Name == "geometry") { node = n; break; }
            }
            foreach (XmlNode n in node) {
                if (n.Name == "location") { node = n; break; }
            }
            foreach (XmlNode n in node.ChildNodes) {
                if (n.Name == "lat")
                    geoLoc.Lat = double.Parse(n.InnerText);
                if (n.Name == "lng")
                    geoLoc.Lon = double.Parse(n.InnerText);
            }
            return "Ok";
        }
    }


    static class GeonamesXml {
        /// <summary>
        /// returned by geonames.org if their servers are busy, simply try the request
        /// again, geonames is slow to begin with, if this is being returned the lookup
        /// will be slower still
        /// </summary>
        public const string SERVERS_OVERLOADED = "GEONAMES_SERVERS_OVERLOADED";

        public static string parse(XmlNode node, ref string address, ref string streetName, ref Location goeLoc) {
            //get the address
            foreach (XmlNode n in node.ChildNodes) {
                if (n.Name == "address") { node = n; break; } else if (n.Name == "status") {
                    return SERVERS_OVERLOADED;
                }
            }
            //get the lat lon
            goeLoc = new Location();
            foreach (XmlNode n in node.ChildNodes) {
                if (n.Name == "street")
                    streetName = n.InnerText;
                if (n.Name == "streetNumber")
                    address = n.InnerText + " " + streetName;
                if (n.Name == "lat")
                    goeLoc.Lat = double.Parse(n.InnerText);
                if (n.Name == "lng")
                    goeLoc.Lon = double.Parse(n.InnerText);
            }
            return "Ok";
        }

    }
}
