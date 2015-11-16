namespace Taimer
{
	partial class frmTaimer
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
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panMain = new System.Windows.Forms.Panel();
			this.lbInfo = new System.Windows.Forms.Label();
			this.lbTime = new System.Windows.Forms.Label();
			this.timerMain = new System.Windows.Forms.Timer(this.components);
			this.timerSec = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.panMain);
			this.panel1.Controls.Add(this.lbInfo);
			this.panel1.Controls.Add(this.lbTime);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(293, 458);
			this.panel1.TabIndex = 0;
			// 
			// panMain
			// 
			this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panMain.Location = new System.Drawing.Point(0, 58);
			this.panMain.Name = "panMain";
			this.panMain.Size = new System.Drawing.Size(293, 400);
			this.panMain.TabIndex = 2;
			// 
			// lbInfo
			// 
			this.lbInfo.AutoSize = true;
			this.lbInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbInfo.Location = new System.Drawing.Point(0, 29);
			this.lbInfo.Name = "lbInfo";
			this.lbInfo.Size = new System.Drawing.Size(52, 29);
			this.lbInfo.TabIndex = 1;
			this.lbInfo.Text = "Info";
			// 
			// lbTime
			// 
			this.lbTime.AutoSize = true;
			this.lbTime.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbTime.Location = new System.Drawing.Point(0, 0);
			this.lbTime.Name = "lbTime";
			this.lbTime.Size = new System.Drawing.Size(69, 29);
			this.lbTime.TabIndex = 0;
			this.lbTime.Text = "Time";
			// 
			// timerMain
			// 
			this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
			// 
			// timerSec
			// 
			this.timerSec.Tick += new System.EventHandler(this.timerSec_Tick);
			// 
			// frmTaimer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(293, 458);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(5);
			this.MaximizeBox = false;
			this.Name = "frmTaimer";
			this.Text = "Taimer";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmTaimer_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lbTime;
		private System.Windows.Forms.Label lbInfo;
		private System.Windows.Forms.Panel panMain;
		private System.Windows.Forms.Timer timerMain;
		private System.Windows.Forms.Timer timerSec;
	}
}

