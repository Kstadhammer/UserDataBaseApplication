using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDatabaseApp.Interfaces;

namespace UserDatabaseApp.Services
{
    public class DisplayService
    {
        private readonly IUserService _userService;

        public DisplayService(IUserService userService)
        {
            _userService = userService;
        }

        public void DisplayAllUsers()
        {
            var users = _userService.GetAllUsers();
        }
    }
}
