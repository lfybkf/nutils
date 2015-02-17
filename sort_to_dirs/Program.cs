using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sort_to_dirs
{
	class Program
	{
		static string bat = "sort_to.bat";
		static List<string> lines = new List<string>();
		static void Main(string[] args)
		{
			IEnumerable<string> files = Directory.EnumerateFiles(Environment.CurrentDirectory)
				.Select(f => Path.GetFileName(f))
				.Where(f => f.StartsWith("sort_to") == false)
				.ToArray();

			var rex = new Regex(@"(?<Pref>.*)[_](?<Num>[0-9]*)"); // Prefix_Num
			IEnumerable<string> groups = files.Where(s => rex.IsMatch(s))
				.Select(s => rex.Match(s).Groups["Pref"].Value)
				.Distinct()
				.OrderBy(s => s)
				.ToArray();

			foreach (var grp in groups)
			{
				lines.Add("mkdir " + grp);
				foreach (var file in files.Where(s => s.StartsWith(grp)))
				{
					lines.Add(string.Format(@"move {0} {1}\", file, grp));
				}//for
			}//for

			File.AppendAllLines(bat, lines);
		}//function
	}//class
}//ns
