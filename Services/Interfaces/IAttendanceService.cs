using StudentAssignmentAPI.DTOs;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();
        Task<AttendanceDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<AttendanceDto> SaveAsync(AttendanceDto dto); // Create or Update
    }
}