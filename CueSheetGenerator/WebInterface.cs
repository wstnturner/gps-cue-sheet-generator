﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using System.Threading;

namespace CueSheetGenerator {
    /// <summary>
    ///	web interface, retrieves web pages (XML) and images (maps)
    /// </summary>
    class WebInterface {
        int _errorCounter = 0;

        string _status = "Ok";
        public string Status {
            /// <summary>
            /// status of web interface class instance
            /// </summary>
            get { return _status; }
        }

        //adapted from
        //C# Code Snippet - Download Image from URL
        //http://www.digitalcoding.com/Code-Snippets/C-Sharp/C-Code-Snippet-Download-Image-from-URL.html
        /// <summary>
        /// function to download an image from a website
        /// </summary>
        /// <param name="url">URL address to download image</param>
        /// <returns>Image</returns>
        public Image downloadImage(string url) {
            Image tmpImage = null;
            try {
                // Open a connection
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.AllowWriteStreamBuffering = true; 
                // set timeout for 5 seconds
                httpWebRequest.Timeout = 5000;
                // Request response:
                WebResponse webResponse = httpWebRequest.GetResponse();
                // Open data stream:
                Stream webStream = webResponse.GetResponseStream();
                // convert webstream to image
                tmpImage = Image.FromStream(webStream);
                // Cleanup
                webResponse.Close();
                webResponse.Close();
                _errorCounter = 0;
            } catch (Exception e) {
                _errorCounter++;
                if (_errorCounter > 10) {
                    //Thread.CurrentThread.Abort(e);
                }
                _status = e.Message;
            }
            return tmpImage;
        }

        //adapted from:
        //Download a web page
        //http://www.jonasjohn.de/snippets/csharp/download-webpage.htm
        /// <summary>
        /// Returns the content of a given web adress as string.
        /// </summary>
        /// <param name="url">URL of the webpage</param>
        /// <returns>Website content</returns>
        public string downloadWebPage(string url) {
            string pageContent = "";
            try {
                // Open a connection
                HttpWebRequest webRequestObject = (HttpWebRequest)HttpWebRequest.Create(url);
                // set timeout for 5 seconds
                webRequestObject.Timeout = 5000;
                // Request response:
                WebResponse response = webRequestObject.GetResponse();
                // Open data stream:
                Stream webStream = response.GetResponseStream();
                // Create reader object:
                StreamReader reader = new StreamReader(webStream);
                // Read the entire stream content:
                pageContent = reader.ReadToEnd();
                // Cleanup
                reader.Close();
                webStream.Close();
                response.Close();
                _errorCounter = 0;   
            } catch (Exception e) {
                _status = e.Message;
                _errorCounter++;
                if (_errorCounter > 10) {
                   // Thread.CurrentThread.Abort(e);
                }
            }
            return pageContent; 
        }
    }
}
