using System.Security.Cryptography;
using System.Text;

namespace PasswordManagerLib.Crypto;

public class SHA256PasswordHasher : IPasswordHasher
{
    public async Task<string> HashPassword(string plainPassword)
    {
        return await GetHashedPassword(plainPassword);
    }

    private async Task<byte[]> GetHash(string inputString)
    {  
        using(HashAlgorithm algorithm = SHA256.Create())
            return await Task.Run<byte[]>(() => algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
    }

    private async Task<string> GetHashedPassword(string inputString)
    {
        StringBuilder hash = new StringBuilder();

        foreach (byte item in await GetHash(inputString))
        {
            hash.Append(item.ToString("X2"));
        }

        return hash.ToString();
    }
}
