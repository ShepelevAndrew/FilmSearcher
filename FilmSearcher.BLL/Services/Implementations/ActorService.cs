using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementation
{
    public class ActorService : ICrudService<Actor>
    {
        private readonly ApplicationDbContext _dbContext;

        public ActorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Actor actor)
        {
            await _dbContext.AddAsync(actor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actor = _dbContext.Actors.FirstOrDefault(a => a.ActorId == id);
            _dbContext.Actors.Remove(actor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var actors = await _dbContext.Actors.ToListAsync();
            return actors;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var actor = await _dbContext.Actors.FirstOrDefaultAsync(a => a.ActorId == id);
            return actor;
        }

        public async Task UpdateAsync(int id, Actor actor)
        {
            actor.ActorId = id;
            _dbContext.Update(actor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
