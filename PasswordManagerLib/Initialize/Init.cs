using PasswordManagerLib.Manipulators;
using PasswordManagerLib.Models;

namespace PasswordManagerLib.Initialize;

public class Init
{
    public async static Task InitRepo()
    {
        //Check if Folder exist
        if(!Directory.Exists(Statics.AppData))
        {
            //Create a folder
            Directory.CreateDirectory(Statics.AppData);
            //Create file for all acounts

            IAccountsManipulator writer = new XmlAccountsManipulator();
            await writer.WriteAccounst(new List<Account>(), Statics.AccountsFile);
            return;
        }
        else 
        {
            if(!File.Exists(Statics.AppData))
            {
                 IAccountsManipulator writer = new XmlAccountsManipulator();
                await writer.WriteAccounst(new List<Account>(), Statics.AccountsFile);
            }
            return;
        }
    }
}