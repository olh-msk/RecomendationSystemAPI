using RecomendationSystemAPI.DTOs.Auth;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<StudentDto?> RegisterAsync(RegisterDto dto);
        // Authenticate and return the Student entity (used to build JWT)
        Task<Student?> AuthenticateAsync(LoginDto dto);
    }
}