using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class CreateTaskViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public string Description { get; set; }
}