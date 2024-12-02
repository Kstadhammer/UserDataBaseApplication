using UserDatabaseApp.Models;

namespace UserDatabaseApp.Services;

public class UserService
{
    private List<User> _users = [];
    private readonly FileService _fileService = new();

    public UserService()
    {
        _users = _fileService.LoadListFromFile();
    }

    public void Add(User user)
    {
        _users = _fileService.LoadListFromFile();  // Load current users first
        _users.Add(user);
        _fileService.SaveListToFile(_users);
    }

    public IEnumerable<User> GetAll()
    {
        _users = _fileService.LoadListFromFile();
        return _users;
    }
}
