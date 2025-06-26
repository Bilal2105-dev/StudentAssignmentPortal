using StudentAssignmentAPI.DTOs;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDto>> GetAllAsync();
        Task<GradeDto?> GetByIdAsync(int id);
        Task<GradeDto> SaveAsync(GradeDto dto); // Unified create/update
        Task<bool> DeleteAsync(int id);
    }
}