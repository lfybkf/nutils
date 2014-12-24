using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenameRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            string myName = "RenameRandom";
            string baseDir = Environment.CurrentDirectory;
            string prefix = baseDir.Split(Path.DirectorySeparatorChar).Last();
            Random rand = new Random();
            string newName;
            foreach (var file in Directory.GetFiles(baseDir))
            {
                newName = string.Format("{0}_{1:D7}{2}", prefix, rand.Next(9999999), Path.GetExtension(file));
                //newName = Path.GetRandomFileName();
                if (File.Exists(newName) == false && Path.GetFileNameWithoutExtension(file) != myName )
                    File.Move(file, newName);
            }//for
        }//function
    }
}
