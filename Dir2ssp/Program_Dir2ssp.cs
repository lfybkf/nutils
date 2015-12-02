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
		static void Main(string[] args)
		{
			int[] transitions = { 1, 4, 25, 26, 27, 28, 29, 30 };
			Random rand = new Random();
			string pathImg = Environment.CurrentDirectory;
			string Name = Path.GetFileNameWithoutExtension(pathImg);
			string[] filesImg = Directory.GetFiles(pathImg, "*.jpg");

			#region xml header
			XDeclaration xdecl = new XDeclaration("1.0", "utf-16", "yes");
			//XDocumentType xdtd = new XDocumentType("SSProject", null, null, null);
			XDocument xdoc = new XDocument(xdecl);
			#endregion

			#region functions
			Func<string, XElement> makeImg = (s) =>
			{
				return new XElement("Fragment"
					, new XAttribute("TmFragmentType", 0)
					, new XAttribute("DurationMs", 5000)
					, new XAttribute("ZoomType", 0)
					, new XAttribute("BackColor", 0)
					, new XElement("Src", new XText(s))
					);
			};

			Func<int, XElement> makeTrans = (i) =>
			{
				return new XElement("Fragment"
					, new XAttribute("TmFragmentType", 1)
					, new XAttribute("DurationMs", 1000)
					, new XAttribute("TransType", i)
					);
			};

			Func<string, IEnumerable<XElement>> makeImgAndTrans = (s) =>
			{
				int trans = transitions.ElementAt(rand.Next(0, transitions.Length));
				return new XElement[] { makeTrans(trans), makeImg(s) };
			};
			#endregion

			#region xml content
			XElement xroot = new XElement("SSProject"
				,	new XAttribute("Version", "1")
				, new XElement("VideoStream"
					, new XAttribute("VideoDimension", 10)
					, filesImg.Take(1).Select(s => makeImg(s))
					, filesImg.Skip(1).SelectMany(s => makeImgAndTrans(s))
					)
				, new XElement("AudioStream")
				);
			#endregion

			#region xml save
			xdoc.Add(xroot);
			xdoc.Save("{0}.ssp".fmt(Name));
			#endregion
		}
	}
}
