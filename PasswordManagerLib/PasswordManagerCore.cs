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

         try{
         this.Accounts = (List<Account>)this.LoadAccounts();

         }catch(Exception e)
         {
            System.Console.WriteLine("Exeption");
         }

         this.AccountsIsEmpty = this.Accounts == null ? true : false;
         if(this.AccountsIsEmpty)this.Accounts = new List<Account>();
      }


      public void CreateAccount(Account newAccount)
      {
         System.Console.WriteLine(newAccount.ToString());
         this.Accounts.Add(newAccount);
         File.Create(Path.Combine(SaveDir, newAccount.FileName));
         this.SaveAccounts();
      }

      private void SaveAccounts()
      {
         using(XmlWriter xw = XmlWriter.Create(AccountFile))
         {
            this.AccountsSerializer.Serialize(xw, this.Accounts);
         }
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

      private List<Account> LoadAccounts()
      {
         List<Account> ac;

            using(XmlReader reader = XmlReader.Create(AccountFile))
            {
               try
               {
                  ac = (List<Account>)this.AccountsSerializer.Deserialize(reader);
               }catch(InvalidOperationException e)
               {
                  ac = new List<Account>();
               }
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
         File.Create(AccountFile);
         GC.Collect();
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
            File.Create(AccountFile);
            GC.Collect();
          }
         return false;
       }
      } 

   }
}