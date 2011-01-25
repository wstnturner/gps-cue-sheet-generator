using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CueSheetGenerator {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string s = null;
			if (args.Length > 0) s = args[0];
			Application.Run(new MainForm(s));
		}
	}
}
