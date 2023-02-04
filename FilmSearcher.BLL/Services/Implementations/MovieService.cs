using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementation
{
    public class MovieService : ICrudService<Movie>
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

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.MovieId == id);
            return movie;
        }

        public async Task UpdateAsync(int id, Movie movie)
        {
            movie.MovieId = id;
            _dbContext.Movies.Update(movie);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var movie = _dbContext.Movies.FirstOrDefault(m => m.MovieId == id);
            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();
        }
    }
}
