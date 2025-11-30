using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.Services.Interfaces;
using RecomendationSystemAPI.DTOs.Courses;
using System.Security.Claims;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _courseService.GetAllCoursesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        // Teacher only: CreatedById is taken from JWT claim; ignore client supplied CreatedById
        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            // get user id from claims
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var userId))
                return Forbid();

            dto.CreatedById = userId;
            await _courseService.AddCourseAsync(dto);
            return Ok();
        }

        // Teacher only: delete course (soft delete could be implemented later)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            // optional: verify caller owns the course (can be added in service)
            await _courseService.DeleteCourseAsync(id);
            return Ok();
        }
    }
}
