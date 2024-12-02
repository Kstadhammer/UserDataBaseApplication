using UserDatabaseApp.Interfaces;
using UserDatabaseApp.Models;
using UserDatabaseApp.Services;

namespace UserDatabaseApp.UI;

public class LoginMenu
{
    private int _selectedIndex = 0;
    private const string ValidUsername = "admin";
    private const string ValidPassword = "admin";
    private int _loginAttempts = 0;
    private const int MaxLoginAttempts = 3;

    private void DisplayLoginLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            @"
        ██╗      ██████╗  ██████╗ ██╗███╗   ██╗
        ██║     ██╔═══██╗██╔════╝ ██║████╗  ██║
        ██║     ██║   ██║██║  ███╗██║██╔██╗ ██║
        ██║     ██║   ██║██║   ██║██║██║╚██╗██║
        ███████╗╚██████╔╝╚██████╔╝██║██║ ╚████║
        ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝╚═╝  ╚═══╝"
        );
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    public bool ShowLogin()
    {
        while (_loginAttempts < MaxLoginAttempts)
        {
            string[] options = { "Login", "Exit" };
            string username = "";
            string password = "";
            bool isInputting = true;
            bool isUsername = true;

            while (isInputting)
            {
                Console.Clear();
                DisplayLoginLogo();

                Console.WriteLine("Welcome to User Database Login");
                Console.WriteLine("Use ↑↓ arrows to navigate and Enter to select:\n");

                // Display credentials
                Console.WriteLine($"Username: {(isUsername ? "_" : username)}");
                Console.WriteLine(
                    $"Password: {(isUsername ? "" : (password.Length > 0 ? new string('*', password.Length) : "_"))}\n"
                );

                // Display options
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == _selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }

                // Display attempts remaining if any failed attempts
                if (_loginAttempts > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nAttempts remaining: {MaxLoginAttempts - _loginAttempts}");
                    Console.ResetColor();
                }

                var key = Console.ReadKey(true);

                if (isUsername || (!isUsername && _selectedIndex == 0))
                {
                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (isUsername)
                        {
                            isUsername = false;
                        }
                        else
                        {
                            isInputting = false;
                        }
                    }
                    else if (
                        key.Key == ConsoleKey.Backspace
                        && (
                            (isUsername && username.Length > 0)
                            || (!isUsername && password.Length > 0)
                        )
                    )
                    {
                        if (isUsername)
                            username = username[..^1];
                        else
                            password = password[..^1];
                    }
                    else if (key.KeyChar >= 32 && key.KeyChar <= 126) // Printable characters
                    {
                        if (isUsername)
                            username += key.KeyChar;
                        else
                            password += key.KeyChar;
                    }
                }
                else
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            _selectedIndex = (_selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            _selectedIndex = (_selectedIndex + 1) % options.Length;
                            break;
                        case ConsoleKey.Enter:
                            isInputting = false;
                            break;
                    }
                }
            }

            // Handle selected option
            if (_selectedIndex == 0) // Login selected
            {
                if (ValidateCredentials(username, password))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLogin successful! Welcome to the User Database App.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    _loginAttempts++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid username or password!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
            else // Exit selected
            {
                ExitApp();
                return false;
            }
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nMaximum login attempts exceeded. Application will now exit.");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return false;
    }

    private bool ValidateCredentials(string username, string password)
    {
        return username == ValidUsername && password == ValidPassword;
    }

    private void ExitApp()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\nThank you for using the User Database App!");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Closing...");
        Console.ResetColor();

        Thread.Sleep(1500);
        Environment.Exit(0);
    }
}
