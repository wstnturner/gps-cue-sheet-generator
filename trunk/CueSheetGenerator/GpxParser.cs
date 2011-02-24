using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace CueSheetGenerator {
    /// <summary>
    /// parses the input .gpx file, get lat, lon, and elevation
    /// see: http://en.wikipedia.org/wiki/GPS_eXchange_Format
    /// </summary>
    class GpxParser : TrackFileParser {
        public GpxParser(string fileName, TrackPath path) {
            try {
                StreamReader sr = new StreamReader(fileName);
                string file = sr.ReadToEnd();
                sr.Close();
                _doc = new XmlDocument();
                _doc.LoadXml(file);
                XmlNode node = _doc.ChildNodes[0];
                foreach (XmlNode n in _doc.ChildNodes) {
                    if (n.Name == "gpx") { node = n; break; }
                }
                foreach (XmlNode n in node) {
                    if (n.Name == "trk") { node = n; break; }
                }
                foreach (XmlNode n in node) {
                    if (n.Name == "trkseg") { node = n; break; }
                }
                foreach (XmlNode n in node.ChildNodes) {
                    if (n.Name == "trkpt") {
                        Location wpt = new Location(double.Parse(n.Attributes[0].Value)
                        , double.Parse(n.Attributes[1].Value));
                        wpt.Elevation = double.Parse(n.FirstChild.InnerText);
                        path.Waypoints.Add(wpt);
                    }
                }
                _status = "Read " + path.Waypoints.Count + " waypoints";
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }

    /// <summary>
    /// parses the input .gpx file, get lat, lon, and elevation
    /// see: http://en.wikipedia.org/wiki/GPS_eXchange_Format
    /// </summary>
    class KmlParser : TrackFileParser {
        public KmlParser(string fileName, TrackPath path) {
            try {
                StreamReader sr = new StreamReader(fileName);
                string file = sr.ReadToEnd();
                sr.Close();
                _doc = new XmlDocument();
                _doc.LoadXml(file);
                XmlNode node = _doc.ChildNodes[0];
                foreach (XmlNode n in _doc.ChildNodes) {
                    if (n.Name == "gpx") { node = n; break; }
                }
                foreach (XmlNode n in node) {
                    if (n.Name == "trk") { node = n; break; }
                }
                foreach (XmlNode n in node) {
                    if (n.Name == "trkseg") { node = n; break; }
                }
                foreach (XmlNode n in node.ChildNodes) {
                    if (n.Name == "trkpt") {
                        Location wpt = new Location(double.Parse(n.Attributes[0].Value)
                        , double.Parse(n.Attributes[1].Value));
                        wpt.Elevation = double.Parse(n.FirstChild.InnerText);
                        path.Waypoints.Add(wpt);
                    }
                }
                _status = "Read " + path.Waypoints.Count + " waypoints";
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }
}
