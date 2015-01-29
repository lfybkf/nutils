using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		public static string KeyFromName(this TextBox tb) { return tb.Name.Substring(3); }//function
		public static string fmt(this string s, params string[] ss) { return string.Format(s, ss); }//function
		public static string setTo(this string what, string where) { return string.Format(where, what); }//function

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

		/// <summary>
		/// достает из строки одну группу по паттерну
		/// </summary>
		/// <param name="s"></param>
		/// <param name="patern"></param>
		/// <returns></returns>
		public static string regOne(this string s, string patern) { return new Regex(patern).Match(s).Groups[1].Value; }//function
	}//class
}//ns
