using TestProject.Application.Interfaces.Repositories;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;
using TestProject.DTOs.Enrollments;

namespace TestProject.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(
        IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    // public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
    // {
    //     return await _enrollmentRepository.GetByIdAsync(id);
    // }

    public async Task<List<EnrollmentResponseDto>> GetAllEnrollmentsAsync()
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        
        return enrollments.Select(e => new EnrollmentResponseDto
        {
            StudentId = e.StudentId,
            SubjectId = e.SubjectId,
            Student = e.Student != null ? new StudentEnrollmentDto
            {
                Id = e.Student.Id,
                StudentId = e.Student.StudentId,
                Name = e.Student.Name
            } : null,
            Subject = e.Subject != null ? new SubjectEnrollmentDto
            {
                Id = e.Subject.Id,
                SubjectId = e.Subject.SubjectId,
                SubjectName = e.Subject.SubjectName
            } : null
        }).ToList();
    }

    public async Task AssignStudentToSubjectAsync(
        int studentId,
        int subjectId)
    {
        var existingEnrollment =
            await _enrollmentRepository.GetAsync(
                studentId,
                subjectId);

        if (existingEnrollment is not null)
        {
            throw new InvalidOperationException(
                "Enrollment already exists.");
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            SubjectId = subjectId
        };

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
    }

    //Unassign Enrollment
    public async Task UnassignSubjectAsync(
    int studentId,
    int subjectId)
    {
        var enrollment =
            await _enrollmentRepository.GetAsync(
                studentId,
                subjectId);

        if (enrollment is null)
            throw new Exception(
                "Enrollment not found");

        await _enrollmentRepository
            .DeleteAsync(enrollment);

        await _enrollmentRepository
            .SaveChangesAsync();
    }
}