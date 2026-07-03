
using System.ComponentModel.DataAnnotations;

public class CreateStudentDto
{
    [Required]
    public string StudentId { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(18, 100)]
    public int Age { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MinLength(5)]
    public string Address { get; set; } = string.Empty;
}