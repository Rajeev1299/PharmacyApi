using PharmacyApi.Entity;

namespace PharmacyApi.Repositories
{
    public interface IGeneriRepository<T> where T : class, IBaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
