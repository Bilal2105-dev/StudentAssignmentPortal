using StudentAssignmentAPI.DTOs;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<DepartmentDto> SaveAsync(DepartmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}