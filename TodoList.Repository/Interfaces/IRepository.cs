using Domain.Models;

namespace Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
   Task<T?> GetByIdAsync(Guid id);
   Task<IEnumerable<T>> GetAllAsync();
   System.Threading.Tasks.Task<T> AddAsync(T entity);
   System.Threading.Tasks.Task<T> UpdateAsync(T entity);
   System.Threading.Tasks.Task DeleteAsync(Guid id); 
}