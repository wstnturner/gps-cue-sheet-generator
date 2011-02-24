using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CueSheetGenerator {
    class MapPainter {
        FiducialStrategy _fd = null;

        public MapPainter(FiducialStrategy f) {
            _fd = f;
        }

        public void drawWaypoints(ref Image map, Location[] points) {
            Graphics g = Graphics.FromImage(map);
            SolidBrush sb = new SolidBrush(Color.Blue);
            Point pt;
            for (int i = 1; i < points.Length; i++) {
                pt = _fd.getPoint(points[i]);
                g.FillEllipse(sb, pt.X - 2, pt.Y - 2, 4, 4);
            }
        }

        public void drawTurn(ref Image map, Turn current) {
            Graphics g = Graphics.FromImage(map);
            SolidBrush sb = new SolidBrush(Color.Red);
            Font f = new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold);
            Point pt0, pt1, pt2;
            pt0 = _fd.getPoint(current.Locs[0].GpxLocation);
            //Rectangle retB = new Rectangle(pt0.X-2, pt0.Y-2, 6, 7);
            //g.FillRectangle(sb, retB);
            g.FillEllipse(sb, pt0.X - 3, pt0.Y - 3, 6, 6);
            pt1 = _fd.getPoint(current.Locs[1].GpxLocation);
            //Rectangle retM = new Rectangle(pt1.X-2, pt1.Y-2, 6, 7);
            //g.FillRectangle(sb, retM);
            g.FillEllipse(sb, pt1.X - 3, pt1.Y - 3, 6, 6);
            pt2 = _fd.getPoint(current.Locs[2].GpxLocation);
            //Rectangle ret = new Rectangle(pt2.X-2, pt2.Y-2, 7, 6);
            //g.FillRectangle(sb, ret);
            g.FillEllipse(sb, pt2.X - 3, pt2.Y - 3, 6, 6);
        }

        public void drawBeginAndEndPoints(ref Image map, Location begin, Location end) {
            Graphics g = Graphics.FromImage(map);
            Font f = new Font(FontFamily.GenericMonospace, 12, FontStyle.Bold);
            SolidBrush sbB = new SolidBrush(Color.Green);
            SolidBrush sbE = new SolidBrush(Color.DarkRed);
            Point ptB;
            ptB = _fd.getPoint(begin);
            g.FillEllipse(sbB, ptB.X - 5, ptB.Y - 5, 10, 10);
            g.DrawString("B", f, sbB, ptB);
            Point ptE;
            ptE = _fd.getPoint(end);
            g.FillEllipse(sbE, ptE.X - 5, ptE.Y - 5, 10, 10);
            g.DrawString("E", f, sbE, ptE);
        }

        public void drawPointsOfInterest(ref Image map, PointOfInterest[] pois) {
            //draw the points of interest on the map
        }
    }

}