using PasswordManager.Command;
using PasswordManager.Exeption;
using PasswordManagerLib;

namespace PasswordManager.Printer;

public class Prompt
{
    private readonly ICore _core;
    private readonly CommandContainer _CommandContainer;

    public Prompt(ICore Manager, CommandContainer commandContainer)
    {
        _core = Manager;
        _CommandContainer = commandContainer;
        RegisterExit();
    }


    private bool ProgramRunning;
    public async Task Start()
    {
        ProgramRunning = true;
        while(ProgramRunning)
        {
           try
           {
               Console.Write(">: ");
               var command = Console.ReadLine();
               await _CommandContainer.GetCommand(command, _core.IsLogged()).Execute();
           }
           catch(InvalidCommandNameExeption)
           {
               PrintHelp(_CommandContainer.GetHelp(_core.IsLogged()));
           }
           catch(EnterCommandWithBadTagExeption e)
           {
               System.Console.WriteLine(e.Message);
           }
        }
    }

    private void RegisterExit()
    {
        var exitCommand = new ExitProgramCommand();
        exitCommand.RegisterEvent(Exit);
        _CommandContainer.RegisterCommads(exitCommand);
    }

    private void PrintHelp(IEnumerable<string> help)
    {
        foreach (var item in help)
        {
            System.Console.WriteLine(item);
        }
    }

    private async Task Exit()
    {
        await Task.Run(() => {ProgramRunning = false;});
    }
}
