using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using URL_Short.Core;
using URL_Short.Infrastructure;
using URL_Short.Web.Filters;

namespace URL_Short.Web;


[Route("/")]
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


    [HttpGet("api/Url")] // Route for GetAll action
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

    [HttpPost("api/Url")] // Route for ShortenUrl action
    public async Task<IActionResult> ShortenUrl([FromBody] ShortUrlRequestDTO requestDto)
    {
        try
        {
            string Generatedshorturl;
            bool urlExists;

            do
            {
                Generatedshorturl = _urlShortenerService.GenerateShortUrl();
                urlExists = await _urlsRepository.ExistsAsync(Generatedshorturl);
            } while (urlExists);


            string fullShortUrl = Url.Action(
                       action: nameof(RedirectToUrl),
                       controller: "Url",
                       values: new { shortUrl = Generatedshorturl },
                       protocol: Request.Scheme
                   );
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
                    await _usersRepository.AddShortenedUrlAsync(user, fullShortUrl);
                }
            }
            var responseDTO = new ShortUrlResponseDTO
            {
                Short_URL = fullShortUrl,
            };

            return Ok(responseDTO);

        }
        catch (Exception e)
        {

            return StatusCode(500, "An unexpected error occurred.");
        }

    }

    [HttpGet("{shortUrl}")] // Route for accessing short URLs directly
    public async Task<IActionResult> RedirectToUrl(string shortUrl)
    {
        try
        {
            var urls = await _urlsRepository.GetAsync(u => u.Short_URL.ToLower() == shortUrl.ToLower());
            var urlEntity = urls.FirstOrDefault();

            if (urlEntity == null)
            {
                return NotFound();
            }

            return Redirect(urlEntity.Default_URL);
        }
        catch (Exception)
        {
            return StatusCode(500, "An unexpected error occurred.");
        }
    }




}
