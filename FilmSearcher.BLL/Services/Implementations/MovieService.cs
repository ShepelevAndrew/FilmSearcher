using FilmSearcher.BLL.Models;
using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Implementations;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IBaseRepository<Movie> _movieRepository;
        private readonly IActorMovieRepository _actorMovieRepository;
        private readonly IMovieUserRepository _movieUserRepository;

        public MovieService(IBaseRepository<Movie> movieRepository, IBaseRepository<Actor> actorRepository, IActorMovieRepository actorMovieRepository, IMovieUserRepository movieUserRepository)
        {
            _movieRepository = movieRepository;
            _actorMovieRepository = actorMovieRepository;
            _movieUserRepository = movieUserRepository;
        }

        //Movie
        [Authorize(Roles = "Moderator, Admin, Owner")]
        public async Task AddAsync(Movie entity)
        {
            await _movieRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _actorMovieRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MovieDTO>> GetAllAsync(int? userId)
        {
            var movies = await _movieRepository.GetAllAsync();
            var moviesDto = new List<MovieDTO>();

            var moviesUsers = await _movieUserRepository.GetAllAsync();
            var moviesUsersInBookmark = moviesUsers.ToList().FindAll(mu => mu.UserId == userId && mu.InBookmark == true);

            foreach (var movie in movies)
            {
                MovieDTO movieDto = new()
                {
                    MovieId = movie.MovieId,
                    Name = movie.Name,
                    Category = movie.Category,
                    Description = movie.Description,
                    EndDate = movie.EndDate,
                    StartDate = movie.StartDate,
                    Score = movie.Score,
                    ImageURL = movie.ImageURL,
                    IsInBookmark = moviesUsersInBookmark.Any(mu => mu.MovieId == movie.MovieId),
                    ProducerId = movie.ProducerId,
                    CinemaId = movie.CinemaId
                };

                moviesDto.Add(movieDto);
            }

            return moviesDto;
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
            var actorsMovies = await _actorMovieRepository.GetAllAsync();
            var movieActors = _actorMovieRepository.GetByMovieId(id).ToList();

            foreach(var actor in actors) {
                var actorMovie = new ActorMovie()
                {
                    MovieId = id,
                    ActorId = actor.ActorId
                };

                if(!actorsMovies.Any(am => am.MovieId == actorMovie.MovieId && am.ActorId == actorMovie.ActorId))
                {
                    await _actorMovieRepository.AddAsync(actorMovie);
                }

                movieActors.Remove(movieActors.Find(ma => ma.ActorId == actor.ActorId));
            }

            if(movieActors != null) { 
                foreach (var deleteActors in movieActors)
                    await _actorMovieRepository.DeleteByActorIdAsync(deleteActors.ActorId);
            }
        }

        //MovieUser
        public async Task AddOrRemoveInBookmarkAsync(MovieUser movieUser)
        {
            var moviesUsers = await _movieUserRepository.GetAllAsync();
            var movieUserEquals = moviesUsers.ToList().FirstOrDefault(mu => mu.MovieId == movieUser.MovieId && mu.UserId == movieUser.UserId);

            if (movieUserEquals == null)
            {
                await _movieUserRepository.AddAsync(movieUser);
            }
            else
            {
                if (movieUserEquals.InBookmark)
                    movieUserEquals.InBookmark = false;
                else
                    movieUserEquals.InBookmark = true;

                await _movieUserRepository.UpdateAsync(movieUserEquals);
            }
        }

        public async Task AddOrUpdateScore(MovieUser movieUser)
        {
            var moviesUsers = await _movieUserRepository.GetAllAsync();
            var movieUserEquals = moviesUsers.ToList().FirstOrDefault(mu => mu.MovieId == movieUser.MovieId && mu.UserId == movieUser.UserId);

            if (movieUserEquals == null)
            {
                await _movieUserRepository.AddAsync(movieUser);
            }
            else
            {
                movieUserEquals.MovieScore = movieUser.MovieScore;
                await _movieUserRepository.UpdateAsync(movieUserEquals);
            }
        }

        public async Task<MovieUser> GetMovieUserByMovieId(int id)
        {
            var moviesUsers = await _movieUserRepository.GetAllAsync();
            var movieUser = moviesUsers.FirstOrDefault(mu => mu.MovieId == id);

            return movieUser;
        }

    }
}
