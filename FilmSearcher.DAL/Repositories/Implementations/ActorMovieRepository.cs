using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.DAL.Repositories.Implementations
{
    public class ActorMovieRepository : IActorMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ActorMovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ActorMovie entity)
        {
            await _dbContext.ActorsMovies.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actorMovie = await _dbContext.ActorsMovies.FirstOrDefaultAsync(am => am.MovieId == id && am.ActorId == id);
            _dbContext.ActorsMovies.Remove(actorMovie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteByActorIdAsync(int id)
        {
            var actorMovie = await _dbContext.ActorsMovies.FirstOrDefaultAsync(am => am.ActorId == id);
            _dbContext.ActorsMovies.Remove(actorMovie);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ActorMovie>> GetAllAsync()
        {
            var actorsMovies = await _dbContext.ActorsMovies.ToListAsync();
            return actorsMovies;
        }

        public IEnumerable<Actor> GetByMovieId(int id)
        {
            var actorMovie = _dbContext.ActorsMovies.ToList().FindAll(am => am.MovieId == id);
            var actors = _dbContext.Actors.ToList();

            var actorsId = new List<int>();
            var actorsByMovie = new List<Actor>();

            foreach (var actor in actorMovie)
                actorsId.Add(actor.ActorId);

            for (int i = 0; i < actorsId.Count; i++)
                actorsByMovie.Add(actors.FirstOrDefault(a => a.ActorId == actorsId[i]));

            return actorsByMovie;
        }

        public IEnumerable<Movie> GetByActorId(int id)
        {
            var actorMovie = _dbContext.ActorsMovies.ToList().FindAll(am => am.ActorId == id);
            var movies = new List<Movie>();

            foreach (var movie in actorMovie)
                movies.Add(movie.Movie);

            return movies;
        }

        public async Task<ActorMovie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.ActorsMovies.FirstOrDefaultAsync(am => am.MovieId == id && am.ActorId == id);
            return movie;
        }

        public async Task UpdateAsync(ActorMovie entity)
        {
            _dbContext.ActorsMovies.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
