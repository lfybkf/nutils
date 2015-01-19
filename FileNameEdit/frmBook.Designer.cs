namespace FileNameEdit
{
    partial class frmBook
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctlName = new System.Windows.Forms.TextBox();
            this.ctlAuthor = new System.Windows.Forms.TextBox();
            this.ctlYear = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.43505F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.56496F));
            this.tableLayoutPanel1.Controls.Add(this.btnOK, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ctlName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ctlAuthor, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.ctlYear, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(661, 134);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(177, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(481, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "button1";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "Автор";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 33);
            this.label2.TabIndex = 6;
            this.label2.Text = "Год";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctlName
            // 
            this.ctlName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlName.Location = new System.Drawing.Point(177, 3);
            this.ctlName.Name = "ctlName";
            this.ctlName.Size = new System.Drawing.Size(481, 31);
            this.ctlName.TabIndex = 7;
            this.ctlName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctl_KeyUp);
            // 
            // ctlAuthor
            // 
            this.ctlAuthor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlAuthor.Location = new System.Drawing.Point(177, 36);
            this.ctlAuthor.Name = "ctlAuthor";
            this.ctlAuthor.Size = new System.Drawing.Size(481, 31);
            this.ctlAuthor.TabIndex = 8;
            this.ctlAuthor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctl_KeyUp);
            // 
            // ctlYear
            // 
            this.ctlYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlYear.Location = new System.Drawing.Point(177, 69);
            this.ctlYear.Name = "ctlYear";
            this.ctlYear.Size = new System.Drawing.Size(481, 31);
            this.ctlYear.TabIndex = 9;
            this.ctlYear.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ctl_KeyUp);
            // 
            // frmBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 134);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmBook";
            this.Text = "frmBook";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ctlName;
        private System.Windows.Forms.TextBox ctlAuthor;
        private System.Windows.Forms.TextBox ctlYear;
    }
}