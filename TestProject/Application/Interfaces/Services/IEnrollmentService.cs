using TestProject.Domain.Entities;
using TestProject.DTOs.Enrollments;

namespace TestProject.Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<List<EnrollmentResponseDto>> GetAllEnrollmentsAsync();

    Task AssignStudentToSubjectAsync(
        int studentId,
        int subjectId);

    //Unassign enrollment
    Task UnassignSubjectAsync(
        int studentId,
        int subjectId);

}