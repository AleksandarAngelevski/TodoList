namespace Domain.Models;
using Microsoft.AspNetCore.Identity;
public class ApplicationUser : IdentityUser 
{
    public  string FullName { get; set; }
    public ICollection<Task> Tasks { get; set; }
}