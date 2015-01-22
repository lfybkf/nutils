using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	static class Program
	{

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length != 1)
				return;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Chooser[] chooses = {Chooser.createVideo(), Chooser.createBook(), Chooser.createDistrib()};

			string Old = args.First();
			string Ext = Path.GetExtension(Old);

			Chooser obj = chooses.First(c => c.IsMatch(Ext));
			obj.Old = Path.GetFileNameWithoutExtension(Old);
			Application.Run(obj.frm);

			if (string.IsNullOrWhiteSpace(obj.New) == false)
			{
				string New = Path.Combine(Path.GetDirectoryName(Old), obj.New) + Ext;
				File.Move(Old, New);
			}//if
		}
	}
}
