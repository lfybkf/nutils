namespace FileNameEdit
{
	partial class frmVideo
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
			this.ctlOld = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.ctlName = new System.Windows.Forms.TextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.ctlYear = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctlOld
			// 
			this.ctlOld.Dock = System.Windows.Forms.DockStyle.Top;
			this.ctlOld.Location = new System.Drawing.Point(0, 0);
			this.ctlOld.Name = "ctlOld";
			this.ctlOld.ReadOnly = true;
			this.ctlOld.Size = new System.Drawing.Size(669, 34);
			this.ctlOld.TabIndex = 0;
			this.ctlOld.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78, 29);
			this.label1.TabIndex = 1;
			this.label1.Text = "Name";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ctlName);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 34);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(669, 49);
			this.panel1.TabIndex = 2;
			// 
			// ctlName
			// 
			this.ctlName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlName.Location = new System.Drawing.Point(78, 0);
			this.ctlName.Name = "ctlName";
			this.ctlName.Size = new System.Drawing.Size(591, 34);
			this.ctlName.TabIndex = 2;
			this.ctlName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctlName_KeyUp);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ctlYear);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 83);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(669, 49);
			this.panel2.TabIndex = 3;
			// 
			// ctlYear
			// 
			this.ctlYear.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlYear.Location = new System.Drawing.Point(64, 0);
			this.ctlYear.Name = "ctlYear";
			this.ctlYear.Size = new System.Drawing.Size(605, 34);
			this.ctlYear.TabIndex = 2;
			this.ctlYear.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctlName_KeyUp);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 29);
			this.label2.TabIndex = 1;
			this.label2.Text = "Year";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btnOK);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 132);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(669, 40);
			this.panel3.TabIndex = 4;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOK.Location = new System.Drawing.Point(0, 0);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(669, 40);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// frmVideo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 184);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ctlOld);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(5);
			this.Name = "frmVideo";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.frmVideo_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox ctlOld;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox ctlName;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TextBox ctlYear;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnOK;
	}
}

