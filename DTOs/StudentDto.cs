namespace StudentAssignmentAPI.DTOs;
public class StudentDto
{
    public int? Id  { get; set; }  // Nullable allows you to ski[p or input manually
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public int CourseId { get; set; }   
}