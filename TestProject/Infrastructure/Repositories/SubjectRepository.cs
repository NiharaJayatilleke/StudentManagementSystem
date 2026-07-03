using Microsoft.EntityFrameworkCore;
using TestProject.Application.DTOs;
using TestProject.Application.Interfaces.Repositories;
using TestProject.Domain.Entities;
using TestProject.Infrastructure.Persistence;

namespace TestProject.Infrastructure.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly ApplicationDbContext _context;

    public SubjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Subject>> GetPagedAsync(int page, int pageSize)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 10;

        var totalCount = await _context.Subjects.CountAsync();

        var items = await _context.Subjects
            .OrderBy(s => s.SubjectName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Subject>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task AddAsync(Subject subject)
    {
        await _context.Subjects.AddAsync(subject);
    }

    public async Task<Subject?> GetByIdAsync(int id)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Subject>> GetAllAsync()
    {
        return await _context.Subjects.ToListAsync();
    }

    public async Task<bool> SubjectIdExistsAsync(string subjectId)
    {
        return await _context.Subjects
            .AnyAsync(s => s.SubjectId == subjectId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    //Update and Delete subjects
    public Task UpdateAsync(Subject subject)
    {
        _context.Subjects.Update(subject);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(Subject subject)
    {
        _context.Subjects.Remove(subject);

        return Task.CompletedTask;
    }
}