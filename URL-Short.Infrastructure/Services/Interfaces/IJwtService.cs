using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using URL_Short.Core;

namespace URL_Short.Infrastructure;

public interface IJwtService
{
    ClaimsPrincipal GetClaimsPrincipalFromToken(string jwtToken);
    bool IsAdmin(ClaimsPrincipal claimsPrincipal);
    string ExtractTokenFromHttpContextAccessor(HttpContextAccessor httpContextAccessor);
    string GenerateToken(User user); // Declare GenerateToken method in the interface

}
