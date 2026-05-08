using Domain.Dtos;

namespace Domain.Models;

public class Task : BaseAuditableEntity<ApplicationUser>
{
    public string Name { get; set; }
    public string? Description { get; set; } = "";
    public bool Finished { get; set; }
    public string UserId { get; set; }
    public ApplicationUser? User { get; set; }
     
}