using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task CreateStudentAsync(Student student)
        {
            await _repo.AddAsync(student);
            await _repo.SaveAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);
    }
}
