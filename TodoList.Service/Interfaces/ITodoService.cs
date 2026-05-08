using Task = Domain.Models.Task;

namespace Service.Interfaces;

public interface ITodoService
{
   bool CreateTodo(Domain.Models.Task task);
   IEnumerable<Task> ListTasks();
}