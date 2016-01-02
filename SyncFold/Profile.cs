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
		public List<string> filesRefresh = new List<string>();
		public List<string> filesDel = new List<string>();

		static List<Profile> _profiles = new List<Profile>();
		public static int Count { get { return _profiles == null ? 0 : _profiles.Count; } }
		public static IList<Profile> activeProfiles = new List<Profile>();

		private void Read(XElement src)
		{
			Name = src.Attribute("Name").Value;
			SrcVolume = src.Attribute("SrcVolume").Value;
			DstVolume = src.Attribute("DstVolume").Value;
			SrcFolder = src.Attribute("SrcFolder").Value;
			DstFolder = src.Attribute("DstFolder").Value;
		}//function

		public bool zIsGoodToAdd(string file)
		{
			return true;
		}//function

		private static bool zIsEndEqual(string s1, string s2, int len)
		{
			int l1 = s1.Length;
			int l2 = s2.Length;
			for (int i = 1; i < len; i++)
			{
				if (s1[l1 - i] != s2[l2 - i])
				{
					return false;
				}//if
			}//for
			return true;
		}//function

		public void Load()
		{
			IsActive = false;

			#region test drive
			Func<string, DriveInfo> getOnVolume = (vol) =>
			{
				return drives.FirstOrDefault
					(z => z.VolumeLabel.Equals(vol, StringComparison.InvariantCultureIgnoreCase));
			};
			Func<string, string, string> driveFolder = (root, folder) =>
			{
				//still "fold\subfold", not "drive:\fold\subfold"
				if (Directory.Exists(folder) == false)
					return Path.Combine(root, folder);
				else
					return folder;
			};

			DriveInfo dr;
			if ((dr = getOnVolume(SrcVolume)) == null)
			{
				Error = "No drive for SrcVolume={0}".fmt(SrcVolume);
				return;
			}//if
			else
			{
				SrcFolder = driveFolder(dr.RootDirectory.FullName, SrcFolder);
			}//else

			if ((dr = getOnVolume(DstVolume)) == null)
			{
				Error = "No drive for DstVolume={0}".fmt(DstVolume);
				return;
			}//if
			else
			{
				DstFolder = driveFolder(dr.RootDirectory.FullName, DstFolder);
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
						//if (new FileInfo(item).Length != new FileInfo(s).Length){	}//if
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

		private static bool Copy(string from, string to, bool overwrite = false)
		{
			try
			{
				File.Copy(from, to, overwrite);
				return true;
			}//try
			catch (Exception exception)
			{
				log.execute(exception.Message);
				return false;
			}//catch
		}//function

		public void Copy(IProgress<string> progress)
		{
			string src, dst;
			string item;
			while ((item = filesAdd.FirstOrDefault()) != null)
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
						var dstFolder = Path.GetDirectoryName(dst);
						if (Directory.Exists(dstFolder) == false)
						{
							Directory.CreateDirectory(dstFolder);
						}//if
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
					log.execute("{0} - isn't in list for delete".fmt(s));
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

		/// <summary>
		/// считать xml в static IEnumerable
		/// </summary>
		public static void ReadAll()
		{
			XDocument xdoc = XDocument.Load("SyncFold.xml");
			Profile prof;
			foreach (var item in xdoc.Root.Elements("Profile"))
			{
				prof = new Profile();
				prof.Read(item);
				_profiles.Add(prof);
			}//for
		}//function

		public static async Task LoadAllAsync(IProgress<string> progress)
		{
			Action run = () => {
				foreach (var prof in _profiles)
				{
					prof.Load();
					if (prof.IsActive) { activeProfiles.Add(prof); }

					progress.Report(prof.ToString());
				}//for
			};
			await Task.Run(run);
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
				//return "{Name} ({Error})".fmto(this);
			}//else
		}
	}//class
}//namespace
