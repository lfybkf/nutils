using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace killProcess
{
	class Program_killProcess
	{
		static void Main(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.ForegroundColor = ConsoleColor.Yellow;

			if (args.Length > 0)
			{
				foreach (string s in args) { kill(s); }//for
			}//if
			else
			{
				Console.Write("Program to kill: ");
				string arg = Console.ReadLine();
				kill(arg);
			}//else
		}//function

		static void kill(string pname)
		{
			var plist = Process.GetProcessesByName(pname);
			Console.WriteLine("Want to kill " + pname);
			Console.WriteLine("  plist.Count = " + plist.Length);
			foreach (var p in plist)
			{
				Console.WriteLine("    killing = " + p.MainWindowTitle);
				p.Kill();
				p.WaitForExit(10000);
			}//for
		}//function
	}//class
}
