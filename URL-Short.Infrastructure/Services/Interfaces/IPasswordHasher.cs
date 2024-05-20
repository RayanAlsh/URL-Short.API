namespace URL_Short.Infrastructure;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    public bool VerifyPassword(string enteredPassword, string storedPassword);


}
