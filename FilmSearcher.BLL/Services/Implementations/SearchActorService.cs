using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class SearchActorService : ISearchService<Actor>
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchActorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Actor>> Search(string searchString)
        {
            var searchItems = _dbContext.Actors.ToList().FindAll(x => x.FullName.ToLower().Contains(searchString.ToLower()));

            return searchItems;
        }
    }
}
