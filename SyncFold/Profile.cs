using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BDB;

namespace SyncFold
{
	static class Log 
	{
		static IList<string> data = new List<string>();
		static string fmtCommon = "{0}: {1}: {2}";
		public static void logInfo(string msg, string src = "COMMON") 
		{
			data.Add(fmtCommon.fmt(DateTime.Now.ToShortTimeString(), src, msg));
		}
		//public static string COMMON = "COMMON";

	}//class

	public class Profile
	{
		public static Action<string> log;
		static DriveInfo[] drives = DriveInfo.GetDrives().Where(z => z.IsReady).ToArray();
		static string[] EmptyStrings = new string[0];

		string Name;
		string SrcVolume;
		string SrcFolder;
		string DstVolume;
		string DstFolder;
		bool IsActive = false;
		string Error = string.Empty;
		public List<string> filesAdd = new List<string>();
		public List<string> filesDel = new List<string>();

		private void Read(XElement src)
		{
			Name = src.Attribute("Name").Value;
			SrcVolume = src.Attribute("SrcVolume").Value;
			DstVolume = src.Attribute("DstVolume").Value;
			SrcFolder = src.Attribute("SrcFolder").Value;
			DstFolder = src.Attribute("DstFolder").Value;

			#region test drive
			Func<string, DriveInfo> getOnVolume = (vol) =>
			{
				return drives.FirstOrDefault
					(z => z.VolumeLabel.Equals(vol, StringComparison.InvariantCultureIgnoreCase));
			};
			
			DriveInfo dr;
			if ((dr = getOnVolume(SrcVolume)) == null)
			{
				Error = "No drive for SrcVolume={0}".fmt(SrcVolume);
				return;
			}//if
			else
			{
				SrcFolder = Path.Combine(dr.RootDirectory.FullName, SrcFolder);
			}//else

			if ((dr = getOnVolume(DstVolume)) == null)
			{
				Error = "No drive for DstVolume={0}".fmt(DstVolume);
				return;
			}//if
			else
			{
				DstFolder = Path.Combine(dr.RootDirectory.FullName, DstFolder);
			}//else
			#endregion

			#region test folders
			if (!Directory.Exists(SrcFolder))
			{
				Error = "No SrcFolder = {0}".fmt(SrcFolder);
				return;
			}//if
			if (!Directory.Exists(DstFolder))
			{
				Error = "No DstFolder = {0}".fmt(DstFolder);
				return;
			}//if
			#endregion

			#region test files
			Func<IEnumerable<string>, string, int, bool> isEndHere = (list, s, lend) =>
				{
					int len1, len2 = s.Length;
					bool isEqual;
					foreach (var item in list)
					{
						len1 = item.Length;
						isEqual = true;
						//D:\\Temp\\Windows.torrent
						//i=1 -> 't', i=lend-1 -> 'W'
						for (int i = 1; i < lend; i++)   
						{
							if (item[len1 - i] != s[len2 - i])
							{
								isEqual = false;
								break;
							}//if
						}//for
						
						if (isEqual)
						{
							return true;
						}//if
					}//for
					return false;
				};

			var filesSrc = getFiles(SrcFolder);
			var filesDst = getFiles(DstFolder);
			int lSrc = SrcFolder.Length;
			int lDst = DstFolder.Length;
			filesAdd = filesSrc.Where(z => !isEndHere(filesDst, z, z.Length - lSrc)).ToList();
			filesDel = filesDst.Where(z => !isEndHere(filesSrc, z, z.Length - lDst)).ToList();

			if (!filesAdd.Any() && !filesDel.Any())
			{
				Error = "No files to sync";
				return;
			}//if
			#endregion

			IsActive = true;
		}//function

		private static bool Copy(string from, string to)
		{
			try
			{
				File.Copy (from, to);
				return true;
			}//try
			catch (Exception exception)
			{
				log.execute(exception.Message);
				return false;
			}//catch
		}//function

		public async Task CopyAsync(IProgress<string> progress)
		{
			Action run = () =>
				{
					string src, dst;
					string item;
					while ((item = filesAdd.FirstOrDefault()) != null )
					{
						src = item.after(SrcFolder);
						dst = DstFolder + src;
						progress.Report(item);
						if (Copy(item, dst))
						{
							filesAdd.Remove(item);
							progress.Report(item);
						}//if
					}//for
				};
			await Task.Run(run);
		}//function

		public bool Delete(string s)
		{
			try
			{
				if (filesDel.Contains(s) == false)
				{
					log.execute("{0} - isn't in list for delete");
					return false;
				}//if
				File.Delete(s);
				filesDel.Remove(s);
				return true;
			}//try
			catch (Exception exception)
			{
				log.execute(exception.Message);
				return false;
			}//catch
		}//function

		private static IEnumerable<string> getFiles(string folder, string pattern = "*.*")
		{
			if (folder.isEmpty()) { return EmptyStrings; }
			return Directory.GetFiles(folder, pattern, SearchOption.AllDirectories);
		}

		public static IEnumerable<Profile> LoadAll()
		{
			IList<Profile> result = new List<Profile>();
			XDocument xdoc = XDocument.Load("SyncFold.xml");
			Profile prof;
			foreach (var item in xdoc.Root.Elements("Profile"))
			{
				prof = new Profile();
				prof.Read(item);
				result.Add(prof);
			}//for

			return result.Where(z => z.IsActive).ToArray();
		}//function

		public override string ToString()
		{
			if (IsActive)
			{
				return Name;
			}//if
			else
			{
				return "{0} ({1})".fmt(Name, Error);	
			}//else
		}
	}//class
}//namespace
