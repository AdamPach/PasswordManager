
namespace PasswordManagerLib
{
    public class AccountRecord
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public string ServiceName { get; set; }

        public AccountRecord(){}

        public AccountRecord(string Username, string Password, string Server, string ServiceName)
        {
            this.Username = Username;
            this.Password = Password;
            this.Server = Server;
            this.ServiceName = ServiceName;
        }
    }
}