using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Data;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db) => _db = db;

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _db.Courses
                .Include(c => c.Tags!)
                    .ThenInclude(ct => ct.InterestTag)
                .Include(c => c.Enrollments!)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _db.Courses
                .Include(c => c.Tags!)
                    .ThenInclude(ct => ct.InterestTag)
                .Include(c => c.Enrollments!)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            await _db.Courses.AddAsync(course);
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _db.Courses.FindAsync(id);
            if (c != null) _db.Courses.Remove(c);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
