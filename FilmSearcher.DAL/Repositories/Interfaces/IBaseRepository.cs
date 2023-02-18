namespace FilmSearcher.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
