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
    [Authorize] // Secure endpoints (optional)
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        // GET: api/Assignment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignmentDto>>> GetAll()
        {
            var assignments = await _assignmentService.GetAllAsync();
            return Ok(assignments);
        }

        // GET: api/Assignment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AssignmentDto>> GetById(int id)
        {
            var assignment = await _assignmentService.GetByIdAsync(id);
            if (assignment == null)
                return NotFound();

            return Ok(assignment);
        }

        // POST: api/Assignment
        [HttpPost]
        public async Task<ActionResult<AssignmentDto>> Create([FromBody] AssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                dto.Id = 0; // Ensure new entity
                var created = await _assignmentService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Assignment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<AssignmentDto>> Update(int id, [FromBody] AssignmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest(new { Message = "Id in URL and body must match." });

            var exists = await _assignmentService.GetByIdAsync(id);
            if (exists == null)
                return NotFound();

            try
            {
                var updated = await _assignmentService.SaveAsync(dto);
                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Assignment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _assignmentService.GetByIdAsync(id);
            if (exists == null)
                return NotFound();

            var deleted = await _assignmentService.DeleteAsync(id);
            if (!deleted)
                return BadRequest(new { Message = "Failed to delete assignment." });

            return NoContent();
        }
    }
}