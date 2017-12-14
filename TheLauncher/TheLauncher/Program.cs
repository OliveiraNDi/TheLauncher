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
        
        static void Main(string[] args)
        {
            File.AppendAllText(pathApp, "");
            File.AppendAllText(pathFolder, "");

            string newLine = "";
            string EnterText = "";
            
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("-\tTo see all commands made \"help\"\t\t-");
            Console.WriteLine("-------------------------------------------------");
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
                        switch (word[1])
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
                                    Console.Write("Application or Folder ?\n1|2 ");
                                    char Choise = Console.ReadKey().KeyChar;
                                    if (EnterText == "1")
                                    {
                                        Console.WriteLine("To add an application : add -a [mot-clef] [chemin d'accès]");
                                    }
                                    else if (EnterText == "2")
                                    {
                                        Console.WriteLine("To add a folder : add -f [mot-clef] [chemin d'accès]");
                                    }
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
                                Console.Write("This keyword doesn't exist\nDo you want to create it ?\n y/n : ");
                                char Choise = Console.ReadKey().KeyChar;
                                if (EnterText == "y")
                                {
                                    Console.WriteLine("Folder or application ?\n 1\\2");
                                    Choise = Console.ReadKey().KeyChar;
                                    if (EnterText == "1")
                                    {
                                        WriteFileFolder();
                                    }
                                    else if (EnterText == "2")
                                    {
                                        WriteFileApp();
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
            Console.WriteLine("\tOpen an keyword\t\t\t\t\t [keyword name's]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tAdd a keyword of an application or a folder:");
            Console.WriteLine("\t\tCreate for an application\t\t\tadd -a [keyword] [path]");
            Console.WriteLine("\t\tCreate for a folder\t\t\t\tadd -f [keyword] [path]");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tSee all keywords\t\t\t\tlist");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tClean the console\t\t\tclear");
            Console.WriteLine("\n------------------------------------------------------\n");
            Console.WriteLine("\tLeave the console\t\t\t\t\texit");
            Console.WriteLine("\n------------------------------------------------------\n");
        }

        
    }
}
