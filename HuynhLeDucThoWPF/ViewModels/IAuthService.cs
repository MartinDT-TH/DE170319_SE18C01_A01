using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuynhLeDucThoWPF.ViewModels
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(string email, string password);
    }

}
