using UserDatabaseApp.Models;

namespace UserDatabaseApp.Services;

public class FileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    public FileService(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }

    public void SaveListToFile(List<User> list) { }

    public List<User> LoadListFromFile()
    {
        return new List<User>();
    }
}
