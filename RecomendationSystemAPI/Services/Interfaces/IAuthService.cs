using RecomendationSystemAPI.DTOs.Auth;
using RecomendationSystemAPI.DTOs.Students;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<StudentDto?> RegisterAsync(RegisterDto dto);
        Task<StudentDto?> LoginAsync(LoginDto dto);
    }
}