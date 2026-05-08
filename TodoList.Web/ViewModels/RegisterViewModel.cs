using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class RegisterViewModel
{
    [Required]
    public required string UserName { get; set; }
    
    [Required]
    public required string FullName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage = "Passwords do not match.")]
    public required string ConfirmPassword { get; set; }
    public override string ToString()
    {
        return $"{UserName} {Password}";
    }
}