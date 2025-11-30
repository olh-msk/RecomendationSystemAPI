using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.Services.Interfaces;
using System.Security.Claims;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;

        public EnrollmentsController(IEnrollmentService enrollmentService, ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
        }

        // Student enrolls to a course — student id is taken from JWT
        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Enroll([FromBody] CreateEnrollmentDto dto)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var userId)) return Forbid();

            // ensure student only enrolls self
            dto.StudentId = userId;
            await _enrollmentService.EnrollStudentAsync(dto);
            return Ok();
        }

        // Student or Teacher can unenroll; teacher may remove any student from their course
        [HttpDelete("{enrollmentId}")]
        [Authorize]
        public async Task<IActionResult> Unenroll(int enrollmentId)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var currentUserId)) return Forbid();

            var role = User.FindFirst(ClaimTypes.Role)?.Value ?? "Student";
            var ok = await _enrollmentService.RemoveEnrollmentAsync(enrollmentId, currentUserId, role);
            if (!ok) return Forbid();
            return Ok();
        }

        // Student: get my enrollments
        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMine()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var userId)) return Forbid();

            var list = await _enrollmentService.GetEnrollmentsForStudentAsync(userId);
            return Ok(list);
        }

        // Teacher: list students for a specific course created by this teacher
        [HttpGet("course/{courseId}/students")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetStudentsForCourse(int courseId)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idClaim, out var teacherId)) return Forbid();

            var students = await _enrollmentService.GetStudentsForCourseIfOwnedAsync(courseId, teacherId);
            if (students == null) return Forbid();
            return Ok(students);
        }
    }
}
