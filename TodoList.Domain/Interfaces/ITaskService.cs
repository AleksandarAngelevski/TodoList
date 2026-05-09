using Domain.Dtos;
using Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace Domain.Interfaces;

public interface ITaskService
{
   Task<IEnumerable<Domain.Models.Task>> GetAllByUserIdAsync(string userId);
   Task<Domain.Models.Task> CreateAsync(TaskDto dto);
   Task<Domain.Models.Task> GetByIdAsync(Guid taskId);
   Task<Domain.Models.Task> UpdateAsync(Guid taskId, TaskDto dto);

   Task<List<Domain.Models.Task>> GetAllWithoutAuditColumns();
}