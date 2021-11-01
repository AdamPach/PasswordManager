using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        private Account LoggedAccount { get;  set; }

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
          /// Mehtod create a new accoutn
          /// </summary>
          /// <param name="name">Account name</param>
          /// <param name="password">Passowrd for account</param>
          /// <returns>Return true if password is valid</returns>
          public bool CreateAccount(string name, string password)
          {
            try
            {
                Account newAccount = new Account(name, password, name + ".xml");
                this.Accounts.Add(newAccount);
                this.SaveAccounts();
            }catch(IsInvalidPasswordExeption)
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
                if (this.IsLogged && this.LoggedAccount != null)
                {
                    this.Accounts.Remove(this.LoggedAccount);
                    this.LoggedAccount = null;
                    this.IsLogged = false;
                    this.SaveAccounts();
                    GC.Collect();
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
            if (Accounts.ElementAt(AccountIndex).CanILogIn(password))
            {
                IsLogged = true;
                LoggedAccount = Accounts.ElementAt(AccountIndex);
                LoggedAccount.LogIn(password);
                return true;
            }
            else return false;

             /*if(this.Accounts[AccountIndex].Password.ComparePasswords(password))
             {
                this.IsLogged = true;
                this.LoggedAccount = this.Accounts[AccountIndex];
                return true;
             }
             else
             {
                return false;
             }*/
        }

        /// <summary>
        /// Method for logged out
        /// </summary>
        public void LogOut()
        {
            LoggedAccount.LogOut();
            IsLogged = false;
            LoggedAccount = null;
        }

        /// <summary>
        /// Met
        /// </summary>
        /// <param name="newRecord"></param>
        /// <returns></returns>
        public bool AddRecord(string name, string password, string url, string serviceName)
        {   

            if (this.IsLogged)
            {
                this.LoggedAccount.AddRecord(name, password, url, serviceName);
                SaveAccounts();
                return true;
            }
            else throw new NotLoggedExeption("");
        }

        public bool RemoveRecod(int RecordIndex)
        {
            if (IsLogged)
            {
                this.LoggedAccount.RemoveRecord(RecordIndex);
                WriteAccountFile(this.AccountsSerializer, this.Accounts);
                return true;
            }
            else throw new NotLoggedExeption("");
        }


        /// <summary>
        /// Edit record on specific index. If you enter empty string your record will not change
        /// </summary>
        /// <param name="index">Index of record</param>
        /// <param name="newServer">New url</param>
        /// <param name="newServiceName">New Service Name</param>
        /// <param name="newUsername">New Username</param>
        /// <param name="newPassword">New Password</param>
        /// <returns>Return true if record is updated</returns>
        public bool EditRecordOnIndex(int index, string newServer, string newServiceName, string newUsername, string newPassword)
        {
            if (IsLogged)
            {
                LoggedAccount.EditAccount(index, newServer, newServiceName, newUsername, newPassword);

                SaveAccounts();

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
                return this.LoggedAccount.Records;
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
                    return this.LoggedAccount.Search(serviceName);

                }catch(KeyNotFoundException)
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
        private async static void WriteAccountFile(XmlSerializer xmlSerializer, List<Account> accounts)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            //Create a xmlWriter
            using(XmlWriter xmlWriter = XmlWriter.Create(AccountFile, settings))
            {
                //Write into a account file async
                await Task.Run(()=> 
                {
                    xmlSerializer.Serialize(xmlWriter, accounts);
                });
            }
        }

    }
}