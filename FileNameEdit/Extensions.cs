using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	public static class Extensions
	{
		public static void setChooser(this Form frm, Chooser chooser)  {  frm.Tag = chooser;  }//function
		public static Chooser getChooser(this Form frm) { return (frm.Tag as Chooser); }//function
		public static void setRusLanguageOnEnter(this TextBox tb)	{	tb.setLanguageOnEnter("ru-RU");	}//function
		public static void setEngLanguageOnEnter(this TextBox tb) { tb.setLanguageOnEnter("en-US"); }//function
		public static string fmt(this string s, params string[] ss) { return string.Format(s, ss); }//function

		public static void setLanguageOnEnter(this TextBox tb, string Culture)
		{
			EventHandler enter = (sender, e) =>
			{
				TextBox ctl = (sender as TextBox);
				if (ctl != null)
					InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo(Culture));
			};
			tb.Enter += enter;
		}//function

		
		public static string getValue(this IDictionary<string, string> dict, string key)
		{
			if (dict.ContainsKey(key))
				return dict[key];
			else
				return null;
		}//function

	}//class
}//ns
