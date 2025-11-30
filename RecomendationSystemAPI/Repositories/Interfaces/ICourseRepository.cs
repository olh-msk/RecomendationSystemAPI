using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
