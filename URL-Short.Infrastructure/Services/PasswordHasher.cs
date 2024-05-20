using System.Security.Cryptography;
using System.Text;

namespace URL_Short.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    public bool VerifyPassword(string enteredPassword, string storedPassword)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedEnteredPassword = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
            return hashedEnteredPassword == storedPassword;
        }
    }
}
