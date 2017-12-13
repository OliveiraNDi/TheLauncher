using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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


        static void RunApp(string path)
        {
            Process pProcess = new Process();
            pProcess.StartInfo.FileName = path;
            pProcess.StartInfo.Arguments = ""; //argument
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.Start();
        }
        static void RunFolder(string path)
        {
            Process.Start("explorer.exe", path);
        }

        static void HelpText()
        {
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tOuvrir un mot-clef\t\t\t\t\top [Mot-clef]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tCréer un mot-clef\t\t\t\t\tadd [Mot-clef] [Chemin accès]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tEnlever un ou plusieurs mots-clefs:");
            Console.WriteLine("\t\tEnlever un mot-clef\t\t\t\trm [Mot-clef]");
            Console.WriteLine("\t\tEnlever tous mots-clefs\t\t\t\trm --all");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tModifier le nom du mot-clef ou son chemin d'acces:");
            Console.WriteLine("\t\tModifier le nom d'un mot-clef\t\t\tmd [Mot-clef] [Nouveau nom du mot-clef]");
            Console.WriteLine("\t\tModifier le chemin d'acces\t\t\tmd -c [Nom du mot-clef] [Nouveau chemin d'acces]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tModifier l'interface graphique");
            Console.WriteLine("\t\tModifier la couleur de fond\t\t\tbackground [couleur]");
            Console.WriteLine("\t\tModifier la couleur du texte\t\t\tcolor [couleur]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tAfficher la liste de mots-clefs\t\t\t\tlist");
            Console.WriteLine("\n------------------------------------------------------\n");
        }


    }
}
