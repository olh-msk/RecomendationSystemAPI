using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IInterestTagService
    {
        Task<IEnumerable<InterestTag>> GetAllAsync();
        Task AddTagAsync(InterestTag tag);
    }
}
