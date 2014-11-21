using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static int LIMIT = 70;
        
        static void Main(string[] args)
        {
            string baseDir = Environment.CurrentDirectory;
            string prefix = baseDir.Split(Path.DirectorySeparatorChar).Last();
            string newDir = null;
            var files = Directory.GetFiles(baseDir);

            int portions = files.Length / LIMIT;
            for (int i = 0; i <= portions; i++)
            {
                newDir = string.Format("{0}_{1:D3}", prefix, i);
                Directory.CreateDirectory(newDir);
                var onePortionOfFiles = files.Skip(i * LIMIT).Take(LIMIT);
                foreach (var file in onePortionOfFiles)
                {
                    File.Move(file, Path.Combine(baseDir, newDir, Path.GetFileName(file)));
                }//for
            }//for

            Console.ReadLine();
        }//function
    }//class
}
