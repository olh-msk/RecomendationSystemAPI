using RecomendationSystemAPI.DTOs.Courses;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
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
            return courses.Select(c => DtoMapper.ToDto(c));
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var c = await _courseRepo.GetByIdAsync(id);
            if (c == null) return null;

            return DtoMapper.ToDto(c);
        }

        public async Task AddCourseAsync(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                CreditHours = dto.CreditHours,
                Tags = dto.InterestTagIds.Select(id => new CourseTag { InterestTagId = id }).ToList(),
                CreatedById = dto.CreatedById
            };

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveAsync();
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepo.DeleteAsync(id);
            await _courseRepo.SaveAsync();
        }
    }
}
