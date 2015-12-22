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

namespace BackupFolder
{
	public partial class frmMainSyncFold : Form
	{
		IList<Kommand> kmds = new List<Kommand>();
		string baseDst { get { return txtDst.Text; } set { txtDst.Text = value; } }
		string baseSrc { get; set; }
		string itemSrc {
			get {
				return listFolders.SelectedItems.Count > 0 ?
					listFolders.SelectedItems[0].ToString() : string.Empty;
			}
		}

		public frmMainSyncFold()
		{
			InitializeComponent();
		}

		private void InitKmd()
		{
			kmds.Add(new Kommand("List", kmdList));
			kmds.Add(new Kommand("Work", kmdWork));
			kmds.Add(new Kommand("Dst", kmdDst));

			foreach (Kommand kmd in kmds) { kmd.isExecuted += kmd_isExecuted; }
			foreach (var btn in this.panManage.Controls.OfType<Button>())
			{
				kmds.LinkToComponent(btn);
			}//for
		}

		private void kmdDst()
		{
			var dialog = dialogDst;
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				baseDst = dialog.SelectedPath;
			}//if
			else
			{
				baseDst = string.Empty;
			}//else
		}

		void kmd_isExecuted(object sender, EventArgs e) { this.Text = (sender as Kommand).Caption; }


		private void kmdWork()
		{
			Func<string, string> newPath = (old) => {
				string part = old.after(baseSrc);
				string result = Path.Combine(baseDst, part);//baseDst.addToPath(part);
				return result;
			};

			Action<string> Copy_Bat = (s) => {
				string cmd = "copy \"{0}\" \"{1}\"".fmt(s, newPath(s));
				string bat = itemSrc + ".bat";
				if (!File.Exists(bat))
				{
					//File.CreateText(bat);
				}//if
				File.AppendAllLines(bat, new string[] { cmd });
			};

			Action<string> Copy = (s) =>
			{
				File.Copy(s, newPath(s));
			};

			while (listAdd.Items.Count > 0)
			{
				var item = listAdd.Items[0];
				this.Text = "Copy: {0}".fmt(item);
				Copy(item.ToString());
				listAdd.Items.Remove(item);
			}
		}

		private void kmdList()
		{
			Func<IEnumerable<string>, string, string, bool> isHere =
				(files, begin, file) =>
				{
					return files.Any(s => s.after(begin) == file);
				};
			var filesSrc = getFiles(baseSrc.addToPath(itemSrc)).ToArray();
			var filesDst = getFiles(baseDst.addToPath(itemSrc)).ToArray();
			var filesAdd = filesSrc.Where(s => !isHere(filesDst, baseDst, s.after(baseSrc)));
			var filesRemove = filesDst.Where(s => !isHere(filesSrc, baseSrc, s.after(baseDst)));

			listAdd.Items.Clear();
			listRemove.Items.Clear();
			listAdd.Items.AddRange(filesAdd.OrderBy(s => s).ToArray());
			listRemove.Items.AddRange(filesRemove.OrderBy(s => s).ToArray());
		}

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

			baseSrc = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
			var folders = Directory.GetDirectories(baseSrc)
				.Select(s => Path.GetFileName(s))
				.ToArray();
			listFolders.Items.AddRange(folders);

			kmdDst();

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
				kmdList();
			}//if
			else if (e.KeyCode == Keys.Space)
			{
				kmdWork();
			}//if
		}//function
	}//class
}
