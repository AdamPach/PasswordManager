using PasswordManager.Printer;
using PasswordManagerLib;

PasswordManagerLib.Initialize.Init.InitRepo();

ICore Manager = new Core();
var Prompt = new Prompt(Manager);

Prompt.Start();