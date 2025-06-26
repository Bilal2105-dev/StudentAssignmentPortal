using StudentAssignmentAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAssignmentAPI.Services.Interfaces
{
    public interface ISubmissionService
    {
        Task<IEnumerable<SubmissionDto>> GetAllAsync();
        Task<SubmissionDto?> GetByIdAsync(int id);
        Task<SubmissionDto> SaveAsync(SubmissionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}