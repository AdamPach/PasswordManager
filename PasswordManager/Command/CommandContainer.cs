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

    public IEnumerable<string> GetHelp(bool IsLogged)
    {
        List<string> helps;
        
        if(IsLogged)
            helps = (from h in Commands
                    where h.Value.AuthTag == Tags.AuthTag.HavetoBeAuth 
                    orderby h.Value.CommandName descending
                    select h.Value.Description).ToList();

        else
            helps = (from h in Commands
                    where h.Value.AuthTag == Tags.AuthTag.DontHavetoBeAuth 
                    orderby h.Value.CommandName descending
                    select h.Value.Description).ToList();
                
        var both =  from h in Commands
                    where h.Value.AuthTag == Tags.AuthTag.Both 
                    orderby h.Value.CommandName descending
                    select h.Value.Description; 

        helps.AddRange(both);  
        return helps;    
    }

    public void RegisterCommads(params BaseCommand[] RegistredCommands)
    {
        foreach (BaseCommand command in RegistredCommands)
        {
            Commands.Add(command.CommandName, command);
        }
    }
}
