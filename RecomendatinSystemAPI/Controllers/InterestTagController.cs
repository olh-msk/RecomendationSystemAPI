using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendationSystemAPI.DTOs.InterestTags;
using RecomendationSystemAPI.Helpers;

namespace RecomendationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestTagController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public InterestTagController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterestTagDto>>> GetAll()
        {
            var tags = await _context.InterestTags.ToListAsync();
            return Ok(tags.Select(DtoMapper.ToDto));
        }
    }
}
