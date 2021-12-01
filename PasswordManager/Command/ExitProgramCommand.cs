namespace PasswordManager.Command
{
    public class ExitProgramCommand : BaseCommand
    {

        public ExitProgramCommand()
        {
            CommandName = "exit";
            Description = "Enter exit when you need exit program";
        }
        
        protected override event CommandExecution CommandEvent;

        public async override Task Execute()
        {
            await CommandEvent();
        }

        public override void RegisterEvent(CommandExecution Event)
        {
            CommandEvent += Event;
        }
    }
}