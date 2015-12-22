namespace BackupFolder
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
			this.btnDst = new System.Windows.Forms.Button();
			this.txtDst = new System.Windows.Forms.TextBox();
			this.btnWork = new System.Windows.Forms.Button();
			this.btnList = new System.Windows.Forms.Button();
			this.listFolders = new System.Windows.Forms.ListBox();
			this.listAdd = new System.Windows.Forms.ListBox();
			this.listRemove = new System.Windows.Forms.ListBox();
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
			this.panMain.Controls.Add(this.listFolders, 0, 0);
			this.panMain.Controls.Add(this.listAdd, 1, 0);
			this.panMain.Controls.Add(this.listRemove, 1, 1);
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
			this.panManage.Controls.Add(this.btnDst);
			this.panManage.Controls.Add(this.txtDst);
			this.panManage.Controls.Add(this.btnWork);
			this.panManage.Controls.Add(this.btnList);
			this.panManage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panManage.Location = new System.Drawing.Point(3, 383);
			this.panManage.Name = "panManage";
			this.panManage.Size = new System.Drawing.Size(202, 219);
			this.panManage.TabIndex = 1;
			// 
			// btnDst
			// 
			this.btnDst.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnDst.Location = new System.Drawing.Point(0, 92);
			this.btnDst.Name = "btnDst";
			this.btnDst.Size = new System.Drawing.Size(202, 46);
			this.btnDst.TabIndex = 3;
			this.btnDst.Text = "button1";
			this.btnDst.UseVisualStyleBackColor = true;
			// 
			// txtDst
			// 
			this.txtDst.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.txtDst.Location = new System.Drawing.Point(0, 189);
			this.txtDst.Name = "txtDst";
			this.txtDst.ReadOnly = true;
			this.txtDst.Size = new System.Drawing.Size(202, 30);
			this.txtDst.TabIndex = 3;
			// 
			// btnWork
			// 
			this.btnWork.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnWork.Location = new System.Drawing.Point(0, 46);
			this.btnWork.Name = "btnWork";
			this.btnWork.Size = new System.Drawing.Size(202, 46);
			this.btnWork.TabIndex = 2;
			this.btnWork.Text = "button1";
			this.btnWork.UseVisualStyleBackColor = true;
			// 
			// btnList
			// 
			this.btnList.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnList.Location = new System.Drawing.Point(0, 0);
			this.btnList.Name = "btnList";
			this.btnList.Size = new System.Drawing.Size(202, 46);
			this.btnList.TabIndex = 1;
			this.btnList.Text = "button1";
			this.btnList.UseVisualStyleBackColor = true;
			// 
			// listFolders
			// 
			this.listFolders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listFolders.FormattingEnabled = true;
			this.listFolders.ItemHeight = 25;
			this.listFolders.Location = new System.Drawing.Point(3, 3);
			this.listFolders.Name = "listFolders";
			this.listFolders.Size = new System.Drawing.Size(202, 374);
			this.listFolders.TabIndex = 0;
			this.listFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listFolders_KeyDown);
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
			// listRemove
			// 
			this.listRemove.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listRemove.FormattingEnabled = true;
			this.listRemove.ItemHeight = 25;
			this.listRemove.Location = new System.Drawing.Point(211, 383);
			this.listRemove.Name = "listRemove";
			this.listRemove.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listRemove.Size = new System.Drawing.Size(545, 219);
			this.listRemove.TabIndex = 40;
			this.listRemove.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listRemove_KeyDown);
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
			this.panManage.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel panMain;
		private System.Windows.Forms.Panel panManage;
		private System.Windows.Forms.Button btnWork;
		private System.Windows.Forms.Button btnList;
		private System.Windows.Forms.TextBox txtDst;
		private System.Windows.Forms.Button btnDst;
		private System.Windows.Forms.ListBox listFolders;
		private System.Windows.Forms.ListBox listAdd;
		private System.Windows.Forms.ListBox listRemove;
		private System.Windows.Forms.FolderBrowserDialog dialogDst;
	}
}

