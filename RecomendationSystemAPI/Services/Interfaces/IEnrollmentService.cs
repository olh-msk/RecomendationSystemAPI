using RecomendationSystemAPI.DTOs.Enrollments;
using RecomendationSystemAPI.DTOs.Students;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task EnrollStudentAsync(CreateEnrollmentDto dto);
        Task<IEnumerable<EnrollmentDto>> GetAllEnrollmentsAsync();
        Task<bool> RemoveEnrollmentAsync(int enrollmentId, int currentUserId, string currentUserRole);
        Task<IEnumerable<EnrollmentDto>> GetEnrollmentsForStudentAsync(int studentId);
        Task<IEnumerable<StudentDto>> GetStudentsForCourseAsync(int courseId);
        Task<IEnumerable<StudentDto>?> GetStudentsForCourseIfOwnedAsync(int courseId, int teacherId);
    }
}
