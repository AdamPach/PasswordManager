namespace PasswordManager.Exeption;

public class InvalidCommandNameExeption : Exception
{
    public InvalidCommandNameExeption(string Message) : base(Message)
    {
        
    }
    public InvalidCommandNameExeption(){}
}
