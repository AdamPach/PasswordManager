using PasswordManagerLib;

namespace PasswordManager.Command;

public class LoginCommand : BaseCommand
{
    private readonly ICore _core;

    public LoginCommand(ICore core)
    {
        _core = core;
        CommandName = "login";
        Description = "LogIn - Log in into your account";
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
        CommandEvent += LogInDefinistion;
    }

    private async Task LogInDefinistion()
    {
        Console.Write("Enter Username: ");
        var name = Console.ReadLine();

        Console.Write("Enter Password");
        var password = Console.ReadLine();

        if(await _core.LogIn(name, password))
            System.Console.WriteLine("You are log in!");
        else
            System.Console.WriteLine("Bad password - try it again");

    }
}
