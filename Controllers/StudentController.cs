using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Interfaces;
using StudentAssignmentAPI.Services.Interfaces;

namespace StudentAssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        // GET: api/Student/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/Student (Create)
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create([FromBody] StudentDto dto)
        {
            if (dto == null)
                return BadRequest("Student data is required.");

            dto.Id = 0; // Ensure this is a new student

            try
            {
                var created = await _studentService.SaveAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Student/{id} (Update)
        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> Update(int id, [FromBody] StudentDto dto)
        {
            if (dto == null)
                return BadRequest("Student data is required.");

            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound();

            try
            {
                var updated = await _studentService.SaveAsync(dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Student/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
                return NotFound();

            var deleted = await _studentService.DeleteAsync(id);
            if (!deleted)
                return BadRequest("Failed to delete student.");

            return NoContent();
        }
    }
}