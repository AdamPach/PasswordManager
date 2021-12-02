using System.Text;

namespace PasswordManager.Printer;

public static class Input
{
    public static string ReadPassword()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo key;

        do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    if(password.Length > 0)
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                else if(key.Key != ConsoleKey.Enter)
                {
                    password.Append(key.KeyChar);
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return password.ToString();
    }
}
