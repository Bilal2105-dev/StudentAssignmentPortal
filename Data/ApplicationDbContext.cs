using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAssignmentAPI.Models;

namespace StudentAssignmentAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // === DbSets ===
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Course> Courses { get; set; }

        // Add Department DbSet
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // === Allow Manual ID Insertion ===
            builder.Entity<Student>().Property(s => s.Id).ValueGeneratedNever();
            builder.Entity<Lecturer>().Property(l => l.Id).ValueGeneratedNever();
            builder.Entity<Assignment>().Property(a => a.Id).ValueGeneratedNever();
            builder.Entity<Submission>().Property(s => s.Id).ValueGeneratedNever();
            builder.Entity<Grade>().Property(g => g.Id).ValueGeneratedNever();
            builder.Entity<Course>().Property(c => c.Id).ValueGeneratedNever();
            builder.Entity<Attendance>().Property(a => a.Id).ValueGeneratedNever();
            builder.Entity<Department>().Property(d => d.Id).ValueGeneratedNever();

            // === Relationships ===

            // Lecturer → Department (many-to-one)
            builder.Entity<Lecturer>()
                .HasOne(l => l.Department)
                .WithMany(d => d.Lecturers)
                .HasForeignKey(l => l.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lecturer → Assignments
            builder.Entity<Lecturer>()
                .HasMany(l => l.Assignments)
                .WithOne(a => a.Lecturer)
                .HasForeignKey(a => a.LecturerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lecturer → Courses
            builder.Entity<Lecturer>()
                .HasMany(l => l.Courses)
                .WithOne(c => c.Lecturer)
                .HasForeignKey(c => c.LecturerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Lecturer → Grades
            builder.Entity<Lecturer>()
                .HasMany(l => l.Grades)
                .WithOne(g => g.Lecturer)
                .HasForeignKey(g => g.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Lecturer → Attendances Taken
            builder.Entity<Lecturer>()
                .HasMany(l => l.AttendancesTaken)
                .WithOne(a => a.Lecturer)
                .HasForeignKey(a => a.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student → Submissions
            builder.Entity<Student>()
                .HasMany(s => s.Submissions)
                .WithOne(sub => sub.Student)
                .HasForeignKey(sub => sub.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assignment → Submissions
            builder.Entity<Assignment>()
                .HasMany(a => a.Submissions)
                .WithOne(sub => sub.Assignment)
                .HasForeignKey(sub => sub.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Submission → Grade (1-to-1)
            builder.Entity<Submission>()
                .HasOne(sub => sub.Grade)
                .WithOne(g => g.Submission)
                .HasForeignKey<Grade>(g => g.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student → Attendances
            builder.Entity<Student>()
                .HasMany(s => s.Attendances)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}