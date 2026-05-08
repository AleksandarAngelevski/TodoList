using Domain.Dtos;
using Domain.Models;
using Microsoft.CodeAnalysis.Differencing;
using Npgsql;
using Web.Request;
using Web.Response;
using Task = Domain.Models.Task;

namespace Web.Extensions;

public static class TaskExtensions
{
   public static Domain.Models.Task ToTask(this CreateTaskRequest e, string userId)
   {
      return new Task()
      {
         Name = e.Name,
         Description = e.Description,
         UserId = userId
      };
   }

   public static TaskDto ToDto(this CreateTaskRequest e, string userId)
   {
      return new TaskDto(userId, e.Name, e.Description);
   }

//   public static Task ToTask(this TaskDto e)
//   {
//      return new Task(e.Name, e.UserId,e.Finished, e.Description);
//   }

   public static TaskResponse ToResponse(this Task e)
   {
      return new TaskResponse(e.Id, e.Name, e.Description,e.Finished);
   }

   public static IEnumerable<TaskResponse> ToResponse(this IEnumerable<Task> e)
   {
      return e.Select(x => x.ToResponse());
   }


   public static TaskDto ToDto(this EditTaskRequest e, string userId)
   {
      return new TaskDto(userId, e.Name, e.Description, e.Finished);
   }
}