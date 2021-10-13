using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace PasswordManagerLib
{
    
    public class Account
    {
        public string Name { get; set; }
        public AccountPassword Password { get; set; }
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

        public IEnumerable<AccountRecord>Search(string ServiceName)
        {
            var searched = from ar in this.Records
                           where ar.ServiceName.ToLower() == ServiceName.ToLower()
                           select ar;

            if (searched.Count() == 0) throw new KeyNotFoundException("We cant find any results!");

            return searched;
        }
    }
}