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
	public partial class frmDistrib : Form
	{
		Chooser obj;

		public frmDistrib()
		{
			InitializeComponent();

			#region Events
			ctlName.setEngLanguageOnEnter();
			ctlVersion.setEngLanguageOnEnter();
			this.Load += frm_Load;
			btnOK.Click += (s, e) => { Do(); };
			ctlName.KeyUp += ctl_KeyUp;
			ctlVersion.KeyUp += ctl_KeyUp;
			#endregion

			#region Design
			btnOK.Text = "OK";
			#endregion

		}//function

		private void Do()
		{
			string Name = ctlName.Text;
			string Version = ctlVersion.Text;
			if (string.IsNullOrWhiteSpace(Version))
				obj.New = "{0}_setup".fmt(Name);
			else
				obj.New = "{0}_{1}_setup".fmt(Name, Version);

			this.setChooser(null);
			Close();
		}//fucntion


		private void ctl_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) { Do(); } //if
			else if (e.KeyCode == Keys.Escape) { obj.New = null; Close(); }//else
		}//function

		private void frm_Load(object sender, EventArgs e)
		{
			obj = this.getChooser();
			Text = obj.Old;
			bool parsed = obj.Parse();
			if (parsed)
			{
				ctlName.Text = obj.parse.getValue("Name") ?? string.Empty;
				ctlVersion.Text = obj.parse.getValue("Version") ?? string.Empty;
			}//if
			else
				ctlName.Text = obj.Old;
		}//function

	}//class
}//ns

/*
 		Regex rex; Match m; //MatchCollection mc; CaptureCollection cc; 
		Regex rexGood = new Regex(@"(.*)_([0-9].*)_setup"); // Name_Version_setup
		Regex rexMaybe = new Regex(@"(\D*)([0-9].*)_setup"); // NameVersion_setup
		Regex rexTry = new Regex(@"(\D*)([0-9].*)"); // NameVersion
		Regex rexSimple = new Regex(@"(\D*)_setup"); // Name_setup
		if (rexGood.IsMatch(obj.Old))
		{
			rex = rexGood;
			m = rex.Match(obj.Old);
			ctlName.Text = m.Groups[1].Value;
			ctlVersion.Text = m.Groups[2].Value;
		}//else
		else if (rexMaybe.IsMatch(obj.Old))
		{
			rex = rexMaybe;
			m = rex.Match(obj.Old);
			ctlName.Text = m.Groups[1].Value;
			ctlVersion.Text = m.Groups[2].Value;
		}//else
		else if (rexTry.IsMatch(obj.Old))
		{
			rex = rexTry;
			m = rex.Match(obj.Old);
			ctlName.Text = m.Groups[1].Value;
			ctlVersion.Text = m.Groups[2].Value;
		}//else
		else if (rexSimple.IsMatch(obj.Old))
		{
			rex = rexSimple;
			m = rex.Match(obj.Old);
			ctlName.Text = m.Groups[1].Value;
		}//else
		else		{			ctlName.Text = obj.Old;		}//else

 */
