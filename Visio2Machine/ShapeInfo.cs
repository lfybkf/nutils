using Microsoft.Office.Interop.Visio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visio2Machine
{
	enum ShType
	{
		NONE, Transition, State, Device, Act, Check, Test, Change
	}//enum

	class ShInfo
	{
		internal static Action<string> line;
		static string sign = "-";
		static int csvID = 0;

		internal string ID, Name, Text, From, To, Device, Getter, Type, Pushes, Expr;
		string[] Checks, Acts;
		internal ShType shType { get; private set; }

		internal bool IsConnector { get { return ID.StartsWith(R.DynamicСonnector); } }
		private bool IsState { get { return ID.StartsWith(R.Rectangle); } }
		private bool IsDevice { get { return ID.StartsWith(R.Ellipse); } }
		private bool IsActOrCheck { get { return ID.StartsWith(R.Diamond); } }
		
		internal IEnumerable<string> pushes { get { return Pushes.splitValue(); } }
		internal IEnumerable<string> acts { get { return Acts; } }
		internal IEnumerable<string> checks { get { return Checks; } }

		public ShInfo()
		{
			shType = ShType.NONE;
			set(string.Empty, ID, Name, Text, From, To, Device, Getter, Type, Pushes, Expr);
		}//constructor

		private void set(string value, params string[] list) { list.forEach(s => s = value); }

		internal ShInfo FillFrom(Shape shape)
		{
			ID = shape.NameU;
			line("parsing 1 shape {0}".fmt(ID));
			Text = shape.Text;
			if (IsConnector)
			{
				From = shape.CellFormula(R.BegTrigger).midst("(", "!");
				To = shape.CellFormula(R.EndTrigger).midst("(", "!");
			}//if
			return this;
		}//function

		internal ShInfo FillFrom(string csv)
		{
			string[] ss = csv.splitCSV();
			string What = ss.get(0, string.Empty);
			if (What.isEmpty()) { shType = ShType.NONE; return this; }
			ID = "{0}_{1}".fmt(What, csvID++);
			line("parsing csv {0}".fmt(ID));
			if (What == R.DEVICE)
			{
				shType = ShType.Device;
				Name = ss.get(1, string.Empty);
				Type = ss.get(2, string.Empty);
				Getter = ss.get(3, string.Empty);
			}//if
			else if (What == R.ACT)
			{
				shType = ShType.Act;
				Name = ss.get(1, string.Empty);
				Device = ss.get(2, string.Empty);
				Expr = ss.get(3, string.Empty);
			}//if
			else if (What == R.CHECK)
			{
				shType = ShType.Check;
				Name = ss.get(1, string.Empty);
				Device = ss.get(2, string.Empty);
				Expr = ss.get(3, string.Empty);
			}//if
			else {shType = ShType.NONE;}
			return this;
		}//function

		private static ShInfo Get(IEnumerable<ShInfo> shapes, string ID)
		{
			return shapes.FirstOrDefault(sh => sh.ID == ID);
		}//function

		internal void DoConnector(IEnumerable<ShInfo> shapes)
		{
			if (shType != ShType.NONE) { return; }
			if (IsConnector == false) { return; }

			line("parsing 2 connector {0}".fmt(ID));
			string[] ss = Text.splitLine();
			
			if (Get(shapes, From).IsState && Get(shapes, To).IsState)
			{
				shType = ShType.Transition;
				Pushes = ss.get(0, string.Empty);
				Checks = ss.Skip(1).Where(z => z.StartsWith(sign)).Select(z => z.after(sign)).ToArray();
				Acts = ss.Skip(1).Where(z => !z.StartsWith(sign)).ToArray();
			}//if
			else if (Get(shapes, From).IsActOrCheck && Get(shapes, To).IsDevice)
			{
				shType = ShType.Change;
				Name = ss.get(0, string.Empty);
			}//if
			else if (Get(shapes, From).IsDevice && Get(shapes, To).IsActOrCheck)
			{
				shType = ShType.Test;
				Name = ss.get(0, string.Empty);
			}//if

			line("\t resolved as {0}".fmt(shType));
		}//function

		internal void DoShape(IEnumerable<ShInfo> shapes)
		{
			if (shType != ShType.NONE) { return; }
			if (IsConnector == true) { return; }

			line("parsing 2 shape {0}".fmt(ID));
			string[] ss = Text.splitLine();

			if (IsState)
			{
				shType = ShType.State;
				Name = ss[0];
			}//if
			else if (IsDevice)
			{
				shType = ShType.Device;
				Name = ss[0];
				Type = ss.get(1, string.Empty);
				Getter = ss.get(2, string.Empty);
			}//if
			else if (IsActOrCheck)
			{
				Name = ss[0];
				var change = shapes.FirstOrDefault(z => z.shType == ShType.Change && z.From == ID);
				var test = shapes.FirstOrDefault(z => z.shType == ShType.Test && z.To == ID);
				if (change != null)
				{
					shType = ShType.Act;
					Expr = change.Name;
					var deviceID = change.To;
					Device = shapes.FirstOrDefault(z => z.IsDevice && z.ID == deviceID).Name;
				}//if
				else if (test != null)
				{
					shType = ShType.Check;
					Expr = test.Name;
					var deviceID = test.From;
					Device = shapes.FirstOrDefault(z => z.IsDevice && z.ID == deviceID).Name;
				}//if
			}//if

			line("\t resolved as {0}".fmt(shType));
		}//function

		public override string ToString()
		{
			if (shType == ShType.Transition)
			{
				return "transition {0} -> {1}".fmt(From, To);
			}//if
			else if (shType == ShType.Change)
			{
				return "change {1}.{0}".fmt(From, To);
			}//if
			else if (shType == ShType.Test)
			{
				return "test {0}.{1}".fmt(From, To);
			}//if
			else if (shType == ShType.Device)
			{
				return "[{0}] {1} {2}".fmt(ID, Type, Name);
			}//if
			else
			{
				return "[{0}] {1}".fmt(ID, Name);
			}//else
		}
	}//class
}
