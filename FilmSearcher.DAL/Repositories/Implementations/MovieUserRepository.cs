using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.DAL.Repositories.Implementations
{
    public class MovieUserRepository : IMovieUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieUserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(MovieUser entity)
        {
            await _dbContext.MoviesUsers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByUserId(int id)
        {
            var movieUser = await _dbContext.MoviesUsers.FirstOrDefaultAsync(mu => mu.UserId == id);
            _dbContext.MoviesUsers.Remove(movieUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByMovieId(int id)
        {
            var movieUser = await _dbContext.MoviesUsers.FirstOrDefaultAsync(mu => mu.MovieId == id);
            _dbContext.MoviesUsers.Remove(movieUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieUser>> GetAllAsync()
        {
            var moviesUsers = await _dbContext.MoviesUsers.ToListAsync();
            return moviesUsers;
        }

        public Task<MovieUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUserByMovieId(int id)
        {
            var movieUser = _dbContext.MoviesUsers.ToList().FindAll(mu => mu.MovieId == id);
            var users = new List<User>();
            foreach (var movie in movieUser)
                users.Add(movie.User);

            return users;
        }

        public IEnumerable<Movie> GetMoviesByUserId(int id)
        {
            var moviesUsers = _dbContext.MoviesUsers.ToList().FindAll(mu => mu.UserId == id);
            var movies = _dbContext.Movies.ToList();

            var moviesId = new List<int>();
            var moviesByUser = new List<Movie>();

            foreach (var movie in moviesUsers)
                moviesId.Add(movie.MovieId);

            for (int i = 0; i < moviesId.Count; i++)
                moviesByUser.Add(movies.FirstOrDefault(m => m.MovieId == moviesId[i]));

            return moviesByUser;
        }

        public async Task UpdateAsync(MovieUser entity)
        {
            _dbContext.MoviesUsers.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
