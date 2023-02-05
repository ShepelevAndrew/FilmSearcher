﻿using FilmSearcher.BLL.Services.Interfaces;
using FilmSearcher.DAL.EF;
using FilmSearcher.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmSearcher.BLL.Services.Implementations
{
    public class CinemaService : ICrudService<Cinema>
    {
        private readonly ApplicationDbContext _dbContext;

        public CinemaService(ApplicationDbContext dbContext)
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
        public async Task UpdateAsync(int id, Cinema cinema)
        {
            cinema.CinemaId = id;
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
