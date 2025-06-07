using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task SaveAsync();
    }
}
