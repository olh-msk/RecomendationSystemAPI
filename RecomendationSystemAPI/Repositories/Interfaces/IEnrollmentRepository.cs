using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task EnrollAsync(Enrollment enrollment);
        Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync();
    }
}
