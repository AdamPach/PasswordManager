using PasswordManagerLib.Crypto;
using PasswordManagerLib.Exeption;
using PasswordManagerLib.Manipulators;
using PasswordManagerLib.Models;

namespace PasswordManagerLib;

public class Core : ICore
{
    private IAccountsManipulator manipulator;
    public async Task<Account> CreateAccount(string Name, string Password)
    {
        IPasswordHasher hasher = new SHA256PasswordHasher();

        Account NewAccount = new Account{Name = Name, Password = await hasher.HashPassword(Password)};
        //Must create file for this account where you save all passwords

        var Accounts = await GetAccountsAsync();

        if(CheckDuplicitNames(Accounts, NewAccount))
            throw new DuplicitAccountNaneExeption("This name is exist!");

        Accounts.Add(NewAccount);
        await WriteAccountsAsync(Accounts);
        return NewAccount;
    }

    private Account LogedAccount;

    public bool IsLogged()
    {
        return LogedAccount != null ? true : false;
    }

    public async Task<bool> LogIn(string Name, string Password)
    {
        List<Account> accounts = await GetAccountsAsync();

        Account accountToLogin = (from a in accounts where a.Name == Name select a).FirstOrDefault();

        if(accountToLogin == null)
            throw new NoExistingAccountExeption("You enter a name which is not existing!");
        
        IPasswordComparator comparator = new PasswordComparator(new SHA256PasswordHasher());
        if(await comparator.ComparePassword(accountToLogin.Password, Password))
        {
            LogedAccount = accountToLogin;
            return true;
        }
        
        return false;
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

    private bool CheckDuplicitNames(IEnumerable<Account> accounts, Account NewAccount)
    {
        var account = from a in accounts where NewAccount.Name == a.Name select a;
        return account.Count() > 0;
    }

    private void FreeMemory()
    {
        manipulator = null;
    }
}
