// Assign student to a subject and store

namespace TestProject.Domain.Entities;
public class Enrollment
{
    public int StudentId { get; set; } // References Student.Id PK

    public Student Student { get; set; } = null!;

    public int SubjectId { get; set; } // References Subject.Id PK

    public Subject Subject { get; set; } = null!;
}
