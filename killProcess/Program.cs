using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace killProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (string s in args)   { kill(s); }//for
            }//if
        }//function

        static void kill(string pname)
        {
            var plist = Process.GetProcessesByName(pname);
            Console.WriteLine("Want to kill " + pname);
            Console.WriteLine("  plist.Count = " + plist.Length);
            foreach (var p in plist)
            {
                Console.WriteLine("    killing = " + p.MainWindowTitle);
                p.Kill();
                p.WaitForExit(10000);
            }//for
        }//function
    }//class
}
