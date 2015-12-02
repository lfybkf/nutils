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
		public frmVideo()
		{
			InitializeComponent();

			ctlName.TabIndex = 0;
			ctlYear.TabIndex = 1;
			ctlSeria.TabIndex = 2;
			btnOK.TabIndex = 100;

			#region Events
			ctlName.setRusLanguageOnEnter();
			btnOK.Click += (s, e) => { Finish(true); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlYear.KeyUp += ctl_KeyUp;
			ctlSeria.KeyUp += ctl_KeyUp;
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
