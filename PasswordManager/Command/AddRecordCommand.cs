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
        CommandEvent += AddRecordAsync;
    }

    private async Task AddRecordAsync()
    {
        Console.Write("Enter Service name: ");
        var sn = Console.ReadLine();

        Console.Write("Enter Url: ");
        var Url = Console.ReadLine();

        Console.Write("Enter Name: ");
        var Name = Console.ReadLine();

        Console.Write("Enter Password: ");
        var Password = Console.ReadLine();

        await _core.CreateRecord(Name, Password, sn, Url);
    }
}
