using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace move2dirOnBegin
{
	class Program
	{
		static void Main(string[] args)
		{
			Mover mover;
			if (args.Length == 0)
			{
				var dirs = Directory.EnumerateDirectories(Environment.CurrentDirectory);
				foreach (var dir in dirs)
				{
					mover = new Mover(Path.GetFileNameWithoutExtension(dir));
					mover.Move();
				}//for
				return;
			}//if

			string sBegin = args[0];
			if (sBegin == "HELP")
			{
				Console.WriteLine("run exe pattern");
				Console.WriteLine("folder with name \"pattern\" will be created");
				Console.WriteLine("all files, beginning with pattern, will be moved there");
				Console.WriteLine("press Enter to continue...");
				Console.ReadLine();
				return;
			}//if

			mover = new Mover(sBegin);
			mover.IsCreateDir = true;
			mover.Move();
			return;
		}
	}
}
