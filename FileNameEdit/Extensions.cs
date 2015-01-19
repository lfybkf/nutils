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
        public static void setRusLanguageOnEnter(this TextBox tb) 
        {
            EventHandler enter = (sender, e) =>
            {
                TextBox ctl = (sender as TextBox);
                if (ctl != null)
                    InputLanguage.CurrentInputLanguage = InputLanguage.FromCulture(new System.Globalization.CultureInfo("ru-RU"));
            };
            tb.Enter += enter; 
        }//function
    }//class
}//ns
