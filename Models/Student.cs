using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace StudentAssignmentAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CourseId { get; set; } 

        // Navigation property (optional, helpful for includes)

        // Relationships
        public ICollection<Submission>? Submissions { get; set; }
        public ICollection<Attendance> Attendances { get; set; }    
    }
}