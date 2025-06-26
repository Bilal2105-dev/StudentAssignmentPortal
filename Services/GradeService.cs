using Microsoft.EntityFrameworkCore;
using StudentAssignmentAPI.Data;
using StudentAssignmentAPI.DTOs;
using StudentAssignmentAPI.Models;
using StudentAssignmentAPI.Services.Interfaces;

namespace StudentAssignmentAPI.Services
{
    public class GradeService : IGradeService
    {
        private readonly ApplicationDbContext _context;

        public GradeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GradeDto>> GetAllAsync()
        {
            return await _context.Grades
                .Select(g => new GradeDto
                {
                    Id = g.Id,
                    SubmissionId = g.SubmissionId,
                    LecturerId = g.LecturerId,
                    Score = g.Score,
                    Comments = g.Comments
                })
                .ToListAsync();
        }

        public async Task<GradeDto?> GetByIdAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return null;

            return new GradeDto
            {
                Id = grade.Id,
                SubmissionId = grade.SubmissionId,
                LecturerId = grade.LecturerId,
                Score = grade.Score,
                Comments = grade.Comments
            };
        }

        public async Task<GradeDto> SaveAsync(GradeDto dto)
        {
            // Check submission exists and get its assignment's lecturer
            var submission = await _context.Submissions
                .Include(s => s.Assignment)
                .FirstOrDefaultAsync(s => s.Id == dto.SubmissionId);

            if (submission == null)
                throw new Exception("Submission not found.");

            if (submission.Assignment.LecturerId != dto.LecturerId)
                throw new UnauthorizedAccessException("You are not authorized to grade this submission.");

            Grade grade;
            if (dto.Id.HasValue)
            {
                grade = await _context.Grades.FindAsync(dto.Id.Value) ?? new Grade();
                if (grade.Id == 0)
                    _context.Grades.Add(grade);
            }
            else
            {
                grade = new Grade();
                _context.Grades.Add(grade);
            }

            grade.SubmissionId = dto.SubmissionId;
            grade.LecturerId = dto.LecturerId;
            grade.Score = dto.Score;
            grade.Comments = dto.Comments;

            await _context.SaveChangesAsync();

            dto.Id = grade.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return false;

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}