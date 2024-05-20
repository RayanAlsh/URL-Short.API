using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using URL_Short.Core;

namespace URL_Short.Infrastructure;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;

    }


    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddDays(365),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }


    public (string Role, string Email) VerifyToken(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jsonToken == null)
                throw new SecurityTokenException("Invalid JWT token");

            // Get all claims of specific types
            var role = jsonToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if (role == null || email == null)
                throw new SecurityTokenException("Token doesn't contain all required claims");


            return (Role: role, Email: email);
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Error verifying token", ex);
        }
    }



    public bool IsAdmin(ClaimsPrincipal claimsPrincipal)
    {
        var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;
        return role == "Admin";
    }

    public ClaimsPrincipal GetClaimsPrincipalFromToken(string jwtToken)

    {
        var (role, email) = VerifyToken(jwtToken);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Email, email),
        };

        var identity = new ClaimsIdentity(claims, "JWT");
        return new ClaimsPrincipal(identity);
    }

    public string ExtractTokenFromHttpContextAccessor(HttpContextAccessor httpContextAccessor)
    {
        string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        return token;
    }
}

