using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class ProducerRepository : IBaseRepository<Producer>
    {
        private readonly ApplicationDbContext _dbContext;

        public ProducerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Producer Producer)
        {
            await _dbContext.AddAsync(Producer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            var producers = await _dbContext.Producers.ToListAsync();
            return producers;
        }

        public async Task<Producer> GetByIdAsync(int id)
        {
            var producer = await _dbContext.Producers.FirstOrDefaultAsync(m => m.ProducerId == id);
            return producer;
        }

        public async Task UpdateAsync(Producer producer)
        {
            _dbContext.Producers.Update(producer);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var producer = _dbContext.Producers.FirstOrDefault(m => m.ProducerId == id);
            _dbContext.Producers.Remove(producer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
