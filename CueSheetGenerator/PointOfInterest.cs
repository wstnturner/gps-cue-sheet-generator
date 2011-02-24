using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    /// <summary>
    /// stub class for adding points of interest to the
    /// set of turn directions 
    /// </summary>
	class PointOfInterest : Turn {
        public PointOfInterest(Address one, Address two, Address three) : base(one, two, three) { }
		//Location _loc;
		//string _name;
		//string _description;
	}

}
