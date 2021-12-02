namespace PasswordManager.Exeption;

public class EnterCommandWithBadTagExeption : Exception
{
    public EnterCommandWithBadTagExeption(string message) : base(message)
    {
        
    }

    public EnterCommandWithBadTagExeption()
    {
        
    }
}