using FilmSearcher.BLL.Models;
using System.Security.Claims;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ClaimsIdentity> Register(UserDTO model);

        Task<ClaimsIdentity> Login(UserDTO model);

        Task<bool> ChangePassword(UserDTO model);
    }
}
