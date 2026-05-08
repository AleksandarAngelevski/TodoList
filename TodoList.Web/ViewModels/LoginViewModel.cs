using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class LoginViewModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }


    public override string ToString()
    {
        return UserName + " " + Password;
    }
}