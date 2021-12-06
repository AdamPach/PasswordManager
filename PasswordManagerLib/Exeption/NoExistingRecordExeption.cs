namespace PasswordManagerLib.Exeption;

public class NoExistingRecordExeption : Exception
{
    public NoExistingRecordExeption(string message) : base(message)
    {
        
    }

    public NoExistingRecordExeption()
    {
        
    }
}