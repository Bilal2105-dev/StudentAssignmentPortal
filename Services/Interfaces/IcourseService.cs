using StudentAssignmentAPI.DTOs;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto?> GetByIdAsync(int id);
        Task<CourseDto> SaveAsync(CourseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}