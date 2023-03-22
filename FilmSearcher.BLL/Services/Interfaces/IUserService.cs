﻿using FilmSearcher.BLL.Models;
using FilmSearcher.DAL.Entities;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDTO model);

        Dictionary<int, string> GetRoles();

        Task<IEnumerable<UserDTO>> GetUsers();

        Task<UserDTO> GetUserById(int id);

        Task<bool> DeleteUser(int id);

        Task<bool> UpdateUser(UserDTO user);

        IEnumerable<MovieDTO> GetMoviesByUserId(int Id);
    }
}
