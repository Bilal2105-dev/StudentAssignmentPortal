using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StudentAssignmentAPI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public AttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            return await _context.Attendances
                .Select(a => new AttendanceDto
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    Date = a.Date,
                    Present = a.Present,
                    LecturerId = a.LecturerId
                })
                .ToListAsync();
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return null;

            return new AttendanceDto
            {
                Id = attendance.Id,
                StudentId = attendance.StudentId,
                Date = attendance.Date,
                Present = attendance.Present,
                LecturerId = attendance.LecturerId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null) return false;

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AttendanceDto> SaveAsync(AttendanceDto dto)
        {
            Attendance attendance;

            if (dto.Id.HasValue)
            {
                attendance = await _context.Attendances.FindAsync(dto.Id.Value);
                if (attendance == null) throw new Exception("Attendance record not found");

                attendance.StudentId = dto.StudentId;
                attendance.Date = dto.Date;
                attendance.Present = dto.Present;
                attendance.LecturerId = dto.LecturerId;
            }
            else
            {
                attendance = new Attendance
                {
                    StudentId = dto.StudentId,
                    Date = dto.Date,
                    Present = dto.Present,
                    LecturerId = dto.LecturerId
                };
                _context.Attendances.Add(attendance);
            }

            await _context.SaveChangesAsync();

            dto.Id = attendance.Id;
            return dto;
        }
    }
}