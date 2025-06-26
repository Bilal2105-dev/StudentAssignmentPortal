// Services/AssignmentService.cs
using Microsoft.EntityFrameworkCore;
using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;

public class AssignmentService : IAssignmentService
{
    private readonly ApplicationDbContext _context;
    public AssignmentService(ApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<AssignmentDto>> GetAllAsync() =>
        await _context.Assignments.Select(a => new AssignmentDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            DueDate = a.DueDate
        }).ToListAsync();

    public async Task<AssignmentDto?> GetByIdAsync(int id)
    {
        var a = await _context.Assignments.FindAsync(id);
        return a == null ? null : new AssignmentDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            DueDate = a.DueDate
        };
    }

    public async Task<AssignmentDto> SaveAsync(AssignmentDto dto)
    {
        Assignment assignment;
        if (dto.Id.HasValue)
        {
            assignment = await _context.Assignments.FindAsync(dto.Id.Value)
                        ?? throw new KeyNotFoundException("Assignment not found");
            assignment.Title = dto.Title;
            assignment.Description = dto.Description;
            assignment.DueDate = dto.DueDate;
        }
        else
        {
            assignment = new Assignment
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate
            };
            _context.Assignments.Add(assignment);
        }

        await _context.SaveChangesAsync();
        dto.Id = assignment.Id;
        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null) return false;

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return true;
    }
}