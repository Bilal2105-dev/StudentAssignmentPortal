using Microsoft.EntityFrameworkCore;
using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;

namespace StudentAssignmentAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            return await _context.Courses
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Code = c.Code,
                    LecturerId = c.LecturerId
                })
                .ToListAsync();
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Code = course.Code,
                LecturerId = course.LecturerId
            };
        }

        public async Task<CourseDto> SaveAsync(CourseDto dto)
        {
            Course course;

            // If ID is provided and exists, update; otherwise create
            if (dto.Id != 0 && await _context.Courses.AnyAsync(c => c.Id == dto.Id))
            {
                course = await _context.Courses.FindAsync(dto.Id)
                         ?? throw new Exception("Course not found");

                course.Name = dto.Name;
                course.Code = dto.Code;
                course.LecturerId = dto.LecturerId;
            }
            else
            {
                course = new Course
                {
                    Id = dto.LecturerId, // Allow manual ID input
                    Name = dto.Name,
                    Code = dto.Code,
                    LecturerId = dto.LecturerId
                };
                _context.Courses.Add(course);
            }

            await _context.SaveChangesAsync();

            dto.Id = course.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}