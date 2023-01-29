using FilmSearcher.Models;

namespace FilmSearcher.Data.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task<Actor> UpdateAsync(int id, Actor actor);
        Task DeleteAsync(int id);
    }
}
