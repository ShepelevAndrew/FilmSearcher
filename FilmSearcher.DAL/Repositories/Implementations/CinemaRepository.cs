using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using FilmSearcher.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class CinemaRepository : IBaseRepository<Cinema>
    {
        private readonly ApplicationDbContext _dbContext;

        public CinemaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Cinema cinema)
        {
            await _dbContext.AddAsync(cinema);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cinema>> GetAllAsync()
        {
            var cinemas = await _dbContext.Cinemas.ToListAsync();
            return cinemas;
        }

        public async Task<Cinema> GetByIdAsync(int id)
        {
            var cinema = await _dbContext.Cinemas.FirstOrDefaultAsync(c => c.CinemaId == id);
            return cinema;
        }
        public async Task UpdateAsync(Cinema cinema)
        {
            _dbContext.Update(cinema);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cinema = _dbContext.Cinemas.FirstOrDefault(c => c.CinemaId == id);
            _dbContext.Cinemas.Remove(cinema);
            await _dbContext.SaveChangesAsync();
        }
    }
}
