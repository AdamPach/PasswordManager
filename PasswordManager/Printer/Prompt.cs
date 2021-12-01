using PasswordManager.Command;
using PasswordManager.Exeption;
using PasswordManagerLib;

namespace PasswordManager.Printer;

public class Prompt
{
    private readonly ICore _Manager;
    private readonly CommandContainer _CommandContainer;

    public Prompt(ICore Manager, CommandContainer commandContainer)
    {
        _Manager = Manager;
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
               await _CommandContainer.GetCommand(command).Execute();
           }
           catch(InvalidCommandNameExeption)
           {

           }
        }
    }

    private void RegisterExit()
    {
        var exitCommand = new ExitProgramCommand();
        exitCommand.RegisterEvent(Exit);
        _CommandContainer.RegisterCommads(exitCommand);
    }

    private async Task Exit()
    {
        await Task.Run(() => {ProgramRunning = false;});
    }
}
