using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	public class Chooser
	{
		List<string> Extensions = new List<string>();
		public Form frm = null;
		public string Old;
		public string New = null;

        public bool IsGoodExtension(string ext) { return Extensions.Contains(ext); }//function

		public static Chooser createVideo()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmVideo(); Ret.frm.setChooser(Ret);
			Ret.Extensions.Add(".avi");	
            Ret.Extensions.Add(".mkv");	
            Ret.Extensions.Add(".mp4");
			return Ret;
		}//function

        public static Chooser createBook()
        {
            Chooser Ret = new Chooser();
            Ret.frm = new frmBook(); Ret.frm.setChooser(Ret);
            Ret.Extensions.Add(".pdf"); 
            Ret.Extensions.Add(".djvu"); 
            return Ret;
        }//function
	}//class
}//ns
