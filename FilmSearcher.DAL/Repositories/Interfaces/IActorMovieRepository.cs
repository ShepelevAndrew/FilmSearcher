using FilmSearcher.DAL.Entities;

namespace FilmSearcher.DAL.Repositories.Interfaces
{
    public interface IActorMovieRepository : IBaseRepository<ActorMovie>
    {
        IEnumerable<Actor> GetByMovieId(int id);
        IEnumerable<Movie> GetByActorId(int id);
        Task DeleteByActorIdAsync(int id);
    }
}
