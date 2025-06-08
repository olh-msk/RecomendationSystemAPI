using RecomendatinSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo) => _repo = repo;

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(DtoMapper.ToDto);
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            return student == null ? null : DtoMapper.ToDto(student);
        }

        public async Task CreateAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                GPA = dto.GPA,
                Interests = dto.InterestTagIds.Select(id => new StudentInterest { InterestTagId = id }).ToList()
            };
            await _repo.AddAsync(student);
            await _repo.SaveAsync();
        }
    }
}
