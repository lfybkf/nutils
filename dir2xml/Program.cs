using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace dir2xml
{
	class Program
	{
		static string Dir = Environment.CurrentDirectory;
		static bool bAll = false;

		static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				foreach (string s in args)
				{
					Dir = Directory.Exists(s) ? s : Dir;
					bAll = (s == "-all") ? true : bAll;
				}//for
				
			}//if

			DirContent DC = new DirContent();
			DC.Prepare();
			DC.Run(Dir, bAll);
			DC.RunStat();
			DC.Save(string.Empty);
		}//func
	}//class

	class DirContent
	{
		DataSet DS = new DataSet("ROOT");

#region static
		static string fileSave = "dir2xml.xml";
		static string sExclude = "dir2xml";

		static string sFile = "File";
		static string sDirectory = "Directory";
		static string sStat = "Stat";

		static string cPath = "Path";
		static string cName = "Name";
		static string cDir = "Dir";
		static string cExt = "Ext";
		static string cKey = "Key";
		static string cValue = "Value";
#endregion

		public void Prepare()
		{
			MakeStruct(sFile);
			MakeStruct(sDirectory);
			MakeStruct(sStat);
		}//func

		void MakeStruct(string TableName)
		{
			DataTable dt = new DataTable(TableName);
			if (TableName == sFile)
			{
				dt.Columns.Add(cPath, typeof(string));
				dt.Columns.Add(cDir, typeof(string));
				dt.Columns.Add(cName, typeof(string));
				dt.Columns.Add(cExt, typeof(string));
			}//if
			if (TableName == sDirectory)
			{
				dt.Columns.Add(cPath, typeof(string));
				dt.Columns.Add(cDir, typeof(string));
				dt.Columns.Add(cName, typeof(string));
			}//if
			if (TableName == sStat)
			{
				dt.Columns.Add(cKey, typeof(string));
				dt.Columns.Add(cValue, typeof(string));
			}//if
			DS.Tables.Add(dt);
		}//func

		public void RunStat()
		{
			DataTable dt;
			dt = DS.Tables[sStat];
			dt.Rows.Add(sFile, DS.Tables[sFile].Rows.Count);
			dt.Rows.Add(sDirectory, DS.Tables[sDirectory].Rows.Count);
			dt.Rows.Add("Now", DateTime.Now.ToString("G"));
		}//func

		public void Run(string Dir, bool bAll)
		{
			DataRow dr;
			DataTable dt;
			dt = DS.Tables[sFile];
			var files = Directory.GetFiles(Dir);

			foreach (string path in files)
			{
				dr = dt.NewRow();
				dr[cPath] = path;
				dr[cDir] = Path.GetDirectoryName(path);
				dr[cName] = Path.GetFileNameWithoutExtension(path);
				dr[cExt] = Path.GetExtension(path);
				if (dr.Field<string>(cName).StartsWith(sExclude) == false)
					dt.Rows.Add(dr);
			}//for

			dt = DS.Tables[sDirectory];
			var dirs = Directory.GetDirectories(Dir);
			foreach (string path in dirs)
			{
				dr = dt.NewRow();
				dr[cPath] = path;
				dr[cDir] = Path.GetDirectoryName(path);
				dr[cName] = Path.GetFileName(path);
				dt.Rows.Add(dr);
			}//for

			if (bAll)
			{
				RunDirs();
			}//if

		}//func

		void RunDirs()
		{
			DataTable dt = DS.Tables[sDirectory];
			if (dt.Rows.Count == 0)
				return;

			List<string> Dirs = new List<string>(dt.Rows.Count);
			foreach (DataRow dr2 in dt.Rows)
			{
				Dirs.Add((string)dr2[cPath]);
			}//for
			dt.Rows.Clear();
			dt.AcceptChanges();

			foreach (string s in Dirs)
			{
				Run(s, true);
			}//for
		}//func

		public void Save(string file)
		{
			if (file != string.Empty)
				DS.WriteXml(file);
			else
				DS.WriteXml(fileSave);
		}//func
	}//class
}//ns
