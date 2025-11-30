using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Services.Interfaces;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            await _studentService.CreateAsync(dto);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var success = await _studentService.UpdateAsync(dto);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
