using Microsoft.EntityFrameworkCore;
using TestProject.Application.Interfaces.Repositories;
using TestProject.Domain.Entities;
using TestProject.Infrastructure.Persistence;

namespace TestProject.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    //Circular Reference
    // public async Task<Student?> GetByIdAsync(int id)
    // {
    //     return await _context.Students
    //         .AsNoTracking()
    //         .Include(s => s.Enrollments)
    //         .ThenInclude(e => e.Subject)
    //         .FirstOrDefaultAsync(s => s.Id == id);
    // }
    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<bool> StudentIdExistsAsync(
    string studentId)
    {
        studentId = studentId.Trim();
        return await _context.Students
            .AnyAsync(s =>
                s.StudentId == studentId);
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    //Update and Delete students
    public Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);

        return Task.CompletedTask;
    }
}