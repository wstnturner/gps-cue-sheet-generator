﻿using System;
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

		bool _win = true;

		public CacheStrategy() {
			//initialize an array of caches
			//read in the names of the cache files
			//in the cache directory, if it does not exist, then create it
			_win = Environment.OSVersion.VersionString.Contains("Windows");
			if(_win) _cacheDir = System.Windows.Forms.Application.StartupPath + @"\cache\";
			else _cacheDir = System.Windows.Forms.Application.StartupPath + @"/cache/";
			_caches = new List<Cache>();
			if (!Directory.Exists(_cacheDir))
				Directory.CreateDirectory(_cacheDir);
			_cacheFileList = new string[Directory.GetFiles(_cacheDir).Length];
			_cacheFileList = Directory.GetFiles(_cacheDir);
			readCachesFromFile();
		}

		Location _tempLocation = null;
		Cache _currentCache = null;

		public Location lookup(Waypoint wpt) {
			_cacheHit = false;
			//lookup the waypoint in a cache
			//if it is not present, return null
			if (wpt.Zone != _currentCache.Name) {
				foreach (Cache c in _caches) {
					if (wpt.Zone == c.Name)
						_currentCache = c;
				}
			}
			_tempLocation = (Location)_currentCache.read(wpt.Key);
			if (_tempLocation != null) {
				_cacheHit = true;
				_tempLocation.GpxWaypoint = wpt;
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
			// using one tree per UTM zone
			try {
				foreach (string s in _cacheFileList)
					_caches.Add(readCache(s));
			} catch (Exception e) {
				_status = e.Message;
			}
			if (_caches.Count > 0)
				_currentCache = _caches[0];
		}

		Cache readCache(string fileName) {
			//read index, street name, and full street address
			StreamReader sr = new StreamReader(fileName);
			Location loc = null;
			Cache c = null;
			//use windows or unix file paths
			if(_win) c = new Cache(fileName.Remove(0, fileName.LastIndexOf("\\")+1));
			else c = new Cache(fileName.Remove(0, fileName.LastIndexOf("/") + 1));
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
			foreach (Cache c in _caches)
				writeCache(c);
		}

		/// <summary>
		/// perform a pre-order tree traversal on the llrb tree, 
		/// store the nodes in a list and then write them to a file
		/// </summary>
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
			//each cache consists of a tree and a name (UTM zone)
			//and maybe helper functions
			LLRBTree _tree = null;

			//the tree used to store data
			public LLRBTree Tree {
				get { return _tree; }
				set { _tree = value; }
			}

			//cache name, same as UTM zone name
			string _name = "";
			public string Name {
				get { return _name; }
				set { _name = value; }
			}

			//constructor
			public Cache(string name) {
				_name = name;
				_tree = new LLRBTree();
			}

			//look for item in tree
			public object read(long key) {
				return _tree.find(key);
			}

			//add an item to tree
			public void write(long key, object value) {
				_tree.insert(key, value);
			}

		}

	}

}