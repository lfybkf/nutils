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
			if (args.Length == 0)
			{
				Console.WriteLine("run exe pattern");
				Console.WriteLine("folder with name \"pattern\" will be created");
				Console.WriteLine("all files, beginning with pattern, will be moved there");
				Console.WriteLine("press Enter to continue...");
				Console.ReadLine();
				return;
			}//if
			string sBegin = args[0];
			Console.WriteLine("pattern = " + sBegin);

			//list files which match pattern
			IEnumerable<string> files = Directory.EnumerateFiles(Environment.CurrentDirectory)
				.Select(f => Path.GetFileName(f))
				.Where(f => f.StartsWith(sBegin))
				.ToArray();

			//create dir if need
			if (Directory.Exists(sBegin) == false)
			{
				Directory.CreateDirectory(sBegin);	
			}//if
			
			//move there
			string pathNew = null;
			foreach (var item in files)
			{
				pathNew = Path.Combine(sBegin, item);
				if (File.Exists(pathNew))
				{
					continue;
				}//if
				Console.WriteLine(item);
				File.Move(item, pathNew);
			}//for

			//Console.ReadLine();
		}
	}
}
