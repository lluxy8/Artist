using Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task SignInAsync(UserAuthModel model);
        Task SignOutAsync();
        UserAuthModel GetUser();
    }
}
