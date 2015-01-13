using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	class Chooser
	{
		List<string> Ext = new List<string>();
		public Form frm = null;
		public string Old;
		public string New = null;
		

		public static Chooser createVideo()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmVideo() { Tag = Ret };
			Ret.Ext.Add(".avi");	Ret.Ext.Add(".mkv");	Ret.Ext.Add(".mp4");
			return Ret;
		}//function
	}//class
}//ns
