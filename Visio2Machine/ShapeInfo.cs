using Microsoft.Office.Interop.Visio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visio2Machine
{
	class ShapeInfo
	{
		static short iFirstRow = (short)VisRowIndices.visRowFirst;
		static short iSectionProp = (short)VisSectionIndices.visSectionProp;
		static short iCustPropsLabel = (short)VisCellIndices.visCustPropsLabel;
		static short iCustPropsValue = (short)VisCellIndices.visCustPropsValue;

		public string Name, Text;

		public static ShapeInfo Fill(Shape shape)
		{
			ShapeInfo result = new ShapeInfo() { Name = shape.Name, Text = shape.Text };
			/*
			short iRow = (short)VisRowIndices.visRowFirst;
			while (shape.get_CellsSRCExists((short)VisSectionIndices.visSectionProp, iRow, (short)VisCellIndices.visCustPropsValue,	(short)0) != 0)
			{
				result.Text = shape.get_CellsSRC((short)VisSectionIndices.visSectionProp, iRow, (short)VisCellIndices.visCustPropsLabel).get_ResultStr(VisUnitCodes.visNoCast);
				iRow++;
			}//while
			 */
			return result;
		}//function

	}//class
}
