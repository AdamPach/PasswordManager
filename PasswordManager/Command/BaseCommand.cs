namespace PasswordManager.Command;

public abstract class BaseCommand
{
    public delegate Task CommandExecution();
    //Default properties for all comands
    public string  CommandName { get; protected set; }
    public string Description { get; protected set; }
    //Abstract mothods and events
    protected abstract event CommandExecution CommandEvent;
    public abstract void RegisterEvent(CommandExecution Event);
    public abstract Task Execute();
}
