using PasswordManager.Command;
using PasswordManager.Exeption;
using PasswordManagerLib;

namespace PasswordManager.Printer;

public class Prompt
{
    private readonly ICore _Manager;
    private readonly CommandContainer _CommandContainer;

    public Prompt(ICore Manager)
    {
        _Manager = Manager;
        _CommandContainer = new CommandContainer();
        RegisterExit();
    }


    private bool ProgramRunning;
    public void Start()
    {
        ProgramRunning = true;
        while(ProgramRunning)
        {
           try
           {
               Console.Write(">: ");
               var command = Console.ReadLine();
               _CommandContainer.GetCommand(command).Execute();
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

    private void Exit()
    {
        ProgramRunning = false;
    }
}
