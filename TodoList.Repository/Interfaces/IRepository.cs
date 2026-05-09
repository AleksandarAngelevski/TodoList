using System.Linq.Expressions;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
   Task<T?> GetByIdAsync(Guid id);
   Task<IEnumerable<T>> GetAllAsync();
   System.Threading.Tasks.Task<T> AddAsync(T entity);
   System.Threading.Tasks.Task<T> UpdateAsync(T entity);
   System.Threading.Tasks.Task DeleteAsync(Guid id);

   Task<IEnumerable<E>> GetAllAsync<E>(Expression<Func<T, E>> selector,
       Expression<Func<T, bool>>? predicate = null,
       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include =null);
   
}