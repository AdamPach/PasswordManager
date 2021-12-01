namespace PasswordManager.Command;

public abstract class BaseCommand
{
    //Default properties for all comands
    protected string  CommandName { get; set; }
    protected string Description { get; set; }
    //Abstract mothods and events
    public abstract event Action CommandEvent;
    public abstract void Execute();
}
