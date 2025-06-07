using Microsoft.AspNetCore.Mvc;
using RecomendatinSystemAPI.Models;
using RecomendatinSystemAPI.Services.Interfaces;

namespace RecomendatinSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;
        public CoursesController(ICourseService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _service.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Course course)
        {
            await _service.AddCourseAsync(course);
            return Ok();
        }
    }

}
