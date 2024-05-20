namespace URL_Short.Core;

public class User
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; private set; }
    public List<URL> Shortened_URLs { get; set; }
    public User()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        Role = "User"; // Set default role to "User"

    }
}
