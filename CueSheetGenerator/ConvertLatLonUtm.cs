using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtmConvert {
	//references:
	//Converting UTM to Latitude and Longitude (Or Vice Versa)
	//Steven Dutch, Natural and Applied Sciences, University of Wisconsin - Green Bay
	//http://www.uwgb.edu/dutchs/UsefulData/UTMFormulas.HTM

	//this is really intense, but thoroughly debugged, so dont bother trying to 
	//figure out how it works, it converts lat lon to UTM and vice versa thats it
	/// <summary>
	/// converts lat lon to utm and vice versa.
	/// </summary>
    public class ConvertLatLonUtm {
        Datum _datum;
        /// <summary>
        /// public datum object
        /// </summary>
        public Datum Datum {
            get { return _datum; }
            set { _datum = value; }
        }

        double _easting = 491887;
        /// <summary>
        /// easting attribute, distance east from central meridian
        /// </summary>
        public double Easting {
            get { return _easting; }
        }

        public double x {
            get { return _easting; }
        }

        double _northing = 4876938;
        /// <summary>
        /// northing, distance north of the equator
        /// </summary>
        public double Northing {
            get { return _northing; }
        }

        public double y {
            get { return _northing; }
        }

        string _zone = "";
        /// <summary>
        /// UTM zone string
        /// </summary>
        public string Zone {
            get { return _zone; }
        }

        int _centralMeridian = 0;
        /// <summary>
        /// the central meridian of the UTM zone
        /// </summary>
        public int CentralMeridian { 
            get { return _centralMeridian; }
        }

        double _lat;
        /// <summary>
        /// latitude cooresponding to northing
        /// </summary>
        public double Latitude {
            get { return _lat; }
        }

        double _lon;
        /// <summary>
        /// longitude cooresponding to easting
        /// </summary>
        public double Longitude {
            get { return _lon; }
        }

        /// <summary>
        /// constructor for convert lat lon utm
        /// </summary>
        public ConvertLatLonUtm() {
            _datum = new Datum();
            
        }

        const double k0 = 0.9996; //scale along long0 
        double e = 0.08; //approximately. This is the eccentricity of the earth's elliptical cross-section.
        double eTic = 0.007; //The quantity e' only occurs in even powers so it need only be calculated as e'^2.

        /// <summary>
        /// convert lat lon to UTM
        /// </summary>
        public void convertLatLonToUtm(double lat, double lon) {
            calcCM(lon);
            e = Math.Sqrt(1.0 - (Math.Pow(_datum.b / _datum.a, 2)));
            eTic = Math.Pow(e, 2) / (1.0 - Math.Pow(e, 2));
            double n = (_datum.a - _datum.b) / (_datum.a + _datum.b);
            //This is the radius of curvature of the earth in the meridian plane.
            double rho = _datum.a * (1.0 - Math.Pow(e, 2)) / Math.Pow(1.0
                - Math.Pow(e * Math.Sin(lat), 2), 3.0 / 2.0);
            //This is the radius of curvature of the earth perpendicular to the meridian plane. It is also the 
            //distance from the point in question to the polar axis, measured perpendicular to the earth's surface.
            double nu = _datum.a / Math.Pow(1.0 - Math.Pow(e * Math.Sin(lat), 2), 1.0 / 2.0);
            //in radians (This differs from the treatment in the Army reference)
            double p = lon - long0;
            //=a*(1-n+(5*n*n/4)*(1-n)+(81*n^4/64)*(1-n))
            double A0 = _datum.a * (1.0 - n + (5.0 * Math.Pow(n,2) / 4.0) * (1.0 - n) 
                + (81.0 * Math.Pow(n, 4) / 64.0) * (1.0 - n));
            //=(3*a*n/2)*(1-n-(7*n*n/8)*(1-n)+55*n^4/64)
            double B0 = (3.0 * _datum.a * n / 2.0) * (1.0 - n - (7.0 * Math.Pow(n,2) / 8.0) 
                * (1.0 - n) + 55.0 * Math.Pow(n, 4) / 64.0);
            //=(15*a*n*n/16)*(1-n+(3*n*n/4)*(1-n))
            double C0 = (15.0 * _datum.a * Math.Pow(n,2) / 16.0) 
                * (1.0 - n + (3.0 * Math.Pow(n,2) / 4.0) * (1.0 - n));
            //=(35*a*n^3/48)*(1-n+11*n*n/16)
            double D0 = (35.0 * _datum.a * Math.Pow(n, 3) / 48.0) * (1.0 - n + 11.0 * Math.Pow(n, 2) / 16.0);
            //=(315*a*n^4/51)*(1-n)
            double E0 = (315.0 * _datum.a * Math.Pow(n, 4) / 51.0) * (1.0 - n);
            //NOAA Meridional Arc
            //=A0*lat-B0*SIN(2*lat)+C0*SIN(4*lat)-D0*SIN(6*lat)+E0*SIN(8*lat)
            double S = A0 * lat - B0 * Math.Sin(2.0 * lat) + C0 * Math.Sin(4.0 * lat)
                + E0 * Math.Sin(8.0 * lat);

            ////Army Meridional Arc calculation
            //double M = _datum.a * ((1.0 - Math.Pow(e, 2) / 4.0 - 3.0 * Math.Pow(e, 4)
            //    / 64.0 - 5.0 * Math.Pow(e, 6) / 256.0) * lat
            //    - (3.0 * Math.Pow(e, 2) / 8.0 + 3.0 * Math.Pow(e, 4)
            //    / 32.0 + 45.0 * Math.Pow(e, 6) / 1024) * Math.Sin(2.0 * lat)
            //    + (15.0 * Math.Pow(e, 4) / 256.0 + 45.0 * Math.Pow(e, 6) / 1024.0)
            //    * Math.Sin(4 * lat)
            //    - (35.0 * Math.Pow(e, 6) / 3072.0) * Math.Sin(6.0 * lat));

            //=S*k0
            double K1 = S * k0;
            //=nu*SIN(lat)*COS(lat)*k0/2
            double K2 = k0 * nu * Math.Sin(lat) * Math.Cos(lat) / 2.0;
            //=((nu*SIN(lat)*COS(lat)^3)/24)*(5-TAN(lat)^2+9*e1sq*COS(lat)^2+4*e1sq^2*COS(lat)^4)*k0
            double K3 = (k0 * nu * Math.Sin(lat) * Math.Pow(Math.Cos(lat), 3) / 24.0)
                * (5.0 - Math.Pow(Math.Tan(lat), 2) + 9.0 * eTic
                * Math.Pow(Math.Cos(lat), 2)
                + 4.0 * Math.Pow(eTic, 2) * Math.Pow(Math.Cos(lat), 4));
            _northing = K1 + K2 * Math.Pow(p, 2) + K3 * Math.Pow(p, 4);
            //=nu*COS(lat)*k0
            double K4 = nu * Math.Cos(lat) * k0;
            //=(COS(lat))^3*(nu/6)*(1-TAN(lat)^2+e1sq*COS(lat)^2)*k0
            double K5 = Math.Pow(Math.Cos(lat), 3) * (nu / 6.0)
                * (1 - Math.Pow(Math.Tan(lat), 2)
                + eTic * Math.Pow(Math.Cos(lat), 2)) * k0;            
            _easting = 500000.0 + (K4 * p + K5 * Math.Pow(p, 3));
            char c = 'N';
            //implicit casting is suppost to be going on here.
            if (lat > 0)
                c = (char)(((int)((int)(lat * (180.0 / Math.PI))) / 8) + 78);
            else
                c = (char)(((int)((int)(lat * (180.0 / Math.PI))) / 8) + 77);
            if (c >= 'O') c++; //the letters I and O are not used because they can
            if (c == 'I') c--; //be confused with numbers.
            _zone = ((int)(((lon * (180.0 / Math.PI)) + 180) / 6) + 1).ToString() + c;
        }

        double long0 = -123; //central meridian of zone (radians) // maybe should be 123
        private void calcCM(double lon) {
            if (lon < 0)
                _centralMeridian = (int)(((int)(Math.Abs(lon) 
                    * (180.0 / Math.PI))) / 6) * -6 - 3;
            else
                _centralMeridian = (int)(((int)(Math.Abs(lon) 
                    * (180.0 / Math.PI))) / 6) * 6 + 3;
            long0 = (double)_centralMeridian * (Math.PI / 180.0);
        }

        /// <summary>
        /// convert UTM to lat lon
        /// </summary>
        public void convertUtmToLatLon(double x, double y, string zone) {
            e = Math.Sqrt(1.0 - (Math.Pow(_datum.b / _datum.a, 2)));
            eTic = Math.Pow(e, 2) / (1.0 - Math.Pow(e, 2));
            if(int.Parse(zone.Substring(0, zone.Length - 1)) > 30)
                calcCM(ConvertDegRad.getRadians((((int.Parse(zone
                    .Substring(0, zone.Length - 1))- 30) * 6) - 3).ToString(), 'E'));
            else
                calcCM(ConvertDegRad.getRadians(((180 - (int.Parse(zone.Substring
                    (0, zone.Length - 1)) * 6)) + 3).ToString(), 'W'));
            x = x - 500000.0; //subtract 500,000 from conventional UTM coordinate
            double M = y / k0;
            double mu = M / (_datum.a * (1 - Math.Pow(e, 2) / 4.0 - 3.0 
                * Math.Pow(e, 4) / 64.0 - 5.0 * Math.Pow(e, 6) / 256.0));
            double e1 = (1.0 - Math.Pow(1.0 - Math.Pow(e, 2), 0.5)) 
                / (1.0 + Math.Pow(1.0 - Math.Pow(e, 2), 0.5));
            //= (3 * e1 / 2 - 27 e1^3 / 32 ..)
            double J1 = 3.0 * e1 / 2.0 - 27.0 * Math.Pow(e1, 3) / 32.0;
            //= (21 * e1^2 / 16 - 55 * e1^4 / 32 ..)
            double J2 = 21.0 * Math.Pow(e1, 2) / 16.0 - 55.0 * Math.Pow(e1, 4) / 32.0;
            //(151 * e1^3 / 96 ..)
            double J3 = 151.0 * Math.Pow(e1, 3) / 96.0;
            //(1097 * e1^4 / 512 ..)
            double J4 = 1097.0 * Math.Pow(e1, 4) / 512.0;
            //footprint latitude =mu+J1*SIN(2*mu)+J2*SIN(4*mu)+J3*SIN(6*mu)+J4*SIN(8*mu)
            double fp = mu + J1 * Math.Sin(2.0 * mu) + J2 * Math.Sin(4.0 * mu)
                + J3 * Math.Sin(6.0 * mu) + J4 * Math.Sin(8.0 * mu);
            //e'^2 * cos^2(fp)
            double C1 = Math.Pow(eTic, 2) * Math.Pow(Math.Cos(fp), 2);
            //tan^2(fp)
            double T1 = Math.Pow(Math.Tan(fp), 2);
            //a(1 - e^2) / (1 - e^2 * sin^2(fp))^(3 / 2)
            double R1 = _datum.a * (1.0 - Math.Pow(e, 2)) 
                / Math.Pow(1.0 - Math.Pow(e, 2) * Math.Pow(Math.Sin(fp), 2), 3.0 / 2.0);
            //a / (1 - e^2 * sin^2(fp))^(1/2)
            double N1 = _datum.a / Math.Pow(1.0 - Math.Pow(e, 2) * Math.Pow(Math.Sin(fp), 2), 1.0 / 2.0);
            //x / (N1 * k0)
            double D = x / (N1 * k0);
            //N1 * tan(fp) / R1
            double Q1 = N1 * Math.Tan(fp) / R1;
            //(D^2 / 2)
            double Q2 = Math.Pow(D, 2) / 2.0;
            //(5 + 3 * T1 + 10 * C1 - 4 * C1^2 - 9 * e'^2) * D^4 / 24
            double Q3 = (5.0 + 3.0 * T1 + 10.0 * C1 - 4.0 * Math.Pow(C1,2) 
                - 9.0 * Math.Pow(eTic,2)) * Math.Pow(D,4) / 24.0;
            //(61 + 90 * T1 + 298 * C1 + 45 * T1^2 - 3 * C1^2 - 252 * e'^2) * D^6 / 720
            double Q4 = (61.0 + 90.0 * T1 + 289.0 * C1 + 45.0 * Math.Pow(T1, 2)
                - 3.0 * Math.Pow(C1, 2) - 252.0 * Math.Pow(eTic, 2)) * Math.Pow(D, 6) / 720.0;
            _lat = fp - Q1 * (Q2 - Q3 + Q4);
            double Q5 = D;
            //(1 + 2 * T1 + C1)D^3 / 6
            double Q6 = (1.0 + 2.0 * T1 + C1) * Math.Pow(D,3) / 6.0;
            //(5 - 2 * C1 + 28 * T1 - 3 * C1^2 + 8 * e'^2 + 24 * T1^2) * D^5 / 120
            double Q7 = (5.0 - 2.0 * C1 + 28.0 * T1 - 3.0 * Math.Pow(C1,2) + 8.0 * Math.Pow(eTic,2)
                + 24.0 * Math.Pow(T1,2)) * Math.Pow(D,5) / 120.0;
            //long0 + (Q5 - Q6 + Q7) / cos(fp)
            _lon = long0 + (Q5 - Q6 + Q7) / Math.Cos(fp);
        }

    }
}
