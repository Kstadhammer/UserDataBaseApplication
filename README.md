# User Database Application

A C# console application for managing user records with an interactive menu interface.

## Features

- Interactive console menu with arrow key navigation
- Beautiful ASCII art welcome screen
- CRUD operations for user management:
  - Add new users
  - Update existing users
  - Delete users
  - Display all users
  - Search for specific users

## User Information Stored

The application stores the following information for each user:
- First Name
- Last Name
- Email
- Phone Number
- Address
- Postal Code
- City

## How to Use

1. Navigate the menu using the ↑↓ arrow keys
2. Press Enter to select an option
3. Follow the prompts to enter or modify user information
4. Use option 7 to reload the menu at any time
5. Select 'Exit' to close the application

## Technical Details

- Built with C# (.NET)
- Uses console-based user interface with colored output
- Implements service-based architecture for data management
- Includes input validation and user-friendly feedback

## Getting Started

1. Clone the repository
2. Open the solution in your preferred IDE (Visual Studio, VS Code, etc.)
3. Build and run the application
4. Use the arrow keys to navigate and Enter to select menu options

## Project Structure

```
UserDatabaseApp/
├── Models/
│   └── User.cs          # User data model
├── Services/
│   └── UserService.cs   # Business logic for user operations
├── UI/
│   └── MainMenu.cs      # Console interface and menu system
└── Program.cs           # Application entry point
```
