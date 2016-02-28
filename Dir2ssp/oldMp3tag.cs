using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dir2ssp
{
	class oldMp3Tag
	{

		#region property bytes
		byte[] bytesTAGID = new byte[3];      //  3
		byte[] bytesTitle = new byte[30];     //  30
		byte[] bytesArtist = new byte[30];    //  30 
		byte[] bytesAlbum = new byte[30];     //  30 
		byte[] bytesYear = new byte[4];       //  4 
		byte[] bytesComment = new byte[30];   //  30 
		byte[] bytesGenre = new byte[1];      //  1
		#endregion

		#region public properties
		public string Title;
		public string Artist;
		public string Album;
		public string Year;
		public string Comment;
		public string Genre;
		#endregion

		public static oldMp3Tag Read(string file)
		{
			oldMp3Tag tag = null;
			using (FileStream fs = File.OpenRead(file))
			{
				if (fs.Length >= 128)
				{
					tag = new oldMp3Tag();

					#region read bytes
					fs.Seek(-128, SeekOrigin.End);
					fs.Read(tag.bytesTAGID, 0, tag.bytesTAGID.Length);
					fs.Read(tag.bytesTitle, 0, tag.bytesTitle.Length);
					fs.Read(tag.bytesArtist, 0, tag.bytesArtist.Length);
					fs.Read(tag.bytesAlbum, 0, tag.bytesAlbum.Length);
					fs.Read(tag.bytesYear, 0, tag.bytesYear.Length);
					fs.Read(tag.bytesComment, 0, tag.bytesComment.Length);
					fs.Read(tag.bytesGenre, 0, tag.bytesGenre.Length);
					#endregion

					#region convert
					string theTAGID = Encoding.Default.GetString(tag.bytesTAGID);
					if (theTAGID.Equals("TAG"))
					{
						string Title = Encoding.Default.GetString(tag.bytesTitle);
						string Artist = Encoding.Default.GetString(tag.bytesArtist);
						string Album = Encoding.Default.GetString(tag.bytesAlbum);
						string Year = Encoding.Default.GetString(tag.bytesYear);
						string Comment = Encoding.Default.GetString(tag.bytesComment);
						string Genre = Encoding.Default.GetString(tag.bytesGenre);
					}
					#endregion
				}//if
				return tag;
			}//using
		}//function
	}
}//namespace
