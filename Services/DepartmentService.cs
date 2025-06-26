using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class DepartmentService : IDepartmentService
{
    private readonly ApplicationDbContext _context;

    public DepartmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        return await _context.Departments
            .Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var dept = await _context.Departments.FindAsync(id);
        return dept == null ? null : new DepartmentDto { Id = dept.Id, Name = dept.Name };
    }

    public async Task<DepartmentDto> SaveAsync(DepartmentDto dto)
    {
        Department dept;
        if (dto.Id.HasValue)
        {
            dept = await _context.Departments.FindAsync(dto.Id.Value) ?? new Department();
        }
        else
        {
            dept = new Department();
            _context.Departments.Add(dept);
        }

        dept.Name = dto.Name;
        await _context.SaveChangesAsync();

        dto.Id = dept.Id;
        return dto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var dept = await _context.Departments.FindAsync(id);
        if (dept == null) return false;
        _context.Departments.Remove(dept);
        await _context.SaveChangesAsync();
        return true;
    }
}