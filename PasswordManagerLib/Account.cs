namespace PasswordManagerLib
{
    public class Account
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }

        public Account(string Name, string Password, string FileName)
        {
            this.Name = Name;
            this.Password = Password;
            this.FileName = FileName;
        }

        public Account(){}

        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}