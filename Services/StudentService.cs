using Microsoft.EntityFrameworkCore;
using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Interfaces;
using StudentAssignmentAPI.Models;

namespace StudentAssignmentAPI.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext _context;
    public StudentService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<StudentDto>> GetAllAsync() =>
        await _context.Students
            .Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                CourseId = s.CourseId
            })
            .ToListAsync();

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        return student == null ? null : new StudentDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            CourseId = student.CourseId
        };
    }

    public async Task<StudentDto> SaveAsync(StudentDto dto)
    {
        Student student;

        // === If ID is provided, try to find it ===
        if (dto.Id.HasValue && dto.Id.Value != 0)
        {
            student = await _context.Students.FindAsync(dto.Id.Value);
            if (student == null)
            {
                // Manual ID insertion
                student = new Student { Id = dto.Id.Value };
                _context.Students.Add(student);
            }
        }
        else
        {
            student = new Student();
            _context.Students.Add(student);
        }

        student.FullName = dto.FullName;
        student.Email = dto.Email;
        student.CourseId = dto.CourseId;

        await _context.SaveChangesAsync();

        return new StudentDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            CourseId = student.CourseId
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}