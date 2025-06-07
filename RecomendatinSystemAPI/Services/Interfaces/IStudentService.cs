using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudentAsync(Student student);
        Task<Student?> GetStudentByIdAsync(int id);
    }
}
