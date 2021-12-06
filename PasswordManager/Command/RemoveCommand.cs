using PasswordManagerLib;
using PasswordManagerLib.Exeption;

namespace PasswordManager.Command;

public class RemoveCommand : BaseCommand
{
    private readonly ICore _core;

    public RemoveCommand(ICore core)
    {
        _core = core;
        CommandName = "remove";
        Description = "Remove - Remove record by service name!";
        AuthTag = Tags.AuthTag.HavetoBeAuth;
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
        CommandEvent += RemoveDefinition;
    }

    private async Task RemoveDefinition()
    {
        System.Console.Write("Enter Service Name: ");
        var ServiceName = Console.ReadLine();

        try
        {
           var record = await _core.DeleteRecord(ServiceName);
           System.Console.WriteLine("Succesful deleting this record: ");
           System.Console.WriteLine(record);
        }
        catch(NoExistingRecordExeption e)
        {
            System.Console.WriteLine(e.Message);
        }
    }
}