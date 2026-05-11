using Web.Response;

namespace Web.ViewModels;
using Domain.Dtos;
public class TaskViewModel
{
    public IEnumerable<TaskResponse> Tasks { get; set; } = [];
    public string CatFact { get; set; } = string.Empty;
}