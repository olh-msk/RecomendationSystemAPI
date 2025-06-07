using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendationSystemAPI.DTOs.Courses;
using RecomendationSystemAPI.Helpers;

namespace RecomendationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecommendationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRecommendations(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.Interests)
                .ThenInclude(i => i.InterestTag)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
                return NotFound();

            var interestTagIds = student.Interests.Select(i => i.InterestTagId).ToList();

            var recommendedCourses = await _context.Courses
                .Include(c => c.Tags)
                .ThenInclude(t => t.InterestTag)
                .Where(c => c.Tags.Any(t => interestTagIds.Contains(t.InterestTagId)))
                .ToListAsync();

            return Ok(recommendedCourses.Select(DtoMapper.ToDto));
        }
    }
}
