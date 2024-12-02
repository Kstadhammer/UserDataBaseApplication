using UserDatabaseApp.Helpers;
using UserDatabaseApp.Interfaces;
using UserDatabaseApp.Models;
using UserDatabaseApp.Services;

namespace UserDatabaseApp.UI;

public class MainMenu
{
    private readonly UserService _userService = new();
    private readonly FileService _fileService = new();
    private int _selectedIndex = 0;

    // I got help from Claude Sonnet 3.5 To generate the Logo Method.
    // Also got help implementing the Key input from the same source,
    // Since I had no idea how to implement using arrow keys to navigate the menu.

    private void DisplayLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
            @"
         █     █░▓█████  ██▓     ▄████▄   ▒█████   ███▄ ▄███▓▓█████ 
        ▓█░ █ ░█░▓█   ▀ ▓██▒    ▒██▀ ▀█  ▒██▒  ██▒▓██▒▀█▀ ██▒▓█   ▀ 
        ▒█░ █ ░█ ▒███   ▒██░    ▒▓█    ▄ ▒██░  ██▒▓██    ▓██░▒███   
        ░█░ █ ░█ ▒▓█  ▄ ▒██░    ▒▓▓▄ ▄██▒▒██   ██░▒██    ▒██ ▒▓█  ▄ 
        ░░██▒██▓ ░▒████▒░██████▒▒ ▓███▀ ░░ ████▓▒░▒██▒   ░██▒░▒████▒
        ░ ▓░▒ ▒  ░░ ▒░ ░░ ▒░▓  ░░ ░▒ ▒  ░░ ▒░▒░▒░ ░ ▒░   ░  ░░░ ▒░ ░

        ▄▄▄█████▓ ▒█████      ▄▄▄█████▓ ██░ ██ ▓█████ 
        ▓  ██▒ ▓▒▒██▒  ██▒    ▓  ██▒ ▓▒▓██░ ██▒▓█   ▀ 
        ▒ ▓██░ ▒░▒██░  ██▒    ▒ ▓██░ ▒░▒██▀▀██░▒███   
        ░ ▓██▓ ░ ▒██   ██░    ░ ▓██▓ ░ ░▓█ ░██ ▒▓█  ▄ 
          ▒██▒ ░ ░ ████▓▒░      ▒██▒ ░ ░▓█▒░██▓░▒████▒
          ▒ ░░   ░ ▒░▒░▒░       ▒ ░░    ▒ ░░▒░▒░░ ▒░ ░

        ▓█████▄  ▄▄▄     ▄▄▄█████▓ ▄▄▄       ▄▄▄▄    ▄▄▄        ██████ ▓█████ 
        ▒██▀ ██▌▒████▄   ▓  ██▒ ▓▒▒████▄    ▓█████▄ ▒████▄    ▒██    ▒ ▓█   ▀ 
        ░██   █▌▒██  ▀█▄ ▒ ▓██░ ▒░▒██  ▀█▄  ▒██▒ ▄██▒██  ▀█▄  ░ ▓██▄   ▒███   
        ░▓█▄   ▌░██▄▄▄▄██░ ▓██▓ ░ ░██▄▄▄▄██ ▒██░█▀  ░██▄▄▄▄██   ▒   ██▒▒▓█  ▄ 
        ░▒████▓  ▓█   ▓██▒ ▒██▒ ░  ▓█   ▓██▒░▓█  ▀█▓ ▓█   ▓██▒▒██████▒▒░▒████▒
         ▒▒▓  ▒  ▒▒   ▓▒█░ ▒ ░░    ▒▒   ▓▒█░░▒▓███▀▒ ▒▒   ▓▒█░▒ ▒▓▒ ▒ ░░░ ▒░ ░"
        );
        Console.ResetColor();
        Console.WriteLine("\n");
    }

    public void UserMenu()
    {
        bool exit = false;
        string[] options = new[]
        {
            "1. Add a user",
            "2. Update a user",
            "3. Delete a user",
            "4. Display all users",
            "5. Search for a user",
            "6. Exit",
        };

        while (!exit)
        {
            Console.Clear();
            DisplayLogo();
            Console.WriteLine("Welcome to the User Database App!");
            Console.WriteLine("Use ↑↓ arrows to navigate and Enter to select:\n");

            // Display menu options
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

            // Handle key input
            var key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    _selectedIndex = (_selectedIndex - 1 + options.Length) % options.Length;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedIndex = (_selectedIndex + 1) % options.Length;
                    break;
                case ConsoleKey.Enter:
                    switch (_selectedIndex)
                    {
                        case 0:
                            AddUser();
                            break;
                        case 1:
                            UpdateUser();
                            break;
                        case 2:
                            DeleteUser();
                            break;
                        case 3:
                            DisplayAllUsers();
                            break;
                        case 4:
                            SearchUser();
                            break;
                        case 5:
                            ExitApp();
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                    break;
            }
        }
    }

    public void ExitApp()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n Are you sure you want to exit? (y/n)");
        Console.WriteLine("Press any other key to return to the menu.");
        string exitConfirmation = Console.ReadLine()!;
        if (exitConfirmation == "y")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThank you for using the User Database App!");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Saving and closing...");
            Console.ResetColor();

            Thread.Sleep(1500);
            Environment.Exit(0);
        }
        else
        {
            UserMenu();
        }
    }

    public void AddUser()
    {
        User user = new();

        Console.Clear();
        Console.WriteLine("Enter the user's first name:");
        user.FirstName = Console.ReadLine()!;
        Console.WriteLine("Enter a user's last name:");
        user.LastName = Console.ReadLine()!;
        Console.WriteLine("Enter the user's email:");
        user.Email = Console.ReadLine()!;
        Console.WriteLine("Enter the user's phone number:");
        user.PhoneNumber = Console.ReadLine()!;
        Console.WriteLine("Enter the user's address:");
        user.Address = Console.ReadLine()!;
        Console.WriteLine("Enter the user's postal code:");
        user.PostalCode = Console.ReadLine()!;
        Console.WriteLine("Enter the user's city:");
        user.City = Console.ReadLine()!;

        string fullName = user.FirstName + " " + user.LastName;

        Console.WriteLine($"{fullName} added successfully to the database!");
        Console.ResetColor();

        _userService.Add(user);

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        UserMenu();
    }

    public void UpdateUser()
    {
        //update user code
    }

    public void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("Enter the name of the user you want to delete: ");
        string? userFirstName = Console.ReadLine();

        if (string.IsNullOrEmpty(userFirstName))
        {
            Console.WriteLine("Invalid input. Name cannot be empty.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            UserMenu();
            return;
        }

        User? userToDelete = _userService
            .GetAll()
            .FirstOrDefault(user =>
                user.FirstName.Equals(userFirstName, StringComparison.OrdinalIgnoreCase)
            );

        if (userToDelete == null)
        {
            Console.WriteLine("No user found with that name.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            UserMenu();
            return;
        }

        _fileService.DeleteUser(userToDelete.Id);
        Console.WriteLine("User deleted successfully.");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        UserMenu();
    }

    public void DisplayAllUsers()
    {
        Console.Clear();
        DateTime currentTime = DateTime.Now;
        Console.WriteLine("All current users in the database:\n");
        Console.WriteLine($"{currentTime}:\n");
        foreach (var user in _userService.GetAll())
        {
            Console.WriteLine(
                $"""
                Time Created: {user.TimeCreated}
                Id: {user.Id}
                First Name: {user.FirstName}
                Last Name: {user.LastName}
                Email: {user.Email}
                Phone Number: {user.PhoneNumber}
                Address: {user.Address}
                Postal Code: {user.PostalCode}
                City: {user.City}
                {new string('-', 50)}
                """
            );
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        UserMenu();
    }

    public void SearchUser() { }
}
