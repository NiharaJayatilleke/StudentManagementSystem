using TestProject.Application.Interfaces.Services;

namespace TestProject.Interfaces.Services;

public interface IAuthService
{
    Task<string> AuthenticateAsync(string username, string password);

    Task RegisterAsync(string username, string password);
}