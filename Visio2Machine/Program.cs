using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visio2Machine
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1) { Console.WriteLine("Usage: exe file.vsd"); return; }
			string input = args[0];

			Visio V = new Visio(input);
			if (V.Open())
			{
				V.Close();	
			}//if
			else
			{
				Console.WriteLine(V.Error);
				Console.ReadKey();
			}//else
		}//function
	}//class
}//ns

/*
 http://www.codeproject.com/Articles/109558/Creating-VISIO-Organigrams-using-C
 */