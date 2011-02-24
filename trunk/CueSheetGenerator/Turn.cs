using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    /// <summary>
    /// helper turn class
    /// </summary>
    class Turn : Location {
        /// stores any user definded notes, may use this field instead of
        /// the point of interest class
        string _notes = "";
        public string Notes {
            get { return _notes; }
            set { _notes = value; }
        }

        string _turnDirection = "straight";
        /// <summary>
        /// specifies whether the turn is a right turn or a left turn
        /// </summary>
        public string TurnDirection {
            get { return _turnDirection; }
            set { _turnDirection = value; }
        }

        double _turnMagnitude = 0.0;
        /// <summary>
        /// the magnitude of the turn, 0 - 180 degrees
        /// </summary>
        public double TurnMagnitude {
            get { return _turnMagnitude; }
            set { _turnMagnitude = value; }
        }

        double _distance = 0.0;
        /// <summary>
        /// the distance of the turn, distance from this turn to the next one
        /// </summary>
        public double DistanceFromPrevTurn {
            get { return _distance; }
            set { _distance = value; }
        }

        Address[] _locs;
        /// <summary>
        /// array of the locations in turn (3 locations per turn)
        /// </summary>
        public Address[] Locs {
            get { return _locs; }
            set { _locs = value; }
        }

        /// <summary>
        /// constructor for the turn object
        /// </summary>
        public Turn(Address one,
            Address two,
            Address three) {
            _locs = new Address[3];
            _locs[0] = one;
            _locs[1] = two;
            _locs[2] = three;
        }
    }
}
