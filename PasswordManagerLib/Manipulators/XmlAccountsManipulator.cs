using System.Xml;
using System.Xml.Serialization;
using PasswordManagerLib.Models;

namespace PasswordManagerLib.Manipulators;

public class XmlAccountsManipulator : IAccountsManipulator
{
    public XmlSerializer serializer { private get; init; }
    private XmlWriterSettings writerSettings;

    public XmlAccountsManipulator()
    {
        serializer = new XmlSerializer(typeof(List<Account>));
        writerSettings = new XmlWriterSettings();
        writerSettings.Indent = true;
    }

    public async Task<bool> WriteAccounst(List<Account> Accounts, string AccountFileName)
    {
        using(XmlWriter writer = XmlWriter.Create(AccountFileName, writerSettings))
        {
            await Task.Run(() => serializer.Serialize(writer, Accounts));
        }

        return true;
    }
}
