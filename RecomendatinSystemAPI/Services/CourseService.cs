using RecomendatinSystemAPI.Models;
using RecomendatinSystemAPI.Repositories.Interfaces;
using RecomendatinSystemAPI.Services.Interfaces;

namespace RecomendatinSystemAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repo;
        public CourseService(ICourseRepository repo) => _repo = repo;

        public async Task<IEnumerable<Course>> GetAllCoursesAsync() => await _repo.GetAllAsync();

        public async Task<Course?> GetCourseByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task AddCourseAsync(Course course)
        {
            await _repo.AddAsync(course);
            await _repo.SaveAsync();
        }
    }

}
