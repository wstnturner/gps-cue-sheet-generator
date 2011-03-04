using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    abstract class CueSheetWriter {
        protected string _status = "Ok";
        /// <summary>
        /// status string
        /// </summary>
        public string Status {
            get { return _status; }
        }

        public abstract void writeCueSheet(string fileName, string gpxFileName
            , List<Address> locs, List<Turn> turns, string units);
    }
}
