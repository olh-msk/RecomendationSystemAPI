using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
        {
            await _service.EnrollStudentAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
        {
            var enrollments = await _service.GetAllEnrollmentsAsync();
            return Ok(enrollments);
        }
    }
}
