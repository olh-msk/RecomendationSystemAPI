using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationDbContext _context;
        public RecommendationService(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Course>> GetRecommendedCoursesAsync(int studentId)
        {
            var student = await _context.Students
                .Include(student => student.Interests)
                .ThenInclude(studentInterest => studentInterest.InterestTag)
                .FirstOrDefaultAsync(student => student.Id == studentId);

            if (student == null) return Enumerable.Empty<Course>();

            var interestIds = student.Interests.Select(studentInterest => studentInterest.InterestTagId).ToList();

            var recommended = await _context.Courses
                .Include(course => course.Tags)
                .ThenInclude(courseTag => courseTag.InterestTag)
                .Where(course => course.Tags.Any(courseTag => interestIds.Contains(courseTag.InterestTagId)))
                .ToListAsync();

            return recommended;
        }
    }
}
