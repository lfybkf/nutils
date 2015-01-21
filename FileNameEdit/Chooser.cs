using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	public class Chooser
	{
		List<string> Extensions = new List<string>();
		Dictionary<Regex, Action<Regex, Chooser>> rexs = new Dictionary<Regex, Action<Regex, Chooser>>();
		public IDictionary<string, string> parse = new Dictionary<string, string>();
		public Form frm = null;
		public string Old;
		public string New = null;

		public bool IsGoodExtension(string ext) { return Extensions.Contains(ext); }//function

		public bool Parse()
		{
			Regex rex = rexs.Keys.FirstOrDefault(r => r.IsMatch(Old));
			if (rex == null)
				return false;
			else
				rexs[rex](rex, this);

			return true;
		}//function

		public static Chooser createVideo()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmVideo(); Ret.frm.setChooser(Ret);
			Ret.Extensions.Add(".avi");	
			Ret.Extensions.Add(".mkv");	
			Ret.Extensions.Add(".mp4");
			Ret.Extensions.Add(".mpg");

			#region Regex
			Ret.rexs.Add(new Regex(@"(.*) [(]([0-9]{4})[)]") // Name (Year)
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Year", m.Groups[2].Value);
				});
			Ret.rexs.Add(new Regex(@"(.*)([0-9]{4}).*") // NameYear
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Year", m.Groups[2].Value);
				});
			#endregion

			return Ret;
		}//function

		public static Chooser createBook()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmBook(); Ret.frm.setChooser(Ret);
			Ret.Extensions.Add(".chm");
			Ret.Extensions.Add(".pdf"); 
			Ret.Extensions.Add(".djvu");

			#region Regex
			Ret.rexs.Add(new Regex(@"(.*) [(]([0-9]{4}) (.*)[)]") // Name (Year Author)
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Year", m.Groups[2].Value);
					c.parse.Add("Author", m.Groups[3].Value);
				});
			Ret.rexs.Add(new Regex(@"(.*) [(]([0-9]{4})[)]") // Name (Year)
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Year", m.Groups[2].Value);
				});
			Ret.rexs.Add(new Regex(@"(.*) [(](\D*)[)]") // Name (Author)
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Author", m.Groups[2].Value);
				});

			#endregion

			return Ret;
		}//function

		public static Chooser createDistrib()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmDistrib(); Ret.frm.setChooser(Ret);
			Ret.Extensions.Add(".exe");
			Ret.Extensions.Add(".msi");

			#region Regex
			Ret.rexs.Add(new Regex(@"(.*)_([0-9].*)_setup")		// Name_Version_setup
				, (r, c) => 
				{ 
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Version", m.Groups[2].Value);
				});
			Ret.rexs.Add(new Regex(@"(\D*)([0-9].*)_setup")		// NameVersion_setup
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Version", m.Groups[2].Value);
				});
			Ret.rexs.Add(new Regex(@"(\D*)([0-9].*)")		// NameVersion
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
					c.parse.Add("Version", m.Groups[2].Value);
				});
			Ret.rexs.Add(new Regex(@"(\D*)_setup")			// Name_setup
				, (r, c) =>
				{
					Match m = r.Match(c.Old);
					c.parse.Add("Name", m.Groups[1].Value);
				});
			#endregion
			return Ret;
		}//function
	}//class
}//ns
