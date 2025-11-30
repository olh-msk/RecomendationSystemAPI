using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<InterestTag> InterestTags { get; set; } = null!;
        public DbSet<StudentInterest> StudentInterests { get; set; } = null!;
        public DbSet<CourseTag> CourseTags { get; set; } = null!;
        public DbSet<Enrollment> Enrollments { get; set; } = null!;
    }
}
