using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;

namespace PasswordManagerLib
{

    public class Account
    {
        public string Name { get; set; }
        public AccountPassword Password { get; set; }
        public string FileName { get; set; }

        public List<AccountRecord> Records { get; set; }

        [XmlIgnore]
        private string KeyForRecords;

        public Account(string Name, string Password, string FileName)
        {
            this.Name = Name;
            this.Password = Password;
            this.FileName = FileName;
            this.Records = new List<AccountRecord>();
        }

        public Account() { }
        
        public override string ToString()
        {
            return $"{this.Name}";
        }

        public bool CanILogIn(string password)
        {
            if (Password.ComparePasswords(password))
            {
                return true;
            }
            else
                return false;
        }

        public void LogIn(string password)
        {
            KeyForRecords = Password.password + password;
            byte[] k = ComputeKey();
            foreach (var record in Records)
            {
                record.Key = k;
            }
        }

        public void LogOut()
        {
            KeyForRecords = null;
            foreach (var record in Records)
            {
                record.Key = null;
            }
            GC.Collect();
        }

        public void AddRecord(string name, string password, string url, string serviceName)
        {

            Records.Add(new AccountRecord(name, password, url, serviceName, ComputeKey()));
        }

        public void RemoveRecord(int RecordIndex)
        {
            this.Records.RemoveAt(RecordIndex);
        }

        public void EditAccount(int index, string newServer, string newServiceName, string newUsername, string newPassword)
        {
            Records.ElementAt(index).EditRecord(newServer, newServiceName, newUsername, newPassword, ComputeKey());
        }

        public IEnumerable<AccountRecord>Search(string ServiceName)
        {
            var searched = from ar in this.Records
                           where ar.ServiceName.ToLower() == ServiceName.ToLower()
                           select ar;

            if (searched.Count() == 0) throw new KeyNotFoundException("We cant find any results!");

            return searched;
        }

        private byte[] ComputeKey()
        {
            if (KeyForRecords == null)
                throw new NotLoggedExeption("");
            using(HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(KeyForRecords));
            }
        }
    }
}