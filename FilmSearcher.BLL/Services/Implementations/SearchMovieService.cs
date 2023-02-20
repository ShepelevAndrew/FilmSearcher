using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.Domain.Enum;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class SearchMovieService : ISearchService<Movie>
    {
        private readonly IBaseRepository<Movie> _movieRepository;

        public SearchMovieService(IBaseRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<Movie>> Search(string search)
        {
            var moviesRepos = await _movieRepository.GetAllAsync();
            var movies = moviesRepos.ToList();

            var searchMovies = movies.FindAll(x => x.Name.Contains(search));

            searchMovies.AddRange(movies.FindAll(x => x.Description.Contains(search)));

            if (DateTime.TryParse(search, out DateTime date))
            {
                searchMovies.AddRange(movies.FindAll(x => x.StartDate == date));
            }
            else if (Enum.TryParse(typeof(MovieCategory), search, out object? category))
            {
                searchMovies.AddRange(movies.FindAll(x => x.Category == (MovieCategory)(int)category));
            }

            return searchMovies;
        }
    }
}
