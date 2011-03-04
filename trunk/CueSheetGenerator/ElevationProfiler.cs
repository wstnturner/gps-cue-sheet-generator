using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CueSheetGenerator {
    static class ElevationProfiler {
        static double _maxHeight = 0;
        static double _minHeight = double.PositiveInfinity;

        public static Bitmap getElevationProfile(List<Location> locs) {
            if (locs == null || locs.Count < 1) return new Bitmap(100, 100);
            _maxHeight = 0;
            _minHeight = double.PositiveInfinity;
            foreach (Location loc in locs) {
                if (loc.Elevation > _maxHeight) _maxHeight = loc.Elevation;
                if (loc.Elevation < _minHeight) _minHeight = loc.Elevation;
            }
            int delta = (int)(_maxHeight - _minHeight);

            Bitmap profile = new Bitmap(locs.Count, delta + 40);
            Graphics g = Graphics.FromImage(profile);
            SolidBrush b = new SolidBrush(Color.SkyBlue);
            Pen p = new Pen(Color.White);
            g.FillRectangle(b, 0, 0, profile.Width, profile.Height);
            int height = profile.Height;
            int width = profile.Width;
            for (int i = 0; i < width; i++) {
                g.DrawLine(p, i, height, i, height - (int)(locs[i].Elevation - _minHeight));
            }
            SolidBrush sb = new SolidBrush(Color.Black);
            Font f = new Font(FontFamily.GenericSerif, 8);
            Pen p2 = new Pen(sb);
            Point pt = new Point(0, height - 15);
            for (int i = 40; i < width; i += 40) {
                pt.X = i;
                g.DrawLine(p2, i, height, i, 0);
                g.DrawString(Math.Round((locs[i].Distance / 1000), 1).ToString(), f, sb, pt);
            }
            pt.X = 0;
            for (int i = 40; i < delta; i += 40) {
                pt.Y = (height - i) - 15;
                g.DrawLine(p2, 0, (height - i), width, (height - i));
                g.DrawString((_minHeight + i).ToString(), f, sb, pt);
            }
            return profile;
        }
    }
}
