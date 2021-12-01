using PasswordManager.Command;
using PasswordManagerLib;

namespace PasswordManager;

public class ConfigureCommandContainer
{
    private static ICore _core;

    public static void Configure(ICore core, CommandContainer container)
    {
        _core = core;
        container.RegisterCommads(new CreateCommand(_core));
    }
}
