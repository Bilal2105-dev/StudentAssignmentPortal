// DTOs/CourseDto.cs
public class CourseDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // ✅ Add this line
    public string? Description { get; set; }
    public int LecturerId { get; set; }
}