public class GradeDto
{
    public int? Id { get; set; }
    public int SubmissionId { get; set; }
    public int StudentId { get; set; }  // From Submission.StudentId
    public int AssignmentId { get; set; }  // From Submission.AssignmentId
    public double Score { get; set; }
    public string? Comments { get; set; }
    public int LecturerId { get; set; }
}