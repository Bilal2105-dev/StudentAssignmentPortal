﻿namespace StudentAssignmentAPI.DTOs.Auth
{
    public class RegisterDto
    {
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = ""; // e.g. "Student", "Lecturer"
    }
}