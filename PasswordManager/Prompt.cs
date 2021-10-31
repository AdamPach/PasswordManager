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
            string command = EnterComand().ToLower();

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
            string command = EnterComand().ToLower();

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

        private static string EnterComand()
        {
            StringBuilder command = new StringBuilder();
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                switch(key.Key)
                {
                    case ConsoleKey.Tab:
                    {
                        var matches = Autocomplete.Complete(command.ToString());
                        if(matches.Count() > 1)
                            break;
                        ClearPropmt(command.Length);
                        command.Clear();
                        command.Append(matches.ElementAt(0));
                        Console.Write(command.ToString());
                        break;
                    }
                    case ConsoleKey.Backspace:
                    {
                        if(command.Length > 0)
                        {
                            command.Remove(command.Length - 1, 1);
                            Console.Write("\b \b");
                        }
                        break;
                    }
                    default:
                    {
                        if(key.Key != ConsoleKey.Enter)
                        {
                            command.Append(key.KeyChar);
                            Console.Write(key.KeyChar);
                        }
                        break;
                    }
                }

            }while(key.Key != ConsoleKey.Enter);

            return command.ToString();
        }

        private static void ClearPropmt(int PromptLenght)
        {
            for (int i = 0; i < PromptLenght; i++)
            {
                Console.Write("\b \b");
            }
        }
    }
}
