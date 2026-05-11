using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Service.Services;

namespace Service.Jobs;

public class AuditFieldsBackgroundService : BackgroundService
{
   private readonly IServiceScopeFactory _serviceScopeFactory;
   private readonly ILogger<TaskService> _logger;

   public AuditFieldsBackgroundService(IServiceScopeFactory serviceScopeFactory,
       ILogger<TaskService> logger)
   {
       _serviceScopeFactory = serviceScopeFactory;
       _logger = logger;
   }

   protected override async Task ExecuteAsync(CancellationToken stoppingToken)
   {
           using var scope = _serviceScopeFactory.CreateScope();
           var taskService = scope.ServiceProvider.GetRequiredService<ITaskService>();
           var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
           _logger.LogInformation("Filling empty audit fields...");

           var tasks = await taskService.GetAllWithoutAuditColumns();
           var now = DateTime.UtcNow; 
           foreach( var task in tasks)
           {
               task.CreatedById = task.UserId;
               task.LastModifiedBy = task.UserId;
               task.DateCreated = now;
               task.DateLastModified = now;
           }
           await dbContext.SaveChangesAsync(stoppingToken);
           
           _logger.LogInformation("Backfill complete");
   }
}