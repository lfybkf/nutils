using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFold
{
	public class Profile
	{
		static DriveInfo[] drives = DriveInfo.GetDrives();

		string Name;
		string SrcVolume;
		string SrcFolder;
		string DstVolume;
		string DstFolder;
		bool IsActive = false;
		DriveInfo SrcDrive, DstDrive;

		#region props
		DirectoryInfo SrcRoot { get { return SrcDrive.RootDirectory; } }
		DirectoryInfo DstRoot { get { return DstDrive.RootDirectory; } }
		#endregion

		public IEnumerable<Profile> LoadAll()
		{
			IEnumerable<Profile> result = new List<Profile>();

			#region prepare
			Func<string, DriveInfo> getOnVolume = (vol) => {
				return drives.FirstOrDefault
					(dr => dr.VolumeLabel.Equals(vol, StringComparison.InvariantCultureIgnoreCase));
			};

			foreach (var prof in result)
			{
				SrcDrive = getOnVolume(SrcVolume);
				DstDrive = getOnVolume(DstVolume);
				if (SrcDrive == null || DstDrive == null) { continue; }

				IsActive = true;
			}//for

			#endregion

			return result;
		}//function
	}//class
}//namespace
