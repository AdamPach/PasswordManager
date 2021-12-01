using PasswordManager.Command;
using PasswordManagerLib;

namespace PasswordManager;

public class ConfigureCommandContainer
{
    public static void Configure(ICore core, CommandContainer container)
    {
        container.RegisterCommads(new CreateCommand(core));
    }
}
