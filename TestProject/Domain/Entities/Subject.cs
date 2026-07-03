// Should store subject name, subject Id in an internal data structure 
namespace TestProject.Domain.Entities;

public class Subject
{
    public int Id { get; set; }

    public string SubjectId { get; set; } = string.Empty;

    public string SubjectName { get; set; } = string.Empty;

    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}