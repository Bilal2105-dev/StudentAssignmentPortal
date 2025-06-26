// Interfaces/IAssignmentService.cs
using StudentAssignmentAPI.DTOs;

public interface IAssignmentService
{
    Task<IEnumerable<AssignmentDto>> GetAllAsync();
    Task<AssignmentDto?> GetByIdAsync(int id);
    Task<AssignmentDto> SaveAsync(AssignmentDto dto); // Used for both Create & Update
    Task<bool> DeleteAsync(int id);
}