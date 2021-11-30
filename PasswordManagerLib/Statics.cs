namespace PasswordManagerLib;

public static class Statics
{
        public static string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PasswordManager");
        public static string AccountsFile = Path.Combine(AppData, "Accounts.xml");
}
