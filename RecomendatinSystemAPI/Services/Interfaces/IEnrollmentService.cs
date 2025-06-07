using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollStudentAsync(Enrollment enrollment);
    }
}
