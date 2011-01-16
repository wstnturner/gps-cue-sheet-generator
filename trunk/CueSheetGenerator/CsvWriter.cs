using System;
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

		public CsvWriter() {
		}

		public void writeCsvFile(string fileName, string fileContents) {
			try {
				StreamWriter sr = new StreamWriter(fileName);
				sr.WriteLine(fileContents);
				sr.Close();
			} catch (Exception e) {
				_status = e.Message;
			}
		}



	}
}
