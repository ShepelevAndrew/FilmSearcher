using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class SearchMovieService : ISearchService<Movie>
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchMovieService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> Search(string searchString)
        {
            var searchItems = _dbContext.Movies.ToList().FindAll(x => x.Name.Contains(searchString));

            return searchItems;
        }
    }
}
