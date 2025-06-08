using RecomendatinSystemAPI.Models;
using RecomendatinSystemAPI.Repositories.Interfaces;
using RecomendatinSystemAPI.Services.Interfaces;
using RecomendationSystemAPI.DTOs.Courses;

namespace RecomendatinSystemAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;

        public CourseService(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreditHours = c.CreditHours,
                Tags = c.Tags.Select(t => t.InterestTag.Name).ToList()
            });
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var c = await _courseRepo.GetByIdAsync(id);
            if (c == null) return null;

            return new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                CreditHours = c.CreditHours,
                Tags = c.Tags.Select(t => t.InterestTag.Name).ToList()
            };
        }

        public async Task AddCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                CreditHours = dto.CreditHours
            };
            await _courseRepo.AddAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepo.DeleteAsync(id);
        }
    }
}
