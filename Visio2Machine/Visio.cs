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
		private string fileName;
		public string Name { get; private set; }

		Action<object> line = Console.WriteLine;
		
		Application app;
		Document doc;
		Page page; 

		public Visio(string file)
		{
			if (string.IsNullOrWhiteSpace(System.IO.Path.GetDirectoryName(file)))
				this.fileName = System.IO.Path.Combine(Environment.CurrentDirectory, file);
			else
				this.fileName = file;

			this.Name = System.IO.Path.GetFileNameWithoutExtension(file);
		}//constructor

		public void PrintStat()
		{
			line("Connects = {0}".fmt(page.Connects.Count));
			foreach (Connect item in page.Connects)
			{
				line("from {0} to {1}".fmt((item.FromSheet as Shape).with(s => s.Name, "no shape"), (item.ToSheet as Shape).with(s => s.Name, "no shape")));
			}

			line("Shapes = {0}".fmt(page.Shapes.Count));
			ShapeInfo si = null;
			foreach (Shape item in page.Shapes) {
				si = ShapeInfo.Fill(item);
				line("Name={0}, Text={1}, Type={2}".fmt(si.Name, si.Text));
			}//for
		}//function

		public bool Open()
		{
			try
			{
				app = new Application();
				app.Visible = false;
				doc = app.Documents.OpenEx(fileName, (short)VisOpenSaveArgs.visOpenRO);
				//doc = app.Documents.Open(fileName);
				page = doc.Pages[1];


				
			}//try
			catch (Exception exception)
			{
				Error = "File={0}, Exc={1}".fmt(System.IO.Path.GetDirectoryName(fileName), exception.Message);
				Close();
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
