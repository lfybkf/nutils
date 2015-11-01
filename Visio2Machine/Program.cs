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
			try
			{
				V.Open();
			}
			catch (Exception exc)
			{
				(new[] { V.Error, exc.Message }).writeToFile("output.log");
			}
			finally
			{
				V.Close();
			}

			try
			{
				V.PrintStat();
				V.ExportMachine();
			}//try
			catch (Exception exc)
			{
				(new[] { V.Error, exc.Message }).writeToFile("output.log");				
			}//catch
		}//function
	}//class
}//ns

/*
 http://www.codeproject.com/Articles/109558/Creating-VISIO-Organigrams-using-C
 */