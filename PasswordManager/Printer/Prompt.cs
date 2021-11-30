using PasswordManagerLib;

namespace PasswordManager.Printer;

public class Prompt
{
    private readonly ICore _Manager;

    public Prompt(ICore Manager)
    {
        _Manager = Manager;
    }


    private bool ProgramRunning;
    public void Start()
    {
        ProgramRunning = true;
        while(ProgramRunning)
        {

        }
    }
}
