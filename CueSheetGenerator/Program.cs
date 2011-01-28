using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/// <summary>
///	cue sheet generator, namespace for all classes that compose pathfinder 
/// </summary>
namespace CueSheetGenerator {
    /// <summary>
    ///	class program, contains the main function for pathfinder 
    /// </summary>
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            //pass in command line argument 0 if it exists
			string s = null;
			if (args.Length > 0) s = args[0];
			Application.Run(new MainForm(s));
		}
	}
}
