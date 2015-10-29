using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Visio;

namespace Visio2Machine
{
	class Visio
	{
		public string Error { get; private set; }
		private string File;
		public string Name { get; private set; }
		
		Application app;
		Document doc;
		Page page; 

		public Visio(string file)
		{
			this.File = file;
			this.Name = System.IO.Path.GetFileNameWithoutExtension(file);
		}//constructor

		public bool Open()
		{
			try
			{
				app = new Application();
				app.Visible = false;
				//doc = app.Documents.OpenEx(File, (short)VisOpenSaveArgs.visOpenCopy);
				doc = app.Documents.Open(File);
				page = doc.Pages[1]; 
			}//try
			catch (Exception exception)
			{
				Error = exception.Message;
				return false;				
			}//catch
			
			//application.Documents.OpenEx(File,((short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenDocked + (short)Microsoft.Office.Interop.Visio.VisOpenSaveArgs.visOpenRO));
			return true;
		}//function

		public bool Close()
		{
			doc.execute(d => d.Close());
			app.execute(z => z.Quit());
			return true;
		}//function
	}//class
}//ns
