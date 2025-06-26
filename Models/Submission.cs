using StudentAssignmentAPI.Models;

public class Submission
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public int AssignmentId { get; set; }

    // Add these missing properties:
    public string? FilePath { get; set; }          // or FileUrl if you prefer
    public DateTime SubmittedAt { get; set; }

    // Navigation properties if you want EF Core relations
    public virtual Assignment? Assignment { get; set; }
    public virtual Student? Student { get; set; }

    // If Grade is linked to Submission
    public virtual Grade? Grade { get; set; }
}