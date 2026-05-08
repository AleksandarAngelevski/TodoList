using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class EditTaskViewModel
{
   [Required]
   public string Name { get; set; }
   public string Description { get; set; }
   [Required]
   public bool Finished { get; set; }
}