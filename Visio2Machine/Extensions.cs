using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IVisio = Microsoft.Office.Interop.Visio;

namespace Visio2Machine
{
	public static class Extensions
	{
		public static string fmt(this string s, params object[] oo) { return string.Format(s, oo); }
		public static bool notEmpty(this string s) { return !string.IsNullOrWhiteSpace(s); }
		public static bool isEmpty(this string s) { return string.IsNullOrWhiteSpace(s); }
		public static string[] splitLine(this string s) { return s.Split(Environment.NewLine.ToCharArray()); }
		public static string[] splitCSV(this string s) { return s.Split(';'); }
		public static IEnumerable<string> splitValue(this string s) { return s.Split(' ', ',').Where(z => z.notEmpty()); }
		public static string after(this string s, string Prefix)
		{
			if (s.isEmpty()) return string.Empty;
			int i = s.IndexOf(Prefix);
			if (i >= 0)
				return s.Substring(i + Prefix.Length);
			else
				return string.Empty;
		}//func

		public static string before(this string s, string Suffix)
		{
			if (s.isEmpty()) return string.Empty;
			int i = s.IndexOf(Suffix);
			if (i > 0)
				return s.Substring(0, i);
			else
				return string.Empty;
		}//func

		public static string midst(this string s, string Prefix, string Suffix)
		{
			if (s.isEmpty()) return string.Empty;
			int iPrefix = s.IndexOf(Prefix);
			int iSuffix = s.IndexOf(Suffix);
			if (iPrefix >= 0 && iSuffix > 0)
				return s.Substring(iPrefix + Prefix.Length, iSuffix - iPrefix - Prefix.Length);
			else if (iPrefix >= 0)
				return s.Substring(iPrefix + Prefix.Length);
			else if (iSuffix >= 0)
				return s.Substring(0, iSuffix);
			else
				return string.Empty;
		}//func


		public static void writeToFile(this IEnumerable<string> ss, string file, bool append = true) 
		{ 
			if (append)
			{
				File.AppendAllLines(file, ss, Encoding.Default);
			}//if
			else
			{
				File.WriteAllLines(file, ss, Encoding.Default);
			}//else
			
		}//function

		public static TResult with<TSource, TResult>(this TSource source, Func<TSource, TResult> func)
		where TSource : class
		{
			if (source != default(TSource)) { return func(source); }//if
			else { return default(TResult); }//else
		}//function

		public static TResult with<TSource, TResult>(this TSource source, Func<TSource, TResult> func, TResult defaultValue)
		where TSource : class
		{
			if (source != default(TSource)) { return func(source); }//if
			else { return defaultValue; }//else
		}//function

		public static TSource execute<TSource>(this TSource source, Action<TSource> action) where TSource : class
		{
			if (source != default(TSource)) { action(source); } return source;
		}//function

		public static void execute<TSource>(this Action<TSource> action, TSource source)
		{
			if (action != null) { action(source); }
		}//function

		public static IEnumerable<T> forEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
				return null;

			if (action == null)
				return source;

			foreach (var item in source)
				action(item);

			return source;
		}//function

		public static T get<T>(this IEnumerable<T> source, int index, T defValue) where T:class
		{
			if (source == null)
				return defValue;

			T result = source.ElementAtOrDefault(index);
			return result ?? defValue;
		}//function

		/// <summary>
		/// вертает Value или default(TValue)
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
		{
			return dict.ContainsKey(key) ? dict[key] : default(TValue);
		}//function

		/// <summary>
		/// вертает Value или defaultValue
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static TValue get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
		{
			return dict.ContainsKey(key) ? dict[key] : defaultValue;
		}//function


		public static string CellResult(this IVisio.Shape shape, string name)
		{
			short i = shape.CellExistsU[name, 0];
			if (i > 0)
			{
				return shape.CellsU[name].get_ResultStr(IVisio.VisUnitCodes.visNoCast);
			}//if
			else
				return null;
		}//func

		public static string CellFormula(this IVisio.Shape shape, string name)
		{
			//return shape.CellsU[name].FormulaU;

			short i = shape.CellExistsU[name, 0];
			if (i != 0)
			{
				return shape.CellsU[name].FormulaU;
			}//if
			else
				return null;
		}//func


		/// <summary>
		/// from "asd{name}qwer{age}ty" get 3,"{name}" ;14,"{age}"
		/// </summary>
		/// <param name="s"></param>
		/// <param name="cL"></param>
		/// <param name="cR"></param>
		/// <returns></returns>
		internal static IDictionary<int, string> getPlaceholders(this string s, char cL = '{', char cR = '}')
		{
			if (s.IndexOf(cL) < 0) { return null; }//if

			IDictionary<int, string> result = new Dictionary<int, string>();
			char c;
			int iL = -1;
			// "asd{ph}qwerty"
			for (int i = 0; i < s.Length; i++)
			{
				c = s[i];
				if (c == cL)
				{
					iL = i;//i=3
				}//if
				else if (c == cR && iL >= 0) //i=6
				{
					result.Add(iL, s.Substring(iL, i - iL + 1));
					iL = -1;
				}//if
			}//for

			return result;
		}//func


		static string THIS = "this";
		public static string fmto(this string s, object o)
		{
			var phS = s.getPlaceholders();
			if (phS == null) { return s; }

			Type t = o.GetType();
			IDictionary<string, string> values = new Dictionary<string, string>();
			foreach (var item in phS.Values.Distinct())
			{
				values.Add(item
					, o.getPropertyValue(item.Substring(1, item.Length - 2))); //2="{}".Length
			}//for

			string sPh;
			StringBuilder sb = new StringBuilder();
			int iPast = 0;//end of previous placeholder
			foreach (int iPh in phS.Keys)
			{
				sb.Append(s.Substring(iPast, iPh - iPast)); //before Ph
				sPh = phS[iPh]; //"{name}"
				sb.Append(values[sPh] ?? string.Empty);
				iPast = iPh + sPh.Length;
			}//for
			sb.Append(s.Substring(iPast));

			return sb.ToString();
		}//function

		public static string getPropertyValue(this object o, string fieldName)
		{
			if (fieldName == THIS)
			{
				return o.ToString();
			}//if

			Type t = o.GetType();
			System.Reflection.PropertyInfo fi = t.GetProperty(fieldName);
			if (fi == null)
			{
				return null;
			}//if

			return fi.GetValue(o).ToString();
		}//function
	}//class

}
