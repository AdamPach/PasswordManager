using PasswordManager.Printer;
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
        AuthTag = Tags.AuthTag.DontHavetoBeAuth;   
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
        var password = Input.ReadPassword();

        await _core.CreateAccount(name, password);
    }
}
