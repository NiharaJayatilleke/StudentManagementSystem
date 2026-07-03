using TestProject.Application.DTOs;
using TestProject.Application.Interfaces.Repositories;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;

namespace TestProject.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<PagedResult<Subject>> GetSubjectsPagedAsync(int page, int pageSize)
    {
        return await _subjectRepository.GetPagedAsync(page, pageSize);
    }

    public async Task AddSubjectAsync(Subject subject)
    {
        var exists = await _subjectRepository.SubjectIdExistsAsync(subject.SubjectId);

        if (exists)
            throw new InvalidOperationException("Subject ID already exists.");

        await _subjectRepository.AddAsync(subject);

        await _subjectRepository.SaveChangesAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(int id)
    {
        return await _subjectRepository.GetByIdAsync(id);
    }

    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        return await _subjectRepository.GetAllAsync();
    }

    //Update and Delete subjects
    public async Task UpdateSubjectAsync(Subject subject)
    {
        await _subjectRepository.UpdateAsync(subject);

        await _subjectRepository.SaveChangesAsync();
    }

    public async Task DeleteSubjectAsync(int id)
    {
        var subject =
            await _subjectRepository.GetByIdAsync(id);

        if (subject is null)
            throw new Exception("Subject not found");

        await _subjectRepository.DeleteAsync(subject);

        await _subjectRepository.SaveChangesAsync();
    }
}
