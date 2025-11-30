using RecomendationSystemAPI.DTOs.Enrollments;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollStudentAsync(CreateEnrollmentDto dto);
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
    }
}
