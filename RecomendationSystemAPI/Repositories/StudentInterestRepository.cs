using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class StudentInterestRepository : IStudentInterestRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentInterestRepository(ApplicationDbContext context) => _context = context;

        public async Task AssignAsync(int studentId, List<int> interestIds)
        {
            var existing = _context.StudentInterests.Where(studentInterest => studentInterest.StudentId == studentId);
            _context.StudentInterests.RemoveRange(existing);

            var newLinks = interestIds.Select(id => new StudentInterest
            {
                StudentId = studentId,
                InterestTagId = id
            });

            await _context.StudentInterests.AddRangeAsync(newLinks);
            await _context.SaveChangesAsync();
        }
    }
}
