﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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

		Action<object> line = (s) => { output.Add(s.ToString()); };
		static List<string> output = new List<string>();
		static string outputFile = "output.log";

		List<ShInfo> shapes = new List<ShInfo>();
		IEnumerable<ShInfo> states { get { return shapes.Where(s => s.shType == ShType.State); } }
		IEnumerable<ShInfo> transitions { get { return shapes.Where(s => s.shType == ShType.Transition); } }
		IEnumerable<ShInfo> devices { get { return shapes.Where(s => s.shType == ShType.Device); } }
		IEnumerable<ShInfo> acts { get { return shapes.Where(s => s.shType == ShType.Act); } }
		IEnumerable<ShInfo> checks { get { return shapes.Where(s => s.shType == ShType.Check); } }
		IEnumerable<ShInfo> changes { get { return shapes.Where(s => s.shType == ShType.Change); } }
		IEnumerable<ShInfo> tests { get { return shapes.Where(s => s.shType == ShType.Test); } }
		ShInfo getOnID(string aID) {return shapes.Find(sh => sh.ID == aID);}

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
			this.shapes.OrderBy(sh => sh.shType).forEach(sh => line("{0}\t{1}".fmt(sh.shType, sh.ToString())));

			output.writeToFile(outputFile);
		}//function

		public bool Open()
		{
			app = new Application();
			app.Visible = false;
			doc = app.Documents.OpenEx(fileName, (short)VisOpenSaveArgs.visOpenRO);
			page = doc.Pages[1];

			ShInfo.line = line;
			foreach (Shape item in page.Shapes)
			{
				shapes.Add(new ShInfo().FillFrom(item));
			}//for
			shapes.forEach(z => z.DoConnector(shapes));
			shapes.forEach(z => z.DoShape(shapes));
			return true;
		}//function

		public bool Close()
		{
			doc.execute(d => d.Close());
			app.execute(z => z.Quit());
			return true;
		}//function

		public void ExportMachine()
		{
			#region header
string dtd = @"
	<!ELEMENT ROOT (Machine+)>
	<!ELEMENT Machine (Comment*, State+, Transition+, Push+, Check*, Act*, Device*)>
		<!ATTLIST Machine Name ID #REQUIRED>
		<!ATTLIST Machine Info CDATA #IMPLIED>
	<!ELEMENT State (Comment*)>
		<!ATTLIST State Name ID #REQUIRED>
		<!ATTLIST State Enter CDATA #IMPLIED>
		<!ATTLIST State Exit CDATA #IMPLIED>
		<!ATTLIST State Info CDATA #IMPLIED>
		<!ATTLIST State Visio CDATA #IMPLIED>
	<!ELEMENT Transition (Comment*)>
		<!ATTLIST Transition From CDATA #REQUIRED>
		<!ATTLIST Transition To CDATA #REQUIRED>
		<!ATTLIST Transition Pushes CDATA #REQUIRED>
		<!ATTLIST Transition Checks CDATA #IMPLIED>
		<!ATTLIST Transition Acts CDATA #IMPLIED>
		<!ATTLIST Transition Info CDATA #IMPLIED>
		<!ATTLIST Transition Visio CDATA #IMPLIED>
	<!ELEMENT Check (Comment*)>
		<!ATTLIST Check Name ID #REQUIRED>
		<!ATTLIST Check Info CDATA #IMPLIED>
		<!ATTLIST Check Device CDATA #IMPLIED>
		<!ATTLIST Check Test CDATA #IMPLIED>
	<!ELEMENT Act (Comment*)>
		<!ATTLIST Act Name ID #REQUIRED>
		<!ATTLIST Act Info CDATA #IMPLIED>
		<!ATTLIST Act Device CDATA #IMPLIED>
		<!ATTLIST Act Change CDATA #IMPLIED>
	<!ELEMENT Push (Comment*)>
		<!ATTLIST Push Name ID #REQUIRED>
		<!ATTLIST Push Info CDATA #IMPLIED>
	<!ELEMENT Device (Comment*)>
		<!ATTLIST Device Name ID #REQUIRED>
		<!ATTLIST Device Type CDATA #REQUIRED>
		<!ATTLIST Device Getter CDATA #IMPLIED>
		<!ATTLIST Device Info CDATA #IMPLIED>
	<!ELEMENT Comment (#PCDATA)>
		<!ATTLIST Comment Author CDATA #REQUIRED>
";
			#endregion

#region functions
Func<ShInfo, XElement> makeState = (sh) =>
{
	line("export State {0}".fmt(sh.ID));
	return new XElement(R.STATE
		,	new XAttribute(R.NAME, sh.Name)
		, new XAttribute(R.ID, sh.ID)
		);
};

Func<ShInfo, XElement> makeTransition = (sh) =>
{
	line("export Transition {0}".fmt(sh.ID));
	return new XElement(R.TRANSITION
		, new XAttribute(R.FROM, getOnID(sh.From).Name)
		, new XAttribute(R.TO, getOnID(sh.To).Name)
		, new XAttribute(R.Pushes, sh.Pushes)
		, new XAttribute(R.Checks, sh.Checks)
		, new XAttribute(R.Acts, sh.Acts)
		, new XAttribute(R.ID, sh.ID)
		);
};

Func<string, XElement> makePush = (s) =>
{
	line("export Push {0}".fmt(s));
	return new XElement(R.PUSH, new XAttribute(R.NAME, s));
};

Func<ShInfo, XElement> makeDevice = (sh) =>
{
	line("export Device {0}".fmt(sh.ID));
	return new XElement(R.DEVICE
		, new XAttribute(R.NAME, sh.Name)
		, new XAttribute(R.ID, sh.ID)
		);
};

Func<ShInfo, XElement> makeAct = (sh) =>
{
	line("export Act {0}".fmt(sh.ID));
	var change = changes.FirstOrDefault(z => z.From == sh.ID);
	string deviceID = change.with(z => z.To);
	return new XElement(R.ACT
		, new XAttribute(R.NAME, sh.Name)
		, new XAttribute(R.DEVICE, devices.FirstOrDefault(d => d.ID == deviceID))
		, new XAttribute(R.CHANGE, change.with(z => z.Name))
		, new XAttribute(R.ID, sh.ID)
		);
};

Func<ShInfo, XElement> makeCheck = (sh) =>
{
	line("export Check {0}".fmt(sh.ID));
	var change = changes.FirstOrDefault(z => z.To == sh.ID);
	string deviceID = change.with(z => z.From);
	return new XElement(R.ACT
		, new XAttribute(R.NAME, sh.Name)
		, new XAttribute(R.DEVICE, devices.FirstOrDefault(d => d.ID == deviceID))
		, new XAttribute(R.TEST, change.with(z => z.Name))
		, new XAttribute(R.ID, sh.ID)
		);
};

#endregion

			#region xml
			XDeclaration xdecl = new XDeclaration("1.0", "utf-8", "yes");
			XDocumentType xdtd = new XDocumentType("ROOT", null, null, dtd);
			XDocument xdoc = new XDocument(xdecl, xdtd);
			XElement xroot = new XElement("ROOT",
				new XElement(R.MACHINE
					, states.Select(makeState)
					, transitions.Select(makeTransition)
					, transitions.SelectMany(tr => tr.pushes).Distinct().Select(makePush)
					, devices.Select(makeDevice)
					, acts.Select(makeAct)
					, checks.Select(makeCheck)
					, new XAttribute(R.NAME, Name)
				)
			);

			xdoc.Add(xroot);
			#endregion

			xdoc.Save("Machine{0}.xml".fmt(Name));
		}//function

	}//class
}//ns
