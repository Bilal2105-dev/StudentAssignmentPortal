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
    public class SubmissionService : ISubmissionService
    {
        private readonly ApplicationDbContext _context;
        public SubmissionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubmissionDto>> GetAllAsync()
        {
            return await _context.Submissions
                .Select(s => new SubmissionDto
                {
                    Id = s.Id,
                    StudentId = s.StudentId,
                    AssignmentId = s.AssignmentId,
                    SubmittedAt = s.SubmittedAt,
                    FilePath = s.FilePath
                })
                .ToListAsync();
        }

        public async Task<SubmissionDto?> GetByIdAsync(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return null;

            return new SubmissionDto
            {
                Id = submission.Id,
                StudentId = submission.StudentId,
                AssignmentId = submission.AssignmentId,
                SubmittedAt = submission.SubmittedAt,
                FilePath = submission.FilePath
            };
        }

        public async Task<SubmissionDto> SaveAsync(SubmissionDto dto)
        {
            if (dto.Id == null || dto.Id == 0)
            {
                var newSubmission = new Submission
                {
                    StudentId = dto.StudentId,
                    AssignmentId = dto.AssignmentId,
                    SubmittedAt = dto.SubmittedAt,
                    FilePath = dto.FilePath
                };
                _context.Submissions.Add(newSubmission);
                await _context.SaveChangesAsync();

                dto.Id = newSubmission.Id;
            }
            else
            {
                var existing = await _context.Submissions.FindAsync(dto.Id.Value);
                if (existing == null)
                    throw new KeyNotFoundException("Submission not found.");

                existing.StudentId = dto.StudentId;
                existing.AssignmentId = dto.AssignmentId;
                existing.SubmittedAt = dto.SubmittedAt;
                existing.FilePath = dto.FilePath;

                await _context.SaveChangesAsync();
            }
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return false;

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}