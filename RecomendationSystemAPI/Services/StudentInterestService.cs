using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class StudentInterestService : IStudentInterestService
    {
        private readonly IStudentInterestRepository _repo;
        public StudentInterestService(IStudentInterestRepository repo) => _repo = repo;

        public async Task AssignInterestsAsync(int studentId, List<int> interestIds) =>
            await _repo.AssignAsync(studentId, interestIds);
    }
}
