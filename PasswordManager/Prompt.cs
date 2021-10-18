using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerLib;

namespace PasswordManager
{
    class Prompt
    {
        private static string PromptPrefix = ">: ";

        public static void NoLoggedPrompt(PasswordManagerCore ps)
        {
            System.Console.Write($"{PromptPrefix}");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "login":
                    {
                        PSManipulator.LogIn(ps);
                        break;
                    }
                case "add":
                    {
                        PSManipulator.AddAccount(ps);
                        break;
                    }
                case "exit":
                    {
                        ps.Exit();
                        break;
                    }

                default:
                    {
                        Printing.PrintHelp(false);
                        break;
                    };
            }

            System.Console.WriteLine();
        }

        public static void LoggedPrompt(PasswordManagerCore ps)
        {
            System.Console.Write($"{PromptPrefix}");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "help":
                    {
                        Printing.PrintHelp(true);
                        break;
                    }
                case "add":
                    {
                        PSManipulator.AddRecord(ps);
                        break;
                    }
                case "list":
                    {
                        PSManipulator.ListRecords(ps);
                        break;
                    }
                case "remove":
                    {
                        PSManipulator.RemoveRecord(ps);
                        break;
                    }
                case "logout":
                    {
                        PSManipulator.LogOut(ps);
                        break;
                    }
                case "exit":
                    {
                        ps.Exit();
                        break;
                    }
                case "delete":
                    {
                        PSManipulator.RemoveAccount(ps);
                        break;
                    }
                case "search":
                    {
                        PSManipulator.SearchRecord(ps);
                        break;
                    }
                case "edit":
                    {
                        PSManipulator.EditRecord(ps);
                        break;
                    }

                default:
                    {
                        Printing.PrintHelp(true);
                        break;
                    }
            }
            System.Console.WriteLine();
        }

    }
}
