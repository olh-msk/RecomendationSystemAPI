using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> GetByEmailAsync(string email);
        Task AddAsync(Student student);
        Task SaveAsync();
    }
}
