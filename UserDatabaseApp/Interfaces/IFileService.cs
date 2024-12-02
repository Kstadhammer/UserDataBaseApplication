using UserDatabaseApp.Models;

namespace UserDatabaseApp.Interfaces
{
    public interface IFileService
    {
        List<User> LoadListFromFile();
        void SaveListToFile(List<User> users);
        void DeleteUser(string id);
    }
}
