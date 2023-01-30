using FilmSearcher.Data.Services.Interfaces;
using FilmSearcher.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.Data.Services.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _dbContext;

        public MovieService(AppDbContext dbContext)
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
