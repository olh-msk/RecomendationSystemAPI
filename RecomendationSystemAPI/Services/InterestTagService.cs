using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class InterestTagService : IInterestTagService
    {
        private readonly IInterestTagRepository _repo;
        public InterestTagService(IInterestTagRepository repo) => _repo = repo;

        public async Task<IEnumerable<InterestTag>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task AddTagAsync(InterestTag tag)
        {
            await _repo.AddAsync(tag);
            await _repo.SaveAsync();
        }
    }

}
