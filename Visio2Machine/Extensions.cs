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
	}//class

}
