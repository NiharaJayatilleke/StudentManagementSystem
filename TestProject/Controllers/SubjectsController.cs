using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProject.Application.Interfaces.Services;
using TestProject.Domain.Entities;

[Authorize]
[ApiController]
[Route("api/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectService _service;

    public SubjectsController(
        ISubjectService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var subjects = await _service.GetAllSubjectsAsync();

        return Ok(subjects);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetAllPaged(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetSubjectsPagedAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subject =
            await _service.GetSubjectByIdAsync(id);

        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        CreateSubjectDto dto)
    {
        try
        {
            await _service.AddSubjectAsync(
                new Subject
                {
                    SubjectId = dto.SubjectId,
                    SubjectName = dto.SubjectName
                });

            return Created();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateSubjectDto dto)
    {
        var subject =
            await _service.GetSubjectByIdAsync(id);

        if (subject == null)
            return NotFound();

        subject.SubjectName = dto.SubjectName;

        await _service.UpdateSubjectAsync(
            subject);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _service.DeleteSubjectAsync(id);

        return NoContent();
    }
}