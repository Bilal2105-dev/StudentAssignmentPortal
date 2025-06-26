public class AssignmentDto
{
    public int? Id { get; set; }  // Nullable Id for create or update
    public string Title { get; set; } = "";  // Required title of assignment
    public string? Description { get; set; }  // Optional detailed description
    public DateTime DueDate { get; set; }  // Required due date

    // Optional document file URL for assignment instructions (PDF, DOC)
    public string? DocumentFileUrl { get; set; }
}