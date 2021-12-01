using PasswordManagerLib.Models;

namespace PasswordManagerLib.Manipulators
{
    public interface IAccountsManipulator
    {
        public Task<bool> WriteAccounst(List<Account> Accounts, string AccountFileName);
        public Task<List<Account>> ReadAccounts(string AccountFileName);
    }
}