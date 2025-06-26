using StudentAssignmentAPI.Models;

public class Lecturer
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    // Fix: Mark navigation property nullable or use 'required' if you will always assign it
    public Department? Department { get; set; }

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    public ICollection<Attendance> AttendancesTaken { get; set; } = new List<Attendance>();
}