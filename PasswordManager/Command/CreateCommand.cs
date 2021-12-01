namespace PasswordManager.Command;

public class CreateCommand : BaseCommand
{

    public CreateCommand()
    {
        CommandName = "create";
        Description = "Create account for you and your passwords";   
    }

    protected override event CommandExecution CommandEvent;

    public async override Task Execute()
    {
        await CommandEvent();
    }

    public override void RegisterEvent(CommandExecution Event)
    {
        CommandEvent += Event;
    }
}
