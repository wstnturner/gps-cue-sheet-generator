using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CueSheetGenerator {
	class CacheStrategy {

		//cache strategy class, used to look up previously reverse geocoded
		//locations, and to store newly reverse geocoded locations for fast
		//retrieval

		List<Cache> _caches = null;
		string _cacheDir = ".\\cache";
		string[] _cacheList = null;

		public CacheStrategy() {
			//initialize an array of caches
			//read in the names of the cache files
			//in the cache directory, if it does not exist, then create it
			_caches = new List<Cache>();
			if(!Directory.Exists(_cacheDir))
				Directory.CreateDirectory(_cacheDir);
			_cacheList = new string[Directory.GetFiles(_cacheDir).Length];
			_cacheList = Directory.GetFiles(_cacheDir);

		}

		public Location lookup(Waypoint wpt) {
			//lookup the waypoint in a cache
			//if it is not present, return null
			return null;
		}


		public void addToCache(Location loc) {
			//if the location's zone matches a cache file name
			//then store it in the cache
			//else create a new cache
		}

		void readCache(string fileName) {
			//may not be needed if we search all cache files
			//sequentially (inefficient). But if we store the data in
			//some kind of tree, (left leaning red black tree perhaps)
			//then we will need this
			//we could use one tree per UTM zone
		}


		public void writeCache() {
			//write the cache back out to the file system
			//for each tree, write contents to a file
		}

		class Cache {
			//each cache could consist of a tree and a name (UTM zone)
			//and maybe some helper functions
		}

	}
}
