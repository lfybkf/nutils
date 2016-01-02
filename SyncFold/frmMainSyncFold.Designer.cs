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
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnSync = new System.Windows.Forms.Button();
			this.listProfiles = new System.Windows.Forms.ListBox();
			this.listAdd = new System.Windows.Forms.ListBox();
			this.listDel = new System.Windows.Forms.ListBox();
			this.dialogDst = new System.Windows.Forms.FolderBrowserDialog();
			this.listLog = new System.Windows.Forms.ListBox();
			this.progressBar = new System.Windows.Forms.ProgressBar();
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
			this.panMain.Size = new System.Drawing.Size(759, 488);
			this.panMain.TabIndex = 0;
			// 
			// panManage
			// 
			this.panManage.Controls.Add(this.btnDelete);
			this.panManage.Controls.Add(this.btnRefresh);
			this.panManage.Controls.Add(this.btnSync);
			this.panManage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panManage.Location = new System.Drawing.Point(3, 310);
			this.panManage.Name = "panManage";
			this.panManage.Size = new System.Drawing.Size(202, 175);
			this.panManage.TabIndex = 1;
			// 
			// btnDelete
			// 
			this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnDelete.Location = new System.Drawing.Point(0, 92);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(202, 46);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "button1";
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnRefresh
			// 
			this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnRefresh.Location = new System.Drawing.Point(0, 46);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(202, 46);
			this.btnRefresh.TabIndex = 2;
			this.btnRefresh.Text = "button1";
			this.btnRefresh.UseVisualStyleBackColor = true;
			// 
			// btnSync
			// 
			this.btnSync.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSync.Location = new System.Drawing.Point(0, 0);
			this.btnSync.Name = "btnSync";
			this.btnSync.Size = new System.Drawing.Size(202, 46);
			this.btnSync.TabIndex = 1;
			this.btnSync.Text = "button1";
			this.btnSync.UseVisualStyleBackColor = true;
			// 
			// listProfiles
			// 
			this.listProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listProfiles.FormattingEnabled = true;
			this.listProfiles.ItemHeight = 25;
			this.listProfiles.Location = new System.Drawing.Point(3, 3);
			this.listProfiles.Name = "listProfiles";
			this.listProfiles.Size = new System.Drawing.Size(202, 301);
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
			this.listAdd.Size = new System.Drawing.Size(545, 301);
			this.listAdd.TabIndex = 30;
			// 
			// listDel
			// 
			this.listDel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listDel.FormattingEnabled = true;
			this.listDel.ItemHeight = 25;
			this.listDel.Location = new System.Drawing.Point(211, 310);
			this.listDel.Name = "listDel";
			this.listDel.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listDel.Size = new System.Drawing.Size(545, 175);
			this.listDel.TabIndex = 40;
			this.listDel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listDel_KeyDown);
			// 
			// dialogDst
			// 
			this.dialogDst.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.dialogDst.ShowNewFolderButton = false;
			// 
			// listLog
			// 
			this.listLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listLog.FormattingEnabled = true;
			this.listLog.ItemHeight = 18;
			this.listLog.Location = new System.Drawing.Point(0, 488);
			this.listLog.Name = "listLog";
			this.listLog.Size = new System.Drawing.Size(759, 94);
			this.listLog.TabIndex = 1;
			// 
			// progressBar
			// 
			this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar.Location = new System.Drawing.Point(0, 582);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(759, 23);
			this.progressBar.TabIndex = 2;
			// 
			// frmMainSyncFold
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(759, 605);
			this.Controls.Add(this.panMain);
			this.Controls.Add(this.listLog);
			this.Controls.Add(this.progressBar);
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
		private System.Windows.Forms.Button btnSync;
		private System.Windows.Forms.ListBox listProfiles;
		private System.Windows.Forms.ListBox listAdd;
		private System.Windows.Forms.ListBox listDel;
		private System.Windows.Forms.FolderBrowserDialog dialogDst;
		private System.Windows.Forms.ListBox listLog;
		private System.Windows.Forms.Button btnRefresh;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ProgressBar progressBar;
	}
}

