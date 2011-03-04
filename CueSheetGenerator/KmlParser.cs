using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
    class KmlParser : TrackFileParser {

        public KmlParser(string fileName, TrackPath path) {
            try {
                StreamReader sr = new StreamReader(fileName);
                string s = sr.ReadToEnd();
                sr.Close();
                s = s.Substring(s.IndexOf("<coordinates>")
                    , s.IndexOf("</coordinates>") - s.IndexOf("<coordinates>"));
				s = s.Replace("<coordinates>", "");
				s = s.Replace("</coordinates>", "");
				if (s[0] == '\n') s = s.Substring(1);  // get rid of leading newline char
				if (s[s.Length - 1] == '\n') s = s.Substring(0, s.Length - 1);	// ditto, trailing newline char
                s = s.Replace(" ", "");

				// after string processing, have one long string like this:
				// -124.058970,43.979150,0.000000\n
				// -123.177780,44.048160,0.000000\n
				// ...
				
				// so split into lines, then split each line by ','
				string[] lines = s.Split('\n');
				string[] coords;
				foreach (string line in lines) {
					coords = line.Split(',');
					Location loc = new Location(double.Parse(coords[1]), double.Parse(coords[0]));
					loc.Elevation = double.Parse(coords[2]);
					path.Locations.Add(loc);
				}
				_status = "Read " + path.Locations.Count + " locations";
            } catch (Exception e) {
                //_status = e.Message;
                _status = "Parsing error, cannot read input file.";
                path.Locations.Clear();
            }
        }
    }
}
