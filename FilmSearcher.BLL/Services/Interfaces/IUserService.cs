using FilmSearcher.BLL.Models;
using FilmSearcher.DAL.Entities;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDTO model);

        Dictionary<int, string> GetRoles();

        Task<IEnumerable<UserDTO>> GetUsers();

        Task<bool> DeleteUser(int id);

        Task<bool> UpdateUser(UserDTO user);
    }
}
