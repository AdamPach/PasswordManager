namespace PasswordManagerLib.Crypto;

public interface IPasswordComparator
{
    public Task<bool> ComparePassword(string hash, string password);
}
