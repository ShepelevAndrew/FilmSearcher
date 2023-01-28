using FilmSearcher.Data.Services.Interfaces;
using FilmSearcher.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.Data.Services.Implementation
{
    public class ActorService : IActorService
    {
        private readonly AppDbContext _dbContext;

        public ActorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Actor actor)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
            var actors = await _dbContext.Actors.ToListAsync();
            return actors;
        }

        public Actor GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Actor Update(int id, Actor actor)
        {
            throw new NotImplementedException();
        }
    }
}
