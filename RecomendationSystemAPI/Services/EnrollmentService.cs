using RecomendatinSystemAPI.Models;
using RecomendatinSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.Helpers;
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
        }

        public async Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync()
        {
            var list = await _repo.GetAllWithDetailsAsync();
            return list.Select(DtoMapper.ToDto);
        }
    }
}
