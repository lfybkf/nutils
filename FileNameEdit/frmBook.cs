using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
    public partial class frmBook : Form
    {
        Chooser obj;


        public frmBook()
        {
            InitializeComponent();
            
            ctlName.setRusLanguageOnEnter();
            ctlAuthor.setRusLanguageOnEnter();
            this.Load += frm_Load;
            btnOK.Click += btnOK_Click;            

            btnOK.Text = "OK";
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            Do();
        }//function

        private void Do()
        {
            string Name = ctlName.Text;
            string Author = ctlAuthor.Text;
            string Year = ctlYear.Text;
            if (string.IsNullOrWhiteSpace(Year) && string.IsNullOrWhiteSpace(Author))
                obj.New = Name;
            else
            {
                if (string.IsNullOrWhiteSpace(Year))
                    obj.New = string.Format("{0} ({1})", Name, Author);
                else if (string.IsNullOrWhiteSpace(Author))
                    obj.New = string.Format("{0} ({1})", Name, Year);
                else
                    obj.New = string.Format("{0} ({1} {2})", Name, Year, Author);
            }//else

            this.setChooser(null);
            Close();
        }//fucntion


        private void ctl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Do();
            else if (e.KeyCode == Keys.Escape)
            {
                obj.New = null;
                Close();
            }//else
        }//function

        private void frm_Load(object sender, EventArgs e)
        {
            obj = this.getChooser();
            Text = obj.Old;

            Regex rex; Match m; //MatchCollection mc; CaptureCollection cc; 
            Regex rexNameYear = new Regex(@"(.*) [(]([0-9]{4})[)]"); // Name (Year)
            Regex rexNameAuthor = new Regex(@"(.*) [(](.*)[)]"); // Name (Author)
            Regex rexAll = new Regex(@"(.*) [(]([0-9]{4}) (.*)[)]"); // Name (Year Author)
            if (false)
                ;
            else if (rexAll.IsMatch(obj.Old))
            {
                rex = rexAll;
                m = rex.Match(obj.Old);
                ctlName.Text = m.Groups[1].Value;
                ctlYear.Text = m.Groups[2].Value;
                ctlAuthor.Text = m.Groups[3].Value;
            }//else
            else if (rexNameYear.IsMatch(obj.Old))
            {
                rex = rexNameYear;
                m = rex.Match(obj.Old);
                ctlName.Text = m.Groups[1].Value;
                ctlYear.Text = m.Groups[2].Value;
            }//if
            else if (rexNameAuthor.IsMatch(obj.Old))
            {
                rex = rexNameAuthor;
                m = rex.Match(obj.Old);
                ctlName.Text = m.Groups[1].Value;
                ctlAuthor.Text = m.Groups[2].Value;
            }//else
            else
            {
                ctlName.Text = obj.Old;
            }//else
        }//function
    }//class
}
