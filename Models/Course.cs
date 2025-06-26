// Models/Course.cs
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // ✅ Add this line
    public string? Description { get; set; }

    // Relationships
    public int LecturerId { get; set; }
    public Lecturer Lecturer { get; set; }
}