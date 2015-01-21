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
	public partial class frmBook : Form
	{
		Chooser obj;

		public frmBook()
		{
			InitializeComponent();

			#region Events
			ctlName.setRusLanguageOnEnter();
			ctlAuthor.setRusLanguageOnEnter();
			this.Load += frm_Load;
			btnOK.Click += (s, e) => { Do(); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlAuthor.KeyUp += ctl_KeyUp;
			ctlYear.KeyUp += ctl_KeyUp;
			#endregion

			#region Design
			btnOK.Text = "OK";
			#endregion
		}//function


		private void Do()
		{
			string Name = ctlName.Text;
			string Author = ctlAuthor.Text;
			string Year = ctlYear.Text;
			if (string.IsNullOrWhiteSpace(Year) && string.IsNullOrWhiteSpace(Author))
					obj.New = Name;
			else
			{
				if (string.IsNullOrWhiteSpace(Year))
					obj.New = string.Format("{0} ({1})", Name, Author);
				else if (string.IsNullOrWhiteSpace(Author))
					obj.New = string.Format("{0} ({1})", Name, Year);
				else
					obj.New = string.Format("{0} ({1} {2})", Name, Year, Author);
			}//else

			this.setChooser(null);
			Close();
		}//fucntion


		private void ctl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) { Do(); } //if
			else if (e.KeyCode == Keys.Escape)	{	obj.New = null;	Close();	}//else
		}//function

	private void frm_Load(object sender, EventArgs e)
	{
		obj = this.getChooser();
		Text = obj.Old;

		bool parsed = obj.Parse();
		if (parsed)
		{
			ctlName.Text = obj.parse.getValue("Name") ?? string.Empty;
			ctlAuthor.Text = obj.parse.getValue("Author") ?? string.Empty;
			ctlYear.Text = obj.parse.getValue("Year") ?? string.Empty;
		}//if
		else
			ctlName.Text = obj.Old;
		}//function
	}//class
}//ns
