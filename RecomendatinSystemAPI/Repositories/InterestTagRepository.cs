using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;

namespace RecomendationSystemAPI.Repositories
{
    public class InterestTagRepository : IInterestTagRepository
    {
        private readonly ApplicationDbContext _context;
        public InterestTagRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<InterestTag>> GetAllAsync() =>
            await _context.InterestTags.ToListAsync();

        public async Task AddAsync(InterestTag tag) =>
            await _context.InterestTags.AddAsync(tag);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
