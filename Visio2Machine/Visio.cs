using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visio2Machine
{
	class Visio
	{
		public string Error { get; private set; }
		private string File;
		public string Name { get; private set; }

		public Visio(string file)
		{
			this.File = file;
			this.Name = Path.GetFileNameWithoutExtension(file);
		}//constructor

		public bool Open()
		{
			this.Application.Documents.OpenEx(File,
((short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenDocked +
(short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenRO));

			return true;
		}//function

		public bool Close()
		{
			this.Application.ActiveDocument.Close();
			return true;
		}//function
	}//class
}//ns
