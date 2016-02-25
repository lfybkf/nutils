using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace Dir2ssp
{
	class Program_Dir2ssp
	{
		static Dictionary<string, int> Mp3s = new Dictionary<string, int>();

		static void Main(string[] args)
		{
			Paket.Init(Environment.CurrentDirectory);
			Paket.FillImages();
			Paket.MakeAll();
		}//func
	}//class
}//ns
