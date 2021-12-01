namespace PasswordManagerLib.Crypto;

public interface IPasswordHasher
{
    public Task<string> HashPassword(string plainPassword);
}
