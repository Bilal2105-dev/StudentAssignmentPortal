using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StudentAssignmentAPI.Services
{
    public class LecturerService : ILecturerService
    {
        private readonly ApplicationDbContext _context;
        public LecturerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LecturerDto>> GetAllAsync()
        {
            return await _context.Lecturers
                .Include(l => l.Department) // Include Department for name
                .Select(l => new LecturerDto
                {
                    Id = l.Id,
                    FullName = l.FullName,
                    Email = l.Email,
                    DepartmentId = l.DepartmentId,
                    DepartmentName = l.Department.Name  // Include department name here
                })
                .ToListAsync();
        }

        public async Task<LecturerDto?> GetByIdAsync(int id)
        {
            var lecturer = await _context.Lecturers
                .Include(l => l.Department)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lecturer == null) return null;

            return new LecturerDto
            {
                Id = lecturer.Id,
                FullName = lecturer.FullName,
                Email = lecturer.Email,
                DepartmentId = lecturer.DepartmentId,
                DepartmentName = lecturer.Department.Name
            };
        }

        public async Task<LecturerDto> SaveAsync(LecturerDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Lecturer lecturer;

            if (dto.Id.HasValue && dto.Id.Value != 0)
            {
                lecturer = await _context.Lecturers.FindAsync(dto.Id.Value);
                if (lecturer == null)
                    throw new KeyNotFoundException("Lecturer not found.");

                lecturer.FullName = dto.FullName;
                lecturer.Email = dto.Email;
                lecturer.DepartmentId = dto.DepartmentId;
            }
            else
            {
                lecturer = new Lecturer
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    DepartmentId = dto.DepartmentId
                };

                _context.Lecturers.Add(lecturer);
            }

            await _context.SaveChangesAsync();

            dto.Id = lecturer.Id;

            // Optionally fetch department name after save for return DTO
            var dept = await _context.Departments.FindAsync(dto.DepartmentId);
            dto.DepartmentName = dept?.Name;

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null) return false;

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}