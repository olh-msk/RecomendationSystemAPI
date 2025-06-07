using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task EnrollAsync(Enrollment enrollment);
    }
}
