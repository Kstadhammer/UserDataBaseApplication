using UserDatabaseApp.UI;

var loginMenu = new LoginMenu();
if (loginMenu.ShowLogin())
{
    var mainMenu = new MainMenu();
    mainMenu.UserMenu();
}