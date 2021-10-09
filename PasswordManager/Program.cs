﻿using System;
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
                if(!ps.IsLogged)
                {
                    NoLoggedPrompt(ps);
                }
                else
                {
                    LoggedPrompt(ps);
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

            System.Console.Write("Enter a Username: ");
            string username = Console.ReadLine();

            System.Console.Write("Enter a Password: ");
            string password = Console.ReadLine();

            ps.AddRecord(new AccountRecord(username, password, url));
        }

        static void LogIn(PasswordManagerCore ps)
        {
            int count = 0;
            foreach (Account ac in ps.Accounts)
            {
                count++;
                System.Console.WriteLine($"{count}. {ac.Name}");
            }
            System.Console.WriteLine();

            int AccountNumber;
            bool parseGood;
            do
            {
                System.Console.Write("Input Account number: ");
                parseGood = int.TryParse(Console.ReadLine(), out AccountNumber);
            } while (!parseGood);

            AccountNumber -= 1;

            System.Console.Write("Input Password: ");

            string password = Console.ReadLine();
            if(ps.LogIn(AccountNumber, password))System.Console.WriteLine("You logged in!");
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
                System.Console.WriteLine($"{ar.Server}: ");
                System.Console.WriteLine($"\tUsername: {ar.Username}");
                System.Console.WriteLine($"\tPassword: {ar.Password}\n");
            }
        }
    }
}
