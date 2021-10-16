using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace PasswordManagerLib
{
    public class PasswordManagerCore
    {
        //Static 
        private static readonly string SaveDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordManager");
        private static readonly string AccountFile = Path.Combine(SaveDir, "Accounts.xml");

        //Properties
        public bool ProgramRunning { get; private set; }
        public bool IsLogged { get; private set; }
        public List<Account> Accounts { get; private set; }
        public bool AccountsIsEmpty { get; private set; }
        private XmlSerializer AccountsSerializer = new XmlSerializer(typeof(List<Account>));
        public Account loggedAccount { get; private set; }

        public PasswordManagerCore()
        {
            this.ProgramRunning = true;
            this.IsLogged = false;

            this.Accounts = this.LoadAccounts();

            this.AccountsIsEmpty = this.Accounts.Count > 0 ? true : false;

        } 

        /// <summary>
        /// Method for exit a program
        /// </summary>
        public void Exit()
        {
         this.ProgramRunning = false;
        }

          /// <summary>
          /// Method for create a new account
          /// </summary>
          /// <param name="newAccount">Account which you want add</param>
          public bool CreateAccount(string name, string password)
          {
            try
            {
                Account newAccount = new Account(name, password, name + ".xml");
                this.Accounts.Add(newAccount);
                this.SaveAccounts();
            }catch(IsInvalidPasswordExeption e)
            {
                return false;
            }

                return true;
          }


           /// <summary>
          /// If you logged remove this account
          /// </summary>
          /// <returns>If account is remove return true</returns>
          public bool RemoveAccount()
          {
                if (this.IsLogged && this.loggedAccount != null)
                {
                    this.Accounts.Remove(this.loggedAccount);
                    this.loggedAccount = null;
                    this.IsLogged = false;
                    this.SaveAccounts();
                    return true;
                }
                else throw new NotLoggedExeption("You are not auth!");
          }
        /// <summary>
        /// Save all accounts
        /// </summary>
        private void SaveAccounts()
        {
            WriteAccountFile(this.AccountsSerializer, this.Accounts);
        }


        /// <summary>
        /// Method for log in into your account
        /// </summary>
        /// <param name="AccountIndex">Accunt index in list</param>
        /// <param name="password">Password to compare</param>
        /// <returns>If you log succesfull return true</returns>
        public bool LogIn(int AccountIndex,string password)
        {
             if(this.Accounts[AccountIndex].Password.ComparePasswords(password))
             {
                this.IsLogged = true;
                this.loggedAccount = this.Accounts[AccountIndex];
                return true;
             }
             else
             {
                return false;
             }
        }

        /// <summary>
        /// Method for logged out
        /// </summary>
        public void LogOut()
        {
             this.IsLogged = false;
             this.loggedAccount = null;
        }

        /// <summary>
        /// Met
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public bool AddRecord(AccountRecord newRecord)
        {
            if (this.IsLogged)
            {
                this.loggedAccount.AddRecord(newRecord);
                WriteAccountFile(this.AccountsSerializer, this.Accounts);
                return true;
            }
            else throw new NotLoggedExeption("");
        }

        public bool RemoveRecod(int RecordIndex)
        {
            if (IsLogged)
            {
                this.loggedAccount.RemoveRecord(RecordIndex);
                WriteAccountFile(this.AccountsSerializer, this.Accounts);
                return true;
            }
            else throw new NotLoggedExeption("");
        }

        /// <summary>
        /// Method return all record for your account
        /// </summary>
        /// <returns>All records</returns>
        public List<AccountRecord> ReadAllRecords()
        {
             if(this.IsLogged)
             {
                return this.loggedAccount.Records;
             } else throw new NotLoggedExeption("You are not auth!");
        }

        /// <summary>
        ///     Retuen all matched records
        /// </summary>
        /// <param name="serviceName">Searching name</param>
        /// <returns>All results</returns>

       public IEnumerable<AccountRecord> GetSearchedAccounts(string serviceName)
        {
            if (IsLogged)
            {
                try
                {
                    return this.loggedAccount.Search(serviceName);

                }catch(KeyNotFoundException e)
                {
                    return new List<AccountRecord>();
                }
            }
            else throw new NotLoggedExeption("You are not auth!");

        }

      ///<summary>
      ///Method for load Accounts from file
      ///</summary>
      private List<Account> LoadAccounts()
      {
         //Create a list
         List<Account> ac;

            //Create a reader
            using(XmlReader reader = XmlReader.Create(AccountFile))
            {
               //Read from file
               ac = (List<Account>)this.AccountsSerializer.Deserialize(reader);
            }

         return ac;
      }

   ///<summary>
   ///Return true if Save Directory dont exits. Create a Save Direcory
   ///</summary>
        public static bool CreateDirectoryForSave()
        {

            //Check if Dir exist
           if(!Directory.Exists(SaveDir))
           {
              //Create a dir and Accounts Folder
             Directory.CreateDirectory(SaveDir);
             WriteAccountFile(new XmlSerializer(typeof(List<Account>)), new List<Account>());
             //Return true becouse you are creating dir
             return true;
           }
           else
           {
              //If Dir exist
              //Check if Accounts File dont exist
              if(!File.Exists(AccountFile))
              {
                 //Create a Accounts folder
                WriteAccountFile(new XmlSerializer(typeof(List<Account>)), new List<Account>());
              }
             return false;
           }
        } 

          ///<summary>
          ///Static method for Write Accounts to save file
          ///</summary>
        private static void WriteAccountFile(XmlSerializer xmlSerializer, List<Account> accounts)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //Create a xmlWriter
            using(XmlWriter xmlWriter = XmlWriter.Create(AccountFile, settings))
            {
                //Write into a account file
                xmlSerializer.Serialize(xmlWriter, accounts);
            }
        }

    }
}