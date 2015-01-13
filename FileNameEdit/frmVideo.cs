using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
	public partial class frmVideo : Form
	{
		public frmVideo()
		{
			InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			Do();
		}//function

		private void Do()
		{
			string Name = ctlName.Text;
			string Year = ctlYear.Text;
			Chooser obj = (Tag as Chooser);
			if (string.IsNullOrWhiteSpace(Year))
				obj.New = Name;
			else
				obj.New = string.Format("{0} ({1})", Name, Year);

			Tag = null;
			Close();
		}

		private void ctlName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				Do();
		}

		private void frmVideo_Load(object sender, EventArgs e)
		{
			ctlOld.Text = (Tag as Chooser).Old;
		}//function
	}//class
}//ns
