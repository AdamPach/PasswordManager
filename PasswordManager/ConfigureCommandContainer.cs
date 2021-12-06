using PasswordManager.Command;
using PasswordManagerLib;

namespace PasswordManager;

public class ConfigureCommandContainer
{
    public static void Configure(ICore core, CommandContainer container)
    {
        container.RegisterCommads(new CreateCommand(core));
        container.RegisterCommads(new LoginCommand(core));
        container.RegisterCommads(new AddRecordCommand(core));
        container.RegisterCommads(new LogoutCommand(core));
        container.RegisterCommads(new ReadCommand(core));
        container.RegisterCommads(new RemoveCommand(core));
    }
}
