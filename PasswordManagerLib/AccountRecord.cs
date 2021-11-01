
using System.Xml.Serialization;

namespace PasswordManagerLib
{
    public class AccountRecord
    {
        public string Username { get; set; }
        public RecordPassword Password { get; set; }
        public string Server { get; set; }
        public string ServiceName { get; set; }
        [XmlIgnore]
        public byte[] Key { get; set; }

        public AccountRecord(){}

        public AccountRecord(string Username, string Password, string Server, string ServiceName, byte[] hash)
        {
            this.Username = Username;
            this.Password = new RecordPassword(Password, hash);
            Key = hash;
            this.Server = Server;
            this.ServiceName = ServiceName;
        }

        public void EditRecord(string newServer, string newServiceName, string newUsername, string newPassword, byte[] hash)
        {
            if (newServer != "")
            {
                Server = newServer;
            }
            if (newServiceName != "")
            {
                ServiceName = newServiceName;
            }
            if (newUsername != "")
            {
                Username = newUsername;
            }
            if (newPassword != "")
            {
                Password.ChangePassword(newPassword, hash);
            }
        }

        public override string ToString()
        {
            return $"Service: {ServiceName}\n\tServer: {Server}\n\tUsername: {Username}\n\tPassword: {Password.DecryptString(Key)}\n";
        }
    }
}