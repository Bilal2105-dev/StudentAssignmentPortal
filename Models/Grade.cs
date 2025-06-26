using StudentAssignmentAPI.Models;

public class Grade
{
    public int Id { get; set; }

    public int SubmissionId { get; set; }
    public Submission Submission { get; set; } = null!;

    public double Score { get; set; }
    public string? Comments { get; set; }

    // Which Lecturer awarded this grade
    public int LecturerId { get; set; }
    public Lecturer Lecturer { get; set; } = null!;
}