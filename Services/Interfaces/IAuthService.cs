using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.DTOs.Auth;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto model);
    Task<string> LoginAsync(LoginDto model);
    Task<UserDto?> GetUserByIdAsync(int id);
    Task<UserDto?> GetUserByEmailAsync(string email);  // Added for AdminLogin check
    Task<UserDto> UpdateUserAsync(int userId, UserDto model);
    Task<bool> DeleteUserAsync(int userId);
}