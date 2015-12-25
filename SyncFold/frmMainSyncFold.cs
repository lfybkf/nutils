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
		IEnumerable<Profile> profiles;

		public frmMainSyncFold()
		{
			InitializeComponent();
		}

		private void InitKmd()
		{
			//kmds.Add(new Kommand("List", kmdList));

			foreach (Kommand kmd in kmds) { kmd.isExecuted += kmd_isExecuted; }
			foreach (var btn in this.panManage.Controls.OfType<Button>())
			{
				kmds.LinkToComponent(btn);
			}//for
		}

		void kmd_isExecuted(object sender, EventArgs e) { this.Text = (sender as Kommand).Caption; }



		private IEnumerable<string> getFiles(string folder)
		{
			if (folder.isEmpty())	{	return new string[0];	}//if
			if (!Directory.Exists(folder)) { return new string[0]; }//if

			var result = new List<string>();
			result.AddRange(Directory.GetFiles(folder));
			var folders = Directory.GetDirectories(folder);
			foreach (var item in folders)
			{
				result.AddRange(getFiles(item));
			}//for
			return result;
		}

		private void frmMainBackupFolder_Load(object sender, EventArgs e)
		{
			InitKmd();

			profiles = Profile.LoadAll();

			listFolders.Focus();
		}

		private void listRemove_KeyDown(object sender, KeyEventArgs e)
		{
			var deleted = new List<object>();
			if (e.KeyCode == Keys.Delete)
			{
				foreach (var item in listRemove.SelectedItems)
				{
					File.Delete(item.ToString());
					deleted.Add(item);
				}//for

				foreach (var item in deleted)
				{
					listRemove.Items.Remove(item);
				}//for
			}//if
		}

		private void listFolders_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				;
			}//if
			else if (e.KeyCode == Keys.Space)
			{
				;
			}//if
		}//function
	}//class
}
