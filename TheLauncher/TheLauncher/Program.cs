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
            File.AppendAllText(Config, "");
            File.AppendAllText(pathApp, "");
            File.AppendAllText(pathFolder, "");

            string strSaisie = "";

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("-\tPour voir toutes les commandes faites \"help\"\t-");
            Console.WriteLine("---------------------------------------------------------");
            do
            {
                Console.Write("Run\n$ ");
                strSaisie = (Console.ReadLine()); // Lire la saisie de l'utilisateur
                ReadLines(strSaisie); // Regarde directement si la saisie est un mot-clef

                switch (strSaisie)
                {
                    case "exit":
                        Console.WriteLine("Leaving...");
                        Thread.Sleep(500); // attendre 500ms avant de quitter
                        break;
                    case "help":
                        HelpText();     // Call HelpText()
                        break;
                    case "list":
                        ReadFilesList();        // Call ReadFilesList()
                        break;
                    case "add":
                        Console.Write("Si ce n'est pas la bonne commande faites \"exit\"\nDossier ou Fichier ?\n 1\\2 : ");
                        strSaisie = (Console.ReadLine());
                        switch (strSaisie)
                        {
                            case "1":
                                WriteFileFolder();
                                break;
                            case "2":
                                WriteFileFolder();
                        }
                        break;
                    case "add -a":
                    case "add -f":
                        if (strSaisie == "add -a")
                        {
                            WriteFileApp();     // Call WriteMcApp(add -a)
                        }
                        else
                        {
                            WriteFileFolder();   // Call WriteMcFile(add -f)
                        }
                        break;
                    case "clear":
                        Console.Clear();

                        Console.WriteLine("---------------------------------------------------------");
                        Console.WriteLine("-\tPour voir toutes les commandes faites \"help\"\t-");
                        Console.WriteLine("---------------------------------------------------------");
                        break;
                    case "config":

                        break;
                    default:
                        if (strSaisie == "")
                        {

                        }
                        else
                        {
                            Console.Write("Ce mot-clef n'exite pas\nVoulez-vous le créer ?\n y/n : ");
                            strSaisie = Console.ReadLine();
                            if (strSaisie == "y")
                            {
                                Console.WriteLine("Dossier ou Fichier ?\n 1\\2");
                                strSaisie = (Console.ReadLine());
                                switch (strSaisie)
                                {
                                    case "1":
                                    case "2":
                                        WriteFileFolder();
                                        break;
                                }
                            }
                        }
                        break;


                }
            } while (strSaisie != "exit");
        }        

    }
}
