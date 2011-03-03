using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
    class KmlParser : TrackFileParser {

        public void parse(string fileName, TrackPath path) {
            try {
                StreamReader sr = new StreamReader(fileName);
                string s = sr.ReadToEnd();
                sr.Close();
                s = s.Substring(s.IndexOf("<coordinates>")
                    , s.IndexOf("</coordinates>") - s.IndexOf("<coordinates>"));
                s = s.Replace("-", "");
                s = s.Replace("\n", ",");
                s = s.Replace(" ", ",");
                s = s.Replace("<coordinates>", "");
                List<double> latArray = new List<double>();
                List<double> lonArray = new List<double>();
                string num;
                while (s != ",") {
                    num = s.Substring(s.IndexOf(",") + 1, s.IndexOf(",", s.IndexOf(",") + 1) - 1);
                    lonArray.Add(double.Parse(num));
                    s = s.Remove(0, s.IndexOf(",", s.IndexOf(",") + 1));
                    num = s.Substring(s.IndexOf(",") + 1, s.IndexOf(",", s.IndexOf(",") + 1) - 1);
                    latArray.Add(double.Parse(num));
                    s = s.Remove(0, s.IndexOf(",", s.IndexOf(",") + 1));
                    s = s.Remove(0, s.IndexOf(",", s.IndexOf(",") + 1));
                }
                for (int i = 0; i < lonArray.Count; i++) {
                    Location loc = new Location(latArray[i], lonArray[i]);
                    //loc.Elevation = double.Parse(n.FirstChild.InnerText);
                    path.Locations.Add(loc);
                }
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }
}
