using TestProject.Application.Interfaces.Repositories;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;

namespace TestProject.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task AddStudentAsync(Student student)
    {
        if (await _studentRepository.StudentIdExistsAsync(student.StudentId))
        {
            throw new Exception("Student ID already exists.");
        }

        if (student.Age < 18)
        {
            throw new Exception("Student must be at least 18 years old.");
        }
        await _studentRepository.AddAsync(student);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetByIdAsync(id);
    }

    public async Task<bool> StudentIdExistsAsync(
        string studentId)
    {
        return await _studentRepository
            .StudentIdExistsAsync(studentId);
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllAsync();
    }

    //Update and Delete Students

    public async Task UpdateStudentAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student =
            await _studentRepository.GetByIdAsync(id);

        if (student is null)
            throw new Exception("Student not found");

        await _studentRepository.DeleteAsync(student);

        await _studentRepository.SaveChangesAsync();
    }
}