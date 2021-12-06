using System.Xml.Serialization;
using PasswordManagerLib.Crypto;
using PasswordManagerLib.Manipulators;

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
            var fileName = Name + ".xml";
            return Path.Combine(Statics.AppData, fileName);
        }
    }
    [XmlIgnore]
    private List<Record> Records;

    public async Task<bool> ComparePassword(IPasswordComparator comparator, string password)
    {
        return await comparator.ComparePassword(Password, password);
    }

    public async Task<Record> CreateRecord(string Name, string Password, string ServiceName, string Url)
    {
        var record = new Record(Name, Password, ServiceName, Url);

        Records = (List<Record>)await GetRecords();
        Records.Add(record);

        await WriteRecords(Records);

        Records = null;
        return record;
    }

    public async Task WriteRecords(IEnumerable<Record> records)
    {
        await new XmlRecordsManipulator().WriteRecords(records, AccountFileName);
    }

    public async Task<IEnumerable<Record>> GetRecords()
    {
        return await new XmlRecordsManipulator().ReadRecords(AccountFileName);
    }
}
