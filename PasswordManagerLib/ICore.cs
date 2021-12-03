using PasswordManagerLib.Models;

namespace PasswordManagerLib;

public interface ICore
{
    public Task<Account> CreateAccount(string Name, string Password);
    public Task<Record> CreateRecord(string Name, string Password, string ServiceName, string Url);
    public Task<bool> LogIn(string Name, string Password);
    public Task LogOut();
    public bool IsLogged();
}
