using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	public partial class frmVideo : Form
	{
		Chooser obj;

		public frmVideo()
		{
			InitializeComponent();
            ctlName.setRusLanguageOnEnter();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Do();
		}//function

		private void Do()
		{
			string Name = ctlName.Text;
			string Year = ctlYear.Text;
			if (string.IsNullOrWhiteSpace(Year))
				obj.New = Name;
			else
				obj.New = string.Format("{0} ({1})", Name, Year);

            this.setChooser(null);
			Close();
		}

		private void ctlName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				Do();
			else if (e.KeyCode == Keys.Escape)
			{
				obj.New = null;
				Close();
			}//else
		}

		private void frmVideo_Load(object sender, EventArgs e)
		{
			obj = this.getChooser();

			ctlOld.Text = obj.Old;

            Regex rex; Match m; //MatchCollection mc; CaptureCollection cc; 
			rex = new Regex(@"(.*) [(]([0-9]{4})[)]");
			if (rex.IsMatch(obj.Old))
			{
				m = rex.Match(obj.Old);
				ctlName.Text = m.Groups[1].Value;
				ctlYear.Text = m.Groups[2].Value;
			}//if
			else
			{
				rex = new Regex("(.*)([0-9]{4}).*");
				if (rex.IsMatch(obj.Old))
				{
					m = rex.Match(obj.Old);
					ctlName.Text = m.Groups[1].Value;
					ctlYear.Text = m.Groups[2].Value;
				}//if
				else
				{
					ctlName.Text = obj.Old;
				}//else
			}//else
			
		}//function
	}//class
}//ns
