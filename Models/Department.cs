public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Correct navigation property type for related lecturers
    public ICollection<Lecturer> Lecturers { get; set; } = new List<Lecturer>();
}