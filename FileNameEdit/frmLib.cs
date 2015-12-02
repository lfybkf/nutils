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
	public partial class frmLib: Form
	{
		public frmLib()
		{
			InitializeComponent();
			ctlAuthor.TabIndex = 1;
			ctlSeria.TabIndex = 2;
			ctlName.TabIndex = 3;
			ctlNomer.TabIndex = 4;
			btnOK.TabIndex = 100;

			#region Events
			ctlName.setRusLanguageOnEnter();
			ctlAuthor.setRusLanguageOnEnter();
			ctlSeria.setRusLanguageOnEnter();
			btnOK.Click += (s, e) => { Finish(true); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlAuthor.KeyUp += ctl_KeyUp;
			ctlSeria.KeyUp += ctl_KeyUp;
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

	}//class
}//ns
