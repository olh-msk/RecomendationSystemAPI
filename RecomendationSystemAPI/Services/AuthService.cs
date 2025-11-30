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
            var existing = (await _studentRepository.GetAllAsync()).FirstOrDefault(s => s.Email == dto.Email);
            if (existing != null) return null;

            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);

            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                GPA = dto.GPA,
                Interests = dto.InterestTagIds.Select(id => new StudentInterest { InterestTagId = id }).ToList()
            };

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveAsync();

            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                GPA = student.GPA,
                InterestNames = student.Interests?.Select(i => i.InterestTag?.Name).Where(n => n != null).Cast<string>().ToList() ?? new List<string>()
            };
        }

        public async Task<StudentDto?> LoginAsync(LoginDto dto)
        {
            var student = (await _studentRepository.GetAllAsync()).FirstOrDefault(s => s.Email == dto.Email);
            if (student == null) return null;

            var ok = PasswordHasher.Verify(dto.Password, student.PasswordHash, student.PasswordSalt);
            if (!ok) return null;

            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                GPA = student.GPA,
                InterestNames = student.Interests?.Select(i => i.InterestTag?.Name).Where(n => n != null).Cast<string>().ToList() ?? new List<string>()
            };
        }
    }
}