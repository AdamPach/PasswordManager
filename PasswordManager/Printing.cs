﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    class Printing
    {

        public static void PrintHelp(bool IsLogged)
        {
            System.Console.WriteLine("Help for PasswordManager");
            Console.WriteLine();

            if (IsLogged)
            {
                System.Console.WriteLine("Add - Add a password");
                System.Console.WriteLine("Exit - End of this program");
                System.Console.WriteLine("List - Show all your accounts");
                System.Console.WriteLine("LogOut - LogOut");
                System.Console.WriteLine("Remove - Remove a password");
                Console.WriteLine("Search - Search specific Service name");
                System.Console.WriteLine("Delete - Delete this account");
            }
            else
            {
                System.Console.WriteLine("LogIn - Log into account");
                System.Console.WriteLine("Add - Create a new account");
                System.Console.WriteLine("Exit - End of this program");
            }
            
        }

        public static int PrintCollectionAndReturnIndexInList<T>(T list, string message) where T : IEnumerable
        {
            int count = 0, index;
            Console.WriteLine();

            foreach (var item in list)
            {
                count++;
                Console.WriteLine($"{count} - {item}");
            }

            bool ParseGood;

            do
            {
                Console.Write(message);
                ParseGood = int.TryParse(Console.ReadLine(), out index);
            } while (!(ParseGood && (index > 0 && index <= count)));

            return index - 1;
        }

        public static void PrintCollection<T>(T list) where T : IEnumerable
        {
            int count = 0;
            Console.WriteLine();
            foreach (var item in list)
            {
                count++;
                Console.WriteLine($"{count} - {item}");
            }
        }
    }
}
