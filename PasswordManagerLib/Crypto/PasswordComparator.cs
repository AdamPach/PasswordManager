namespace PasswordManagerLib.Crypto;

public class PasswordComparator : IPasswordComparator
{
    private readonly IPasswordHasher _hasher;

    public PasswordComparator(IPasswordHasher hasher)
    {
        _hasher = hasher;
    }

    public async Task<bool> ComparePassword(string hash, string password)
    {
        var passwordHash = await _hasher.HashPassword(password);
        if(hash == passwordHash)
            return true;
        return false;
    }
}
