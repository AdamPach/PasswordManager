using System;
using System.IO;

namespace PasswordManagerLib
{
   public class PasswordManagerCore
   {
   
   ///<summary>
   ///Return true if Save Directory dont exits. Create a Save Direcory
   ///</summary>
    public static bool CreateDirectoryForSave()
      {
       string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordManager");


      //Check if Dir exist
       if(!Directory.Exists(dirPath))
       {
          //Create a dir and Accounts Folder
         Directory.CreateDirectory(dirPath);
         File.Create(Path.Combine(dirPath, "Accounts"));

         //Return true becouse you are creating dir
         return true;
       }
       else
       {
          //If Dir exist
          //Check if Accounts File dont exist
          if(!File.Exists(Path.Combine(dirPath, "Accounts.xml")))
          {
             //Create a Accounts folder
            File.Create(Path.Combine(dirPath, "Accounts.xml"));
          }
         return false;
       }
      } 

   }
}