namespace URL_Short.Web;

public class RegisterUserResponseDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateOnly CreatedAt { get; set; }

}
