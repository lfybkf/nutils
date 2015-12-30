using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace move2dirOnBegin
{
	public class Mover
	{
		public static Action<string> log = Console.WriteLine;
		string sBegin;
		public string dir = Environment.CurrentDirectory;
		public bool IsCreateDir = false;

		public Mover(string beg)
		{
			this.sBegin = beg;
		}//constructor

		public void Move()
		{
			log("pattern = " + sBegin);
			
			//list files which match pattern
			IEnumerable<string> files = Directory.EnumerateFiles(dir)
				.Select(f => Path.GetFileName(f))
				.Where(f => f.StartsWith(sBegin))
				.ToArray();

			//create dir if need
			if (IsCreateDir && Directory.Exists(sBegin) == false)
			{
				Directory.CreateDirectory(sBegin);
			}//if

			if (Directory.Exists(sBegin) == false)
			{
				log("no directory " + sBegin);
				return;
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
				log(item);
				File.Move(item, pathNew);
			}//for

		}//function
	}//class
}//namespace
