using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRandom
{
	class Program
	{
		static void Main(string[] args)
		{
			string baseDir = Environment.CurrentDirectory;
			string prefix = args.Length > 0 ? args[0] 
				: baseDir.Split(Path.DirectorySeparatorChar).Last();
			Random rand = new Random();
			string newName;

			var files = Directory.GetFiles(baseDir)
				.Where(f => f.EndsWith(".exe")==false); //программы не переименоваем
			foreach (var file in files)
			{
				newName = string.Format("{0}_{1:D7}{2}", prefix, rand.Next(9999999), 
					Path.GetExtension(file));
				if (File.Exists(newName) == false)
					File.Move(file, newName);
			}//for
		}//function
	}//class
}
