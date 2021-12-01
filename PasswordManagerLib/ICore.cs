using PasswordManagerLib.Models;

namespace PasswordManagerLib;

public interface ICore
{
    public Task<Account> CreateAccount(string Name, string Password);
    public bool LogIn(string Name, string Password);
}
