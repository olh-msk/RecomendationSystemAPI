using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;

        public EnrollmentService(IEnrollmentRepository repo, IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            _repo = repo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
        }

        public async Task EnrollStudentAsync(CreateEnrollmentDto dto)
        {
            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Grade = dto.Grade,
                Semester = dto.Semester
            };
            await _repo.EnrollAsync(enrollment);
            await _repo.SaveAsync();
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var list = await _repo.GetAllWithDetailsAsync();
            return list.Select(DtoMapper.ToDto);
        }

        public async Task<bool> RemoveEnrollmentAsync(int enrollmentId, int currentUserId, string currentUserRole)
        {
            var enrollment = await _repo.GetByIdWithDetailsAsync(enrollmentId);
            if (enrollment == null) return false;

            // If student is removing own enrollment
            if (currentUserRole == "Student" && enrollment.StudentId == currentUserId)
            {
                await _repo.DeleteAsync(enrollmentId);
                await _repo.SaveAsync();
                return true;
            }

            // If teacher: check ownership of the course
            if (currentUserRole == "Teacher")
            {
                var course = await _courseRepo.GetByIdAsync(enrollment.CourseId);
                if (course != null && course.CreatedById == currentUserId)
                {
                    await _repo.DeleteAsync(enrollmentId);
                    await _repo.SaveAsync();
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsForStudentAsync(int studentId)
        {
            var list = await _repo.GetByStudentIdWithDetailsAsync(studentId);
            return list.Select(DtoMapper.ToDto);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsForCourseAsync(int courseId)
        {
            var list = await _repo.GetStudentsForCourseAsync(courseId);
            return list.Select(DtoMapper.ToDto);
        }

        public async Task<IEnumerable<StudentDto>?> GetStudentsForCourseIfOwnedAsync(int courseId, int teacherId)
        {
            var course = await _courseRepo.GetByIdAsync(courseId);
            if (course == null || course.CreatedById != teacherId) return null;
            return (await GetStudentsForCourseAsync(courseId));
        }
    }
}
