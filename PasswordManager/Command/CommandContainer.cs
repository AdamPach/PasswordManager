using System.Collections;
using PasswordManager.Exeption;

namespace PasswordManager.Command;

public class CommandContainer
{
    private Dictionary<string, BaseCommand> Commands;

    public CommandContainer()
    {
        Commands = new Dictionary<string, BaseCommand>();
    }

    public BaseCommand GetCommand(string key)
    {
        var command = Commands.GetValueOrDefault(key.ToLower());
        if(command == null) throw new InvalidCommandNameExeption();
        return command;
    }

    public void RegisterCommads(params BaseCommand[] RegistredCommands)
    {
        foreach (BaseCommand command in RegistredCommands)
        {
            Commands.Add(command.CommandName, command);
        }
    }
}
