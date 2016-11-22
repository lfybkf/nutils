using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using io = System.IO;

namespace LinkBoard
{
	public partial class frmLinkBoard : Form
	{
		static List<string> links = new List<string>();
		static readonly string http = @"http";
		//static readonly string https = @"https";
		static string prefix = "lb";
		static string path;

		#region dllImport
		[DllImport("User32.dll")]
		protected static extern int SetClipboardViewer(int hWndNewViewer);
		// Добавляет новое окно в цепочку окон уведомляемых сообщением wm_DrawClipboard 
		// пpи изменении буфеpа
		// hWind указатель на окно
		// Возвращает указатель на следующие окно в цепочке

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
		// Удаляет окно hWindDel из цепочки просмотра буфера изаменяет его hWindNext
		// Возвращает true если окно найдено и удалено

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);
		// Посылает сообщение окну
		// hWind указатель на окно, msg код сообщения, 
		// wparam1 и lparam дополнительная информация о сообщении
		// Возвращает значение переданое функцией окна

		IntPtr nextClipboardViewer; //Для хранения указателя на следующее окно
		#endregion

		public frmLinkBoard()
		{
			InitializeComponent();
			nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
		}//func

		private void frmLinkBoard_Load(object sender, EventArgs e)
		{
			Clipboard.Clear();
			prefix = Prompt.ShowDialog(prefix, "get a Prefix", new[] {"spa", "other"});
			path = io.Path.Combine(Environment.CurrentDirectory, string.Format("{0}_{1}.txt", DateTime.Now.ToString("MMdd_HHmm"), prefix));
			Text = io.Path.GetFileNameWithoutExtension(path);
		}//func

		private void frmLinkBoard_FormClosed(object sender, FormClosedEventArgs e)
		{
			ChangeClipboardChain(this.Handle, nextClipboardViewer);
		}//func

		// переопределяем метод
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			// defined in winuser.h
			const int WM_DRAWCLIPBOARD = 0x308;
			const int WM_CHANGECBCHAIN = 0x030D;

			switch (m.Msg)
			{
				case WM_DRAWCLIPBOARD: //содержимое буфера изменилось можно работать
					DisplayClipboardData();
					SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
					break;

				case WM_CHANGECBCHAIN: //одно из окон удаено из цепочки, нужно восстановить цепочку
					if (m.WParam == nextClipboardViewer)// Если удаляемое окно это следующие окно в цепочке
						nextClipboardViewer = m.LParam;//Следующим окном делаем окно идущее в цепочке за удаляемым
					else
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);// Посылаем сообщение этому окну
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		public void ListRefresh(ListBox lb)
		{
			if (lb.DataSource == null)
			{
				lb.DataSource = links;
			}//if
			var bc = lb.BindingContext[lb.DataSource];
			(bc as CurrencyManager).Refresh();
		}//function

		void DisplayClipboardData()
		{
			try
			{
				if (path == null)	{ Clipboard.Clear(); return; }
				
				IDataObject iData = new DataObject();
				iData = Clipboard.GetDataObject();

				if (iData.GetDataPresent(DataFormats.Text))
				{
					string s = (string)iData.GetData(DataFormats.Text);
					if (s.StartsWith(http) && links.Contains(s) == false)
					{
						links.Add(s);
						ListRefresh(listLinks);
						io.File.WriteAllLines(path, links);
					}//if
				}//if
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

	}//class

	public static class Prompt
	{
		public static string ShowDialog(string text, string caption, string[] choose = null)
		{
			Form frm = new Form()
			{
				Width = 300,
				Height = 150,
				FormBorderStyle = FormBorderStyle.FixedDialog,
				Text = caption,
				StartPosition = FormStartPosition.CenterScreen,
				MinimizeBox = false,
				MaximizeBox = false,
			};
			frm.Font = new Font("Courier", 14);
			ComboBox ctlText = new ComboBox() { 
				Text = text, 
				Dock = DockStyle.Top, 
				AutoCompleteMode = AutoCompleteMode.SuggestAppend,
				AutoCompleteSource = AutoCompleteSource.ListItems,
			};
			if (choose != null)
			{
				ctlText.Items.AddRange(choose);		
			}//if
			Button btnOK = new Button() { Text = "OK", Dock = DockStyle.Fill, DialogResult = DialogResult.OK };
			btnOK.Click += (sender, e) => { frm.Close(); };
			frm.Controls.Add(ctlText);
			frm.Controls.Add(btnOK);
			frm.AcceptButton = btnOK;
			btnOK.BringToFront();


			return frm.ShowDialog() == DialogResult.OK ? ctlText.Text : string.Empty;
		}
	}
}//ns
