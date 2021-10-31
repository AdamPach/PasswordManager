using System.Collections.Generic;
using System.Linq;

namespace PasswordManager
{
    public class Autocomplete
    {
        private readonly static string[] AllCommands = {"add", "exit", "list", "logout", "remove", "search", "edit" , "delete"};

        public static IEnumerable<string> Complete(string commandToComplete)
        {
            var matches = from cm in AllCommands
                            where cm.StartsWith(commandToComplete)
                            select cm;
            
            return matches;
        }
    }
}