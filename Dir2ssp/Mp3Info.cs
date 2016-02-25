using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dir2ssp
{
	public class Mp3Info
	{
		static string sOpenF = "open {0} type waveaudio alias Media";
		static string sLength = "status Media length";
		static string sClose = "close Media";

		[DllImport("winmm.dll")]
		private static extern long mciSendString(string strCommand, StringBuilder strReturn,
																						 int iReturnLength, IntPtr hwndCallback);
		
		public static int getDuration(string file)
		{
			long h;
			h = mciSendString(sOpenF.fmt(file), null, 0, IntPtr.Zero);

			StringBuilder sb = new StringBuilder(128);
			h = mciSendString(sLength, sb, 128, IntPtr.Zero);
			string sResult = sb.ToString();
			int result = Convert.ToInt32(sResult);

			h = mciSendString(sClose, null, 0, IntPtr.Zero);

			return result;
		}//function

		public static int getDurationNAudio(string file)
		{
			var reader = new NAudio.Wave.Mp3FileReader(file);
			TimeSpan duration = reader.TotalTime;

			return duration.Milliseconds;
		}//function
	}//class
}//namespace
