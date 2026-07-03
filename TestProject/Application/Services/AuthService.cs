using TestProject.Application.Interfaces.Repositories;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;
using TestProject.Interfaces.Services;

namespace TestProject.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var passwordMatches = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        var isLegacyPlainTextPassword = string.Equals(password, user.PasswordHash, StringComparison.Ordinal);

        if (!passwordMatches && !isLegacyPlainTextPassword)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return _jwtService.GenerateToken(user.Username);
    }

    public async Task RegisterAsync(string username, string password)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(username);
        if (existingUser is not null)
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        await _userRepository.AddAsync(user);
    }
}


