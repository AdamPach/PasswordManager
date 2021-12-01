namespace PasswordManager.Command
{
    public class ExitProgramCommand : BaseCommand
    {
        public ExitProgramCommand()
        {
            CommandName = "exit";
            Description = "Enter exit when you need exit program";
        }

        protected override void RegisterCommand(){}
    }
}