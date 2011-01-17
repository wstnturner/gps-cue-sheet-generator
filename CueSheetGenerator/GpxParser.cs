﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CueSheetGenerator {
	class GpxParser {
		XmlDocument _doc;
		public XmlDocument Doc {
			get { return _doc; }
			set { _doc = value; }
		}

		string _status = "Ok";

		public string Status {
			get { return _status; }
			set { _status = value; }
		}

		public GpxParser(string fileName, ref TrackPath path) {
			try {
				StreamReader sr = new StreamReader(fileName);
				string file = sr.ReadToEnd();
				sr.Close();
				path.Waypoints.Clear();
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
						Waypoint wpt = new Waypoint(double.Parse(n.Attributes[0].Value)
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