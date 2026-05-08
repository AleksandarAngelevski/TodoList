using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;
using Repository.Interfaces;

namespace Web.Controllers;

public class HomeController : Controller
{
    private ITaskRepository _taskRepository;

    public HomeController(ITaskRepository repository)
    {
        _taskRepository = repository;
    }
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [Route("api/task")]
    public async Task<IActionResult> Task([FromBody] CreateTaskViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Unauthorized();
        }
        var task = new Domain.Models.Task()
        {
            Id = Guid.NewGuid(),
            Finished = false,
            UserId = userId,
            Name = model.Name,
            Description = model.Description
            
        };
        await _taskRepository.AddAsync(task);
        return Ok(task);
    }
}