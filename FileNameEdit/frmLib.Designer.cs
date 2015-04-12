namespace FileNameEdit
{
	partial class frmLib
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOK = new System.Windows.Forms.Button();
			this.lbAuthor = new System.Windows.Forms.Label();
			this.lbSeria = new System.Windows.Forms.Label();
			this.lbName = new System.Windows.Forms.Label();
			this.lbNomer = new System.Windows.Forms.Label();
			this.ctlAuthor = new System.Windows.Forms.TextBox();
			this.ctlName = new System.Windows.Forms.TextBox();
			this.ctlSeria = new System.Windows.Forms.TextBox();
			this.ctlNomer = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.43505F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.56495F));
			this.tableLayoutPanel1.Controls.Add(this.btnOK, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.lbAuthor, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lbSeria, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lbName, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lbNomer, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.ctlAuthor, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.ctlSeria, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.ctlName, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.ctlNomer, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(661, 257);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOK.Location = new System.Drawing.Point(177, 207);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(481, 47);
			this.btnOK.TabIndex = 100;
			this.btnOK.Text = "button1";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// lbAuthor
			// 
			this.lbAuthor.AutoSize = true;
			this.lbAuthor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbAuthor.Location = new System.Drawing.Point(3, 0);
			this.lbAuthor.Name = "lbAuthor";
			this.lbAuthor.Size = new System.Drawing.Size(168, 51);
			this.lbAuthor.TabIndex = 5;
			this.lbAuthor.Text = "Автор";
			this.lbAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbSeria
			// 
			this.lbSeria.AutoSize = true;
			this.lbSeria.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbSeria.Location = new System.Drawing.Point(3, 51);
			this.lbSeria.Name = "lbSeria";
			this.lbSeria.Size = new System.Drawing.Size(168, 51);
			this.lbSeria.TabIndex = 1;
			this.lbSeria.Text = "Серия";
			this.lbSeria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbName
			// 
			this.lbName.AutoSize = true;
			this.lbName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbName.Location = new System.Drawing.Point(3, 102);
			this.lbName.Name = "lbName";
			this.lbName.Size = new System.Drawing.Size(168, 51);
			this.lbName.TabIndex = 1;
			this.lbName.Text = "Название";
			this.lbName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbNomer
			// 
			this.lbNomer.AutoSize = true;
			this.lbNomer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbNomer.Location = new System.Drawing.Point(3, 153);
			this.lbNomer.Name = "lbNomer";
			this.lbNomer.Size = new System.Drawing.Size(168, 51);
			this.lbNomer.TabIndex = 6;
			this.lbNomer.Text = "Номер";
			this.lbNomer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ctlAuthor
			// 
			this.ctlAuthor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlAuthor.Location = new System.Drawing.Point(177, 3);
			this.ctlAuthor.Name = "ctlAuthor";
			this.ctlAuthor.Size = new System.Drawing.Size(481, 37);
			this.ctlAuthor.TabIndex = 2;
			// 
			// ctlName
			// 
			this.ctlName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlName.Location = new System.Drawing.Point(177, 54);
			this.ctlName.Name = "ctlName";
			this.ctlName.Size = new System.Drawing.Size(481, 37);
			this.ctlName.TabIndex = 1;
			// 
			// ctlSeria
			// 
			this.ctlSeria.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlSeria.Location = new System.Drawing.Point(177, 105);
			this.ctlSeria.Name = "ctlSeria";
			this.ctlSeria.Size = new System.Drawing.Size(481, 37);
			this.ctlSeria.TabIndex = 1;
			// 
			// ctlNomer
			// 
			this.ctlNomer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlNomer.Location = new System.Drawing.Point(177, 156);
			this.ctlNomer.Name = "ctlNomer";
			this.ctlNomer.Size = new System.Drawing.Size(481, 37);
			this.ctlNomer.TabIndex = 3;
			// 
			// frmLib
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 257);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(6);
			this.Name = "frmLib";
			this.Text = "Book";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.Label lbNomer;
        private System.Windows.Forms.TextBox ctlName;
        private System.Windows.Forms.TextBox ctlAuthor;
        private System.Windows.Forms.TextBox ctlNomer;
				private System.Windows.Forms.Label lbSeria;
				private System.Windows.Forms.TextBox ctlSeria;
    }
}