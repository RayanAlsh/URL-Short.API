using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using URL_Short.Infrastructure;
namespace URL_Short.Web.Filters
{
    public class AdminAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtService>();
            var token = jwtService.ExtractTokenFromHttpContextAccessor(new HttpContextAccessor { HttpContext = context.HttpContext });
            var claims = jwtService.GetClaimsPrincipalFromToken(token);

            if (!jwtService.IsAdmin(claims))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
