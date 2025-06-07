using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Data;
using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.Helpers;

namespace RecomendationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAll()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();

            return Ok(enrollments.Select(DtoMapper.ToDto));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Grade = dto.Grade,
                Semester = dto.Semester
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
