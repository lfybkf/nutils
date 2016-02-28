using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dir2ssp;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace UnitTestProject1
{
	[TestClass]
	public class UnitTestDir2ssp
	{
		public string mp3lib(string z)
		{
			int dur = (int)Mp3.GetMediaDuration(z);
			int m = dur / 60;
			int s = dur % 60;
			return string.Format("{0}\t{1:D2}:{2:D2}", z, m, s);
		}//function

		[TestMethod]
		public void TestLength()
		{
			int len;

			var mp3stereo = @"D:\Audio\Audio_Foreign\__All\Adamo - Tombe La Neige.mp3";
			len = (int)Mp3.GetMediaDuration(mp3stereo);

			var dir = @"D:\Audio\Audio_Foreign\__All\";
			var files = Directory.EnumerateFiles(dir, "*.mp3");
			File.WriteAllLines(@"C:\temp3.txt", files.Select(z => mp3lib(z)));

			len = (int)Mp3.GetMediaDuration(@"C:\train.mp3");
			return;
		}
	}
}
