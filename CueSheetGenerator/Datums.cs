using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtmConvert {
    static class Datums {
        /// <summary>
        ///	a listing of all the datums used
        /// </summary>
        public static readonly List<Datum> datumList;
        static string datumString = "NAD83/WGS84,6378137,6356752.3142,1/298.257223563,Global;GRS 80,6378137,6356752.3141,1/298.257222101,US;WGS72,6378135,6356750.5,1/298.26,NASA DOD;Australian 1965,6378160,6356774.7,1/298.25,Australia;Krasovsky 1940,6378245,6356863.0,1/298.3,Soviet Union;International (1924) -Hayford (1909),6378388,6356911.9,1/297,Global except as listed;Clake 1880,6378249.1,6356514.9,1/293.46,France Africa;Clarke 1866,6378206.4,6356583.8,1/294.98,North America;Airy 1830,6377563.4,6356256.9,1/299.32,Great Britain;Bessel 1841,6377397.2,6356079.0,1/299.15,Central Europe Chile Indonesia;Everest 1830,6377276.3,6356075.4,1/300.80,South Asia";
        static Datums() {
            datumList = new List<Datum>();
            while (datumString.Contains(";")) {
                Datum d = new Datum();
                d.Name = datumString.Substring(0, datumString.IndexOf(","));
                datumString = datumString.Remove(0, datumString.IndexOf(",") + 1);
                d.EquatorialRadius = double.Parse(datumString.Substring(0, datumString.IndexOf(",")));
                datumString = datumString.Remove(0, datumString.IndexOf(",") + 1);
                d.PolarRadius = double.Parse(datumString.Substring(0, datumString.IndexOf(",")));
                datumString = datumString.Remove(0, datumString.IndexOf(",") + 1);
                d.Flattening = (d.EquatorialRadius - d.PolarRadius) / d.EquatorialRadius;
                datumString = datumString.Remove(0, datumString.IndexOf(",") + 1);
                d.Use = datumString.Substring(0, datumString.IndexOf(";"));
                datumString = datumString.Remove(0, datumString.IndexOf(";") + 1);
                datumList.Add(d);
            }
        }
    }
}
