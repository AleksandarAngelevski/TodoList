using System.Security.Claims;
using Web.Mapper;
using Domain.Interfaces;
using Web.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interfaces;
using Web.ViewModels;
namespace Web.Controllers;
public class TaskController : Controller
{
    private ITaskRepository _taskRepository;
    private readonly TaskMapper _taskMapper;
    private readonly ICatFactsApiClient _catFactsApiClient;
    public TaskController(ITaskRepository taskRepository, TaskMapper taskMapper, ICatFactsApiClient catFactsApiClient)
    {
        _taskRepository = taskRepository;
        _taskMapper = taskMapper;
        _catFactsApiClient = catFactsApiClient;
    }


    [HttpGet("tasks/create")]
    [Authorize]
    public IActionResult CreateTask()
    {
        return View();
    } 
    [HttpPost("Tasks/Create")]
    [ActionName("CreateTaskAsync")] 
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTaskAsync([FromForm] CreateTaskRequest request)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Select(x => x.Value.Errors)
                .Where(y => y.Count > 0)
                .SelectMany(k => k.Select(b => b.ErrorMessage))
                .ToList();
            
            errors.ForEach(Console.WriteLine); 
            
            return View("CreateTask", request);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();
       
        await _taskMapper.CreateAsync(request, userId);
        return RedirectToAction("GetTasks");
    }


    [HttpGet("tasks/all")]
    public async Task<IActionResult> GetTasks()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();
        var tasks = await _taskMapper.GetAllByUserIdAsync(userId);
        var catFact = await _catFactsApiClient.GetCatFact();
        var model = new TaskViewModel
        {
            Tasks = tasks,
            CatFact = catFact.Fact,
        };
        return View("Tasks",model);
    }


    [HttpGet("Tasks/Edit/{id}")]
    [ActionName("EditTask")]
    public async Task<IActionResult> EditTask(Guid id)
    {
        var task = await _taskMapper.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        
        return View("EditTask", task);
    }

    [HttpPost("Tasks/Edit/{id}")]
    [ActionName("EditTask")]
    public async Task<IActionResult> EditTask(Guid id, [FromForm] EditTaskRequest model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();
        if (!ModelState.IsValid)
            return View("EditTask");
        var result = await _taskMapper.UpdateTaskAsync(id, model,userId);
        if (result == null) 
        {
            return View("EditTask");
        }
        return RedirectToAction("GetTasks");
    }
}