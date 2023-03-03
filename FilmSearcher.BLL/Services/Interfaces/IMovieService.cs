using FilmSearcher.DAL.Entities;
using System;
using System.Collections.Generic;
namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task UpdateAsync(Movie entity);
        Task DeleteAsync(int id);
        Task AddAsync(Movie entity);

        IEnumerable<Actor> GetActorsById(int id);
        Task AddActorsByIdAsync(int id, IEnumerable<Actor> actors);

        Task<IEnumerable<Actor>> GetActorsAsync();
    }
}
