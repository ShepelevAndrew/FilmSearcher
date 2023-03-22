using FilmSearcher.DAL.Entities;

namespace FilmSearcher.DAL.Repositories.Interfaces
{
    public interface IMovieUserRepository : IBaseRepository<MovieUser>
    {
        Task DeleteByMovieId(int id);
        Task DeleteByUserId(int id);
        IEnumerable<User> GetUserByMovieId(int id);
        IEnumerable<Movie> GetMoviesByUserId(int id);
    }
}
