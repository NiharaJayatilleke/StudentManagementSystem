using TestProject.Application.DTOs;
using TestProject.Domain.Entities;
namespace TestProject.Application.Interfaces.Repositories;

public interface ISubjectRepository
{
    Task AddAsync(Subject subject);

    Task<Subject?> GetByIdAsync(int id);

    Task<List<Subject>> GetAllAsync();

     Task<PagedResult<Subject>> GetPagedAsync(int page, int pageSize);

    Task<bool> SubjectIdExistsAsync(string subjectId);

    Task SaveChangesAsync();

    //Update and Delete subjects
    Task UpdateAsync(Subject subject);

    Task DeleteAsync(Subject subject);
}