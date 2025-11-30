namespace RecomendationSystemAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        // Authentication fields
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        // Academic
        public float GPA { get; set; }

        // Role (Student or Teacher)
        public UserRole Role { get; set; } = UserRole.Student;

        // Navigation
        public ICollection<StudentInterest> Interests { get; set; } = new List<StudentInterest>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        // Courses created by this user (if teacher)
        public ICollection<Course> CreatedCourses { get; set; } = new List<Course>();
    }
}
