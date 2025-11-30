using RecomendationSystemAPI.DTOs.Students;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateStudentDto dto);
        Task<bool> UpdateAsync(UpdateStudentDto dto);
    }
}
