using TestProject.Application.DTOs;
using TestProject.Domain.Entities;

namespace TestProject.Application.Interfaces.Services;

public interface ISubjectService
{
    Task AddSubjectAsync(Subject subject);

    Task<Subject?> GetSubjectByIdAsync(int id);

    Task<List<Subject>> GetAllSubjectsAsync();

     Task<PagedResult<Subject>> GetSubjectsPagedAsync(int page, int pageSize);

    //Update and Delete subjects
    Task UpdateSubjectAsync(Subject subject);

    Task DeleteSubjectAsync(int id);
}