namespace StudentAssignmentAPI.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        // Optional: include roles if you want to return them
        public IList<string>? Roles { get; set; }
    }
}