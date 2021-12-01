namespace PasswordManager.Command;

public abstract class BaseCommand
{
    //Default properties for all comands
    public string  CommandName { get; protected set; }
    public string Description { get; protected set; }
    //Abstract mothods and events
    protected abstract event Action CommandEvent;
    public abstract void RegisterEvent(Action Event);
    public abstract void Execute();
}
