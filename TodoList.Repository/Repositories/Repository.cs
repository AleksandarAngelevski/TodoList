using System.Collections;
using System.Linq.Expressions;
using Domain.Models;
using Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Data;

namespace Repository.Repositories;

public class BaseRepository<T> :  IRepository<T> where T : BaseEntity
{
   protected readonly ApplicationDbContext _context;
   protected readonly DbSet<T> _dbSet;

   public BaseRepository(ApplicationDbContext context)
   {
      _context = context;
      _dbSet = context.Set<T>();
   }

   public async Task<T?> GetByIdAsync(Guid id)
   {
      return await _dbSet.FindAsync(id);
   }

   public async Task<IEnumerable<T>> GetAllAsync()
   {
      return await _dbSet.ToListAsync();
   }

   public async System.Threading.Tasks.Task<T> AddAsync(T entity)
   {
      await _dbSet.AddAsync(entity);
      await _context.SaveChangesAsync();
      return entity;
   }

   public async System.Threading.Tasks.Task<T> UpdateAsync(T entity)
   {
      _dbSet.Update(entity);
      await _context.SaveChangesAsync();
      return entity;
   }

   public async System.Threading.Tasks.Task DeleteAsync(Guid id)
   {
      var entity = await GetByIdAsync(id);
      if (entity != null)
      {
         _dbSet.Remove(entity);
         await _context.SaveChangesAsync();
      }
   }


   public async Task<IEnumerable<E>> GetAllAsync<E>(Expression<Func<T, E>> selector, 
      Expression<Func<T, bool>>? predicate = null, 
      Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
   {
      IQueryable<T> query = _dbSet;
      
      
      if (include != null)
      {
         query = include(query);
      }

      if (predicate != null)
      {
         query = query.Where(predicate);
      }

      if (orderBy != null)
      {
         orderBy(query);
      }

      return await query.Select(selector).ToListAsync();
   }
}