using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io = System.IO;

namespace Visio2Machine
{
	class Program
	{
		static void Main(string[] args)
		{
			string logFile = "output.log";
			string input = args.FirstOrDefault();
			if (input.isEmpty()) 
			{ 
				var filesVisio = io.Directory.GetFiles(Environment.CurrentDirectory, "*.vsd*");
				input = filesVisio.FirstOrDefault();
			}//if
			if (input.isEmpty())
			{
				(new[] { "Put some Visio files here, or point the path" }).writeToFile(logFile);
				return;
			}//if

			Visio V = new Visio(input);
			try
			{
				V.Open();
			}
			catch (Exception exc)
			{
				(new[] { V.Error, exc.Message }).writeToFile(logFile);
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
				(new[] { V.Error, exc.Message }).writeToFile(logFile);
			}//catch
		}//function
	}//class
}//ns
