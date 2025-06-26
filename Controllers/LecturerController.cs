using Microsoft.AspNetCore.Mvc;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Services.Interfaces;

namespace StudentAssignmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly ILecturerService _lecturerService;

        public LecturerController(ILecturerService lecturerService)
        {
            _lecturerService = lecturerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lecturers = await _lecturerService.GetAllAsync();
            return Ok(lecturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lecturer = await _lecturerService.GetByIdAsync(id);
            if (lecturer == null) return NotFound();
            return Ok(lecturer);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] LecturerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var saved = await _lecturerService.SaveAsync(dto);
            return Ok(saved);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _lecturerService.DeleteAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}