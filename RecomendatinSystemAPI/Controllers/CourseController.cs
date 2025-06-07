using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Courses;
using RecomendationSystemAPI.Helpers;

namespace RecomendatinSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _context.Courses.Include(c => c.Tags).ThenInclude(t => t.InterestTag).ToListAsync();
            return Ok(courses.Select(DtoMapper.ToDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                CreditHours = dto.CreditHours,
                Tags = dto.InterestTagIds.Select(id => new CourseTag { InterestTagId = id }).ToList()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
