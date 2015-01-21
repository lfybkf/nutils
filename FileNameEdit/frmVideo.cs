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

			#region Events
			ctlName.setRusLanguageOnEnter();
			this.Load += frm_Load;
			btnOK.Click += (s, e) => { Do(); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlYear.KeyUp += ctl_KeyUp;
			#endregion
		}//function

		private void Do()
		{
			string Name = ctlName.Text;
			string Year = ctlYear.Text;
			if (string.IsNullOrWhiteSpace(Year))
				obj.New = Name;
			else
				obj.New = "{0} ({1})".fmt(Name, Year);

			this.setChooser(null);
			Close();
		}

		private void ctl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) { Do(); } //if
			else if (e.KeyCode == Keys.Escape) { obj.New = null; Close(); }//else
		}

		private void frm_Load(object sender, EventArgs e)
		{
			obj = this.getChooser();
			Text = obj.Old;

			bool parsed = obj.Parse();
			if (parsed)
			{
				ctlName.Text = obj.parse.getValue("Name") ?? string.Empty;
				ctlYear.Text = obj.parse.getValue("Year") ?? string.Empty;
			}//if
			else
				ctlName.Text = obj.Old;
			}//function
	}//class
}//ns
