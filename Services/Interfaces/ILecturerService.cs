using StudentAssignmentAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface ILecturerService
    {
        Task<IEnumerable<LecturerDto>> GetAllAsync();
        Task<LecturerDto?> GetByIdAsync(int id);
        Task<LecturerDto> SaveAsync(LecturerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}