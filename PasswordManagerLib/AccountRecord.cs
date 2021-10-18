
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

        public void EditRecord(string newServer, string newServiceName, string newUsername, string newPassword)
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
                Password = newPassword;
            }
        }

        public override string ToString()
        {
            return $"Service: {ServiceName}\n\tServer: {Server}\n\tUsername: {Username}\n\tPassword: {Password}\n";
        }
    }
}