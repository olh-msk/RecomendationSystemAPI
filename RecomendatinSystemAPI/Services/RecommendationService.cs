using RecomendatinSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.DTOs.Courses;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;

        public RecommendationService(IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        public async Task<IEnumerable<CourseDto>> GetRecommendedCoursesAsync(int studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student == null) return Enumerable.Empty<CourseDto>();

            var interestIds = student.Interests.Select(i => i.InterestTagId).ToList();

            var allCourses = await _courseRepo.GetAllAsync();
            var filtered = allCourses
                .Where(course => course.Tags.Any(tag => interestIds.Contains(tag.InterestTagId)));

            return filtered.Select(DtoMapper.ToDto);
        }
    }
}
