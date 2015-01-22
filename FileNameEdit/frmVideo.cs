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
			obj.Do();
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

			if (obj.Parse() == false)
				ctlName.Text = obj.Old;
		}//function
	}//class
}//ns
