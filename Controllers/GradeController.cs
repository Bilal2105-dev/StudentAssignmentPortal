using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Services.Interfaces;
using System.Security.Claims;

namespace StudentAssignmentAPI.Controllers
{
    [Authorize(Roles = "Lecturer")]
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        // GET: api/Grade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAll()
        {
            var grades = await _gradeService.GetAllAsync();
            return Ok(grades);
        }

        // GET: api/Grade/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetById(int id)
        {
            var grade = await _gradeService.GetByIdAsync(id);
            if (grade == null)
                return NotFound();

            return Ok(grade);
        }

        // POST: api/Grade (Create)
        [HttpPost]
        public async Task<ActionResult<GradeDto>> Create([FromBody] GradeDto dto)
        {
            if (dto == null)
                return BadRequest("Grade data is required.");

            if (dto.Score < 0 || dto.Score > 100)
                return BadRequest("Score must be between 0 and 100.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int lecturerId))
                return Unauthorized();

            dto.LecturerId = lecturerId;
            dto.Id = 0; // Ensure new grade

            try
            {
                var createdGrade = await _gradeService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdGrade.Id }, createdGrade);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Grade/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult<GradeDto>> Update(int id, [FromBody] GradeDto dto)
        {
            if (dto == null)
                return BadRequest("Grade data is required.");

            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            if (dto.Score < 0 || dto.Score > 100)
                return BadRequest("Score must be between 0 and 100.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out int lecturerId))
                return Unauthorized();

            var existingGrade = await _gradeService.GetByIdAsync(id);
            if (existingGrade == null)
                return NotFound();

            dto.LecturerId = lecturerId;

            try
            {
                var updatedGrade = await _gradeService.SaveAsync(dto);
                return Ok(updatedGrade);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Grade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGrade = await _gradeService.GetByIdAsync(id);
            if (existingGrade == null)
                return NotFound();

            var deleted = await _gradeService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Failed to delete grade.");

            return NoContent();
        }
    }
}