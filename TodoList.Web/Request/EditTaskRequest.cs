using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public class EditTaskRequest
{
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool Finished { get; set; }
}