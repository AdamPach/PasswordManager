using PasswordManager.Printer;
using PasswordManagerLib;

await PasswordManagerLib.Initialize.Init.InitRepo();

ICore Manager = new Core();
var Prompt = new Prompt(Manager);

Prompt.Start();