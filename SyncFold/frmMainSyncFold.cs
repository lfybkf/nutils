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
			kmds.Add(new Kommand("Add", kmdAdd));

			foreach (Kommand kmd in kmds) { kmd.isExecuted += kmd_isExecuted; }
			foreach (var btn in this.panManage.Controls.OfType<Button>())
			{
				kmds.LinkToComponent(btn);
			}//for
		}

		private async void kmdAdd()
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
			await profile.CopyAsync(progress);
		}

		void kmd_isExecuted(object sender, EventArgs e) { this.Text = (sender as Kommand).Caption; }

		private void frmMainBackupFolder_Load(object sender, EventArgs e)
		{
			InitKmd();

			profiles = Profile.LoadAll();
			listProfiles.DataSource = profiles;

			listProfiles.Focus();
		}

		public void ListBoxRefresh<T>(ListBox lb, List<T> list)
		{
			lb.DataSource = list;
			var bc = lb.BindingContext[list];
			(bc as CurrencyManager).Refresh();
		}//function

		private void listRemove_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				foreach (var item in listDel.SelectedItems)
				{
					profile.Delete(item.ToString());
				}//for
				ListBoxRefresh(listDel, profile.filesDel);
			}//if
		}

		private void listProfiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxRefresh(listAdd, profile.filesAdd);
			ListBoxRefresh(listDel, profile.filesDel);
		}//function
	}//class
}
