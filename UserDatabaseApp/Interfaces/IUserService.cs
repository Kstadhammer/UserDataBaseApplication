using System;
using UserDatabaseApp.Models;

namespace UserDatabaseApp.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void DeleteUser(string id);
    }
}
