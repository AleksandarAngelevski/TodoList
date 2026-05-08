using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Repository.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){ }
    public DbSet<Domain.Models.Task> Tasks { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers{ get; set; }
    
}