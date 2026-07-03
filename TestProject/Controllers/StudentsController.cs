using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;

[Authorize]
[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(
        IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _service.GetAllStudentsAsync();

        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student =
            await _service.GetStudentByIdAsync(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpGet("exists")]
    public async Task<ActionResult<bool>> Exists(
        [FromQuery] string studentId)
    {
        var exists =
            await _service
                .StudentIdExistsAsync(studentId);

        return Ok(exists);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        CreateStudentDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.AddStudentAsync(
            new Student
            {
                StudentId = dto.StudentId,
                Name = dto.Name,
                Age = dto.Age,
                DateOfBirth = dto.DateOfBirth,
                Address = dto.Address
            });

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateStudentDto dto)
    {
        var student =
            await _service.GetStudentByIdAsync(id);

        if (student == null)
            return NotFound();

        student.Name = dto.Name;
        student.Age = dto.Age;
        student.DateOfBirth =
            dto.DateOfBirth;
        student.Address = dto.Address;

        await _service.UpdateStudentAsync(
            student);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _service.DeleteStudentAsync(id);

        return NoContent();
    }
}