using RecomendationSystemAPI.DTOs.Courses;

namespace RecomendatinSystemAPI.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CreateCourseDto dto);
        Task DeleteCourseAsync(int id);
    }
}
