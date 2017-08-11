using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;

namespace killProcess
{
	class Item { public int ID; public string Name;
		public static int C = 0;
		public Item(int id, string name) { ID = id;  Name = name; }
	}

	class Program_killProcess
	{
		static string pathToList = io.Directory.EnumerateFiles(Environment.CurrentDirectory)
			.FirstOrDefault(f => f.StartsWith("killProcess") && !f.EndsWith("exe"));
		static Item[] items = pathToList != null
			? io.File.ReadAllLines(pathToList).Select(f => new Item(Item.C++, f)).ToArray()
			: new Item[] { new Item(1, "chrome"), new Item(2, "skype") };

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
				foreach (var item in items)
				{
					Console.WriteLine($"{item.ID} - {item.Name}");
				}//for

				Console.Write("Program to kill: ");

				string arg = Console.ReadLine();
				int i;
				if (int.TryParse(arg, out i))
				{
					var item = items.FirstOrDefault(z => z.ID == i);
					if (item != null)
					{
						Console.WriteLine($"killing: {item.Name}");
						kill(item.Name);
					}//if
					else
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"no item in list with ID={i}");
					}//else
				}//if
				else
				{
					Console.WriteLine($"killing: {arg}");
					kill(arg);
				}//else
				
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
				try
				{
					p.Kill();
					p.WaitForExit(10000);
				}//try
				catch (Exception exception)
				{
					Console.WriteLine("    error = " + exception.Message.Substring(0, 30));
				}//catch

			}//for

		}//function
	}//class
}
