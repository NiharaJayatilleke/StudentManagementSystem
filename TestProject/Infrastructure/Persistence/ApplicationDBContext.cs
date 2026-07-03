using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Entities;

namespace TestProject.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    public DbSet<Subject> Subjects => Set<Subject>();

    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.StudentId)
            .IsUnique();

        modelBuilder.Entity<Subject>()
            .HasIndex(s => s.SubjectId)
            .IsUnique();

        modelBuilder.Entity<Enrollment>()
            .HasKey(e => new
            {
                e.StudentId,
                e.SubjectId
            });

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Subject)
            .WithMany(s => s.Enrollments)
            .HasForeignKey(e => e.SubjectId);
    }

    public DbSet<User> Users { get; set; }
}