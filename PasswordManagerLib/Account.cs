using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PasswordManagerLib
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public List<AccountRecord> Records { get;  set; }

        public Account(string Name, string Password, string FileName)
        {
            this.Name = Name;
            this.Password = Password;
            this.FileName = FileName;
            this.Records = new List<AccountRecord>();
        }

        public Account(){}

        public override string ToString()
        {
            return $"{this.Name}";
        }

        public void AddRecord(AccountRecord newRecord)
        {
            this.Records.Add(newRecord);
        }


    }
}