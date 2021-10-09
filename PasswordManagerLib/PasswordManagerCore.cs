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
      [XmlArray("Accounts")]
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

      public void Exit()
      {
         this.ProgramRunning = false;
      }


      public void CreateAccount(Account newAccount)
      {
         this.Accounts.Add(newAccount);
         File.Create(Path.Combine(SaveDir, newAccount.FileName));
         this.SaveAccounts();
      }

      private void SaveAccounts()
      {
         // using(XmlWriter xw = XmlWriter.Create(AccountFile))
         // {
         //    this.AccountsSerializer.Serialize(xw, this.Accounts);
         // }

         WriteAccountFile(this.AccountsSerializer, this.Accounts);
      }

      public bool LogIn(int AccountIndex,string password)
      {
         if(this.Accounts[AccountIndex].Password == password)
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

      public void LogOut()
      {
         this.IsLogged = false;
         this.loggedAccount = null;
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
         //Create a xmlWriter
         using(XmlWriter xmlWriter = XmlWriter.Create(AccountFile))
         {
            //Write into a account file
            xmlSerializer.Serialize(xmlWriter, accounts);
         }
      }

   }
}