using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepository(ApplicationDbContext context) => _context = context;

        public async Task EnrollAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }
    }

}
