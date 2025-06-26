namespace StudentAssignmentAPI.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public string? DocumentFileUrl { get; set; } // optional attached file

        // Optional: Foreign key to Lecturer
        public int? LecturerId { get; set; }
        public Lecturer? Lecturer { get; set; }

        public ICollection<Submission>? Submissions { get; set; }
    }
}