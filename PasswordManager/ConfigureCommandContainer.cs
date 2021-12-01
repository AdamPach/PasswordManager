using PasswordManager.Command;
using PasswordManagerLib;

namespace PasswordManager;

public class ConfigureCommandContainer
{
    private static ICore _core;

    public static void Configure(ICore core, CommandContainer container)
    {
        _core = core;
        container.RegisterCommads(CreateAccount());
    }


    private static BaseCommand CreateAccount()
    {
        var command = new CreateCommand();
        command.RegisterEvent(CreateAccountCommand);
        return command;
    }
    private static async Task CreateAccountCommand()
    {
        Console.Write("Enter name: ");
        var name = Console.ReadLine();

        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        await _core.CreateAccount(name, password);
    }
}
