using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetRecommendations(int studentId)
        {
            var recommendedCourses = await _recommendationService.GetRecommendedCoursesAsync(studentId);
            return Ok(recommendedCourses);
        }
    }
}
