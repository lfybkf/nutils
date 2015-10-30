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
	public partial class frmAudio: Form
	{
		public frmAudio()
		{
			InitializeComponent();
			ctlAuthor.TabIndex = 1;
			ctlNomer.TabIndex = 2;
			ctlName.TabIndex = 3;
			btnOK.TabIndex = 100;

			#region Events
			//ctlName.setRusLanguageOnEnter();
			//ctlAuthor.setRusLanguageOnEnter();
			this.Load += frm_Load;
			btnOK.Click += (s, e) => { Finish(true); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlAuthor.KeyUp += ctl_KeyUp;
			ctlNomer.KeyUp += ctl_KeyUp;
			#endregion

			#region Design
			btnOK.Text = "OK";
			#endregion
		}//function


		private void Finish(bool OK)
		{
			Chooser obj = this.getChooser();
			if (OK)
				obj.Do();
			else
				obj.New = null;

			this.setChooser(null);
			Close();
		}//function

		private void ctl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) { Finish(true); } //if
			else if (e.KeyCode == Keys.Escape) { Finish(false); }//else
		}

		private void frm_Load(object sender, EventArgs e)
		{
			Chooser obj = this.getChooser();
			Text = obj.Old;

			if (obj.Parse() == false)
				ctlName.Text = obj.Old;
		}//function

	}//class
}//ns
