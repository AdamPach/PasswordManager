using System.Xml.Serialization;
using PasswordManagerLib.Crypto;

namespace PasswordManagerLib.Models;

public class Account
{
    public string Name { get; set; }
    public string Password { get; set; }
    [XmlIgnore]
    public string AccountFileName 
    { 
        get
        {
            return Name + ".xml";
        }
    }

    public async Task<bool> ComparePassword(IPasswordComparator comparator, string password)
    {
        return await comparator.ComparePassword(Password, password);
    }
}
