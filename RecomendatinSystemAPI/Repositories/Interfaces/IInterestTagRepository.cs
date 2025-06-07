using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IInterestTagRepository
    {
        Task<IEnumerable<InterestTag>> GetAllAsync();
        Task AddAsync(InterestTag tag);
        Task SaveAsync();
    }
}
