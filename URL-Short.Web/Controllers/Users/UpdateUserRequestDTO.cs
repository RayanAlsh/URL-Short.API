namespace URL_Short.Web;
using System.ComponentModel.DataAnnotations;

public class UpdateUserRequestDTO
{

    [Required(ErrorMessage = "The User Id is required")]
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Role { get; set; }


}

