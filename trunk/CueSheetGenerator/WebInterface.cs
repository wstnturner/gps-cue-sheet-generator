using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

namespace CueSheetGenerator {
	class WebInterface {
		string _status = "Ok";

		public string Status {
			get { return _status; }
			set { _status = value; }
		}

		//adapted from
		//C# Code Snippet - Download Image from URL
		//http://www.digitalcoding.com/Code-Snippets/C-Sharp/C-Code-Snippet-Download-Image-from-URL.html
		/// <summary>
		/// Function to download Image from website
		/// </summary>
		/// <param name="_URL">URL address to download image</param>
		/// <returns>Image</returns>
		public Image downloadImage(string _URL) {
			Image _tmpImage = null;
			try {
				// Open a connection
				HttpWebRequest _HttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(_URL);
				_HttpWebRequest.AllowWriteStreamBuffering = true;
				// Request response:
				WebResponse _WebResponse = _HttpWebRequest.GetResponse();
				// Open data stream:
				Stream _WebStream = _WebResponse.GetResponseStream();
				// convert webstream to image
				_tmpImage = Image.FromStream(_WebStream);
				// Cleanup
				_WebResponse.Close();
				_WebResponse.Close();
			} catch (Exception e) {
				Status = e.Message;
			}
			return _tmpImage;
		}

		//adapted from:
		//Download a web page
		//http://www.jonasjohn.de/snippets/csharp/download-webpage.htm
		/// <summary>
		/// Returns the content of a given web adress as string.
		/// </summary>
		/// <param name="Url">URL of the webpage</param>
		/// <returns>Website content</returns>
		public string downloadWebPage(string url) {
			try {
				// Open a connection
				HttpWebRequest webRequestObject = (HttpWebRequest)HttpWebRequest.Create(url);
				// Request response:
				WebResponse response = webRequestObject.GetResponse();
				// Open data stream:
				Stream webStream = response.GetResponseStream();
				// Create reader object:
				StreamReader reader = new StreamReader(webStream);
				// Read the entire stream content:
				string pageContent = reader.ReadToEnd();
				// Cleanup
				reader.Close();
				webStream.Close();
				response.Close();
				return pageContent;
			} catch (Exception e) {
				_status = e.Message;
				return e.Message;
			}
		}
	}
}
