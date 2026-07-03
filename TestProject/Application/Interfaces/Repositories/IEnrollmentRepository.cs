using TestProject.Domain.Entities;
namespace TestProject.Application.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task AddAsync(Enrollment enrollment);

    Task<List<Enrollment>> GetAllAsync();

    // Task<Enrollment?> GetByIdAsync(int id);

    Task SaveChangesAsync();

    //Get and Delete Enrollment
    Task<Enrollment?> GetAsync(
    int studentId,
    int subjectId);

    Task DeleteAsync(Enrollment enrollment);
}

