using RecomendatinSystemAPI.Models;

namespace RecomendatinSystemAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task SaveAsync();
    }
}
