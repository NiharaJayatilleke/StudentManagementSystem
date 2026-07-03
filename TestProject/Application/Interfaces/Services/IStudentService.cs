using TestProject.Domain.Entities;

namespace TestProject.Application.Interfaces.Services;

public interface IStudentService
{
    Task AddStudentAsync(Student student);

    Task<Student?> GetStudentByIdAsync(int id);

    Task<bool> StudentIdExistsAsync(string studentId);

    Task<List<Student>> GetAllStudentsAsync();

    //Update and Delete students
    Task UpdateStudentAsync(Student student);

    Task DeleteStudentAsync(int id);
}