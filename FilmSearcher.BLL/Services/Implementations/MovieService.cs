using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Movie movie)
        {
            await _dbContext.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            return movies;
        }
    }
}
