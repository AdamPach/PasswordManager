using PasswordManagerLib;

namespace PasswordManager.Command;

public class LogoutCommand : BaseCommand
{
    private readonly ICore _core;

    public LogoutCommand(ICore core)
    {
        _core = core;
        CommandName = "logout";
        Description = "LogOut - Log out you from your account";
        AuthTag = Tags.AuthTag.HavetoBeAuth;
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
        CommandEvent += LogOutDefinition;
    }

    private async Task LogOutDefinition()
    {
        await _core.LogOut();
        System.Console.WriteLine("You are logged out!");
    }
}
