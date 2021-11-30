using System.Xml.Serialization;

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
}
