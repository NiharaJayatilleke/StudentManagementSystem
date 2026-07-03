using System.ComponentModel.DataAnnotations;

namespace TestProject.DTOs.Enrollments;

public class EnrollmentDto
{
    // public int StudentId { get; set; }
    // public int SubjectId { get; set; }

    [Required(ErrorMessage = "Student is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Select a valid student.")]
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Select a valid subject.")]
    public int SubjectId { get; set; }
}

public class EnrollmentResponseDto
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public StudentEnrollmentDto? Student { get; set; }

    public SubjectEnrollmentDto? Subject { get; set; }
}

public class StudentEnrollmentDto
{
    public int Id { get; set; }

    public string StudentId { get; set; } = "";

    public string Name { get; set; } = "";
}

public class SubjectEnrollmentDto
{
    public int Id { get; set; }

    public string SubjectId { get; set; } = "";

    public string SubjectName { get; set; } = "";
}