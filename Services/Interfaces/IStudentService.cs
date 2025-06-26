using StudentAssignmentAPI.DTOs;

namespace StudentAssignmentAPI.Interfaces;
public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllAsync();
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> SaveAsync(StudentDto dto); // Handles both Create and Update
    Task<bool> DeleteAsync(int id);
}