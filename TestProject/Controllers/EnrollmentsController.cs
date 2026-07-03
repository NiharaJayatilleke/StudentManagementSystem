using Microsoft.AspNetCore.Mvc;
using TestProject.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using TestProject.Domain.Entities;
using TestProject.DTOs.Enrollments;

[Authorize]
[ApiController]
[Route("api/enrollments")]
public class EnrollmentsController
    : ControllerBase
{
    private readonly IEnrollmentService
        _service;

    public EnrollmentsController(
        IEnrollmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _service.GetAllEnrollmentsAsync();

        return Ok(enrollments);
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById(int id)
    // {
    //     var enrollment =
    //         await _service.GetEnrollmentByIdAsync(id);

    //     if (enrollment == null)
    //         return NotFound();

    //     return Ok(enrollment);
    // }

    [HttpPost]
    public async Task<IActionResult> Assign(
        EnrollmentDto dto)
    {
        try
        {
            await _service
                .AssignStudentToSubjectAsync(
                    dto.StudentId,
                    dto.SubjectId);

            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Unassign(
        EnrollmentDto dto)
    {
        await _service.UnassignSubjectAsync(
            dto.StudentId,
            dto.SubjectId);

        return NoContent();
    }
}