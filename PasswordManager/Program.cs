using System;
using System.Collections;
using System.Collections.Generic;
using PasswordManagerLib;

namespace PasswordManager
{
    class Program
    {

        static void Main(string[] args)
        {
            if(PasswordManagerCore.CreateDirectoryForSave())
            {
                System.Console.WriteLine("Init a PasswordManager");
            }

            PasswordManagerCore ps = new PasswordManagerCore();

            if(!ps.AccountsIsEmpty) PSManipulator.AddAccount(ps);

            while(ps.ProgramRunning)
            {
                try
                {
                    if(!ps.IsLogged)
                    {
                        Prompt.NoLoggedPrompt(ps);
                    }
                    else
                    {
                        Prompt.LoggedPrompt(ps);
                    }
                }catch(NotLoggedExeption e)
                {
                    ps.LogOut();
                }

            }
        }

    }
}
