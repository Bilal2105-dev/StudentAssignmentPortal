using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.DTOs.Auth;
using StudentAssignmentAPI.Services.Interfaces;
using System.Security.Claims;

namespace StudentAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                await _authService.RegisterAsync(model);
                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            try
            {
                var token = await _authService.LoginAsync(model);
                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { Message = "Login failed: Invalid credentials." });

                return Ok(new { Message = "Login successful.", Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = $"Login failed: {ex.Message}" });
            }
        }

        // POST: api/Auth/admin-login
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto model)
        {
            try
            {
                var token = await _authService.LoginAsync(model);
                if (string.IsNullOrEmpty(token))
                    return Unauthorized(new { Message = "Login failed: Invalid credentials." });

                // Verify user role is Admin
                var user = await _authService.GetUserByEmailAsync(model.Email);
                if (user == null || user.Roles == null || !user.Roles.Contains("Admin"))
                    return Unauthorized(new { Message = "Access denied: Admins only." });

                return Ok(new { Message = "Admin login successful.", Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = $"Login failed: {ex.Message}" });
            }
        }

        // GET: api/Auth/me
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var userDto = await _authService.GetUserByIdAsync(userId);

            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        // PUT: api/Auth/update
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            try
            {
                var updatedUser = await _authService.UpdateUserAsync(userId, model);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Auth/delete
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);

            var deleted = await _authService.DeleteUserAsync(userId);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}