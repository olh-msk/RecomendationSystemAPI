﻿using RecomendatinSystemAPI.Models;

namespace RecomendationSystemAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task SaveAsync();
    }
}
