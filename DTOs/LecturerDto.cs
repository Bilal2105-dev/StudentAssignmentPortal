public class LecturerDto
{
    public int? Id { get; set; }  // nullable for new entries

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int DepartmentId { get; set; }  // foreign key for department

    public string? DepartmentName { get; set; } // optional, for output only
}