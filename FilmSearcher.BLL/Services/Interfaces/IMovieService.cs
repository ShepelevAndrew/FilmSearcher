using FilmSearcher.DAL.Entities;

namespace FilmSearcher.BLL.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task AddAsync(Movie movie);
    }
}
