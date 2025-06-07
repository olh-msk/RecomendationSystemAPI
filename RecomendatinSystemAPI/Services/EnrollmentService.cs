using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;
        public EnrollmentService(IEnrollmentRepository repo) => _repo = repo;

        public async Task EnrollStudentAsync(Enrollment enrollment) =>
            await _repo.EnrollAsync(enrollment);
    }

}
