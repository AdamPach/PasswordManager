namespace PasswordManagerLib.Initialize;

public class Init
{
    private static string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordManager");

    public static void InitRepo()
    {
        //Check if Folder exist
        if(!Directory.Exists(AppData))
        {
            //Create a folder
            Directory.CreateDirectory(AppData);
            //Create file for all acounts

            return;
        }
        else 
        {
            if(!File.Exists(Path.Combine(AppData, "Accounts.xml")))
            {
                //Create file for all accounts if file doesnt exits
            }
            return;
        }
    }
}