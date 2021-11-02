using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerLib;

namespace PasswordManager
{
    class PSManipulator
    {
        public static void AddAccount(PasswordManagerCore ps)
        {
            string name, Password;
            try
            {
                do
                {
                    System.Console.WriteLine("\nYou are creating a account\nEnter 0 into Account name if you want interrupt create account");
                    System.Console.Write("Input Account Name: ");
                    name = Console.ReadLine();
                    if(name == "0")
                        throw new ExitExeption();

                    System.Console.Write("Input Account Password: ");
                    Password = ReturnValidPassword();

                } while (!ps.CreateAccount(name, Password));
            }
            catch(ExitExeption)
            {
                return;
            }
        }

        public static void RemoveAccount(PasswordManagerCore ps)
        {
            if (ps.RemoveAccount())
            {
                System.Console.WriteLine("Succes deleted");
            }
            else
            {
                System.Console.WriteLine("Deleted faild!");
            }
        }

        public static void LogIn(PasswordManagerCore ps)
        {
            try
            {
                if (ps.Accounts.Count == 0)
                {
                    Console.WriteLine("There is no accounts!");
                    return;
                }

                int index = Printing.PrintCollectionAndReturnIndexInList(ps.Accounts, "Enter Account number: ");

                Console.Write("Input your password: ");
                string password = EnterPassword();

                if (ps.LogIn(index, password))
                    Console.WriteLine("You are logged in");
                else
                    Console.WriteLine("Incorect Password");
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("There is no account!");
            }
            catch (ExitExeption)
            {
                return;
            }
        }

        public static void LogOut(PasswordManagerCore ps)
        {
            ps.LogOut();
        }

        public static void AddRecord(PasswordManagerCore ps)
        {
            try
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Enter 0 into url if you want interrupt Add record");
                System.Console.Write("Enter a url: ");
                string url = Console.ReadLine();
                if(url == "0")
                    throw new ExitExeption();

                System.Console.Write("Enter a Service Name: ");
                string ServiceName = Console.ReadLine();

                System.Console.Write("Enter a Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();


                ps.AddRecord(username, password, url, ServiceName);
            } 
            catch (ExitExeption)
            {
                return;
            }
        }

        public static void ListRecords(PasswordManagerCore ps)
        {
            Printing.PrintCollection(ps.ReadAllRecords());
        }

        public static void SearchRecord(PasswordManagerCore ps)
        {
            Console.WriteLine();
            Console.Write("Enter a searching service name: ");
            string serviceName = Console.ReadLine();

            Printing.PrintCollection(ps.GetSearchedAccounts(serviceName));
        }

        public static void RemoveRecord(PasswordManagerCore ps)
        {
            try
            {
                if (ps.RemoveRecod(Printing.PrintCollectionAndReturnIndexInList(ps.ReadAllRecords(), "Enter index of record: ")))
                {
                    Console.WriteLine("Record is Removed!");
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You dont have any record");
            }
            catch (ExitExeption)
            {
                return;
            }
        }

        public static void EditRecord(PasswordManagerCore ps)
        {
            try
            {
                int index = Printing.PrintCollectionAndReturnIndexInList(ps.ReadAllRecords(), "Enter index of record: ");
                Console.WriteLine();
                Console.WriteLine("Enter empty string if you wouldnt change this record!");

                Console.Write("Enter new Server: ");
                string newServer = Console.ReadLine();

                Console.Write("Enter new Service name: ");
                string newService = Console.ReadLine();

                Console.Write("Enter new Username: ");
                string newUsername = Console.ReadLine();

                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                ps.EditRecordOnIndex(index, newServer, newService, newUsername, newPassword);

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You dont have any record!");
            }
            catch (ExitExeption)
            {
                return;
            }
        }

        private static string ReturnValidPassword()
        {
            string password = EnterPassword();

            while (!AccountPassword.IsValidPassword(password))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid password!");
                Console.Write("Enter valid password: ");
                password = EnterPassword();
            }

            return password;
        }

        private static string EnterPassword()
        {
            StringBuilder password = new StringBuilder();

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    if(password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else if(key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return password.ToString();
        }
    }
}
