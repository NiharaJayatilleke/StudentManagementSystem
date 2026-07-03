using TestProject.Domain.Entities;

namespace TestProject.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);

    Task AddAsync(User user);

    Task SaveChangesAsync();
}