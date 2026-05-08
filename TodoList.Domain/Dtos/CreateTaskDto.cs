using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Dtos;

public class CreateTaskDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public string UserId { get; set; }
    public CreateTaskDto(string name, string userId, string? description = null)
    {
        Name = name;
        Description = description;
        UserId = userId;
    }
}