using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BDB;

namespace SyncFold
{
	public partial class frmMainSyncFold : Form
	{
		IList<Kommand> kmds = new List<Kommand>();
		Profile profile { get { return (Profile)listProfiles.SelectedItem; } }
		IEnumerable<Profile> profiles;

		public frmMainSyncFold()
		{
			InitializeComponent();
		}

		private void InitKmd()
		{
			kmds.Add(new Kommand("Sync", kmdSync));
			kmds.Add(new Kommand("Refresh", kmdRefresh));
			kmds.Add(new Kommand("Delete", kmdDelete));

			foreach (Kommand kmd in kmds) { kmd.isExecuted += kmd_isExecuted; }
			foreach (var btn in this.panManage.Controls.OfType<Button>())
			{
				kmds.LinkToComponent(btn);
			}//for
		}

		private void kmdDelete()
		{
			foreach (var item in listDel.SelectedItems)
			{
				profile.Delete(item.ToString());	
			}//for
			ListBoxRefresh(listDel, profile.filesDel);
			
		}

		private void kmdRefresh()
		{
			profile.Refresh();
		}

		void frmStart()
		{
			Profile.log = Log;

			InitKmd();

			profiles = Profile.LoadAll();
			listProfiles.DataSource = profiles;

			listProfiles.Focus();

		}//function

		void Log(string msg)
		{
			listLog.Items.Add(msg);
		}//function

		private async void kmdSync()
		{
			string sWork = string.Empty;
			Action<string> report = (s) => 
			{
				if (s != sWork) //copy begin
				{
					sWork = s;
					this.Text = sWork;
				}//if
				else // copy end
				{
					ListBoxRefresh(listAdd, profile.filesAdd);
				}//else
			};
			Progress<string> progress = new Progress<string>(report);
			//await Task.Run((profile.Copy(progress));
			await profile.CopyAsync(progress);
		}

		void kmd_isExecuted(object sender, EventArgs e) 
		{
			var o = new { time = DateTime.Now.ToShortTimeString(), caption = (sender as Kommand).Caption };
			this.Text = "{time} {caption}".fmto(o);
		}

		private void frmMainBackupFolder_Load(object sender, EventArgs e)
		{
			frmStart();
		}

		public void ListBoxRefresh<T>(ListBox lb, List<T> list)
		{
			lb.DataSource = list;
			var bc = lb.BindingContext[list];
			(bc as CurrencyManager).Refresh();
		}//function

		private void listDel_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				kmdDelete();
			}//if
		}

		private void listProfiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxRefresh(listAdd, profile.filesAdd);
			ListBoxRefresh(listDel, profile.filesDel);
		}//function
	}//class
}
