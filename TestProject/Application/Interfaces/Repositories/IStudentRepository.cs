using TestProject.Domain.Entities;
namespace TestProject.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task AddAsync(Student student);

    Task<Student?> GetByIdAsync(int id);

    Task<bool> StudentIdExistsAsync(string studentId);

    Task<List<Student>> GetAllAsync();

    Task SaveChangesAsync();

    //Add and Delete students
    Task UpdateAsync(Student student);

    Task DeleteAsync(Student student);

}

