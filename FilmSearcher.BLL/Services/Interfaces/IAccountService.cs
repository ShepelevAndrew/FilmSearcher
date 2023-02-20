using FilmSearcher.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ClaimsIdentity> Register(User model);

        Task<ClaimsIdentity> Login(User model);

        Task<bool> ChangePassword(User model);
    }
}
