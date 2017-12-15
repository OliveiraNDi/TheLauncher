// Auteur: Diogo Oliveira Nunes
// Projet: TheLauncher
// Date: 21.11.2017
// Version de VS: 15.4
// OS: Win10 Pro

// But: Ouvrir plus facilement des fichers ou des dossiers
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

        const string pathApp = "keywordsApp.txt";
        const string pathFolder = "keywordsFolder.txt";
        const string Config = "config.txt";
        static string Name; 
        static string Pc;
        
        static void Main(string[] args)
        {
            File.AppendAllText(pathApp, "");
            File.AppendAllText(pathFolder, "");
            File.AppendAllText(Config, "");

            string newLine = "";
            string EnterText = "";
            string strSecondWord;


            Console.Write("One spacebar\nInsert your name : ");
            ConfigTest(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-\tTo see all commands made \"help\"\t\t-");
            Console.WriteLine("-------------------------------------------------");
            do
            {
                Console.Write("\n{0}@{1}-TheLauncher\n$ ", Name, Pc);
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
                    case "rm":
                        if (word.Length < 3)
                        {
                            strSecondWord = "-h";
                        }
                        else
                        {
                            strSecondWord = word[1];
                        }
                        switch (strSecondWord)
                        {
                            case "-a":
                                RemoveKeywordApp(word[2]);
                                break;
                            case "-f":
                                RemoveKeywordFolder(word[2]);
                                break;
                            case "-h":
                                HelpText();
                                break;
                        }
                        break;
                    case "add":
                        if (word.Length < 2)
                        {
                            strSecondWord = "-h";
                        }
                        else
                        {
                            strSecondWord = word[1];
                        }
                        switch (strSecondWord)
                        {
                            case "-a":
                                if (word.Length < 4)
                                {
                                    HelpText();
                                }
                                else
                                {
                                    newLine += word[2];
                                    newLine += " ";
                                    newLine += word[3];
                                    newLine += "\n";
                                    File.AppendAllText(pathApp, newLine);
                                }
                                break;
                            case "-f":
                                if (word.Length < 4)
                                {
                                    HelpText();
                                }
                                else
                                {
                                    newLine += word[2];
                                    newLine += " ";
                                    newLine += word[3];
                                    newLine += "\n";
                                    File.AppendAllText(pathFolder, newLine);
                                }
                                break;
                            case "-h":
                                HelpText();
                                break;
                            default:
                                HelpText(); // Au cas où...
                                break;
                        }
                        break;
                    case "clear":
                        Console.Clear();

                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("-\tTo see all commands made \"help\"\t\t-");
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    default:
                        if (EnterText != "")
                        {
                            bool LauncherOK = StartApp(EnterText); // Regarde directement si la saisie est un mot-clef
                            if (!LauncherOK)
                            {
                                Console.Write("This keyword doesn't exist\nDo you want to create it ?\n y/n :");
                                char Choise = Console.ReadKey().KeyChar;
                                if (EnterText == "y")
                                {
                                    Console.WriteLine("\nFolder or application ?\n 1\\2");
                                    Choise = Console.ReadKey().KeyChar;
                                    string choise = Convert.ToString(Choise);
                                    switch(choise)
                                    {
                                        case "1":
                                            WriteFileFolder();
                                            break;
                                        case "2":
                                            WriteFileApp();
                                            break;
                                    }
                                }
                            }

                        }
                        break;


                }
            } while (EnterText != "exit");
        }

        static void ConfigTest(string searchedName)
        {
            string EnterText;
            bool test = false;
            string[] linesConfig = File.ReadAllLines(Config);
            int i = 0;
            while (i < linesConfig.Length)
            {
                string[] word = linesConfig[i].Split(new char[] { ' ' }, 2);
                if (word[0] == searchedName)
                {
                    Name = word[0];
                    Pc = word[1];
                    test = true;
                }
                i++;
            }
            if (test == false)
            {
                Console.Write("\nYou haven't an account !\nOnly one spacebar\nInsert exit for leave\nInsert your name and the name of your computer : ");
                EnterText = Console.ReadLine();
                if (EnterText != "exit")
                {
                    string[] name = EnterText.Split(new char[] { ' ' }, 2);
                    Name = name[0];
                    Pc = name[1];
                    string newLine = searchedName;
                    newLine += " ";
                    newLine += Pc;
                    newLine += "\n";
                    File.AppendAllText(Config, newLine);
                }
                else
                {
                    Console.WriteLine("Leaving ...");
                    Thread.Sleep(200);
                    Environment.Exit(0);
                }
            }
        }
        

        static void RemoveKeywordApp(string SearchedWord)
        {
            string[] linesApp = File.ReadAllLines(pathApp);
            int i = 0;
            string newLine;
            int iLineToDelete = -1;
            while (i < linesApp.Length)
            {
                newLine = linesApp[i];
                string[] word = linesApp[i].Split(new char[] { ' ' }, 2);
                if (word[0] == SearchedWord)
                {
                    iLineToDelete = i;
                }

                i++;
            }

            if (iLineToDelete > -1)
            {
                List<string> lstNewLinesApp = new List<string>(linesApp);
                lstNewLinesApp.RemoveAt(iLineToDelete);
                linesApp = lstNewLinesApp.ToArray();
                File.WriteAllLines(pathApp, linesApp);
            }
        }
        static void RemoveKeywordFolder(string SearchedWord)
        {
            string[] linesFolder = File.ReadAllLines(pathFolder);
            int i = 0;
            string newLine;
            int iLineToDelete = -1;
            while (i < linesFolder.Length)
            {
                newLine = linesFolder[i];
                string[] word = linesFolder[i].Split(new char[] { ' ' }, 2);
                if (word[0] == SearchedWord)
                {
                    iLineToDelete = i;
                }

                i++;
            }

            if (iLineToDelete > -1)
            {
                List<string> lstNewLinesApp = new List<string>(linesFolder);
                lstNewLinesApp.RemoveAt(iLineToDelete);
                linesFolder = lstNewLinesApp.ToArray();
                File.WriteAllLines(pathFolder, linesFolder);
            }
        }

        static void WriteFileFolder()
        {
            string newLine = "";
            Console.WriteLine("Create a keyword for a folder");
            Console.Write("Whitout spacebar\nEnter the name of the keyword: ");
            newLine += Console.ReadLine();
            newLine += " ";
            Console.Write("\nWithout spacebar\nEnter the path of your keyword: ");
            newLine += Console.ReadLine();
            newLine += "\n";
            File.AppendAllText(pathFolder, newLine);
        }

        static void WriteFileApp()
        {
            string newLine = "";
            Console.WriteLine("Create a keyword for an application");
            Console.Write("Whitout spacebar\nEnter the name of the keyword: ");
            newLine += Console.ReadLine();
            newLine += " ";
            Console.Write("\nWithout spacebar\nEnter the path of your keyword: ");
            newLine += Console.ReadLine();
            newLine += "\n";
            File.AppendAllText(pathApp, newLine);
        }

        static void ReadFilesList()
        {
            string[] linesApp = File.ReadAllLines(pathApp);
            string[] linesFolder = File.ReadAllLines(pathFolder);
            Console.WriteLine("Here are all keywords of applications:");
            int i = 0;
            while (i < linesApp.Length)
            {
                string line = linesApp[i];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t{0}\n", line);
                i++;
            }
            Console.ResetColor();
            Console.WriteLine("Here are all keywords of folder:");
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
            Console.WriteLine("\tOpen a keyword\t\t\t\t\t [keyword's name]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tAdd a keyword:");
            Console.WriteLine("\t\tCreate for an application\t\tadd -a [keyword] [path]");
            Console.WriteLine("\t\tCreate for a folder\t\t\tadd -f [keyword] [path]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tRemove a keyword:");
            Console.WriteLine("\t\tRemove a keyword from applications\trm -a [keyword]");
            Console.WriteLine("\t\tRemove a keyword from folders\t\trm -f [keyword]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tSee all keywords\t\t\t\tlist");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tClean the console\t\t\t\tclear");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tLeave the console\t\t\t\texit");
            Console.WriteLine("\n------------------------------------------------------\n");
        }
    }
}
