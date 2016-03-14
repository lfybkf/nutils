using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace imgAL
{
	class Program
	{
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		public static extern Int32 SetForegroundWindow(int hWnd);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		static void Main(string[] args)
		{
			List<string> list = new List<string>();
			bool HasError = false;
			Action<string> log = Console.WriteLine;
			string[] exts = { "bmp", "gif", "jpg", "jpeg", "png" }; 
			string dir = Environment.CurrentDirectory;
			string dirLandscape = Path.Combine(dir, "_Landscape");
			string fileLandscape;
			var files = Directory.EnumerateFiles(dir).Where(file => exts.Any(ext => file.EndsWith(ext))).ToArray();
			log("files.Count= " + files.Length);
			log("dirLandscape= " + dirLandscape);
			Image img;
			foreach (var file in files)
			{
				try
				{
					img = Image.FromFile(file);
					if (img.Height < img.Width)
					{
						img.Dispose();
						if (Directory.Exists(dirLandscape) == false)	{	Directory.CreateDirectory(dirLandscape);}
						fileLandscape = Path.Combine(dirLandscape, Path.GetFileName(file));
						//fileLandscape = Path.GetFileName(file).Replace("_", "_z");
						log("move file=" + file + " to " + fileLandscape);
						File.Move(file, fileLandscape);
					}//if
				}//try
				catch (Exception exception)
				{
					HasError = true;
					log("ERROR file=" + file + " // " + exception.Message);
				}//catch
			}//for

			if (HasError)
			{
				Console.WriteLine("Press any knopka...");
				Console.ReadKey();

				Console.Title = "Has Errors";
				System.Threading.Thread.Sleep(50);
				IntPtr handle = FindWindow (null, Console.Title);
			}//if
			else
			{
				//Console.ReadKey();
			}//else
		}//function
	}
}
