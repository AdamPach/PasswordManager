using PasswordManagerLib;

namespace PasswordManager.Command;

public class AddRecordCommand : BaseCommand
{
    private readonly ICore _core;

    public AddRecordCommand(ICore core)
    {
        _core = core;
        CommandName = "add";
        Description = "Add - Add record with you password into your account";
        AuthTag = Tags.AuthTag.HavetoBeAuth;
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
    }
}
