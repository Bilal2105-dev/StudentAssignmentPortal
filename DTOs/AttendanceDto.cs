public class AttendanceDto
{
    public int? Id { get; set; }
    public int StudentId { get; set; }
    public DateTime Date { get; set; }
    public bool Present { get; set; }

    public int? LecturerId { get; set; } // Who recorded attendance
}