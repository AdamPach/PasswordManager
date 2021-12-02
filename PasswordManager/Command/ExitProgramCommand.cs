namespace PasswordManager.Command
{
    public class ExitProgramCommand : BaseCommand
    {
        public ExitProgramCommand()
        {
            CommandName = "exit";
            Description = "Exit - Enter exit when you need exit program";
            AuthTag = Tags.AuthTag.Both;
        }

        protected override void RegisterCommand(){}
    }
}