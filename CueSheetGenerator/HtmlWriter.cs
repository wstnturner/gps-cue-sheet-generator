using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
    class HtmlWriter : CueSheetWriter {
        /// <summary>
        /// write the comma seperated value list of turns out to a file
        /// </summary>
        public override void writeCueSheet(string fileName, string gpxFileName
            , List<Address> locs, List<Turn> turns, string units) {
            try {
                string shortFileName = null;
                string shortGpxFimeName = null;
                if (fileName.Contains("\\")) {
                    shortFileName = fileName.Remove(0, fileName.LastIndexOf("\\") + 1);
                    shortGpxFimeName = gpxFileName.Remove(0, gpxFileName.LastIndexOf("\\") + 1);
                } else if (fileName.Contains("/")) {
                    shortFileName = fileName.Remove(0, fileName.LastIndexOf("/") + 1);
                    shortGpxFimeName = gpxFileName.Remove(0, gpxFileName.LastIndexOf("/"));
                }
                StreamWriter sr = new StreamWriter(fileName);
                sr.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">");
                sr.WriteLine("<html><head><meta content=\"text/html; charset=ISO-8859-1\" http-equiv=\"content-type\">");
                sr.WriteLine("<title>Cue Sheet for: " + shortGpxFimeName + "</title></head><body>");
                sr.WriteLine("<table style=\"text-align: left; width: 100%;\" cellpadding=\"2\" cellspacing=\"2\">");
                sr.WriteLine("<tbody><tr><td style=\"vertical-align: top;\">");
                sr.WriteLine("<tbody><tr><td style=\"vertical-align: top;\">");
                sr.WriteLine("<table style=\"text-align: left; width: 100%;\" border=\"0\" cellpadding=\"2\" cellspacing=\"2\">");
                sr.WriteLine("<tbody><tr><td><b>Directions for: " + shortGpxFimeName + "</b><br>");
                sr.WriteLine("</td></tr><tr><td>Start at " + locs[0].AddressString + "</td></tr><tr><td>");
                sr.WriteLine("<table style=\"text-align: left; width: 100%;\" border=\"1\" cellpadding=\"4\" cellspacing=\"0\"><tbody>");
                sr.WriteLine("<tr><td><b>Turn #</b></td><td><b>Distance</b></td><td><b>Turn</b></td><td><b>Street Name</b></td></tr>");
                for (int i = 0; i < turns.Count; i++) {
                    sr.WriteLine("<tr><td>" + (i + 1) + "</td><td>"
                        + getDistanceInUnits(turns[i].Locs[1].GpxLocation.Distance, units) + "</td><td>"
                        + turns[i].TurnDirection + "</td><td>"
                        + turns[i].Locs[2].StreetName + "</td></tr>");
                }
                sr.WriteLine("</tbody></table></td></tr><tr>");
                sr.WriteLine("<td>End at " + locs[locs.Count - 1].AddressString + "<br>");
                sr.Write("Total distance: " + getDistanceInUnits(locs[locs.Count - 1].GpxLocation.Distance, units) + " ");
                sr.WriteLine(units + "</td></tr></tbody></table></td>");
                sr.WriteLine("<td><img style=\"width: 600px; height: 600px;\" alt=\"ride map\" src=\"" + shortFileName + ".bmp\"></td>");
                sr.WriteLine("</tr></tbody></table></body></html>");
                sr.Close();
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }
}
