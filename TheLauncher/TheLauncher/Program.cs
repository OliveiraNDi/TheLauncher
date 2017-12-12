using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLauncher
{
    class Program
    {

        const string pathFile = "..\\..\\mcFile.txt";
        const string pathFolder = "..\\..\\mcFolder.txt";

        static void Main(string[] args)
        {
            File.AppendAllText(pathFile, "");
            File.AppendAllText(pathFolder, "");

        }

    }
}
