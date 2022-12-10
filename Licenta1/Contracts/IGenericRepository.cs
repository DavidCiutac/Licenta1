using Licenta1.Data;
using Licenta1.Models;

namespace Licenta1.Contracts
{
    public interface IGenericRepository<T> where T :class
    {
        ApplicationDbContext getContext();
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> MultipleAddAsync(List<T> entities);
        Task<T> AddAsync(T entity);
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
  
    }
}
