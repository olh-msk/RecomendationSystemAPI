using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IRecommendationService
    {
        Task<IEnumerable<Course>> GetRecommendedCoursesAsync(int studentId);
    }
}
