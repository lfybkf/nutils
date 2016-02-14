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
		static int[] transitions = { 1, 4, 25, 26, 27, 28, 29, 30 };
		static Random rand = new Random();
		static string Name;
		static Dictionary<int, string[]> packs = new Dictionary<int, string[]>();

		static void Main(string[] args)
		{
			string pathImg = Environment.CurrentDirectory;
			Name = Path.GetFileNameWithoutExtension(pathImg);
			string[] filesImg = Directory.GetFiles(pathImg, "*.jpg");
			int portion = Convert.ToInt32(args.ElementAtOrDefault(0) ?? "10000");

			#region make packs
			int count = filesImg.Length;
			int nOfPacks = count/portion + 1;
			if (nOfPacks == 1)
			{
				packs[0] = filesImg;
			}//if
			else
			{
				for (int i = 0; i < nOfPacks; i++)
				{
					//если остаток маленький, пишем его в предыдущий пакет
					if (i == nOfPacks - 2 && (count % portion < portion / 2))
					{
						packs[i] = filesImg.Skip(i * portion).ToArray();
						break;
					}//if

					packs[i] = filesImg.Skip(i * portion).Take(portion).ToArray();
				}//for
			}//else
			#endregion

			#region make files
			foreach (int num in packs.Keys)
			{
				MakeSSP(num, packs[num]);
			}//for
			#endregion
			
		}//func

		static XElement makeImg (string s)
		{
			return new XElement("Fragment"
				, new XAttribute("TmFragmentType", 0)
				, new XAttribute("DurationMs", 5000)
				, new XAttribute("ZoomType", 0)
				, new XAttribute("BackColor", 0)
				, new XElement("Src", new XText(s))
				);
		}//function

		static XElement makeTrans(int i)
		{
			return new XElement("Fragment"
				, new XAttribute("TmFragmentType", 1)
				, new XAttribute("DurationMs", 1000)
				, new XAttribute("TransType", i)
				);
		}//function

		static IEnumerable<XElement> makeImgAndTrans(string s)
		{
			int trans = transitions.ElementAt(rand.Next(0, transitions.Length));
			return new XElement[] { makeTrans(trans), makeImg(s) };
		}//function

		static bool MakeSSP(int num, IEnumerable<string> files)
		{
			if (files.Any() == false)	{return false;}//if

			#region xml header
			XDeclaration xdecl = new XDeclaration("1.0", "utf-16", "yes");
			//XDocumentType xdtd = new XDocumentType("SSProject", null, null, null);
			XDocument xdoc = new XDocument(xdecl);
			#endregion

			#region xml content
			XElement xroot = new XElement("SSProject"
				, new XAttribute("Version", "1")
				, new XAttribute("Count", files.Count())
				, new XElement("VideoStream"
					, new XAttribute("VideoDimension", 10)
					, files.Take(1).Select(s => makeImg(s))
					, files.Skip(1).SelectMany(s => makeImgAndTrans(s))
					)
				, new XElement("AudioStream")
				);
			#endregion

			#region xml save
			xdoc.Add(xroot);
			xdoc.Save("{0}{1:D5}.ssp".fmt(Name, num+1));
			#endregion

			return true;
		}//function
	}//class

}//ns
