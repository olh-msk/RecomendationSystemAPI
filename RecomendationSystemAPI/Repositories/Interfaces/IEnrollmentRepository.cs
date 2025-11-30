using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task EnrollAsync(Enrollment enrollment);
        Task SaveAsync();
        Task<IEnumerable<Enrollment>> GetAllWithDetailsAsync();
        Task<Enrollment?> GetByIdWithDetailsAsync(int id);
        Task DeleteAsync(int id);

        Task<IEnumerable<Enrollment>> GetByStudentIdWithDetailsAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsForCourseAsync(int courseId);
    }
}
