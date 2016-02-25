using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dir2ssp
{
	public class Mp3
	{
		public string Pass { get; set; }
		public int Duration { get; set; }
		public string Name { get { return Path.GetFileNameWithoutExtension(Pass); } }

		const string mp3lib = "mp3*.txt";
		static Mp3[] Empty = new Mp3[0];



		public static Mp3[] GetLib()
		{
			string path = Directory.EnumerateFiles(Environment.CurrentDirectory, mp3lib).FirstOrDefault();
			return File.ReadAllLines(path)
				.TakeWhile(z => z.Trim().Any())
				.Select(z => Parse(z))
				.Where(z => z != null)
				.ToArray();
		}//function

		static Mp3 Parse(string s)
		{
			Mp3 result = null;
			string[] ss = s.Split('\t', ';');
			if (ss.Length == 2)
			{
				TimeSpan ts;
				if (TimeSpan.TryParseExact(ss[1], @"m\:ss", null, out ts))
				{
					result = new Mp3 { Pass = ss[0], Duration = (int)ts.TotalMilliseconds };
				}//if
			}//if
			return result;
		}//function
	}//class
}//namespace
