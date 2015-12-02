using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dir2ssp
{
	public static class Extensions
	{
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }
	}//class
}//namespace
