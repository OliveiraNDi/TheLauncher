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

        const string pathApp = "..\\..\\keywordsApp.txt";
        const string pathFolder = "..\\..\\keywordsFolder.txt";
        const string Config = "..\\..\\config.txt";


        static void Main(string[] args)
        {
            File.AppendAllText(Config, "");
            File.AppendAllText(pathApp, "");
            File.AppendAllText(pathFolder, "");

            string newLine = "";
            string EnterText = "";

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("-\tPour voir toutes les commandes faites \"help\"\t-");
            Console.WriteLine("---------------------------------------------------------");
            do
            {
                Console.Write("Run\n$ ");
                EnterText = (Console.ReadLine()); // Lire la saisie de l'utilisateur
                string[] word = EnterText.Split(' ');

                switch (word[0])
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
                        switch(word[1])
                        {
                            case "-a":
                                newLine += word[2];
                                newLine += " ";
                                newLine += word[3];
                                newLine += "\n";
                                File.AppendAllText(pathApp, newLine);
                                break;
                            case "-f":
                                newLine += word[2];
                                newLine += " ";
                                newLine += word[3];
                                newLine += "\n";
                                File.AppendAllText(pathFolder, newLine);
                                break;
                            default:
                                Console.Write("App ou Dossier ?\n1|2 ");
                                EnterText
                                break;
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
                        if (EnterText == "")
                        {

                        }
                        else
                        {
                            bool LauncherOK = StartApp(EnterText); // Regarde directement si la saisie est un mot-clef
                            if (!LauncherOK)
                            {
                                Console.Write("Ce mot-clef n'exite pas\nVoulez-vous le créer ?\n y/n : ");
                                EnterText = Console.ReadLine();
                                if (EnterText == "y")
                                {
                                    Console.WriteLine("Dossier ou Fichier ?\n 1\\2");
                                    EnterText = (Console.ReadLine());
                                    switch (EnterText)
                                    {
                                        case "1":
                                        case "2":
                                            WriteFileFolder();
                                            break;
                                    }
                                }
                            }
                        }
                        break;


                }
            } while (EnterText != "exit");
        }

        static void WriteFileFolder()
        {
            string newLine = "";
            Console.WriteLine("Créer un mot-clef pour un dossier");
            Console.Write("Sans Espace au début\nEntre le nom du mot-clef: ");
            newLine += Console.ReadLine();
            newLine += " ";
            Console.Write("\nSans Espace au début\nEntre le chemin d'accès: ");
            newLine += Console.ReadLine();
            newLine += "\n";
            File.AppendAllText(pathFolder, newLine);
        }

        static void WriteFileApp()
        {
            string newLine = "";
            Console.WriteLine("Créer un mot-clef pour un fichier");
            Console.Write("Sans Espace au début\nEntre le nom du mot-clef: ");
            newLine += Console.ReadLine();
            newLine += " ";
            Console.Write("\nSans Espace au début\nEntre le chemin d'accès: ");
            newLine += Console.ReadLine();
            newLine += "\n";
            File.AppendAllText(pathApp, newLine);
        }

        static void ReadFilesList()
        {
            string[] linesApp = File.ReadAllLines(pathApp);
            string[] linesFolder = File.ReadAllLines(pathFolder);
            Console.WriteLine("Voici tous les mots-clefs qui exécutent des applications :");
            int i = 0;
            while (i < linesApp.Length)
            {
                string line = linesApp[i];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t{0}\n", line);
                i++;
            }
            Console.ResetColor();
            Console.WriteLine("Voici tout les mots-clef des dossiers :");
            i = 0;
            while (i < linesFolder.Length)
            {
                string line = linesFolder[i];
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\t{0}\n", line);
                i++;
            }
            Console.ResetColor();
        }
        

        static bool StartApp(string searchedWord)
        {
            string[] linesApp = File.ReadAllLines(pathApp);
            string CommandApp = FindCommand(searchedWord, linesApp);

            string[] linesFolder = File.ReadAllLines(pathFolder);
            string CommandFolder = FindCommand(searchedWord, linesFolder);

            if (CommandApp != "")
            {
                RunApp(CommandApp);
                return true;
            }
            else if (CommandFolder != "")
            {
                RunFolder(CommandFolder);
                return true;
            }
            else
            {
                return false;
            }
        }

        static string FindCommand(string Keyword, string[] List)
        {
            string command = "";
            int i = 0;
            while (i < List.Length)
            {
                string[] word = List[i].Split(new char[] { ' ' }, 2);
                if (word[0] == Keyword)
                {
                    command = word[1];
                }
                i++;
            }
            return command;
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
