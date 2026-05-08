using Domain.Models;

namespace Repository.Interfaces;

public interface ITaskRepository : IRepository<Domain.Models.Task>
{ 
    Task<IEnumerable<Domain.Models.Task>> GetAllByIdAsync(string userId);

}