using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taimer
{
	public partial class frmTaimer : Form
	{
		static int[] mins = {1,5,15,30,60,90,120};
		Taim[] taims = null;
		Taim CurTaim = null;

		void doInit()
		{
			this.SuspendLayout();
			this.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
				//Taimer.Properties.Resources.Limewire;


			Taim.timerMain = this.timerMain;
			Taim.timerSec = this.timerSec;
			this.timerSec.Interval = 3000;
			
			taims = mins.OrderByDescending(i => i).Select(i => new Taim(i)).ToArray();

			doShowTime();
			doShowInfo();

			Button button;
			foreach (var taim in taims)
			{
				button = new Button() { Text = taim.ToString()};
				panMain.Controls.Add(button);
				button.Click += button_Click;
				button.Dock = DockStyle.Top;
				button.Height = 40;
				button.Tag = taim;
			}//for
			
			this.ResumeLayout();
		}//function

		public void doShowTime()
		{
			lbTime.Text = DateTime.Now.ToString();
		}//function

		public void doShowInfo()
		{
			if (CurTaim == null) { this.Text = lbInfo.Text = "No timer works"; return; }

			this.Text = string.Format("Осталось {0}", CurTaim.MinRest);
			lbInfo.Text = CurTaim.Info;
		}//function

		public void doStartTaim()
		{
			CurTaim.Start();
			this.WindowState = FormWindowState.Minimized;
		}//function

		public void doStopTaim()
		{
			if (CurTaim == null) { return; }
			CurTaim.Stop();
			CurTaim = null;
			this.WindowState = FormWindowState.Normal;
		}//function

		private void button_Click(object sender, EventArgs e)
		{
			doStopTaim();
			CurTaim = ((sender as Button).Tag as Taim);
			doStartTaim();
		}//function

		public frmTaimer()
		{
			InitializeComponent();
		}

		private void frmTaimer_Load(object sender, EventArgs e)
		{
			doInit();
		}

		private void timerMain_Tick(object sender, EventArgs e)
		{
			doStopTaim();
		}

		private void timerSec_Tick(object sender, EventArgs e)
		{
			doShowInfo();
			doShowTime();
		}
	}//class
}//ns
