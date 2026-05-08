using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public class CreateTaskRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    public string? Description { get; set; }
    
}
