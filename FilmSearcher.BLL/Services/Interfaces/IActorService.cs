using FilmSearcher.DAL.Entities;

namespace FilmSearcher.BLL.Services.Interfaces
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
