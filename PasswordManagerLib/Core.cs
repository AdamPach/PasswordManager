using PasswordManagerLib.Manipulators;
using PasswordManagerLib.Models;

namespace PasswordManagerLib;

public class Core : ICore
{
    private IAccountsManipulator manipulator;
    public async Task<Account> CreateAccount(string Name, string Password)
    {
        Account NewAccount = new Account{Name = Name, Password = Password};
        //Must create file for this account where you save all passwords

        var Accounts = await GetAccountsAsync();
        Accounts.Add(NewAccount);
        await WriteAccountsAsync(Accounts);

        return NewAccount;
    }

    public bool LogIn(string Name, string Password)
    {
        throw new NotImplementedException();
    }

    private async Task<List<Account>> GetAccountsAsync()
    {
        manipulator = new XmlAccountsManipulator();
        var Accounts = await manipulator.ReadAccounts(Statics.AccountsFile);
        FreeMemory();
        return Accounts;
    }

    private async Task WriteAccountsAsync(List<Account> accounts)
    {
        manipulator = new XmlAccountsManipulator();
        await manipulator.WriteAccounst(accounts, Statics.AccountsFile);
        FreeMemory();
    }

    private void FreeMemory()
    {
        manipulator = null;
    }
}
