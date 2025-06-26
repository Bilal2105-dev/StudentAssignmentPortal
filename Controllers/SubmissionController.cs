using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Services.Interfaces;

namespace StudentAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        // GET: api/Submission
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubmissionDto>>> GetAll()
        {
            var submissions = await _submissionService.GetAllAsync();
            return Ok(submissions);
        }

        // GET: api/Submission/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SubmissionDto>> GetById(int id)
        {
            var submission = await _submissionService.GetByIdAsync(id);
            if (submission == null)
                return NotFound();

            return Ok(submission);
        }

        // POST: api/Submission (Create)
        [HttpPost]
        public async Task<ActionResult<SubmissionDto>> Create([FromBody] SubmissionDto dto)
        {
            if (dto == null)
                return BadRequest("Submission data is required.");

            dto.Id = 0; // Ensure it's treated as new

            try
            {
                var created = await _submissionService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Submission/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult<SubmissionDto>> Update(int id, [FromBody] SubmissionDto dto)
        {
            if (dto == null)
                return BadRequest("Submission data is required.");

            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var existing = await _submissionService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            try
            {
                var updated = await _submissionService.SaveAsync(dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Submission/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _submissionService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var deleted = await _submissionService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Failed to delete submission.");

            return NoContent();
        }
    }
}