public class SubmissionDto
{
    public int? Id { get; set; }  // Nullable Id for create or update
    public int StudentId { get; set; }  // Student submitting the assignment
    public int AssignmentId { get; set; }  // Assignment being submitted
    public DateTime SubmittedAt { get; set; }  // Timestamp of submission

    // File path or URL to the student’s submitted document (required)
    public string FilePath { get; set; } = "";
}