using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Short.Infrastructure;

namespace URL_Short.Web;


[Route("api/[controller]")]
[ApiController]
[Authorize] // Add this attribute to require authorization for all actions in this controller

public class UrlController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    private readonly IJwtService jwtService;
    private readonly URLsRepository _urlsRepository;
    private readonly UsersRepository _usersRepository;

    public UrlController(ApplicationDbContext dbContext, IJwtService jwtService, URLsRepository urlsRepository, UsersRepository usersRepository)
    {
        this.dbContext = dbContext;
        this.jwtService = jwtService;
        this._urlsRepository = urlsRepository;
        this._usersRepository = usersRepository;


    }
}
