using Domain.Dtos;
using Domain.Interfaces;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class TaskMapper
{
    private readonly ITaskService _taskService;

    public TaskMapper(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IEnumerable<TaskResponse>> GetAllByUserIdAsync(string userId)
    {
        var result = await _taskService.GetAllByUserIdAsync(userId);
        if(result==null)
            throw new NullReferenceException("All task list is null");
        return result.ToResponse();
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request,string userId)
    {
        var result = await _taskService.CreateAsync(request.ToDto(userId));
        return result.ToResponse();
    }

    public async Task<TaskResponse> GetByIdAsync(Guid id)
    {
        var result = await _taskService.GetByIdAsync(id);
        return result.ToResponse();
    }


    public async Task<TaskResponse?> UpdateTaskAsync(Guid id, EditTaskRequest model, string userId)
    {
        
        var result = await _taskService.UpdateAsync(id, model.ToDto(userId));
        return result.ToResponse();
    }
}