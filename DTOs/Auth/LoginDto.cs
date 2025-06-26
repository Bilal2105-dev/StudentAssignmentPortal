namespace StudentAssignmentAPI.DTOs.Auth
{
    public class LoginDto
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = default!;// this is fine
    }
}
