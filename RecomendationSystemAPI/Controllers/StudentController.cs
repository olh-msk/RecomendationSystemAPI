using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Services.Interfaces;
using System.Security.Claims;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<StudentDto>> GetMe()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var id)) return Forbid();
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateMe([FromBody] UpdateStudentDto dto)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var id)) return Forbid();
            if (id != dto.Id) return BadRequest();

            var success = await _studentService.UpdateAsync(dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // admin/teacher endpoints could be added below to view students of teacher's courses
    }
}
