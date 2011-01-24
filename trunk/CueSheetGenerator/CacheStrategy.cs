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
		string _cacheDir = "";
		string[] _cacheFileList = null;
		string _status = "Ok";

		bool _cacheHit = false;
		public bool CacheHit {
			get { return _cacheHit; }
		}

		public CacheStrategy() {
			//initialize an array of caches
			//read in the names of the cache files
			//in the cache directory, if it does not exist, then create it
			_cacheDir = System.Windows.Forms.Application.StartupPath + @"\cache\";
			_caches = new List<Cache>();
			if (!Directory.Exists(_cacheDir))
				Directory.CreateDirectory(_cacheDir);
			_cacheFileList = new string[Directory.GetFiles(_cacheDir).Length];
			_cacheFileList = Directory.GetFiles(_cacheDir);
			readCachesFromFile();
		}

		Location _tempLocation = null;

		public Location lookup(Waypoint wpt) {
			_cacheHit = false;
			//lookup the waypoint in a cache
			//if it is not present, return null
			foreach (Cache c in _caches) {
				if (wpt.Zone == c.Name)
					_tempLocation = (Location)c.read(wpt.Key);
				if (_tempLocation != null) {
					_cacheHit = true;
					_tempLocation.GpxWaypoint = wpt;
				}
				return _tempLocation;
			}
			return null;
		}

		public void addToCache(Location loc) {
			//if the location's zone matches a cache file name
			//then store it in the cache
			//else create a new cache
			bool foundCache = false;
			foreach (Cache c in _caches) {
				if (loc.GpxWaypoint.Zone == c.Name) {
					foundCache = true;
					c.write(loc.GpxWaypoint.Key, loc);
				}
			}
			//create the cache and store the location
			if (!foundCache) {
				Cache c = new Cache(loc.GpxWaypoint.Zone);
				c.write(loc.GpxWaypoint.Key, loc);
				_caches.Add(c);
			}
		}

		void readCachesFromFile() {
			//may not be needed if we search all cache files
			//sequentially (inefficient). But if we store the data in
			//some kind of tree, (left leaning red black tree perhaps)
			//then we will need this.
			//we could use one tree per UTM zone
			try {
				foreach (string s in _cacheFileList) {
					_caches.Add(readCache(s));
				}
			} catch (Exception e) {
				_status = e.Message;
			}

		}

		Cache readCache(string fileName) {
			//what do we want to store?
			//key, address, street name
			StreamReader sr = new StreamReader(fileName);
			Location loc = null;
			Cache c = new Cache(fileName.Remove(0, fileName.LastIndexOf("\\")+1));
			string key, address, streetName, s;
			while (!sr.EndOfStream) {
				s = sr.ReadLine();
				key = s.Substring(0, s.IndexOf("\t"));
				s = s.Remove(0, s.IndexOf("\t")+1);
				streetName = s.Substring(0, s.IndexOf("\t"));
				s = s.Remove(0, s.IndexOf("\t")+1);
				address = s;
				loc = new Location(address, streetName);
				c.Tree.insert(long.Parse(key), loc);
			}
			sr.Close();
			return c;
		}

		public void writeCachesToFile() {
			//write the cache back out to the file system
			//for each tree, write contents to a file
			foreach (Cache c in _caches) {
				//do some kind of IEnumerable post order traversal of the tree
				//that way it will be written out preserving its internal structure
				//but for now just do it in order (remove min)
				writeCache(c);
			}
		}

		private void writeCache(Cache c) {
			List<LLRBTree.Node> locs = c.Tree.getPreOrederList();
			StreamWriter sr = new StreamWriter(_cacheDir + c.Name);
			Location loc = null;
			foreach (LLRBTree.Node n in locs) {
				loc = (Location)(n.getValue());
				sr.WriteLine(n.getKey().ToString() + "\t" + loc.StreetName
					+ "\t" + loc.Address);
			}
			sr.Close();
		}

		class Cache {
			//each cache could consist of a tree and a name (UTM zone)
			//and maybe some helper functions
			LLRBTree _tree = null;

			public LLRBTree Tree {
				get { return _tree; }
				set { _tree = value; }
			}
			string _name = "";

			public string Name {
				get { return _name; }
				set { _name = value; }
			}
			public Cache(string name) {
				_name = name;
				_tree = new LLRBTree();
			}

			public object read(long key) {
				return _tree.find(key);
			}

			public void write(long key, object value) {
				_tree.insert(key, value);
			}

			IComparable key = 0;
			public object getMin() {
				key = _tree.min();
				return _tree.find(key);
			}

			public void removeMin() {
				if(_tree.TreeSize > 0)
				_tree.deleteMin();
			}

		}

	}

}
