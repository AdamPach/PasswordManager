using System;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PasswordManagerLib
{
    public class AccountPassword : IPassword
    {
        //Propeties

        [XmlAttribute("Hash")]
        public string password { get; set; }

        public AccountPassword(string password)
        {
            if (!IsValidPassword(password))
                throw new IsInvalidPasswordExeption();
            this.password = GetHashedPassword(password);
        }

        public AccountPassword() { }

        public static implicit operator AccountPassword(string ps)
        {
            return new AccountPassword(ps);
        }

        public static explicit operator string(AccountPassword ap)
        {
            return ap.password;
        }

        public bool ComparePasswords(string psToCompare)
        {
            return GetHashedPassword(psToCompare) == this.password;
        }

        private byte[] GetHash(string inputString)
        {
            using (HashAlgorithm hashAlgorithm = SHA256.Create()) 
                return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashedPassword(string inputString)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in GetHash(inputString))
            {
                builder.Append(item.ToString("X2"));
            }

            return builder.ToString();
        }

        public static bool IsValidPassword(string newPassword)
        {
            Regex rx = new Regex(@"^[a-zA-Z0-9]{8,}$");

            return rx.IsMatch(newPassword);
        }
    }
}