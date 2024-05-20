namespace URL_Short.Web;
using System.ComponentModel.DataAnnotations;

public class LoginUserRequestDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]

    public required string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]

    public required string Password { get; set; }
}
