using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Data;
namespace Repository.Repositories;

public class TaskRepository : BaseRepository<Domain.Models.Task> , ITaskRepository
{
    
   public TaskRepository(ApplicationDbContext context) : base(context)
   { 
   }

   public   async Task<IEnumerable<Domain.Models.Task>> GetAllByIdAsync(string userId)
   {
       return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
   }

   public async Task<IEnumerable<Domain.Models.Task>> GetAllWithEmptyAudits()
   {
       return await GetAllAsync<Domain.Models.Task>(selector: x => x, predicate: x => x.CreatedById == null || x.DateCreated == null);
   }
}