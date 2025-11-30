using RecomendationSystemAPI.Models;
using RecomendationSystemAPI.DTOs.Students;
using RecomendationSystemAPI.Helpers;
using RecomendationSystemAPI.Repositories.Interfaces;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentInterestRepository _studentInterestRepository;

        public StudentService(
            IStudentRepository studentRepository,
            IStudentInterestRepository studentInterestRepository)
        {
            _studentRepository = studentRepository;
            _studentInterestRepository = studentInterestRepository;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(DtoMapper.ToDto);
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
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

            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(UpdateStudentDto dto)
        {
            var student = await _studentRepository.GetByIdAsync(dto.Id);
            if (student == null) return false;

            student.FullName = dto.FullName;
            student.GPA = (float)dto.GPA;

            await _studentInterestRepository.AssignAsync(dto.Id, dto.InterestIds);
            await _studentRepository.SaveAsync();

            return true;
        }
    }
}
