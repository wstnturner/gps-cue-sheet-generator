﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
	class CsvWriter {
		string _status = "Ok";

		public string Status {
			get { return _status; }
		}

		public CsvWriter() {}

		//write the comma seperated value list of directions out to a file
		public void writeCsvFile(string fileName
			, List<Location> locs, List<Turn> turns, string units) {
			try {
				StringBuilder csvFile = new StringBuilder();
				//case for meters, kilometers, and miles
				csvFile.Append("Start at " + locs[0].Address + "\r\n");
				csvFile.Append("Interval " + units + ",Total " + units + ",Turn"
				+ ",Degrees,Street,Notes,Latitude,Longitude,Elevation (m)"
				+ ",UTM Zone,Northing,Easting\r\n");
				string notes = "";
				for (int i = 0; i < turns.Count; i++) {
					notes = turns[i].Locs[0].Notes + "|"
						+ turns[i].Locs[1].Notes + "|" + turns[i].Locs[2].Notes;
					csvFile.Append(getDistanceInUnits(turns[i].Distance, units)
						+ "," + getDistanceInUnits(turns[i].Locs[1].GpxWaypoint.Distance, units)
						+ "," + turns[i].TurnDirection
						+ "," + Math.Round(turns[i].TurnMagnitude)
						+ "," + turns[i].Locs[2].StreetName + "," + notes
						+ "," + turns[i].Locs[1].GpxWaypoint.Lat
						+ "," + turns[i].Locs[1].GpxWaypoint.Lon
						+ "," + turns[i].Locs[1].GpxWaypoint.Elevation
						+ "," + turns[i].Locs[1].GpxWaypoint.Zone
						+ "," + turns[i].Locs[1].GpxWaypoint.Northing
						+ "," + turns[i].Locs[1].GpxWaypoint.Easting + "\r\n");
				}
				csvFile.Append("End at " + locs[locs.Count - 1].Address
					+ "\r\ntotal distance: " + getDistanceInUnits(locs[locs.Count - 1]
					.GpxWaypoint.Distance, units) + units + "\r\n");
				StreamWriter sr = new StreamWriter(fileName);
				sr.WriteLine(csvFile.ToString());
				sr.Close();
			} catch (Exception e) {
				_status = e.Message;
			}
		}

		private string getDistanceInUnits(double distance, string units) {
			switch (units) {
				case "Meters": return Math.Round(distance, 1).ToString();
				case "Kilometers": return Math.Round(distance / 1000.0, 1).ToString();
				case "Miles": return Math.Round(distance 
					/ PathfinderStrategy.METERS_PER_MILE, 1).ToString();
				default: return null;
			}
		}



	}
}
