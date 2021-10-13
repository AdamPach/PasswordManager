using System;
using System.Collections.Generic;
using PasswordManagerLib;

namespace PasswordManager
{
    class Program
    {

        static readonly string PromptPrefix = ">: ";

        static void Main(string[] args)
        {
            if(PasswordManagerCore.CreateDirectoryForSave())
            {
                System.Console.WriteLine("Init a PasswordManager");
            }

            PasswordManagerCore ps = new PasswordManagerCore();

            if(!ps.AccountsIsEmpty) AddAccount(ps);

            while(ps.ProgramRunning)
            {
                try
                {
                    if(!ps.IsLogged)
                    {
                        NoLoggedPrompt(ps);
                    }
                    else
                    {
                        LoggedPrompt(ps);
                    }
                }catch(NotLoggedExeption e)
                {
                    ps.LogOut();
                }

            }
        }

        static void AddAccount(PasswordManagerCore ps)
        {
            System.Console.WriteLine("You are creating a account");
            System.Console.Write("Input Account Name: ");
            string name = Console.ReadLine();

            System.Console.Write("Input Account Password: ");
            string Password = Console.ReadLine();

            ps.CreateAccount(new Account(name, Password, name + ".xml"));
        }

        static void RemoveAccount(PasswordManagerCore ps)
        {
            if(ps.RemoveAccount())
            {
                System.Console.WriteLine("Succes deleted");
            }
            else
            {
                System.Console.WriteLine("Deleted faild!");
            }
        }

        static void AddRecord(PasswordManagerCore ps)
        {
            System.Console.WriteLine();
            System.Console.Write("Enter a url: ");
            string url = Console.ReadLine();

            System.Console.Write("Enter a Service Name: ");
            string ServiceName = Console.ReadLine();

            System.Console.Write("Enter a Username: ");
            string username = Console.ReadLine();

            string password = ReturnValidPassword();


            ps.AddRecord(new AccountRecord(username, password, url, ServiceName));
        }

        static void LogIn(PasswordManagerCore ps)
        {
            int count = 0, AccountNumber;

            if (ps.Accounts.Count == 0)
            {
                Console.WriteLine("There is no accounts!");
                return;
            }

            foreach (Account ac in ps.Accounts)
            {
                count++;
                System.Console.WriteLine($"{count}. {ac.Name}");
            }

            System.Console.WriteLine();

            bool ParseGood;

            do
            {
                System.Console.Write("Input Account number: ");
                ParseGood = int.TryParse(Console.ReadLine(), out AccountNumber);
            } while (!(ParseGood && (AccountNumber > 0 && AccountNumber <= ps.Accounts.Count)));

            AccountNumber -= 1;

            string password = ReturnValidPassword();

            if(ps.LogIn(AccountNumber, password))
                Console.WriteLine("You are logged in");
            else
                Console.WriteLine("Incorect Password");
        }

        static void LogOut(PasswordManagerCore ps)
        {
            ps.LogOut();
        }

        static void NoLoggedPrompt(PasswordManagerCore ps)
        {
            System.Console.Write($"{PromptPrefix}");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "login":
                    {
                        LogIn(ps);
                        break;
                    }
                case "add":
                    {
                        AddAccount(ps);
                        break;
                    }
                case "exit":
                    {
                        ps.Exit();
                        break;
                    }

                default:
                    {
                        System.Console.WriteLine("LogIn - Log into account");
                        System.Console.WriteLine("Add - Create a new account");
                        System.Console.WriteLine("Exit - End of this program");
                        break;
                    };
            }

            System.Console.WriteLine();
        }

        static void LoggedPrompt(PasswordManagerCore ps)
        {
            System.Console.Write($"{PromptPrefix}");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "help":
                    {
                        PrintHelp();
                        break;
                    }
                case "add":
                    {
                        AddRecord(ps);
                        break;
                    }
                case "list":
                    {
                        PrintAllRecords(ps);
                        break;
                    }
                case "remove":
                    {
                        System.Console.WriteLine("Removing a password");
                        break;
                    }
                case "logout":
                    {
                        LogOut(ps);
                        break;
                    }
                case "exit":
                    {
                        ps.Exit();
                        break;
                    }
                case "delete":
                    {
                        RemoveAccount(ps);
                        break;
                    }
                case "search":
                    {
                        SearchRecord(ps);
                        break;
                    }
                
                default: 
                    {
                        PrintHelp();
                        break;
                    }
            }
            System.Console.WriteLine();
        }

        static void PrintHelp()
        {
            System.Console.WriteLine("Help for PasswordManager");
            System.Console.WriteLine("Add - Add a password");
            System.Console.WriteLine("Exit - End of this program");
            System.Console.WriteLine("List - Show all your accounts");
            System.Console.WriteLine("LogOut - LogOut");
            System.Console.WriteLine("Remove - Remove a password");
            Console.WriteLine("Search - Search specific Service name");
            System.Console.WriteLine("Delete - Delete account");

        }

        static void PrintAllAccounts(List<Account> accounts)
        {
            int num = 1;

            foreach (Account ac in accounts)
            {
                System.Console.WriteLine($"{num}: {ac.ToString()}");
                num++;
            }

        }

        static void PrintAllRecords(PasswordManagerCore ps)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("All your records\n");
            foreach(AccountRecord ar in ps.ReadAllRecords())
            {
                PrintRecord(ar);
            }
        }

        static void PrintRecord(AccountRecord ar)
        {
            System.Console.WriteLine($"{ar.Server}: ");
            System.Console.WriteLine($"\tService Name: {ar.ServiceName}");
            System.Console.WriteLine($"\tUsername: {ar.Username}");
            System.Console.WriteLine($"\tPassword: {ar.Password}\n");
        }

        static void SearchRecord(PasswordManagerCore ps)
        {
            Console.WriteLine();
            Console.Write("Enter a searching service name: ");
            string serviceName = Console.ReadLine();

            foreach (var ar in ps.GetSearchedAccounts(serviceName))
            {
                PrintRecord(ar);
            }
        }

        static string ReturnValidPassword()
        {
            Console.Write("Enter valid password: ");
            string password = Console.ReadLine();

            while(password == "")
            {
                Console.WriteLine();
                Console.WriteLine("Invalid password!");
                Console.Write("Enter valid password: ");
                password = Console.ReadLine();
            }

            return password;
        }
    }
}
