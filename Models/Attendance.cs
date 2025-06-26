using StudentAssignmentAPI.Models;

public class Attendance
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public DateTime Date { get; set; }
    public bool Present { get; set; }

    // Optional: Who recorded this attendance?
    public int? LecturerId { get; set; }
    public Lecturer? Lecturer { get; set; }
}