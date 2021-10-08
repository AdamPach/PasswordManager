using System;
using PasswordManagerLib;

namespace PasswordManager
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordManagerCore ps = new PasswordManagerCore();

            if(PasswordManagerCore.CreateDirectoryForSave())
            {
                System.Console.WriteLine("Creating folder");
            }
            else System.Console.WriteLine("Folder exist");
        }
    }
}
