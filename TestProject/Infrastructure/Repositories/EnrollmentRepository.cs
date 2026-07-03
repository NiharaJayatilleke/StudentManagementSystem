using Microsoft.EntityFrameworkCore;
using TestProject.Application.Interfaces.Repositories;
using TestProject.Domain.Entities;
using TestProject.Infrastructure.Persistence;

namespace TestProject.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly ApplicationDbContext _context;

    public EnrollmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    // public async Task<List<Enrollment>> GetAllAsync()
    // {
    //     return await _context.Enrollments.ToListAsync();
    // }

    public async Task<List<Enrollment>> GetAllAsync()
    {
        return await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    //Get and Delete Enrollment
    public async Task<Enrollment?> GetAsync(
        int studentId,
        int subjectId)
    {
        return await _context.Enrollments
            .FirstOrDefaultAsync(e =>
                e.StudentId == studentId &&
                e.SubjectId == subjectId);
    }

    public Task DeleteAsync(
        Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);

        return Task.CompletedTask;
    }
}