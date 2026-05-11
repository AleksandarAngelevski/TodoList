using Domain.Interfaces;
using Domain.Dtos;
using Repository.HttpClient;
using Repository.Interfaces;
using Service.Interfaces;
using Task = Domain.Models.Task;

namespace Service.Services;

public class TaskService: ITaskService
{
    private ITaskRepository _taskRepository;
    private ICatFactsApiClient _catFactsApiClient;
    public TaskService(ITaskRepository taskRepository, ICatFactsApiClient catFactsApiClient)
    {
        _taskRepository = taskRepository;
        _catFactsApiClient = catFactsApiClient;
    }



    public async Task<IEnumerable<Domain.Models.Task>> GetAllByUserIdAsync(string userId)
    {
        return await _taskRepository.GetAllByIdAsync(userId);
    }

    public async Task<Domain.Models.Task> CreateAsync(TaskDto dto)
    {
        var task = new Task()
        {
           Name = dto.Name,
           Description = dto.Description ?? "",
           Finished = false,
           UserId = dto.UserId,
           
        };
        return await _taskRepository.AddAsync(task);
    }

    public async Task<Domain.Models.Task> GetByIdAsync(Guid taskId)
    {
        return await _taskRepository.GetByIdAsync(taskId);
    }

    public async Task<Domain.Models.Task> UpdateAsync(Guid taskId, TaskDto model)
    {
        var result = await _taskRepository.GetByIdAsync(taskId);
        result.Name = model.Name;
        result.UserId = model.UserId;
        result.Description = model.Description;
        result.Finished = model.Finished;

        return await _taskRepository.UpdateAsync(result);
    }


    public async Task<List<Task>> GetAllWithoutAuditColumns()
    {
        var tasks = await _taskRepository.GetAllWithEmptyAudits();
        
        return tasks.ToList();
    }
}