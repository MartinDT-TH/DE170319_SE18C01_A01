using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuynhLeDucThoWPF.ViewModels
{
    public class AuthService : IAuthService
    {
        private const string AdminEmail = "admin@FUMiniHotelSystem.com";
        private const string AdminPassword = "@@abc123@@";
        public Task<bool> AuthenticateAsync(string email, string password)
        {
            return Task.FromResult(email == AdminEmail && password == AdminPassword);
        }
    }

}
