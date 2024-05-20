using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Short.Core;
using URL_Short.Infrastructure;
using URL_Short.Web.Filters;

namespace URL_Short.Web;


[Route("api/[controller]")]
[ApiController]

public class UrlController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;

    private readonly IJwtService jwtService;
    private readonly URLsRepository _urlsRepository;
    private readonly UsersRepository _usersRepository;
    private readonly UrlShortenerService _urlShortenerService;
    public UrlController(ApplicationDbContext dbContext, IJwtService jwtService, URLsRepository urlsRepository, UsersRepository usersRepository, UrlShortenerService urlShortenerService)
    {
        this.dbContext = dbContext;
        this.jwtService = jwtService;
        this._urlsRepository = urlsRepository;
        this._usersRepository = usersRepository;
        this._urlShortenerService = urlShortenerService;


    }


    [HttpGet]
    [Authorize]
    [AdminAuthorize]

    public async Task<IActionResult> GetAll()
    {
        try
        {
            var URLs = await _urlsRepository.GetAllAsync();
            return Ok(URLs);
        }
        catch (Exception e)
        {

            return StatusCode(500, "An unexpected error occurred.");
        }

    }

    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortUrlRequestDTO requestDto)
    {
        try
        {
            string Generatedshorturl = $"{Request.Scheme}://{Request.Host}/" + _urlShortenerService.GenerateShortUrl();


            URL newUrl = (new URL
            {
                Default_URL = requestDto.Default_URL,
                Short_URL = Generatedshorturl,
            });

            await _urlsRepository.AddAsync(newUrl);

            if (requestDto.UserId != Guid.Empty) // check if UserId is provided
            {
                var user = await _usersRepository.GetByIdAsync(requestDto.UserId);
                if (user != null)
                {
                    await _usersRepository.AddShortenedUrlAsync(user, newUrl.Short_URL);
                }
            }
            var responseDTO = new ShortUrlResponseDTO
            {
                Short_URL = newUrl.Short_URL,
            };

            return Ok(responseDTO);

        }
        catch (Exception e)
        {

            return StatusCode(500, "An unexpected error occurred.");
        }

    }

}
