using FilmSearcher.Models;

namespace FilmSearcher.Data.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task AddAsync(Movie movie);
    }
}
