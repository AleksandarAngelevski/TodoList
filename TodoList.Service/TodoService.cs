using Repository.Interfaces;
using Service.Interfaces;
using Task = Domain.Models.Task;

namespace Service;

public class TodoService : ITodoService
{
   ITaskRepository _taskRepository;

   public TodoService(ITaskRepository repository)
   {
      _taskRepository = repository;
   }
   public bool CreateTodo(Task task)
   {
      _taskRepository.AddAsync(task);
      return true;
   }

   public IEnumerable<Task> ListTasks()
   {
      throw new NotImplementedException();
   }
}