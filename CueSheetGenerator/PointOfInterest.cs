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
        Location _locationFromMouse = null;
        public Location LocationFromMouse {
            get { return _locationFromMouse; }
        }

        string _name = null;
        public string Name {
            get { return _name; }
            set {  
                _name = value;
                Locs[2].StreetName = value;
            }
        }

        public PointOfInterest(Location loc, Address one, Address two, Address three) : base(one, two, three) {
            _locationFromMouse = loc;
        }
		
	}

}
