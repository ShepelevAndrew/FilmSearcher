using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IBaseRepository<Movie> _movieRepository;
        private readonly IBaseRepository<Actor> _actorRepository;
        private readonly IActorMovieRepository _actorMovieRepository;

        public MovieService(IActorMovieRepository actorMovieRepository, IBaseRepository<Movie> movieRepository, IBaseRepository<Actor> actorRepository)
        {
            _actorMovieRepository = actorMovieRepository;
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
        }

        //Movie
        public async Task AddAsync(Movie entity)
        {
            await _movieRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _actorMovieRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            return movie;
        }

        public async Task UpdateAsync(Movie entity)
        {
            await _movieRepository.UpdateAsync(entity);
        }

        //ActorMovie
        public IEnumerable<Actor> GetActorsById(int id)
        {
            var actors = _actorMovieRepository.GetByMovieId(id);
            return actors;
        }
        public async Task AddActorsByIdAsync(int id, IEnumerable<Actor> actors)
        {
            foreach(var actor in actors) {
                var actorMovie = new ActorMovie()
                {
                    MovieId = id,
                    ActorId = actor.ActorId
                };

                await _actorMovieRepository.AddAsync(actorMovie);
            }
        }

        //Actor
        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
            var actors = await _actorRepository.GetAllAsync();

            return actors;
        }

    }
}
