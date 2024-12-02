using System.Threading.Tasks;
using UserDatabaseApp.Models;

namespace UserDatabaseApp.Interfaces
{
    public interface IValidationService
    {
        bool ValidateUser(User user);
    }
}
