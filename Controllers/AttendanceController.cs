using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Optional: protect endpoints
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET: api/Attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAll()
        {
            var attendances = await _attendanceService.GetAllAsync();
            return Ok(attendances);
        }

        // GET: api/Attendance/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AttendanceDto>> GetById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);
            if (attendance == null)
                return NotFound();

            return Ok(attendance);
        }

        // POST: api/Attendance
        [HttpPost]
        public async Task<ActionResult<AttendanceDto>> Create([FromBody] AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Id = 0; // Ensure new record
            try
            {
                var created = await _attendanceService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Attendance/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AttendanceDto>> Update(int id, [FromBody] AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest(new { Message = "Id in URL and body must match." });

            var existing = await _attendanceService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            try
            {
                var updated = await _attendanceService.SaveAsync(dto);
                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Attendance/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _attendanceService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var deleted = await _attendanceService.DeleteAsync(id);
            if (!deleted)
                return BadRequest(new { Message = "Failed to delete attendance." });

            return NoContent();
        }
    }
}