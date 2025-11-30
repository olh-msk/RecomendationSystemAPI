using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Data;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _db;
        public EnrollmentRepository(ApplicationDbContext db) => _db = db;

        public async Task EnrollAsync(Enrollment enrollment) => await _db.Enrollments.AddAsync(enrollment);

        public async Task SaveAsync() => await _db.SaveChangesAsync();

        public async Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync() =>
            await _db.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();

        public async Task<Enrollment?> GetByIdWithDetailsAsync(int id) =>
            await _db.Enrollments.Include(e => e.Student).Include(e => e.Course).FirstOrDefaultAsync(e => e.Id == id);

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Enrollments.FindAsync(id);
            if (e != null) _db.Enrollments.Remove(e);
        }

        public async Task<IEnumerable<Enrollment>> GetByStudentIdWithDetailsAsync(int studentId) =>
            await _db.Enrollments.Where(e => e.StudentId == studentId).Include(e => e.Course).Include(e => e.Student).ToListAsync();

        public async Task<IEnumerable<Student>> GetStudentsForCourseAsync(int courseId)
        {
            return await _db.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .Select(e => e.Student)
                .Distinct()
                .ToListAsync();
        }
    }
}
