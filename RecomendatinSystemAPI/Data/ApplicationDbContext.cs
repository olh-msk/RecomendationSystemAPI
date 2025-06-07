using Microsoft.EntityFrameworkCore;
using RecomendatinSystemAPI.Models;


namespace RecomendatinSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<InterestTag> InterestTags => Set<InterestTag>();
        public DbSet<StudentInterest> StudentInterests => Set<StudentInterest>();
        public DbSet<CourseTag> CourseTags => Set<CourseTag>();
    }
}
