using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
	class AddressCache {

		//the first fuzzy lookup implementation failed, the data structures are too big.
		//this is a linear lookup, and the lookup time will grow linearly with each
		//new gpx file that is reverse geocoded for. This is not ideal

		List<Cache> _caches = null;
		string _cacheDir = ".\\cache";

		public const int BIN_SIZE = 10; //meters
		public const int ZONE_SIZE = 1000; //meters

		public AddressCache() {
			_caches = new List<Cache>();
			//read the location cache from the file system
			if(!Directory.Exists(_cacheDir))
				Directory.CreateDirectory(_cacheDir);
			foreach (string fn in Directory.GetFiles(_cacheDir))
				readCache(_cacheDir + fn);
		}

		public Location lookup(Waypoint wpt) {
			return null;
		}



		public void addToCache(Location loc) {
			bool cacheExists = false;
			for (int i = 0; i < _caches.Count; i++) {
				if (loc.GpxWaypoint.Zone == _caches[i].CacheName)
					cacheExists = true;
			}
			if (!cacheExists) _caches.Add(new Cache(loc.GpxWaypoint.Zone));
			int x = (int)(loc.GpxWaypoint.Easting / (double)BIN_SIZE);
			int y = (int)(loc.GpxWaypoint.Northing / (double)BIN_SIZE);
			for (int i = 0; i < _caches.Count; i++) {
				if (loc.GpxWaypoint.Zone == _caches[i].CacheName)
					_caches[i].LocationCache[y][x].Add(loc);
			}
		}

		void readCache(string fileName) {
			StreamReader sr = new StreamReader(fileName);



		}

		public void writeCache() {

		}

		class Cache {

			public Cache(string name) {
				_cacheName = name;
				_locationCache = new List<List<List<Location>>>();
				for (int i = 0; i < ZONE_SIZE / BIN_SIZE; i ++) {
					_locationCache.Add(new List<List<Location>>());
					for (int j = 0; j < ZONE_SIZE / BIN_SIZE; j ++)
						_locationCache[i].Add(new List<Location>());
				}
			}

			List<List<List<Location>>> _locationCache = null;

			public List<List<List<Location>>> LocationCache {
				get { return _locationCache; }
				set { _locationCache = value; }

			}

			string _cacheName = "";

			public string CacheName {
				get { return _cacheName; }
				set { _cacheName = value; }
			}
		}

	}
}
