using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Helpers;

namespace RecomendationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll()
        {
            var students = await _context.Students.Include(student => student.Interests).ThenInclude(studentInterest =>
            studentInterest.InterestTag).ToListAsync();

            return Ok(students.Select(DtoMapper.ToDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                GPA = dto.GPA,
                Interests = dto.InterestTagIds.Select(id => new StudentInterest { InterestTagId = id }).ToList()
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
