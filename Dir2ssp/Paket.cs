using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dir2ssp
{
	public class Paket
	{
		static string[] no_images = new string[0];
		static int[] transitions = { 1, 4, 25, 26, 27, 28, 29, 30 };
		static Random rand = new Random();
		static string Name;
		static string[] allImages;
		static Mp3[] allMp3s;
		static int ItemDuration;
		public static Paket[] Pakets;

		public static void Init(string dir)
		{
			Name = Path.GetFileNameWithoutExtension(dir);

			#region get media
			allImages = Directory.EnumerateFiles(dir).Where(z => z.EndsWith("jpg") || z.EndsWith("png")).ToArray();
			allMp3s = Mp3.GetLib();
			#endregion

			#region create pakets
			Pakets = allMp3s.Select(z => 
				new Paket { Music = z }).ToArray();
			#endregion
		}//function

		string[] Images;
		public Mp3 Music;
		public int Duration { get { return Music.Duration; } }
		int Count;

		public static void FillImages()
		{
			if (Pakets.Any() == false || allImages.Any() == false)
			{
				return;
			}//if

			#region counts
			int TotalDuration = Pakets.Sum(z => z.Duration);
			int TotalCount = allImages.Count();
			ItemDuration = TotalDuration / TotalCount;
			foreach (var paket in Pakets)
			{
				paket.Count = paket.Duration / ItemDuration;
			}//for
			int nevyazka = TotalCount - Pakets.Sum(z => z.Count);
			Pakets.Last().Count += nevyazka;
			#endregion

			#region images
			int skip = 0;
			foreach (var paket in Pakets)
			{
				paket.Images = allImages.Skip(skip).Take(paket.Count).ToArray();
				skip += paket.Count;
			}//for
			#endregion
		}//function

		static XElement makeImg(string s)
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

		public static void MakeAll()
		{
			foreach (var item in Pakets)
			{
				item.Make();
			}//for
		}//function

		bool Make()
		{
			if (Images.Any() == false) { return false; }//if

			#region xml header
			XDeclaration xdecl = new XDeclaration("1.0", "utf-16", "yes");
			//XDocumentType xdtd = new XDocumentType("SSProject", null, null, null);
			XDocument xdoc = new XDocument(xdecl);
			#endregion

			#region xml content
			XElement xroot = new XElement("SSProject"
				, new XAttribute("Version", "1")
				, new XAttribute("Count", Images.Count())
				, new XAttribute("Duration", Duration)
				, new XAttribute("ItemDuration", ItemDuration)
				, new XElement("VideoStream"
					, new XAttribute("VideoDimension", 10)
					, Images.Take(1).Select(s => makeImg(s))
					, Images.Skip(1).SelectMany(s => makeImgAndTrans(s))
					)
				, new XElement("AudioStream"
					, new XElement("Fragment"
						, new XAttribute("DurationMs", Duration)
						, new XAttribute("SPACWI", 0)
							, new XElement("Src", Music.MediaFile)
					))
				);
			#endregion

			#region xml save
			xdoc.Add(xroot);
			xdoc.Save("{0}_{1}.ssp".fmt(Name, Music.Name));
			#endregion

			return true;
		}//function
	}//class
}//namespace
