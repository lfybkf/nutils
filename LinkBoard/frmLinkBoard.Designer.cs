namespace LinkBoard
{
	partial class frmLinkBoard
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
			this.listLinks = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// listLinks
			// 
			this.listLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listLinks.FormattingEnabled = true;
			this.listLinks.ItemHeight = 20;
			this.listLinks.Location = new System.Drawing.Point(0, 0);
			this.listLinks.Name = "listLinks";
			this.listLinks.Size = new System.Drawing.Size(750, 410);
			this.listLinks.TabIndex = 0;
			// 
			// frmLinkBoard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(750, 410);
			this.Controls.Add(this.listLinks);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "frmLinkBoard";
			this.Text = "LinkBoard";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLinkBoard_FormClosed);
			this.Load += new System.EventHandler(this.frmLinkBoard_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox listLinks;
	}
}

