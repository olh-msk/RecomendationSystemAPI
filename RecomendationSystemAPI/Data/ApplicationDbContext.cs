using Microsoft.EntityFrameworkCore;
using RecomendationSystemAPI.Models;

namespace RecomendationSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<InterestTag> InterestTags { get; set; }
        public DbSet<StudentInterest> StudentInterests { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
