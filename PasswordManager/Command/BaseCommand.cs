using PasswordManagerLib;

namespace PasswordManager.Command;

public abstract class BaseCommand
{
    public delegate Task CommandExecution();
    //Default properties for all comands
    public string  CommandName { get; protected set; }
    public string Description { get; protected set; }
    //Abstract mothods and events
    protected abstract void RegisterCommand();
    protected event CommandExecution CommandEvent;
    public void RegisterEvent(CommandExecution Event)
    {
        CommandEvent += Event;
    }
    public async Task Execute()
    {
        await CommandEvent();
    }
}
