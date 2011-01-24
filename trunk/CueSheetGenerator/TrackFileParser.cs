using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CueSheetGenerator {
	abstract class TrackFileParser {
		protected XmlDocument _doc;
		public XmlDocument Doc {
			get { return _doc; }
			set { _doc = value; }
		}

		protected string _status = "Ok";

		public string Status {
			get { return _status; }
			set { _status = value; }
		}
	}
}
