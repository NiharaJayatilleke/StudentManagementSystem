using Microsoft.AspNetCore.Mvc;
using TestProject.Application.DTOs;
using TestProject.Application.Interfaces.Services;
using TestProject.Interfaces.Services;

namespace TestProject.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController
    : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IAuthService _authService;

    public AuthController(
        IJwtService jwtService,
        IAuthService authService)
    {
        _jwtService = jwtService;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(
        LoginDto dto)
    {
        try
        {
            var token = await _authService.AuthenticateAsync(dto.Username, dto.Password);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterDto dto)
    {
        try
        {
            await _authService.RegisterAsync(dto.Username, dto.Password);
            return Ok(new { message = "User created successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}


// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using TestProject.Application.DTOs;
// using TestProject.Application.Interfaces.Services;
// using TestProject.Infrastructure.Persistence;

// namespace TestProject.Controllers;

// [ApiController]
// [Route("api/auth")]
// public class AuthController
//     : ControllerBase
// {
//     private readonly IJwtService _jwtService;
//     private readonly ApplicationDbContext _context;

//     public AuthController(
//         IJwtService jwtService,
//         ApplicationDbContext context)
//     {
//         _jwtService = jwtService;
//         _context = context;
//     }

//     [HttpPost("login")]
//     public async Task<IActionResult> LoginAsync(
//         LoginDto dto)
//     {
//         // if (
//         //     dto.Username != "admin"
//         //     ||
//         //     dto.Password != "password"
//         //    )
//         // {
//         //     return Unauthorized();
//         // }
//         var user =
//     await _context.Users
//         .FirstOrDefaultAsync(
//             u => u.Username == dto.Username);

//         if (
//             user == null
//             ||
//             !BCrypt.Net.BCrypt.Verify(
//                 dto.Password,
//                 user.PasswordHash))
//         {
//             return Unauthorized();
//         }

//         var token =
//             _jwtService
//                 .GenerateToken(
//                     dto.Username);

//         return Ok(
//             new
//             {
//                 token
//                 // message = "Login endpoint reached."
//             });

//     }

//     [HttpPost("register")]
//     public async Task<IActionResult> Register(
//         RegisterDto dto)
//     {
//         var existingUser =
//             await _context.Users
//                 .FirstOrDefaultAsync(
//                     u => u.Username == dto.Username);

//         if (existingUser != null)
//         {
//             return BadRequest(
//                 "Username already exists");
//         }

//         var user = new User
//         {
//             Username = dto.Username,

//             PasswordHash =
//                 BCrypt.Net.BCrypt.HashPassword(
//                     dto.Password)
//         };

//         _context.Users.Add(user);

//         await _context.SaveChangesAsync();

//         return Ok(
//             new
//             {
//                 message = "User created successfully"
//             });
//     }

// }
