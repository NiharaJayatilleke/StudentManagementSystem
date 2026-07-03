namespace TestProject.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(string username);
}