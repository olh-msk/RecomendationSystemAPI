using RecomendationSystemAPI.DTOs.Courses;

public interface IRecommendationService
{
    Task<IEnumerable<CourseDto>> GetRecommendedCoursesAsync(int studentId);
}
