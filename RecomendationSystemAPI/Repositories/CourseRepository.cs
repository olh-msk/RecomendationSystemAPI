using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Data;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Course>> GetAllAsync()
            => await _context.Courses.Include(course => course.Tags).ThenInclude(courseTag => courseTag.InterestTag).ToListAsync();

        public async Task<Course?> GetByIdAsync(int id)
            => await _context.Courses.Include(course => course.Tags).ThenInclude(courseTag => courseTag.InterestTag)
                .FirstOrDefaultAsync(course => course.Id == id);

        public async Task AddAsync(Course course) => await _context.Courses.AddAsync(course);

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
                _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
        }

    }
}
