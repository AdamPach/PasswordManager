namespace PasswordManager.Command
{
    public class ExitProgramCommand : BaseCommand
    {

        public ExitProgramCommand()
        {
            CommandName = "exit";
            Description = "Enter exit when you need exit program";
        }
        
        protected override event Action CommandEvent;

        public override void Execute()
        {
            CommandEvent();
        }

        public override void RegisterEvent(Action Event)
        {
            CommandEvent += Event;
        }
    }
}