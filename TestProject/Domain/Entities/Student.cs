// Should store student name, Id, age, DoB, Address in an internal data structure
namespace TestProject.Domain.Entities;
public class Student
{
    public int Id { get; set; }

    public string StudentId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = string.Empty;

    public ICollection<Enrollment> Enrollments { get; set; }
        = new List<Enrollment>();
}