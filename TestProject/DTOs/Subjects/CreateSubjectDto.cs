using System.ComponentModel.DataAnnotations;

public class CreateSubjectDto
{
    [Required]
    public string SubjectId { get; set; } = string.Empty;

    [Required]
    public string SubjectName { get; set; } = string.Empty;
}