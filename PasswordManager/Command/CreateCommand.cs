using PasswordManagerLib;

namespace PasswordManager.Command;

public class CreateCommand : BaseCommand
{
    private readonly ICore _core;

    public CreateCommand(ICore core)
    {
        _core = core;
        CommandName = "create";
        Description = "Create account for you and your passwords";   
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
        CommandEvent += CreateCommandDefinition;
    }

    private async Task CreateCommandDefinition()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();

        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        await _core.CreateAccount(name, password);
    }
}
