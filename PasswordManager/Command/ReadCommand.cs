using PasswordManagerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Command;

public class ReadCommand : BaseCommand
{
    private readonly ICore _core;

    public ReadCommand(ICore core)
    {
        _core = core;
        CommandName = "read";
        Description = "Read - Read all your records and display it!";
        AuthTag = Tags.AuthTag.HavetoBeAuth;
        RegisterCommand();
    }

    protected override void RegisterCommand()
    {
        CommandEvent += ReadDefinition;
    }

    private async Task ReadDefinition()
    {
        Console.WriteLine("Your recods\n");

        foreach (var item in await _core.ReadRecords())
        {
            Console.WriteLine(item);
        }
    }
}
