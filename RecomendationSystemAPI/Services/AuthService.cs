using RecomendationSystemAPI.DTOs.Auth;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentInterestRepository _studentInterestRepository;

        public AuthService(IStudentRepository studentRepository, IStudentInterestRepository studentInterestRepository)
        {
            _studentRepository = studentRepository;
            _studentInterestRepository = studentInterestRepository;
        }

        public async Task<StudentDto?> RegisterAsync(RegisterDto dto)
        {
            var existing = await _studentRepository.GetByEmailAsync(dto.Email);
            if (existing != null) return null;

            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);

            var role = UserRole.Student;
            if (!string.IsNullOrWhiteSpace(dto.Role) && Enum.TryParse<UserRole>(dto.Role, true, out var parsed))
                role = parsed;

            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                GPA = dto.GPA,
                Role = role,
                Interests = dto.InterestTagIds.Select(id => new StudentInterest { InterestTagId = id }).ToList()
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveAsync();

            return DtoMapper.ToDto(student);
        }

        // NEW: returns Student entity (with navigation props loaded by repository)
        public async Task<Student?> AuthenticateAsync(LoginDto dto)
        {
            var student = await _studentRepository.GetByEmailAsync(dto.Email);
            if (student == null) return null;

            var ok = PasswordHasher.Verify(dto.Password, student.PasswordHash, student.PasswordSalt);
            if (!ok) return null;

            return student;
        }
    }
}