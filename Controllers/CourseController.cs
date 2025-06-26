using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // POST: api/Course (Create)
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create([FromBody] CourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.Id = 0; // Ensure creating new entity

            try
            {
                var created = await _courseService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Course/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> Update(int id, [FromBody] CourseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest(new { Message = "Id in URL and body must match." });

            var existing = await _courseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            try
            {
                var updated = await _courseService.SaveAsync(dto);
                return Ok(updated);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/Course/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _courseService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            var deleted = await _courseService.DeleteAsync(id);
            if (!deleted)
                return BadRequest(new { Message = "Failed to delete course." });

            return NoContent();
        }
    }
}