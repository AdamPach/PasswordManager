using PasswordManager.Printer;
using PasswordManager.Command;
using PasswordManagerLib;
using PasswordManager;

await PasswordManagerLib.Initialize.Init.InitRepo();

ICore Manager = new Core();
var commandContainer = new CommandContainer();

ConfigureCommandContainer.Configure(Manager, commandContainer);

var Prompt = new Prompt(Manager, commandContainer);

await Prompt.Start();