using Microsoft.AspNetCore.Mvc;
using URL_Short.Core;
using URL_Short.Infrastructure;
using URL_Short.Web.Filters;

namespace URL_Short.Web;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext dbContext;
    private readonly IJwtService jwtService;
    private readonly IConfiguration configuration;
    private readonly IPasswordHasher passwordHasher;
    private readonly UsersRepository _usersRepository;


    public UsersController(ApplicationDbContext dbContext, IJwtService jwtService, IPasswordHasher _passwordHasher, UsersRepository usersRepository)
    {
        this.dbContext = dbContext;
        this.jwtService = jwtService;
        passwordHasher = _passwordHasher;
        _usersRepository = usersRepository;

    }

    [HttpGet]
    [AdminAuthorize]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _usersRepository.GetAllAsync();
        return Ok(users);
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            // Debug: inspect ModelState errors
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            // Return bad request with ModelState errors for debugging
            return BadRequest(errors);
        }
        var existingUser = await _usersRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            // User with the same email already exists
            return Conflict(new { message = "User with this email already exists." });
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);



        var user = await _usersRepository.AddAsync(new User
        {

            Email = request.Email,
            Password = hashedPassword,
        });
        var responseDto = new RegisterUserResponseDTO
        {
            Id = user.Id,
            Email = user.Email,
            CreatedAt = DateOnly.FromDateTime(user.CreatedAt)
        };
        return Ok(responseDto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDTO request)
    {
        // Find user by email

        var user = await _usersRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });

        }

        if (!passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Unauthorized(new { message = "Invalid Email or Password" });
        }


        var token = jwtService.GenerateToken(user);
        var responseDto = new LoginUserResponseDTO
        {
            Token = token
        };
        return Ok(responseDto);
    }

    [HttpDelete("{userid}")]
    [AdminAuthorize]

    public async Task<IActionResult> DeleteUser(Guid userId)
    {


        var user = await _usersRepository.GetByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        await _usersRepository.DeleteAsync(user.Id);

        return Ok(); // Successful deletion
    }
    [HttpPut]
    [AdminAuthorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _usersRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Password))
        {
            var hashedPassword = passwordHasher.HashPassword(request.Password);

            user.Password = hashedPassword;
        }

        if (!string.IsNullOrEmpty(request.Role))
        {
            user.Role = request.Role;
        }


        await _usersRepository.UpdateAsync(user);

        var responseDto = new UpdateUserResponseDTO
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
        };
        return Ok(responseDto);



    }

}

