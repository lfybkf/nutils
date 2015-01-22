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
		List<Regex> rexs = new List<Regex>();
		IDictionary<string, string> content = new Dictionary<string, string>();

		public Form frm = null;
		public string Old;
		public string New = null;

		public bool IsMatch(string ext) { return Extensions.Contains(ext); }//function
		IEnumerable<TextBox> GetTextBoxes() { return frm.Controls.Cast<Control>().SelectMany(c => c.Controls.OfType<TextBox>()); } //function

		/// <summary>
		/// формировать New
		/// </summary>
		public void Do()
		{
			//забираем контент с формы
			content.Clear();
			foreach (var tb in GetTextBoxes())
			{
				if (string.IsNullOrWhiteSpace(tb.Text))
					continue;

				content.Add(tb.KeyFromName(), tb.Text);
			}//for

			#region frm
			if (frm is frmBook)
			{
				string Name = content.ContainsKey("Name") ? content["Name"] : null;
				string Author = content.ContainsKey("Author") ? content["Author"] : null;
				string Year = content.ContainsKey("Year") ? content["Year"] : null;
				if (string.IsNullOrWhiteSpace(Year) && string.IsNullOrWhiteSpace(Author))
					New = Name;
				else
				{
					if (string.IsNullOrWhiteSpace(Year))
						New = string.Format("{0} ({1})", Name, Author);
					else if (string.IsNullOrWhiteSpace(Author))
						New = string.Format("{0} ({1})", Name, Year);
					else
						New = string.Format("{0} ({1} {2})", Name, Year, Author);
				}//else
			}//if
			else if (frm is frmDistrib)
			{
				string Name = content.ContainsKey("Name") ? content["Name"] : null;
				string Version = content.ContainsKey("Version") ? content["Version"] : null;
				if (string.IsNullOrWhiteSpace(Version))
					New = "{0}_setup".fmt(Name);
				else
					New = "{0}_{1}_setup".fmt(Name, Version);
			}//if
			else if (frm is frmVideo)
			{
				if (content.ContainsKey("Year"))
					New = "{0} ({1})".fmt(content["Name"], content["Year"]);
				else
					New = content["Name"];
			}//if

			#endregion
		}//function

		public bool Parse()
		{
			Regex rex = rexs.FirstOrDefault(r => r.IsMatch(Old));

			if (rex == null)
				return false;

			var names = rex.GetGroupNames().ToList();
			Match m = rex.Match(Old);
			names.ForEach(s => content.Add(s, m.Groups[s].Value));

			if (frm != null)
			{
				foreach (var tb in GetTextBoxes()) { tb.Text = content.getValue(tb.Name.Substring(3)) ?? string.Empty; }//for
			}//if
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
			Ret.rexs.Add(new Regex(@"(?<Name>.*) [(](?<Year>[0-9]{4})[)]")); // Name (Year)
			Ret.rexs.Add(new Regex(@"(?<Name>.*)(?<Year>[0-9]{4}).*")); // NameYear
			#endregion

			return Ret;
		}//function

		public static Chooser createBook()
		{
			Chooser Ret = new Chooser();
			Ret.frm = new frmBook(); Ret.frm.setChooser(Ret);
			Ret.Extensions.Add(".chm");
			Ret.Extensions.Add(".djvu");
			Ret.Extensions.Add(".pdf"); 

			#region Regex
			Ret.rexs.Add(new Regex(@"(?<Name>.*) [(](?<Year>[0-9]{4}) (?<Author>.*)[)]")); // Name (Year Author)
			Ret.rexs.Add(new Regex(@"(?<Name>.*) [(](?<Year>[0-9]{4})[)]")); // Name (Year)
			Ret.rexs.Add(new Regex(@"(?<Name>.*) [(](?<Author>\D*)[)]")); // Name (Author)
			Ret.rexs.Add(new Regex(@"(?<Name>.*)(?<Year>[0-9]{4}).*")); // NameYear
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
			Ret.rexs.Add(new Regex(@"(?<Name>.*)_(?<Version>[0-9].*)_setup"));		// Name_Version_setup
			Ret.rexs.Add(new Regex(@"(?<Name>\D*)(?<Version>[0-9].*)_setup"));	// NameVersion_setup
			Ret.rexs.Add(new Regex(@"(?<Name>\D*)(?<Version>[0-9].*)"));		// NameVersion
			Ret.rexs.Add(new Regex(@"(?<Name>\D*)_setup"));			// Name_setup
			#endregion
			return Ret;
		}//function

	}//class
}//ns
