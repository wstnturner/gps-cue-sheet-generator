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
        public override void writeCueSheet(string fileName
            , List<Address> locs, List<Turn> turns, string units) {
            try {
                StringBuilder csvFile = new StringBuilder();
                //case for meters, kilometers, and miles
                csvFile.Append("Start at " + locs[0].AddressString.Replace(",", "") + "\r\n");
                csvFile.Append("Interval " + units + ",Total " + units + ",Turn"
                + ",Degrees,Street,Notes,Latitude,Longitude,Elevation (m)"
                + ",UTM Zone,Easting,Northing\r\n");
                for (int i = 0; i < turns.Count; i++) {
                    csvFile.Append(getDistanceInUnits(turns[i].Distance, units)
                        + "," + getDistanceInUnits(turns[i].Locs[1].GpxLocation.Distance, units)
                        + "," + turns[i].TurnDirection
                        + "," + Math.Round(turns[i].TurnMagnitude)
                        + "," + turns[i].Locs[2].StreetName + "," + turns[i].Notes
                        + "," + turns[i].Locs[1].GpxLocation.Lat
                        + "," + turns[i].Locs[1].GpxLocation.Lon
                        + "," + turns[i].Locs[1].GpxLocation.Elevation
                        + "," + turns[i].Locs[1].GpxLocation.Zone
                        + "," + turns[i].Locs[1].GpxLocation.Easting
                        + "," + turns[i].Locs[1].GpxLocation.Northing + "\r\n");
                }
                csvFile.Append("End at " + locs[locs.Count - 1].AddressString.Replace(",", "")
                    + "\r\ntotal distance: " + getDistanceInUnits(locs[locs.Count - 1]
                    .GpxLocation.Distance, units) + " " + units + "\r\n");
                StreamWriter sr = new StreamWriter(fileName);
                sr.WriteLine(csvFile.ToString());
                sr.Close();
            } catch (Exception e) {
                _status = e.Message;
            }
        }
    }
}
