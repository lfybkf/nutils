using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileNameEdit
{
    public static class Extensions
    {
        public static void setChooser(this Form frm, Chooser chooser)  {  frm.Tag = chooser;  }//function
        public static Chooser getChooser(this Form frm) { return (frm.Tag as Chooser); }//function
    }//class
}//ns
