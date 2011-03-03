using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {

    /// <summary>
    /// class csv writer, composes csv files from processed gpx files
    /// </summary>
    class CsvWriter : CueSheetWriter {


        /// <summary>
        /// constructor for CSV writer
        /// </summary>
        public CsvWriter() { }

        /// <summary>
        /// write the comma seperated value list of turns out to a file
        /// </summary>
        public override void writeCueSheet(string fileName, string gpxFileName
            , List<Address> locs, List<Turn> turns, string units) {
            try {
                StreamWriter sr = new StreamWriter(fileName);
                //case for meters, kilometers, and miles
                sr.WriteLine("Start at " + locs[0].AddressString.Replace(",", ""));
                sr.WriteLine("Interval " + units + ",Total " + units + ",Turn"
                    + ",Degrees,Street,Notes,Latitude,Longitude,Elevation (m)"
                    + ",UTM Zone,Easting,Northing");
                for (int i = 0; i < turns.Count; i++) {
                    sr.WriteLine(getDistanceInUnits(turns[i].Distance, units)
                        + "," + getDistanceInUnits(turns[i].Locs[1].GpxLocation.Distance, units)
                        + "," + turns[i].TurnDirection
                        + "," + Math.Round(turns[i].TurnMagnitude)
                        + "," + turns[i].Locs[2].StreetName + "," + turns[i].Notes
                        + "," + turns[i].Locs[1].GpxLocation.Lat
                        + "," + turns[i].Locs[1].GpxLocation.Lon
                        + "," + turns[i].Locs[1].GpxLocation.Elevation
                        + "," + turns[i].Locs[1].GpxLocation.Zone
                        + "," + turns[i].Locs[1].GpxLocation.Easting
                        + "," + turns[i].Locs[1].GpxLocation.Northing);
                }
                sr.WriteLine("End at " + locs[locs.Count - 1].AddressString.Replace(",", "")
                    + "\r\ntotal distance: " + getDistanceInUnits(locs[locs.Count - 1]
                    .GpxLocation.Distance, units) + " " + units);
                sr.Close();
            } catch (Exception e) {
                _status = e.Message;
            }
        }



    }
}
