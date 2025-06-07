using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentInterestController : ControllerBase
    {
        private readonly IStudentInterestService _service;

        public StudentInterestController(IStudentInterestService service)
        {
            _service = service;
        }

        [HttpPost("{studentId}")]
        public async Task<IActionResult> Assign(int studentId, [FromBody] List<int> interestIds)
        {
            await _service.AssignInterestsAsync(studentId, interestIds);
            return Ok();
        }
    }
}
