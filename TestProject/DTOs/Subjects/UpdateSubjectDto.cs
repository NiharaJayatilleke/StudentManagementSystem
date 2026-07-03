using System.ComponentModel.DataAnnotations;

public class UpdateSubjectDto
{
    [Required]
    public string SubjectName { get; set; } = string.Empty;
}