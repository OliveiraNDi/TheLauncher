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

        const string pathApp = "..\\..\\McApp.txt";
        const string pathFolder = "..\\..\\McFolder.txt";
        const string Config = "..\\..\\Config.txt";
        static void Main(string[] args)
        {
            File.AppendAllText(pathApp, "");
            File.AppendAllText(pathFolder, "");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("-\tPour voir toutes les commandes faites \"help\"\t-");
            Console.WriteLine("---------------------------------------------------------");
            string strSaisie = "";
            do
            {
                Console.WriteLine("Run");
                Console.Write("$");
                strSaisie = (Console.ReadLine());

                switch(strSaisie)
                {
                    case "help":
                                                // Call HelpText()
                        break;
                    case "list":
                                                // Call ReadFilesList()
                        break;
                    case "add":
                        
                        break;
                    case "add -a":
                    case "add -f":
                        if (strSaisie == "add -a")
                        {
                            // Call WriteMcApp(add -a)
                        }
                        else
                        {
                            // Call WriteMcFile(add -f)
                        }
                        break;
                    case "config":
                        break;
                    default:
                            // Call ReadLines(strSaisie)
                        break;

                        
                }
            } while (strSaisie != "exit");
        }

    }
}
