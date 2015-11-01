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
		NONE, Transition, State, Device, Act, Check, Getter, Setter
	}//enum

	class ShInfo
	{
		internal static Action<string> line;

		internal string ID, Name, Text, From, To, Pushes, Device, Getter, Setter, Type;
		internal ShType shType { get; private set; }

		internal bool IsConnector { get { return ID.StartsWith(R.DynamicСonnector); } }
		private bool IsState { get { return ID.StartsWith(R.Rectangle); } }
		private bool IsDevice { get { return ID.StartsWith(R.Ellipse); } }
		private bool IsActOrCheck { get { return ID.StartsWith(R.Diamonde); } }
		internal IEnumerable<string> pushes { get { return Pushes.Split(' ', ',').Where(s => s.notEmpty()); } }

		public ShInfo()
		{
			shType = ShType.NONE;
			ID = Name = Text = From = To = Pushes = Device = Getter = Setter = string.Empty;
		}//constructor

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
			string[] ss = Text.Split(Environment.NewLine.ToCharArray());
			
			if (Get(shapes, From).IsState && Get(shapes, To).IsState)
			{
				shType = ShType.Transition;
				Pushes = ss[0];
			}//if
			else if (Get(shapes, From).IsActOrCheck && Get(shapes, To).IsDevice)
			{
				shType = ShType.Setter;
				Setter = ss[0];
			}//if
			else if (Get(shapes, From).IsDevice && Get(shapes, To).IsActOrCheck)
			{
				shType = ShType.Getter;
				Getter = ss[0];
			}//if
		}//function

		internal void DoShape(IEnumerable<ShInfo> shapes)
		{
			if (shType != ShType.NONE) { return; }
			if (IsConnector == true) { return; }

			line("parsing 2 shape {0}".fmt(ID));
			string[] ss = Text.Split(Environment.NewLine.ToCharArray());

			if (IsState)
			{
				shType = ShType.State;
				Name = ss[0];
			}//if
			else if (IsDevice)
			{
				shType = ShType.Device;
				Name = ss[0];
				Type = ss.Length > 1 ? ss[1] : string.Empty;
			}//if
			else if (IsActOrCheck)
			{
				Name = ss[0];
				var setters = shapes.Where(z => z.shType == ShType.Setter);
				var getters = shapes.Where(z => z.shType == ShType.Getter);
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
			else if (shType == ShType.Setter)
			{
				return "act {0} -> {1}".fmt(From, To);
			}//if
			else if (shType == ShType.Getter)
			{
				return "check {0} -> {1}".fmt(From, To);
			}//if
			else
			{
				return "{0} {1}".fmt(ID, Text);
			}//else
		}
	}//class
}
