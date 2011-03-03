using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CueSheetGenerator {
    /// <summary>
    /// searches the returned google map for two balloons
    /// of a specific color, the colors are defined in the
    /// track path class as: 0xff00ee and 0xff00dd, purple
    /// </summary>
    class FiducialStrategy {
        const int BALLOON_OFFSET_Y = 12;
        const int BALLOON_OFFSET_X = 2;
        int _height = 0;
        int _width = 0;

        Color _col1;
        Color _col2;

        List<Point> _col1s = null;
        List<Point> _col2s = null;

        int _verticalSum = 0;
        int _horizontalSum = 0;
        const int VERTICAL_LIMIT = 53;
        const int HORIZONTAL_LIMIT = 23;
        const int VERTICAL_ERROR = 10;
        const int HORIZONTAL_ERROR = 10;

        bool _mapLocated = false;
        /// <summary>
        /// flag showing whether the map has been located
        /// properly by the image processor
        /// </summary>
        public bool MapLocated {
            get { return _mapLocated; }
        }

        Point _upperLeft;
        Point _lowerRight;
        double _verticalScale = 0.0;
        double _horizontalScale = 0.0;
        double _verticalOffset = 0.0;
        double _horizontalOffset = 0.0;

        /// <summary>
        /// relates the pixels in the image to lat lon
        /// </summary>
        public FiducialStrategy() {
            _col1 = Color.FromArgb(255, 0, 221);
            _col2 = Color.FromArgb(255, 0, 238);
            _col1s = new List<Point>();
            _col2s = new List<Point>();
        }

        /// <summary>
        /// get pixels of two particular shades
        /// </summary>
        public void processImage(Bitmap b) {
            if (b == null) {
                _mapLocated = false;
                return;
            }
            _height = b.Height;
            _width = b.Width;
            _col1s.Clear();
            _col2s.Clear();
            for (int i = 0; i < b.Height; i++) {
                for (int j = 0; j < b.Width; j++) {
                    if (b.GetPixel(j, i) == _col1)
                        _col1s.Add(new Point(j, i));
                    if (b.GetPixel(j, i) == _col2)
                        _col2s.Add(new Point(j, i));
                }
            }
            //if pixels appear in image, check the shapes
            if (_col1s.Count > 0 && _col2s.Count > 0) {
                _upperLeft = checkShape(_col1s);
                _lowerRight = checkShape(_col2s);
                _upperLeft.Y += BALLOON_OFFSET_Y;
                _lowerRight.Y += BALLOON_OFFSET_Y;
                _upperLeft.X += BALLOON_OFFSET_X;
                _lowerRight.X += BALLOON_OFFSET_X;
            }

        }

        //check for a balloon shape
        Point checkShape(List<Point> points) {
            Point top = new Point(0, 0);
            _verticalSum = 0;
            _horizontalSum = 0;
            top.X = points[0].X;
            top.Y = points[0].Y;
            for (int i = 0; i < points.Count; i++) {
                _verticalSum += points[i].Y - top.Y;
                _horizontalSum += points[i].X - top.X;
            }
            //check that the shape is within limits
            if (Math.Abs(_verticalSum - VERTICAL_LIMIT) < VERTICAL_ERROR
                && Math.Abs(_horizontalSum - HORIZONTAL_LIMIT) < HORIZONTAL_ERROR)
                _mapLocated = true;
            else _mapLocated = false;
            return top;
        }

        /// <summary>
        /// sets a correspondence between the image pixels and UTM coordinates
        /// </summary>
        public void setCorrespondence(Location wpt1, Location wpt2) {
            double dx1 = wpt2.Easting - wpt1.Easting;
            double dy1 = wpt2.Northing - wpt1.Northing;
            double dx2 = _lowerRight.X - _upperLeft.X;
            double dy2 = _lowerRight.Y - _upperLeft.Y;
            _horizontalScale = dx1 / dx2;
            _verticalScale = dy1 / dy2;
            _horizontalOffset = wpt1.Easting - _upperLeft.X * _horizontalScale;
            _verticalOffset = wpt1.Northing - _upperLeft.Y * _verticalScale;
        }


        /// <summary>
        /// get a location from a system.drawing point
        /// </summary>
        public Location getLocation(Point pt) {
            Location wpt = new Location();
            wpt.Easting = pt.X * _horizontalScale + _horizontalOffset;
            wpt.Northing = pt.Y * _verticalScale + _verticalOffset;
            return wpt;
        }

        /// <summary>
        /// get a system.drawing point from a location
        /// </summary>
        public Point getPoint(Location wpt) {
            Point pt = new Point();
            pt.X = (int)((wpt.Easting - _horizontalOffset) / _horizontalScale);
            pt.Y = (int)((wpt.Northing - _verticalOffset) / _verticalScale);
            if (pt.X < 0 || pt.X >= _width || pt.Y < 0 || pt.Y >= _height) {
                pt.X = 0;
                pt.Y = 0;
            }
            return pt;
        }
    }

}
