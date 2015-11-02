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

		internal string ID, Name, Text, From, To, Device, Getter, Type, Test, Change, Checks, Acts, Pushes;
		internal ShType shType { get; private set; }

		internal bool IsConnector { get { return ID.StartsWith(R.DynamicСonnector); } }
		private bool IsState { get { return ID.StartsWith(R.Rectangle); } }
		private bool IsDevice { get { return ID.StartsWith(R.Ellipse); } }
		private bool IsActOrCheck { get { return ID.StartsWith(R.Diamonde); } }
		
		internal IEnumerable<string> pushes { get { return Pushes.splitValue(); } }
		internal IEnumerable<string> acts { get { return Pushes.splitValue(); } }
		internal IEnumerable<string> checks { get { return Pushes.splitValue(); } }

		public ShInfo()
		{
			shType = ShType.NONE;
			set(string.Empty, ID, Name, Text, From, To, Device, Getter, Type, Test, Change, Checks, Acts, Pushes);
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
				Checks = ss.FirstOrDefault(z => z.StartsWith(R.CHECK)).midst(R.CHECK + "(", ")");
				Acts = ss.FirstOrDefault(z => z.StartsWith(R.ACT)).midst(R.ACT + "(", ")");
			}//if
			else if (Get(shapes, From).IsActOrCheck && Get(shapes, To).IsDevice)
			{
				shType = ShType.Change;
				Change = ss.get(0, string.Empty);
			}//if
			else if (Get(shapes, From).IsDevice && Get(shapes, To).IsActOrCheck)
			{
				shType = ShType.Test;
				Test = ss.get(0, string.Empty);
			}//if
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
				var setters = shapes.Where(z => z.shType == ShType.Change);
				var getters = shapes.Where(z => z.shType == ShType.Test);
				if (setters.Any(z => z.From == ID))
				{
					shType = ShType.Act;
				}//if
				else if (getters.Any(z => z.To == ID))
				{
					shType = ShType.Check;
				}//if
			}//if
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
			else
			{
				return "{0} {1}".fmt(ID, Text);
			}//else
		}
	}//class
}
