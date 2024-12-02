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
        ╚══════╝ ╚═════╝  ╚═════╝ ╚═╝���═╝  ╚═══╝"
        );
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    public bool ShowLogin()
    {
        while (_loginAttempts < MaxLoginAttempts)
        {
            string[] options = { "Login", "Register", "Exit" };
            string username = "";
            string password = "";
            bool isInputting = true;
            int fieldIndex = 0; // 0: username, 1: password, 2: buttons

            while (isInputting)
            {
                Console.Clear();
                DisplayLoginLogo();

                Console.WriteLine("Welcome to User Database Login");
                Console.WriteLine("Use ↑↓ arrows to navigate and Enter to select:\n");

                // Display credentials with highlighting
                Console.Write("Username: ");
                if (fieldIndex == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"{(username.Length > 0 ? username : "_")}");
                Console.ResetColor();
                Console.WriteLine();

                Console.Write("Password: ");
                if (fieldIndex == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                Console.Write($"{(password.Length > 0 ? new string('*', password.Length) : "_")}");
                Console.ResetColor();
                Console.WriteLine("\n");

                // Display options
                for (int i = 0; i < options.Length; i++)
                {
                    if (fieldIndex == 2 && i == _selectedIndex)
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

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (fieldIndex == 2)
                        {
                            _selectedIndex = (_selectedIndex - 1 + options.Length) % options.Length;
                        }
                        else
                        {
                            fieldIndex = (fieldIndex - 1 + 3) % 3;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (fieldIndex == 2)
                        {
                            _selectedIndex = (_selectedIndex + 1) % options.Length;
                        }
                        else
                        {
                            fieldIndex = (fieldIndex + 1) % 3;
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (fieldIndex == 2)
                        {
                            isInputting = false;
                        }
                        else
                        {
                            // Move to next field when pressing Enter
                            fieldIndex = (fieldIndex + 1) % 3;
                        }
                        break;

                    case ConsoleKey.Backspace:
                        if (fieldIndex == 0 && username.Length > 0)
                            username = username[..^1];
                        else if (fieldIndex == 1 && password.Length > 0)
                            password = password[..^1];
                        break;

                    default:
                        if (key.KeyChar >= 32 && key.KeyChar <= 126) // Printable characters
                        {
                            if (fieldIndex == 0)
                                username += key.KeyChar;
                            else if (fieldIndex == 1)
                                password += key.KeyChar;
                        }
                        break;
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
