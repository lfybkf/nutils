namespace SyncFold
{
	partial class frmMainSyncFold
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panMain = new System.Windows.Forms.TableLayoutPanel();
			this.panManage = new System.Windows.Forms.Panel();
			this.btnAdd = new System.Windows.Forms.Button();
			this.listProfiles = new System.Windows.Forms.ListBox();
			this.listAdd = new System.Windows.Forms.ListBox();
			this.listDel = new System.Windows.Forms.ListBox();
			this.dialogDst = new System.Windows.Forms.FolderBrowserDialog();
			this.panMain.SuspendLayout();
			this.panManage.SuspendLayout();
			this.SuspendLayout();
			// 
			// panMain
			// 
			this.panMain.ColumnCount = 2;
			this.panMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.40448F));
			this.panMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.59552F));
			this.panMain.Controls.Add(this.panManage, 0, 1);
			this.panMain.Controls.Add(this.listProfiles, 0, 0);
			this.panMain.Controls.Add(this.listAdd, 1, 0);
			this.panMain.Controls.Add(this.listDel, 1, 1);
			this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panMain.Location = new System.Drawing.Point(0, 0);
			this.panMain.Name = "panMain";
			this.panMain.RowCount = 2;
			this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.91667F));
			this.panMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.08333F));
			this.panMain.Size = new System.Drawing.Size(759, 605);
			this.panMain.TabIndex = 0;
			// 
			// panManage
			// 
			this.panManage.Controls.Add(this.btnAdd);
			this.panManage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panManage.Location = new System.Drawing.Point(3, 383);
			this.panManage.Name = "panManage";
			this.panManage.Size = new System.Drawing.Size(202, 219);
			this.panManage.TabIndex = 1;
			// 
			// btnAdd
			// 
			this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnAdd.Location = new System.Drawing.Point(0, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(202, 46);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "button1";
			this.btnAdd.UseVisualStyleBackColor = true;
			// 
			// listProfiles
			// 
			this.listProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listProfiles.FormattingEnabled = true;
			this.listProfiles.ItemHeight = 25;
			this.listProfiles.Location = new System.Drawing.Point(3, 3);
			this.listProfiles.Name = "listProfiles";
			this.listProfiles.Size = new System.Drawing.Size(202, 374);
			this.listProfiles.TabIndex = 0;
			this.listProfiles.SelectedIndexChanged += new System.EventHandler(this.listProfiles_SelectedIndexChanged);
			// 
			// listAdd
			// 
			this.listAdd.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listAdd.FormattingEnabled = true;
			this.listAdd.ItemHeight = 25;
			this.listAdd.Location = new System.Drawing.Point(211, 3);
			this.listAdd.Name = "listAdd";
			this.listAdd.Size = new System.Drawing.Size(545, 374);
			this.listAdd.TabIndex = 30;
			// 
			// listDel
			// 
			this.listDel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listDel.FormattingEnabled = true;
			this.listDel.ItemHeight = 25;
			this.listDel.Location = new System.Drawing.Point(211, 383);
			this.listDel.Name = "listDel";
			this.listDel.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listDel.Size = new System.Drawing.Size(545, 219);
			this.listDel.TabIndex = 40;
			this.listDel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listRemove_KeyDown);
			// 
			// dialogDst
			// 
			this.dialogDst.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.dialogDst.ShowNewFolderButton = false;
			// 
			// frmMainSyncFold
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(759, 605);
			this.Controls.Add(this.panMain);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "frmMainSyncFold";
			this.Text = "BackupFolder";
			this.Load += new System.EventHandler(this.frmMainBackupFolder_Load);
			this.panMain.ResumeLayout(false);
			this.panManage.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel panMain;
		private System.Windows.Forms.Panel panManage;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ListBox listProfiles;
		private System.Windows.Forms.ListBox listAdd;
		private System.Windows.Forms.ListBox listDel;
		private System.Windows.Forms.FolderBrowserDialog dialogDst;
	}
}

