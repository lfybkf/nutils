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
			obj.Do();
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

		if (obj.Parse() == false)
			ctlName.Text = obj.Old;
		}//function
	}//class
}//ns
