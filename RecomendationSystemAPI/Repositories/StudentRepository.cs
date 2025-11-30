using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Data;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context) => _context = context;

        public async Task<Student?> GetByIdAsync(int id) =>
            await _context.Students.Include(student => student.Interests).ThenInclude(studentInterest => studentInterest.InterestTag)
                                   .Include(student => student.Enrollments)
                                   .FirstOrDefaultAsync(student => student.Id == id);

        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Interests)
                .ThenInclude(si => si.InterestTag)
                .ToListAsync();
        }
    }
}
