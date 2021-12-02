namespace PasswordManagerLib.Exeption;

public class NoExistingAccountExeption : Exception
{
    public NoExistingAccountExeption(string message) : base(message)
    {
        
    }

    public NoExistingAccountExeption()
    {
        
    }

}
